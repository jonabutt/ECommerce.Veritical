using ECommerce.Modules.Catalog.Data;
using ECommerce.Modules.Catalog.Data.Domain;
using ECommerce.Modules.Catalog.Features.GetHomepageItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Modules.Catalog.Features.CreateProduct
{
    public static class CreateProductHandler
    {
        public static async Task<CreateProductResponse> Handle(
            CreateProductCommand command,
            CatalogDbContext db,
            CancellationToken ct)
        {
            Product product = new Product
            {
                Name = command.Name,
                CategoryId = command.CategoryId,
                IsNew = true,
                Price = command.Price
            };
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return new CreateProductResponse(product.Id, product.Name);
        }
    }
}
