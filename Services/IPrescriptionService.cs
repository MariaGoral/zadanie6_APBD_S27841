
using PrescriptionAPI.Models;

namespace PrescriptionAPI.Services
{
    public interface IPrescriptionService
    {
        Task AddPrescriptionAsync(Prescription prescription, Patient patient, Doctor doctor, List<(int idMedicament, int dose, string description)> medicaments);
        Task<Patient?> GetPatientDataAsync(int idPatient);
    }
}
