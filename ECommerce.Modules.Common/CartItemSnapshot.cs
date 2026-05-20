namespace ECommerce.Modules.Common;

public record CartItemSnapshot(int ProductId, string ProductName, decimal UnitPrice, int Quantity);
