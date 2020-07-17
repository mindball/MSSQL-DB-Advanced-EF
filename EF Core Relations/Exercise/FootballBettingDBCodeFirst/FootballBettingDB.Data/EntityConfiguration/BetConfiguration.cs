using FootballBettingDB.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBettingDB.Data.EntityConfiguration
{
    public class BetConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.HasKey(b => b.BetId);

            builder.Property(b => b.Amount)
                .IsRequired();

            builder.Property(b => b.Prediction)
                .IsRequired();

            builder.Property(b => b.DateTime)
                .IsRequired();

            builder.HasOne(u => u.User)
                .WithMany(b => b.Bets)
                .HasForeignKey(u => u.UserId);

            builder.HasOne(g => g.Game)
                .WithMany(b => b.Bets)
                .HasForeignKey(g => g.GameId);
        }
    }
}
