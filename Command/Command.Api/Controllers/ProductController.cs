using Command.Api.Commands;
using Command.Api.Dtos;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Command.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ICommandDispatcher dispatcher) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto productDto)
        {
            await dispatcher.SendAsync(new CreateProduct(
                productDto.Name, productDto.Description, productDto.Price, productDto.StockQuantity,
                productDto.Category, productDto.Tags));

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductDto productDto)
        {
            await dispatcher.SendAsync(new UpdateProductDetails(
                productDto.Id, productDto.Name, productDto.Description, productDto.Price,
                productDto.Category, productDto.Tags));

            await dispatcher.SendAsync(new UpdateProductStockQuantity(
                productDto.Id, productDto.StockQuantity));

            return Ok();
        }

        [HttpPatch("{id:guid}/discontinue")]
        public async Task<IActionResult> Discontinue([FromRoute] Guid id)
        {
            await dispatcher.SendAsync(new DiscontinueProduct(id));

            return Ok();
        }

        [HttpPatch("{id:guid}/unlock")]
        public async Task<IActionResult> Unlock([FromRoute] Guid id)
        {
            await dispatcher.SendAsync(new UnlockProduct(id));

            return Ok();
        }
    }
}
