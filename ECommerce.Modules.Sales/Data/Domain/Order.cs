namespace ECommerce.Modules.Sales.Data.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<OrderItem> Items { get; set; } = new();

        public decimal TotalAmount => Items.Sum(x => x.UnitPrice * x.Quantity);
    }
}
