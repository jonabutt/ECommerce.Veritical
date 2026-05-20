using ECommerce.Modules.Cart.Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Modules.Cart.Data.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.ProductName).HasMaxLength(200).IsRequired();

        builder.Property(x => x.UnitPrice).HasPrecision(18, 4);

        builder.HasIndex(x => new { x.CartId, x.ProductId }).IsUnique();
    }
}
