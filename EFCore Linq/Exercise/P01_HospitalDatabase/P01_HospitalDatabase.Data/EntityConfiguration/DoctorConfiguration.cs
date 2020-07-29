using P01_HospitalDatabase.Data.Models;
using P01_HospitalDatabase.Data.EntityConfiguration.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.DoctorId);

            builder.Property(d => d.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(GlobalConstants.NameMaxLength);

            builder.Property(d => d.Specialty)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(100);
        }
    }
}
