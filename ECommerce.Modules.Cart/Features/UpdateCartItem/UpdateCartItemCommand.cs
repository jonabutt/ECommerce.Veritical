namespace ECommerce.Modules.Cart.Features.UpdateCartItem;

public record UpdateCartItemCommand(int CustomerId, int ProductId, int Quantity);
