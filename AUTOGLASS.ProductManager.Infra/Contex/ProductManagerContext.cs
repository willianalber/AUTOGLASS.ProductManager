﻿using AUTOGLASS.ProductManager.Domain.Entities;
using AUTOGLASS.ProductManager.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AUTOGLASS.ProductManager.Infra.Contex
{
    public class ProductManagerContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Supplier { get; set; }

        public ProductManagerContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductManagerContext).Assembly);
            modelBuilder.ApplyConfiguration(new ProductMapping());
            modelBuilder.ApplyConfiguration(new SupplierMapping());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }

    }
}
