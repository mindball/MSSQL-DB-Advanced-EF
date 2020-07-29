using P01_HospitalDatabase.Data.Models;
using P01_HospitalDatabase.Data.EntityConfiguration.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    public class VisitationConfiguration : IEntityTypeConfiguration<Visitation>
    {
        public void Configure(EntityTypeBuilder<Visitation> builder)
        {
            builder.HasKey(v => v.VisitationId);

            builder.Property(v => v.Date)
                .IsRequired(true);

            builder.Property(v => v.Comments)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(GlobalConstants.CommentsMaxLength);

            builder.HasOne(d => d.Doctor)
                .WithMany(v => v.Visitations)
                .HasForeignKey(d => d.DoctorId);

            builder.HasOne(p => p.Patient)
                .WithMany(v => v.Visitations)
                .HasForeignKey(p => p.PatientId);


        }
    }
}
