using P01_HospitalDatabase.Data.Models;
using P01_HospitalDatabase.Data.EntityConfiguration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.PatientId);

            builder.Property(p => p.FirstName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(GlobalConstants.NameMaxLength);

            builder.Property(p => p.LastName)
               .IsRequired(true)
               .IsUnicode(true)
               .HasMaxLength(GlobalConstants.NameMaxLength);

            builder.Property(p => p.Address)
               .IsRequired(true)
               .IsUnicode(true)
               .HasMaxLength(GlobalConstants.CommentsMaxLength);

            builder.Property(p => p.Email)
               .HasDefaultValue("no email")
               .IsUnicode(false)
               .HasMaxLength(80);

            builder.Property(p => p.HasInsurance)
               .IsRequired(true);


               
        }
    }
}
