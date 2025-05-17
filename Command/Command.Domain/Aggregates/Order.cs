using Common.Events;
using Common.Models;
using Core;

namespace Command.Domain.Aggregates;

public class Order : AggregateRoot
{
    #region Fields

    private Guid _orderId;
    private Guid _customerId;
    private List<OrderItem> _items;
    private DateTime _createdAt;
    private OrderStatus _status;  // Changed from string to OrderStatus enum
    private decimal _totalAmount;

    public List<OrderItem> Items => _items;

    #endregion

    #region Constructors

    public Order() { }

    public Order(Guid customerId, List<OrderItem> items)
    {
        var total = items.Sum(item => item.UnitPrice * item.Quantity);
        RaiseEvent(new OrderCreated(Guid.NewGuid(), customerId, items, DateTime.UtcNow, total));
    }

    #endregion

    #region Command Handlers

    public void Confirm()
    {
        if (_status != OrderStatus.Pending)
            throw new InvalidOperationException("Only pending orders can be confirmed.");

        RaiseEvent(new OrderConfirmed(Id));
    }

    public void Cancel(string reason)
    {
        if (_status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Order is already cancelled.");

        RaiseEvent(new OrderCancelled(Id, reason));
    }

    public void AddItem(Guid productId, string productName, decimal unitPrice, uint quantity)
    {
        if (_status != OrderStatus.Pending)
            throw new InvalidOperationException("Cannot modify non-pending order.");

        RaiseEvent(new OrderItemAdded(Id, productId, productName, unitPrice, quantity));
    }

    #endregion

    #region Event Handlers

    private void On(OrderCreated @event)
    {
        _id = @event.Id;
        _orderId = @event.Id;
        _customerId = @event.CustomerId;
        _items = new List<OrderItem>(@event.Items);
        _createdAt = @event.CreatedAt;
        _status = OrderStatus.Pending; // Default status is Pending
        _totalAmount = @event.TotalAmount;
    }

    private void On(OrderConfirmed _)
    {
        _status = OrderStatus.Confirmed;
    }

    private void On(OrderCancelled _)
    {
        _status = OrderStatus.Cancelled;
    }

    private void On(OrderItemAdded @event)
    {
        _items.Add(new OrderItem(
            @event.ProductId, @event.ProductName, @event.UnitPrice, @event.Quantity));

        _totalAmount += @event.UnitPrice * @event.Quantity;
    }

    #endregion
}
