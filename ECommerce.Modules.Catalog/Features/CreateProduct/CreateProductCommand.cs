using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Modules.Catalog.Features.CreateProduct
{
    public record CreateProductCommand(string Name, int CategoryId, decimal Price);
}
