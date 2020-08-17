namespace ProductShop.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CategoryProductsConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.HasKey(cp => new { cp.CategoryId, cp.ProductId });

            builder.HasOne(c => c.Category)
                .WithMany(cp => cp.CategoryProducts)
                .HasForeignKey(c => c.CategoryId);

            builder.HasOne(p => p.Product)
                .WithMany(cp => cp.CategoryProducts)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
