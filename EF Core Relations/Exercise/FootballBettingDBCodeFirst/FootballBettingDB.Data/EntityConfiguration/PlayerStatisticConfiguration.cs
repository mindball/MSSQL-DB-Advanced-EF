using FootballBettingDB.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBettingDB.Data.EntityConfiguration
{
    class PlayerStatisticConfiguration : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
        {
            builder.HasKey(ps => new {ps.PlayerId, ps.GameId });

            //ScoredGoals 
            //    Assists 
            //        MinutesPlayed

            builder.Property(ps => ps.ScoredGoals)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ps => ps.Assists)
                 .IsRequired();

            builder.Property(ps => ps.MinutesPlayed)
                .IsRequired();

            builder.HasOne(p => p.Player)
                .WithMany(ps => ps.PlayerStatistics)
                .HasForeignKey(p => p.PlayerId);

            builder.HasOne(g => g.Game)
                .WithMany(ps => ps.PlayerStatistics)
                .HasForeignKey(g => g.GameId);
        }
    }
}
