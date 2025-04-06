using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Query.Api.Queries.Client;

namespace Query.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListAllClientsRequest request) =>
            Ok(await _mediator.Send(request));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) =>
            Ok(await _mediator.Send(new GetClientByIdRequest(id)));
    }
}
