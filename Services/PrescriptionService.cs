
using Microsoft.EntityFrameworkCore;
using PrescriptionAPI.Models;

namespace PrescriptionAPI.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly PrescriptionContext _context;

        public PrescriptionService(PrescriptionContext context) => _context = context;

        public async Task AddPrescriptionAsync(Prescription prescription, Patient patient, Doctor doctor, List<(int idMedicament, int dose, string description)> medicaments)
        {
            if (prescription.DueDate < prescription.Date)
                throw new ArgumentException("DueDate must be >= Date");

            if (medicaments.Count > 10)
                throw new ArgumentException("Max 10 medicaments allowed");

            foreach (var (idMedicament, _, _) in medicaments)
                if (!await _context.Medicaments.AnyAsync(m => m.IdMedicament == idMedicament))
                    throw new ArgumentException($"Medicament {idMedicament} does not exist");

            var existingPatient = await _context.Patients.FirstOrDefaultAsync(p =>
                p.FirstName == patient.FirstName && p.LastName == patient.LastName && p.Birthdate == patient.Birthdate);

            if (existingPatient == null)
            {
                existingPatient = patient;
                _context.Patients.Add(existingPatient);
                await _context.SaveChangesAsync();
            }

            var existingDoctor = await _context.Doctors.FindAsync(doctor.IdDoctor);
            if (existingDoctor == null)
                throw new ArgumentException("Doctor not found");

            prescription.IdPatient = existingPatient.IdPatient;
            prescription.IdDoctor = doctor.IdDoctor;
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            foreach (var (idMedicament, dose, description) in medicaments)
            {
                _context.PrescriptionMedicaments.Add(new PrescriptionMedicament
                {
                    IdMedicament = idMedicament,
                    IdPrescription = prescription.IdPrescription,
                    Dose = dose,
                    Description = description
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Patient?> GetPatientDataAsync(int idPatient)
        {
            return await _context.Patients
                .Include(p => p.Prescriptions)
                    .ThenInclude(pr => pr.PrescriptionMedicaments)
                        .ThenInclude(pm => pm.Medicament)
                .Include(p => p.Prescriptions)
                    .ThenInclude(pr => pr.Doctor)
                .FirstOrDefaultAsync(p => p.IdPatient == idPatient);
        }
    }
}
