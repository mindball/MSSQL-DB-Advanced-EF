using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaporStore.Models;

namespace VaporStore.Data.Config
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(u => u.User)
                .WithMany(c => c.Cards)
                .HasForeignKey(u => u.UserId);
        }
    }
}
