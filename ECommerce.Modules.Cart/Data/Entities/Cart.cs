namespace ECommerce.Modules.Cart.Data.Entites;

public class ShoppingCart
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<CartItem> Items { get; set; } = [];

    public decimal TotalAmount => Items.Sum(x => x.UnitPrice * x.Quantity);
}
