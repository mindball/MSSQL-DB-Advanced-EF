using P01_HospitalDatabase.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    public class PatientMedicamentsConfiguration : IEntityTypeConfiguration<PatientMedicaments>
    {
        public void Configure(EntityTypeBuilder<PatientMedicaments> builder)
        {
            builder.HasKey(pm => new { pm.MedicamentId, pm.PationId });

            builder.HasOne(p => p.Patient)
                .WithMany(m => m.Prescriptions)
                .HasForeignKey(p => p.PationId);

            builder.HasOne(m => m.Medicament)
                .WithMany(p => p.PatientMedicaments)
                .HasForeignKey(m => m.MedicamentId);
        }
    }
}
