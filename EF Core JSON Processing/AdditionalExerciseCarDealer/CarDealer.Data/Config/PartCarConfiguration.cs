using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealer.Data.Config
{
    class PartCarConfiguration : IEntityTypeConfiguration<PartCar>
    {
        public void Configure(EntityTypeBuilder<PartCar> builder)
        {
            builder.HasKey(pc => new { pc.CarId, pc.PartId });

            builder.HasOne(p => p.Part)
                .WithMany(pc => pc.PartCars)
                .HasForeignKey(p => p.PartId);

            builder.HasOne(c => c.Car)
                .WithMany(pc => pc.PartCars)
                .HasForeignKey(c => c.CarId);
        }
    }
}
