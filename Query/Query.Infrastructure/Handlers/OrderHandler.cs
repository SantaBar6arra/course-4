using Common.Events;
using Common.Models;
using Core;
using Microsoft.EntityFrameworkCore;
using Query.Domain.Entities;
using OrderItem = Query.Domain.Entities.OrderItem;

namespace Query.Infrastructure.Handlers;

public class OrderHandler(DataContext context)
    : IEventHandler<OrderCreated>
    , IEventHandler<OrderConfirmed>
    , IEventHandler<OrderCancelled>
    , IEventHandler<OrderItemAdded>
{
    private readonly DataContext _context = context;

    public async Task On(OrderCreated @event)
    {
        var order = new Order
        {
            Id = @event.Id,
            CustomerId = @event.CustomerId,
            Status = OrderStatus.Pending, // Assume status is Pending when created
            CreatedAt = DateTime.UtcNow,
            TotalAmount = @event.Items.Sum(item => item.UnitPrice * item.Quantity)
        };

        var orderItems = @event.Items.Select(item => new OrderItem
        {
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            UnitPrice = item.UnitPrice,
            Quantity = item.Quantity,
            OrderId = @event.Id
        });

        await _context.Orders.AddAsync(order);
        await _context.OrderItems.AddRangeAsync(orderItems);
        await _context.SaveChangesAsync();
    }

    public async Task On(OrderConfirmed @event)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .SingleOrDefaultAsync(o => o.Id == @event.Id)
            ?? throw new Exception("Order not found!");

        order.Status = OrderStatus.Confirmed;

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task On(OrderCancelled @event)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .SingleOrDefaultAsync(o => o.Id == @event.Id)
            ?? throw new Exception("Order not found!");

        order.Status = OrderStatus.Cancelled;

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task On(OrderItemAdded @event)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .SingleOrDefaultAsync(o => o.Id == @event.Id)
            ?? throw new Exception("Order not found!");

        var orderItem = new OrderItem
        {
            ProductId = @event.ProductId,
            ProductName = @event.ProductName,
            UnitPrice = @event.UnitPrice,
            Quantity = @event.Quantity,
            OrderId = @event.Id
        };

        order.TotalAmount += orderItem.UnitPrice * orderItem.Quantity;

        await _context.OrderItems.AddAsync(orderItem);
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
}
