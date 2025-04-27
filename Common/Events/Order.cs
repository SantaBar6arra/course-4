using Core;

namespace Common.Events;

public record OrderItem(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    uint Quantity
);

public record OrderCreated(
    Guid Id,
    Guid CustomerId,
    List<OrderItem> Items,
    DateTime CreatedAt,
    decimal TotalAmount
) : BaseEvent(Id, typeof(OrderCreated));

public record OrderConfirmed(
    Guid Id
) : BaseEvent(Id, typeof(OrderConfirmed));

public record OrderCancelled(
    Guid Id,
    string Reason
) : BaseEvent(Id, typeof(OrderCancelled));

public record OrderItemAdded(
    Guid Id,
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    uint Quantity
) : BaseEvent(Id, typeof(OrderItemAdded));
