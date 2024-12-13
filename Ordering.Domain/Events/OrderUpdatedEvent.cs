#pragma warning disable CS9113 // Parameter is unread.
namespace Ordering.Domain.Events;

public class OrderUpdatedEvent(Order Order) : IDomainEvent;