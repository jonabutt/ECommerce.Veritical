namespace ECommerce.Modules.Catalog.Data.Domain
{
    public class Product
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        public bool IsNew { get; set; }

        public Category Category { get; set; } = null!;
    }
}
