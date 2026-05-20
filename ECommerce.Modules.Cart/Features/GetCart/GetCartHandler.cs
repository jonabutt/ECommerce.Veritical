using ECommerce.Modules.Cart.Data;
using ECommerce.Modules.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Cart.Features.GetCart;

public static class GetCartHandler
{
    public static async Task<CartDto?> Handle(
        GetCartQuery query,
        CartDbContext db,
        CancellationToken ct
    )
    {
        var cart = await db
            .Carts.AsNoTracking()
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CustomerId == query.CustomerId, ct);

        return cart is null ? null : CartMapping.ToDto(cart);
    }
}
