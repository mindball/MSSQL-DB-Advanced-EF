using P01_HospitalDatabase.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    public class DiagnoseConfiguration : IEntityTypeConfiguration<Diagnose>
    {
        public void Configure(EntityTypeBuilder<Diagnose> builder)
        {
            builder.HasKey(d => d.DiagnoseId);

            builder.Property(d => d.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

            builder.Property(d => d.Comments)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(250);

            builder.HasOne(p => p.Patient)
                .WithMany(v => v.Diagnoses)
                .HasForeignKey(p => p.PatientId);
        }
    }
}
