namespace ProductShop.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;


    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired(true);

            builder.Property(p => p.Price)
                .IsRequired(true);

            builder.Property(p => p.BuyerId)
                .IsRequired(false);

            builder.Property(p => p.SellerId)
               .IsRequired(true);

            builder.HasOne(b => b.Buyer)
                .WithMany(p => p.ProductsBought)
                .HasForeignKey(b => b.BuyerId);

            builder.HasOne(s => s.Seller)
                .WithMany(p => p.ProductsSold)
                .HasForeignKey(b => b.BuyerId);

        }
    }
}
