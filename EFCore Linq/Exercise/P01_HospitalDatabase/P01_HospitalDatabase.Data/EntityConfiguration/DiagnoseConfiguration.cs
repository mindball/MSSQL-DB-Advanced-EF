using P01_HospitalDatabase.Data.Models;
using P01_HospitalDatabase.Data.EntityConfiguration.Common;
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
                .HasMaxLength(GlobalConstants.NameMaxLength);

            builder.Property(d => d.Comments)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(GlobalConstants.CommentsMaxLength);

            builder.HasOne(p => p.Patient)
                .WithMany(v => v.Diagnoses)
                .HasForeignKey(p => p.PatientId);
        }
    }
}
