using Core;

namespace Command.Api.Commands;

public record CreateOrder(
    Guid Id,
    Guid CustomerId,
    List<OrderItemDto> Items
) : BaseCommand;

public record ConfirmOrder(
    Guid Id
) : BaseCommand;

public record CancelOrder(
    Guid Id,
    string Reason
) : BaseCommand;

public record AddOrderItem(
    Guid Id,
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    uint Quantity
) : BaseCommand;

public record OrderItemDto(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    uint Quantity
);
