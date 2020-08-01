namespace BookShop.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(a => new {a.BookId, a.CategoryId });

            builder.HasOne(a => a.Book)
                .WithMany(bc => bc.BooksCategories)
                .HasForeignKey(a => a.BookId);

            builder.HasOne(c => c.Category)
                .WithMany(bc => bc.BooksCategories)
                .HasForeignKey(c => c.CategoryId);
        }
    }
}
