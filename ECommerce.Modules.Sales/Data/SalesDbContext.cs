using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Modules.Sales.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Sales.Data
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options)
            : base(options) { }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This is the magic line for Modular Monoliths
            modelBuilder.HasDefaultSchema("sales");

            // Apply configurations for this module only
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);
        }
    }
}
