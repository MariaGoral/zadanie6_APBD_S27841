
using Microsoft.EntityFrameworkCore;

namespace PrescriptionAPI.Models
{
    public class PrescriptionContext : DbContext
    {
        public PrescriptionContext(DbContextOptions options) : base(options) { }

        public DbSet<Patient> Patients { get; set; } = default!;
        public DbSet<Doctor> Doctors { get; set; } = default!;
        public DbSet<Medicament> Medicaments { get; set; } = default!;
        public DbSet<Prescription> Prescriptions { get; set; } = default!;
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = default!;

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            // Klucz główny PrescriptionMedicament
            modelBuilder.Entity<PrescriptionMedicament>()
                .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });

            // Relacje
            modelBuilder.Entity<PrescriptionMedicament>()
                .HasOne(pm => pm.Medicament)
                .WithMany(m => m.PrescriptionMedicaments)
                .HasForeignKey(pm => pm.IdMedicament);

            modelBuilder.Entity<PrescriptionMedicament>()
                .HasOne(pm => pm.Prescription)
                .WithMany(p => p.PrescriptionMedicaments)
                .HasForeignKey(pm => pm.IdPrescription);

            // Dane startowe: lekarz
            modelBuilder.Entity<Doctor>().HasData(new Doctor
            {
                IdDoctor = 1,
                FirstName = "Anna",
                LastName = "Nowak",
                Email = "anna@example.com"
            });

            // Dane startowe: lek
            modelBuilder.Entity<Medicament>().HasData(new Medicament
            {
                IdMedicament = 1,
                Name = "Paracetamol",
                Description = "Painkiller",
                Type = "Tablet"
            });
        }
    }
}
