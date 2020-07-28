using P01_HospitalDatabase.Data.Models;
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
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
               .IsRequired(true)
               .IsUnicode(true)
               .HasMaxLength(50);

            builder.Property(p => p.Address)
               .IsRequired(true)
               .IsUnicode(true)
               .HasMaxLength(250);

            builder.Property(p => p.Email)
               .IsRequired(true)
               .IsUnicode(false)
               .HasMaxLength(80);

            builder.Property(p => p.HasInsurance)
               .IsRequired(true);


               
        }
    }
}
