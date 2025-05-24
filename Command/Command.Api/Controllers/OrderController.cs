using Command.Api.Commands;
using Command.Api.Dtos;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController(ICommandDispatcher dispatcher) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
    {
        var command = new CreateOrder(dto.CustomerId, dto.Items);
        await dispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPost("{id:guid}/confirm")]
    public async Task<IActionResult> ConfirmOrder(Guid id)
    {
        var command = new ConfirmOrder(id);
        await dispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> CancelOrder(Guid id, [FromBody] CancelOrderDto dto)
    {
        var command = new CancelOrder(id, dto.Reason);
        await dispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPost("{id:guid}/items")]
    public async Task<IActionResult> AddOrderItem(Guid id, [FromBody] OrderItemDto dto)
    {
        var command = new AddOrderItem(
            id,
            dto.ProductId,
            dto.ProductName,
            dto.UnitPrice,
            dto.Quantity
        );

        await dispatcher.SendAsync(command);
        return Ok();
    }
}
