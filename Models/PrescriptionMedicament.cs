
namespace PrescriptionAPI.Models
{
    public class PrescriptionMedicament
    {
        public int IdMedicament { get; set; } = default!;
        public Medicament Medicament { get; set; } = default!;
        public int IdPrescription { get; set; } = default!;
        public Prescription Prescription { get; set; } = default!;
        public int Dose { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
