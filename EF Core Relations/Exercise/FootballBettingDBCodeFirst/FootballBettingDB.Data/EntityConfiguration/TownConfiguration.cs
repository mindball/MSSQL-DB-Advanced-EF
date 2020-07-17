using FootballBettingDB.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBettingDB.Data.EntityConfiguration
{
    public class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasKey(t => t.TownId);

            builder.Property(t => t.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

            builder.HasOne(c => c.Coutry)
                .WithMany(t => t.Towns)
                .HasForeignKey(t => t.CountryId);
        }
    }
}
