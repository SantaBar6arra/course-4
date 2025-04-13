using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Api.Queries.Product;

namespace Query.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdcutController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListAllProductsRequest listProductsRequest) =>
            Ok(await mediator.Send(listProductsRequest));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) =>
            Ok(await mediator.Send(new GetProductByIdRequest(id)));
    }
}
