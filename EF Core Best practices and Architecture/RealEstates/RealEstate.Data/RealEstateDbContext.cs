using Microsoft.EntityFrameworkCore;
using RealEstate.Models;

namespace RealEstate.Data
{
    public class RealEstateDbContext : DbContext
    {
        public RealEstateDbContext()
        {
        }

        public RealEstateDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<RealEstateProperty> RealEstateProperties { get; set; }

        public DbSet<BuildingType> BuildingTypes { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<PropertyType> PropertyTypies { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<RealEstatePropertyTag> RealEstatePropertiesTags { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>()
                .HasMany(x => x.RealEstateProperties)
                .WithOne(x => x.District)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RealEstatePropertyTag>()
                .HasKey(x => new { x.RealEstatePropertyId, x.TagId });
        }
    }
}
