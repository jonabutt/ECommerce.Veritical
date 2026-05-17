namespace ECommerce.Modules.Cart.Features.AddToCart;

public record AddToCartCommand(int CustomerId, int ProductId, int Quantity = 1);
