namespace ECommerce.Modules.Cart.Features.RemoveFromCart;

public record RemoveFromCartCommand(int CustomerId, int ProductId);
