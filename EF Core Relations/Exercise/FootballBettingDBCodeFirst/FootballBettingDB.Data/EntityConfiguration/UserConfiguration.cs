using FootballBettingDB.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBettingDB.Data.EntityConfiguration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserName)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(50);

            builder.Property(u => u.Password)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(25);

            builder.Property(u => u.Email)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(80);

            builder.Property(u => u.Name)
               .IsRequired()
               .IsUnicode(true)
               .HasMaxLength(50);

            builder.Property(u => u.Balance)
                .IsRequired();
        }
    }
}
