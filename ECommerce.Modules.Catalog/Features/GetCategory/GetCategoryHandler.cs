using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Modules.Catalog.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Catalog.Features.GetCategory
{
    public static class GetCategoryHandler
    {
        public static async Task<CategoryDto?> Handle(
            GetCategoryQuery query,
            CatalogDbContext db,
            CancellationToken ct
        ) {

            var category = await db.Categories.FirstOrDefaultAsync(c => c.Id == query.CategoryId);
            if (category == null)
                return null;
            var categoryDto = new CategoryDto(category.Id, category.Name);
            return categoryDto;
        }
    }
}
