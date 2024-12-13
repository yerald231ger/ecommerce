namespace Ordering.Infrastructure.SqlServer.Extensions;

internal abstract class InitialData
{
    public static IEnumerable<Customer> Customers => new List<Customer>
    {
        Customer.Create((CustomerId)Guid.Parse("5f6cf443-a2be-44ec-a4d1-981a2e021a7f"), "John", "John.Doe@gmail.com"),
        Customer.Create((CustomerId)Guid.Parse("4e13bbbd-9ab0-411f-8901-2cf0b78bc70e"), "Robert",
            "Iglesias.Robert@gmail.com")
    };

    public static IEnumerable<Product> Products => new List<Product>
    {
        Product.Create((ProductId)Guid.Parse("fa959494-4895-417a-b8b7-d3b8acf38b35"), "Iphone X", 100),
        Product.Create((ProductId)Guid.Parse("67617b0c-9f8d-4b68-856a-d9cfa95ba7d0"), "Google Pixel 9", 200),
        Product.Create((ProductId)Guid.Parse("3bffaff7-783a-4b10-9d1c-0c325ed9eddd"), "Samsung Galaxy S20", 300),
        Product.Create((ProductId)Guid.Parse("485b53be-dfb7-4f85-abe8-403c8089d758"), "Samsung Galaxy S21", 400)
    };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var addressJohn = Address.Of("John", "Doe", "John.Doe@gmail.com", "Lincoln St. 213", "EUA", "NY", "38493");
            var addressRobert = Address.Of("Robert", "Iglesias", "Robert.Iglesias@gmail.com", "Rose Bv", "EUA", "CA",
                "28374");

            var paymentMethodJohn = Payment.Of("John Doe", "6253847571629981", "12/28", "123", 1);
            var paymentMethodRobert = Payment.Of("Robert Iglesias", "8894003384751234", "12/25", "847", 1);

            var orderJohn = Order.Create(
                (OrderId)Guid.CreateVersion7(),
                (CustomerId)Guid.Parse("5f6cf443-a2be-44ec-a4d1-981a2e021a7f"),
                (OrderName)"Order John",
                addressJohn,
                addressJohn,
                paymentMethodJohn);
            
            orderJohn.AddOrderItem((ProductId)Guid.Parse("fa959494-4895-417a-b8b7-d3b8acf38b35"), 1, 100);
            orderJohn.AddOrderItem((ProductId)Guid.Parse("67617b0c-9f8d-4b68-856a-d9cfa95ba7d0"), 2, 200);
            
            var orderRobert = Order.Create(
                (OrderId)Guid.CreateVersion7(),
                (CustomerId)Guid.Parse("4e13bbbd-9ab0-411f-8901-2cf0b78bc70e"),
                (OrderName)"Order Robert",
                addressRobert,
                addressRobert,
                paymentMethodRobert);
            
            orderRobert.AddOrderItem((ProductId)Guid.Parse("3bffaff7-783a-4b10-9d1c-0c325ed9eddd"), 1, 300);
            orderRobert.AddOrderItem((ProductId)Guid.Parse("485b53be-dfb7-4f85-abe8-403c8089d758"), 2, 400);
            
            return new List<Order> { orderJohn, orderRobert };
        }
    }
}