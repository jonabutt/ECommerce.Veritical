using ECommerce.Modules.Cart.Data;
using ECommerce.Modules.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Cart.Features.UpdateCartItem;

public static class UpdateCartItemHandler
{
    public static async Task<CartDto?> Handle(
        UpdateCartItemCommand command,
        CartDbContext db,
        CancellationToken ct
    )
    {
        // Guard against invalid updates early
        if (command.Quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero. Use Remove instead.");

        var cart = await db
            .Carts.Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CustomerId == command.CustomerId, ct);

        if (cart is null)
            return null;

        var line = cart.Items.FirstOrDefault(i => i.ProductId == command.ProductId);
        if (line is null)
            return null;

        // Strictly update quantity
        line.Quantity = command.Quantity;
        cart.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync(ct);
        return CartMapping.ToDto(cart);
    }
}
