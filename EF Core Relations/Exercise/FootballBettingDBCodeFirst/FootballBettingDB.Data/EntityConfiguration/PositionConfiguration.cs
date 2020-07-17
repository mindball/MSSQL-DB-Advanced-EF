using FootballBettingDB.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBettingDB.Data.EntityConfiguration
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(p => p.PositionId);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}
