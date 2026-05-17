using ECommerce.Modules.Cart.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Cart.Features.RemoveFromCart;

public static class RemoveFromCartHandler
{
    public static async Task<CartDto?> Handle(
        RemoveFromCartCommand command,
        CartDbContext db,
        CancellationToken ct)
    {
        var cart = await db.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CustomerId == command.CustomerId, ct);

        if (cart is null)
            return null;

        var line = cart.Items.FirstOrDefault(i => i.ProductId == command.ProductId);
        if (line is null)
            return null;

        cart.Items.Remove(line);
        cart.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(ct);

        return CartMapping.ToDto(cart);
    }
}
