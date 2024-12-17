namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToDto(this IEnumerable<Order> orders)
    {
        return orders.Select(o =>
            new OrderDto
            (
                o.Id,
                o.CustomerId,
                o.OrderName,
                new AddressDto(
                    o.ShippingAddress.FirstName,
                    o.ShippingAddress.LastName,
                    o.ShippingAddress.EmailAddress,
                    o.ShippingAddress.AddressLine,
                    o.ShippingAddress.Country,
                    o.ShippingAddress.State,
                    o.ShippingAddress.ZipCode
                ),
                new AddressDto(
                    o.BillingAddress.FirstName,
                    o.BillingAddress.LastName,
                    o.BillingAddress.EmailAddress,
                    o.BillingAddress.AddressLine,
                    o.BillingAddress.Country,
                    o.BillingAddress.State,
                    o.BillingAddress.ZipCode
                ),
                new PaymentDto(
                    o.Payment.CardHolderName,
                    o.Payment.CardNumber,
                    o.Payment.CardExpiration,
                    o.Payment.CardCvv,
                    o.Payment.PaymentMethod
                ),
                o.Status,
                o.OrderItems.Select(oi =>
                    new OrderItemDto
                    (
                        oi.OrderId,
                        oi.ProductId,
                        oi.Quantity,
                        oi.Price
                    )).ToList()
            ));
    }
}