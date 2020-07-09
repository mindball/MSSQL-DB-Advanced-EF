
namespace P01_HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_HospitalDatabase.Data.Models;

    public class HospitalContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=;Database=SoftUni2EF;User=sa;Password=");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: Constraints and relations
        }
    }
}
