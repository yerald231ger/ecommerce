namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    #region Properties

    public CustomerId CustomerId { get; private set; }

    public OrderName OrderName { get; private set; }

    public Address ShippingAddress { get; private set; }

    public Address BillingAddress { get; private set; }

    public Payment Payment { get; private set; }

    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public decimal TotalPrice { get; private set; }

    private readonly List<OrderItem> _orderItems = [];

    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    #endregion
    
    private Order() { }

    private Order(CustomerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress,
        Payment payment)
    {
        CustomerId = customerId;
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
    }

    public static Order Create(OrderId orderId, CustomerId customerId, OrderName orderName, Address shippingAddress,
        Address billingAddress, Payment payment)
    {
        var order = new Order(customerId, orderName, shippingAddress, billingAddress, payment)
        {
            Id = orderId
        };
        
        order.AddDomainEvent(new OrderCreatedEvent(order));

        return order;
    }

    public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment,
        OrderStatus orderStatus)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = orderStatus;

        AddDomainEvent(new OrderUpdatedEvent(this));
    }

    public void AddOrderItem(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var orderItem = OrderItem.Create(Id, productId, quantity, price);

        _orderItems.Add(orderItem);
        TotalPrice = CalculateTotalPrice();
    }

    public void RemoveOrderItem(ProductId productId)
    {
        var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);

        if (orderItem is not null)
            _orderItems.Remove(orderItem);

        TotalPrice = CalculateTotalPrice();
    }

    private decimal CalculateTotalPrice() => _orderItems.Sum(x => x.Price * x.Quantity);
}