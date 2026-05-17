using ECommerce.Modules.Catalog.Data;
using ECommerce.Modules.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Catalog.Features.GetProduct
{
    public static class GetProductHandler
    {
        public static async Task<ProductDto?> Handle(
            GetProductQuery query,
            CatalogDbContext db,
            CancellationToken ct
        )
        {
            var product = await db
                .Products.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == query.ProductId);

            if (product == null)
                return null;

            ProductDto productDto = new ProductDto(product.Id, product.Name, product.Price);
            return productDto;
        }
    }
}
