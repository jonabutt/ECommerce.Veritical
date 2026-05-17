using ECommerce.Modules.Catalog.Data;
using ECommerce.Modules.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Catalog.Features.GetHomepageItems;

public record GetHomepageQuery();

    public record GetHomepageResponse(List<ProductDto> NewProducts);

    public static class GetHomepageHandler
    {
        public static async Task<GetHomepageResponse> Handle(
            GetHomepageQuery query,
            CatalogDbContext db,
            CancellationToken ct)
        {
            // Fetch the top 20 products marked as 'IsNew' for the homepage
            var items = await db.Products
                .AsNoTracking()
                .Where(x => x.IsNew)
                .OrderByDescending(x => x.Id)
                .Take(20)
                .Select(x => new ProductDto(x.Id, x.Name, x.Price))
                .ToListAsync(ct);

            return new GetHomepageResponse(items);
        }
    }