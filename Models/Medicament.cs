
namespace PrescriptionAPI.Models
{
    public class Medicament
    {
        public int IdMedicament { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Type { get; set; } = default!;
        public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = default!;
    }
}
