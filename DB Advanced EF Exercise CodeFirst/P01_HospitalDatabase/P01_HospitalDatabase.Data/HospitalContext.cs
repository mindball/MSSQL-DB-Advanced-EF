
namespace P01_HospitalDatabase.Data
{
    using System;
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
            optionsBuilder.UseSqlServer("Server=;Database=Hospital;User=sa;Password=");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurePatientEntity(modelBuilder);

            ConfigureVisitationEntity(modelBuilder);

            ConfigureDiagnoseEntity(modelBuilder);

            ConfigureMedicamentEntity(modelBuilder);

            ConfigurePatientMedicamentEntity(modelBuilder);
        }

        private void ConfigurePatientMedicamentEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<PatientMedicament>()
            .HasKey(pm => new {pm.PationId, pm.MedicamentId });

            modelBuilder
            .Entity<PatientMedicament>()
            .HasOne(pm => pm.Patient)
            .WithMany(p => p.Prescription)
            .HasForeignKey(pm => pm.PationId);

            modelBuilder
            .Entity<PatientMedicament>()
            .HasOne(pm => pm.Medicament)
            .WithMany(p => p.Prescription)
            .HasForeignKey(pm => pm.MedicamentId);
        }

        private void ConfigureMedicamentEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
             .Entity<Medicament>()
             .HasKey(m => m.MedicamentId);

            modelBuilder
            .Entity<Medicament>()
            .Property(m => m.Name)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

            //modelBuilder
            //  .Entity<Medicament>()
            //  .HasMany(p => p.Prescription)
            //  .WithOne(pm => pm.Medicament);
        }

        private void ConfigureDiagnoseEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
             .Entity<Diagnose>()
             .HasKey(d => d.DiagnoseId);

            modelBuilder
            .Entity<Diagnose>()
            .Property(d => d.Name)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

            modelBuilder
           .Entity<Diagnose>()
           .Property(d => d.Comments)
           .HasMaxLength(250)
           .IsUnicode();


        }

        private void ConfigureVisitationEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
             .Entity<Visitation>()
             .HasKey(v => v.VisitationId);

            modelBuilder
            .Entity<Visitation>()
            .Property(v => v.Comments)
            .HasMaxLength(250)
            .IsUnicode();
        }

        private void ConfigurePatientEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
               .Entity<Patient>()
               .HasKey(p => p.PatienId);

            modelBuilder
                .Entity<Patient>()
                .Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            modelBuilder
                .Entity<Patient>()
                .Property(p => p.LastName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            modelBuilder
                .Entity<Patient>()
                .Property(p => p.Address)
                .HasMaxLength(250);

            modelBuilder
                .Entity<Patient>()
                .Property(p => p.Email)
                .HasMaxLength(80);

            modelBuilder
               .Entity<Patient>()
               .HasMany(p => p.Visitations)
               .WithOne(v => v.Patient);

            modelBuilder
               .Entity<Patient>()
               .HasMany(p => p.Diagnoses)
               .WithOne(d => d.Patient);

            //modelBuilder
            //  .Entity<Patient>()
            //  .HasMany(p => p.Prescription)
            //  .WithOne(pm => pm.Patient);
        }
    }
}
