
namespace PrescriptionAPI.Models
{
    public class Prescription
    {
        public int IdPrescription { get; set; } = default!;
        public DateTime Date { get; set; } = default!;
        public DateTime DueDate { get; set; } = default!;
        public int IdPatient { get; set; } = default!;
        public Patient Patient { get; set; } = default!;
        public int IdDoctor { get; set; } = default!;
        public Doctor Doctor { get; set; } = default!;
        public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = default!;
    }
}
