namespace ECommerce.Modules.Common;

public record CartSubmitted(
    int CartId,
    int CustomerId,
    IReadOnlyCollection<CartSubmittedItem> Items
);

public record CartSubmittedItem(int ProductId, string ProductName, decimal UnitPrice, int Quantity);
