using ECommerce.Modules.Catalog.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Modules.Catalog.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Explicitly set the table name (optional if using DefaultSchema)
            builder.ToTable("Categories");

            // Identity Column (Auto-increment)
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            // String Constraints
            builder.Property(x => x.Name)
                   .HasMaxLength(200)
                   .IsRequired();

        }
    }
}
