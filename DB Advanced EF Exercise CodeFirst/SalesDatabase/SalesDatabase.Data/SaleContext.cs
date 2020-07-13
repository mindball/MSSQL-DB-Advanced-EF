using Microsoft.EntityFrameworkCore;
using SalesDataBase.Models;
using System;

namespace SalesDatabase.Data
{
    public class SaleContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Store> Store { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=;Database=Sales;User=sa;Password=");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureCustomerEntity(modelBuilder);

            ConfigureProductEntity(modelBuilder);

            ConfigureSaleEntity(modelBuilder);

            ConfigureStoreEntity(modelBuilder);
        }

        private void ConfigureStoreEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
             .Entity<Store>()
             .HasKey(s => s.StoreId);

            modelBuilder
           .Entity<Store>()
           .Property(s => s.Name)
           .HasMaxLength(80)
           .IsRequired()
           .IsUnicode();

            modelBuilder
               .Entity<Store>()
               .HasMany(s => s.Sales)
               .WithOne(s => s.Store);
        }

        private void ConfigureSaleEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Sale>()
            .HasKey(s => s.SaleId);


            //For table Sales make Date column with default value GETDATE() function,
            //called from the database, not the application.
            modelBuilder
           .Entity<Sale>()
           .Property(s => s.Date)
           .HasDefaultValueSql("getdate()");

        }

        private void ConfigureProductEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Product>()
            .HasKey(p => p.ProductId);

            modelBuilder
           .Entity<Product>()
           .Property(p => p.Name)
           .HasMaxLength(50)
           .IsRequired()
           .IsUnicode();

            modelBuilder
           .Entity<Product>()
           .Property(p => p.Description)
           .HasMaxLength(250)
           .IsUnicode()
           .HasDefaultValue("no description");

            modelBuilder
               .Entity<Product>()
               .HasMany(c => c.Sales)
               .WithOne(c => c.Product);
        }

        private void ConfigureCustomerEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
           .Entity<Customer>()
           .HasKey(c => c.CustomerId);

            modelBuilder
           .Entity<Customer>()
           .Property(c => c.Name)
           .HasMaxLength(100)
           .IsRequired()
           .IsUnicode();


            modelBuilder
           .Entity<Customer>()
           .Property(c => c.Email)
           .HasMaxLength(80)
           .IsRequired()
           .IsUnicode(false);

            modelBuilder
           .Entity<Customer>()
           .Property(c => c.CardNumber)           
           .IsUnicode(false);

            modelBuilder
               .Entity<Customer>()
               .HasMany(c => c.Sales)
               .WithOne(c => c.Customer);
        }
    }
}
