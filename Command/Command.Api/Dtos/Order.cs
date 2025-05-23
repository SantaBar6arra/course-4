using Command.Api.Commands;

namespace Command.Api.Dtos;

public record CreateOrderDto(
    Guid CustomerId,
    List<OrderItemDto> Items
);

public record CancelOrderDto(
    string Reason
);
