using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Modules.Sales.Features.CreateOrder
{
    public record OrderItemDetails(Guid ProductId, int Quantity);
}
