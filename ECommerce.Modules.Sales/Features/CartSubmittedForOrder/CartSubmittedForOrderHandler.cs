using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Modules.Common;
using ECommerce.Modules.Sales.Data;
using ECommerce.Modules.Sales.Data.Domain;

namespace ECommerce.Modules.Sales.Features.CartSubmittedForOrder
{
    public static class CreateOrderHandler
    {
        public static async Task<CreateOrderResponse> Handle(
            CartSubmitted command,
            SalesDbContext db,
            CancellationToken ct
        )
        {
            var order = new Order
            {
                CustomerId = command.CustomerId,
                CreatedAt = DateTime.Now,
                Items = command
                    .Items.Select(i => new OrderItem
                    {
                        ProductId = i.ProductId,
                        ProductName = i.ProductName,
                        UnitPrice = i.UnitPrice,
                        Quantity = i.Quantity,
                    })
                    .ToList(),
            };
            db.Orders.Add(order);
            await db.SaveChangesAsync(ct);
            return new CreateOrderResponse(order.Id, order.CreatedAt);
        }
    }
}
