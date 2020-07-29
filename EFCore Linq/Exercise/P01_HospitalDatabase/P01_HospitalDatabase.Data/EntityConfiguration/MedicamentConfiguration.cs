using P01_HospitalDatabase.Data.Models;
using P01_HospitalDatabase.Data.EntityConfiguration.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    public class MedicamentConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.HasKey(m => m.MedicamentId);

            builder.Property(m => m.Name)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(GlobalConstants.NameMaxLength);
        }
    }
}
