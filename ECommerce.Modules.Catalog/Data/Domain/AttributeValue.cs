namespace ECommerce.Modules.Catalog.Data.Domain
{
    public class AttributeValue
    {
        public int Id { get; set; }

        public int AttributeId { get; set; }

        public required string Value { get; set; }
    }
}
