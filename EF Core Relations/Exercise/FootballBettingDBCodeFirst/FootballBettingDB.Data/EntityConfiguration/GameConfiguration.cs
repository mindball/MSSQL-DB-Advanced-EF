using FootballBettingDB.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBettingDB.Data.EntityConfiguration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.GameId);

            builder.Property(g => g.HomeTeamGoals)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(g => g.AwayTeamGoals)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(g => g.DateTime)
                .IsRequired();

            builder.Property(g => g.HomeTeamBetRate)
                .IsRequired();

            builder.Property(g => g.AwayTeamBetRate)
               .IsRequired();

            builder.Property(g => g.DrawBetRate)
               .IsRequired();

            builder.Property(g => g.Result)
             .IsRequired();

            builder.HasOne(t => t.HomeTeam)
                .WithMany(g => g.HomeGames)
                .HasForeignKey(t => t.HomeTeamId);

            builder.HasOne(t => t.AwayTeam)
                .WithMany(g => g.AwayGames)
                .HasForeignKey(t => t.AwayTeamId);
        }
    }
}
