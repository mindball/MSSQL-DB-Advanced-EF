using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaporStore.Models;

namespace VaporStore.Data.Config
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);

            builder.HasOne(g => g.Genre)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.GenreId);

            builder.HasOne(g => g.Developer)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.DeveloperId);
        }
    }
}
