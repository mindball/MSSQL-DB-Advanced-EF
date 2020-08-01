namespace BookShop.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    using Common;

    class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.AuthorId);

            builder.Property(a => a.FirstName)
                .HasMaxLength(AuthorGlobalConstant.NamesMaxCharacters)
                .IsUnicode(true)
                .IsRequired(false);

            builder.Property(a => a.LastName)
                .HasMaxLength(AuthorGlobalConstant.NamesMaxCharacters)
                .IsUnicode(true)
                .IsRequired(true);
        }
    }
}
