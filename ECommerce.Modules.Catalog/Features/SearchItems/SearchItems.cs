using ECommerce.Modules.Catalog.Data;
using ECommerce.Modules.Catalog.Features;
using ECommerce.Modules.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Catalog.Features.Search
{
    public record SearchItemsQuery(string? SearchTerm, string? Category = null, int Page = 1);
    
    public static class SearchItemsHandler
    {
        public static async Task<SearchItemsResponse> Handle(
        SearchItemsQuery query,
        CatalogDbContext db,
        CancellationToken ct)
        {
            //query.SearchTerm = query.SearchTerm.ToLower();
            // Start the query
            var dbQuery = db.Products.AsNoTracking();

            // Apply filters
            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                dbQuery = dbQuery.Where(x => x.Name.ToLower().Contains(query.SearchTerm));
            }

            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                dbQuery = dbQuery.Where(x => x.Category.Name == query.Category);
            }

            // Calculate paging
            int pageSize = 20;
            var total = await dbQuery.CountAsync(ct);

            var items = await dbQuery
                .OrderBy(x => x.Name)
                .Skip((query.Page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new ProductDto(x.Id, x.Name, x.Price))
                .ToListAsync(ct);

            return new SearchItemsResponse(items, total);
        }
    }

    public record SearchItemsResponse(List<ProductDto> Items,int Total);
}
