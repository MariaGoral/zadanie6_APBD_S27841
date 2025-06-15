
namespace PrescriptionAPI.Models
{
    public class Patient
    {
        public int IdPatient { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Birthdate { get; set; } = default!;
        public ICollection<Prescription> Prescriptions { get; set; } = default!;
    }
}
