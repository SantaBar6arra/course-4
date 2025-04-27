using Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Api.Queries.Order;

namespace Query.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<ListAllOrdersResponse>> GetAllOrders(
        [FromQuery] Guid? customerId,
        [FromQuery] OrderStatus? status)
    {
        var request = new ListAllOrdersRequest
        {
            CustomerId = customerId,
            Status = status
        };

        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetOrderByIdResponse>> GetOrderById(Guid id)
    {
        var request = new GetOrderByIdRequest { Id = id };

        var response = await _mediator.Send(request);
        return Ok(response);
    }
}
