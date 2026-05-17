using ECommerce.Modules.Catalog.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Modules.Catalog.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // 1. Explicitly set the table name (optional if using DefaultSchema)
            builder.ToTable("Products");

            // 2. Identity Column (Auto-increment)
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd(); // This is the default, but being explicit is good

            // 3. String Constraints
            builder.Property(x => x.Name)
                   .HasMaxLength(200)
                   .IsRequired();

            // 4. Decimal Precision (Crucial for Price!)
            // Without this, EF defaults to (18,2) which can cause rounding bugs
            builder.Property(x => x.Price)
                   .HasPrecision(18, 4);
        }
    }
}
