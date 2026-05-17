using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Modules.Catalog.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Catalog.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This is the magic line for Modular Monoliths
            modelBuilder.HasDefaultSchema("catalog");

            // Apply configurations for this module only
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        }
    }
}
