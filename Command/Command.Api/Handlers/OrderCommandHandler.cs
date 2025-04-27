using Command.Api.Commands;
using Command.Domain.Aggregates;
using Common.Events;
using Core;

public interface IOrderCommandHandler
{
    Task HandleAsync(CreateOrder command);
    Task HandleAsync(ConfirmOrder command);
    Task HandleAsync(CancelOrder command);
    Task HandleAsync(AddOrderItem command);
}

public class OrderCommandHandler(IEventSourcingHandler<Order> orderHandler)
    : IOrderCommandHandler
{
    public async Task HandleAsync(CreateOrder command)
    {
        var (id, customerId, items) = command;

        var orderItems = items
            .Select(i => new OrderItem(i.ProductId, i.ProductName, i.UnitPrice, i.Quantity))
            .ToList();

        var order = new Order(customerId, orderItems);

        await orderHandler.SaveAsync(order);
    }

    public async Task HandleAsync(ConfirmOrder command)
    {
        var order = await orderHandler.GetByIdAsync(command.Id);
        order.Confirm();
        await orderHandler.SaveAsync(order);
    }

    public async Task HandleAsync(CancelOrder command)
    {
        var order = await orderHandler.GetByIdAsync(command.Id);
        order.Cancel(command.Reason);
        await orderHandler.SaveAsync(order);
    }

    public async Task HandleAsync(AddOrderItem command)
    {
        var order = await orderHandler.GetByIdAsync(command.Id);

        order.AddItem(
            command.ProductId,
            command.ProductName,
            command.UnitPrice,
            command.Quantity
        );

        await orderHandler.SaveAsync(order);
    }
}
