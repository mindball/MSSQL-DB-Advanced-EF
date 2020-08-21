using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CarDealer.Data.Config
{
    class PartConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(s => s.Supplier)
                .WithMany(p => p.Parts)
                .HasForeignKey(s => s.SupplierId);
        }
    }
}
