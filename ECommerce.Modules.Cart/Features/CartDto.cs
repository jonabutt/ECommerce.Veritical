namespace ECommerce.Modules.Cart.Features;

public record CartItemDto(int ProductId, string ProductName, decimal UnitPrice, int Quantity);

public record CartDto(int CartId, int CustomerId, IReadOnlyList<CartItemDto> Items, decimal Total);
