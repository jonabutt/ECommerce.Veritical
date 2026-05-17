namespace ECommerce.Modules.Cart.Data.Entites;

public class CartItem
{
    public int Id { get; set; }

    public int CartId { get; set; }

    public ShoppingCart Cart { get; set; } = null!;

    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }
}
