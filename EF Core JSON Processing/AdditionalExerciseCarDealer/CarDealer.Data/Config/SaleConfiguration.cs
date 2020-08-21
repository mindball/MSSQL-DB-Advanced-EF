using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealer.Data.Config
{
    class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasOne(c => c.Car)
                .WithMany(s => s.Sales)
                .HasForeignKey(c => c.CarId);

            builder.HasOne(c => c.Customer)
                .WithMany(s => s.Sales)
                .HasForeignKey(c => c.CustomerId);


        }
    }
}
