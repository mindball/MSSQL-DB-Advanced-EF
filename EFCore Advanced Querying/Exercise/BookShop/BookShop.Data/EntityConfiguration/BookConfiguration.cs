namespace BookShop.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    using Common;
    class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.BookId);

            builder.Property(b => b.Title)
                .HasMaxLength(BookGlobalConstant.TitleMaxCharacters)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(b => b.Description)
                .HasMaxLength(BookGlobalConstant.DescriptionMaxCharacters)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(b => b.ReleaseDate)              
               .IsRequired(false);

            builder.HasOne(a => a.Author)
                .WithMany(b => b.Books)
                .HasForeignKey(a => a.AuthorId);

        }
    }
}
