using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaporStore.Models;

namespace VaporStore.Data.Config
{
    public class GameTagsConfiguration : IEntityTypeConfiguration<GameTags>
    {
        public void Configure(EntityTypeBuilder<GameTags> builder)
        {
            builder.HasKey(x => new { x.TagId, x.GameId });

            builder.HasOne(g => g.Game)
                .WithMany(gt => gt.GamesTags)
                .HasForeignKey(g => g.GameId);

            builder.HasOne(t => t.Tag)
                .WithMany(gt => gt.GameTags)
                .HasForeignKey(t => t.TagId);
        }
    }
}
