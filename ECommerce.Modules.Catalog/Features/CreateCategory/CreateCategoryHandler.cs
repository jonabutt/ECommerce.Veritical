using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Modules.Catalog.Data;
using ECommerce.Modules.Catalog.Data.Domain;

namespace ECommerce.Modules.Catalog.Features.CreateCategory
{
    public static class CreateCategoryHandler
    {
        public static async Task<CreateCategoryResponse> Handler(
            CreateCategoryCommand createCategory,
            CatalogDbContext db,
            CancellationToken ct
        )
        {
            Category category = new Category
            {
                Name = createCategory.Name,
            };
            db.Categories.Add(category);
            await db.SaveChangesAsync();
            return new CreateCategoryResponse(category.Id, category.Name);
        }
    }
}
