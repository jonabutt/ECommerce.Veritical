using ECommerce.Modules.Cart.Data;
using ECommerce.Modules.Cart.Data.Entites;
using ECommerce.Modules.Common;
using Microsoft.EntityFrameworkCore;
using Wolverine;

namespace ECommerce.Modules.Cart.Features.CheckoutCart;

public static class CheckoutCartHandler
{
    public static async Task Handle(CheckoutCartCommand command, CartDbContext db, IMessageBus bus)
    {
        // get cart list
        ShoppingCart? cart = db
            .Carts.Include(c => c.Items)
            .SingleOrDefault(c => c.CustomerId == command.CustomerId);

        if (cart == null)
            throw new Exception("not found");

        var cartItems = cart.Items;
        if (cartItems.ToList().Count == 0)
            throw new Exception("cart is empty");

        var cartSubmitted = new CartSubmitted(
            cart.Id,
            cart.CustomerId,
            cart.Items.Select(i => new CartSubmittedItem(
                    i.ProductId,
                    i.ProductName,
                    i.UnitPrice,
                    i.Quantity
                ))
                .ToList()
        );

        await bus.PublishAsync(cartSubmitted);
        // remove cart
        db.CartItems.RemoveRange(cart.Items);
        db.Carts.Remove(cart);

        db.SaveChanges();
    }
}
