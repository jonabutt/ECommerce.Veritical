using ECommerce.Modules.Cart.Data;
using ECommerce.Modules.Cart.Data.Entites;
using ECommerce.Modules.Cart.Features;
using ECommerce.Modules.Common;
using Microsoft.EntityFrameworkCore;
using Wolverine;

namespace ECommerce.Modules.Cart.Features.AddToCart;

public static class AddToCartHandler
{
    public static async Task<CartDto?> Handle(
        AddToCartCommand command,
        CartDbContext cartDb,
        IMessageBus bus,
        CancellationToken ct
    )
    {
        if (command.Quantity < 1)
            return null;

        var product = await bus.InvokeAsync<ProductDto?>(
            new GetProductQuery(command.ProductId),
            ct
        );
        if (product is null)
            return null; // Product doesn't exist

        var cart = await cartDb
            .Carts.Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CustomerId == command.CustomerId, ct);

        if (cart is null)
        {
            cart = new ShoppingCart
            {
                CustomerId = command.CustomerId,
                UpdatedAt = DateTime.UtcNow,
            };
            cartDb.Carts.Add(cart);
        }

        var existingLine = cart.Items.FirstOrDefault(i => i.ProductId == command.ProductId);

        if (existingLine is not null)
        {
            return null;
        }

        cart.Items.Add(
            new CartItem
            {
                ProductId = command.ProductId,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = command.Quantity,
            }
        );

        cart.UpdatedAt = DateTime.UtcNow;
        await cartDb.SaveChangesAsync(ct);

        return CartMapping.ToDto(cart);
    }
}
