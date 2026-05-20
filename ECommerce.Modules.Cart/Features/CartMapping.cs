using ECommerce.Modules.Cart.Data.Entites;
using ECommerce.Modules.Common;

namespace ECommerce.Modules.Cart.Features;

internal static class CartMapping
{
    public static CartDto ToDto(ShoppingCart cart) =>
        new(
            cart.Id,
            cart.CustomerId,
            cart.Items.OrderBy(i => i.ProductName)
                .Select(i => new CartItemDto(i.ProductId, i.ProductName, i.UnitPrice, i.Quantity))
                .ToList(),
            cart.TotalAmount
        );
}
