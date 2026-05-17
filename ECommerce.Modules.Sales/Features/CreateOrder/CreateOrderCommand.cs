using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Modules.Sales.Features.CreateOrder
{
    public record CreateOrderCommand(
        int CustomerId,
        List<OrderItemDetails> Items,
        string ShippingAddress,
        string PaymentMethodId
    );
}
