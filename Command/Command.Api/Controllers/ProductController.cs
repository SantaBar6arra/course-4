using Command.Api.Commands;
using Command.Api.Dtos;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Command.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ICommandDispatcher dispatcher, ILogger<ProductController> logger) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto productDto)
        {
            try
            {
                await dispatcher.SendAsync(new CreateProduct(
                    productDto.Name, productDto.Description, productDto.Price, productDto.StockQuantity,
                    productDto.Category, productDto.Tags));

                return Created();
            }
            catch (InvalidOperationException exception)
            {
                logger.LogError("an error occured: {message}", exception.Message);
                return new ObjectResult(new { exception.Message }) { StatusCode = 400 };
            }
            catch
            {
                return new ObjectResult("internal server error") { StatusCode = 500 };
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductDto productDto)
        {
            try
            {
                await dispatcher.SendAsync(new UpdateProductDetails(
                    productDto.Id, productDto.Name, productDto.Description, productDto.Price,
                    productDto.Category, productDto.Tags));

                await dispatcher.SendAsync(new UpdateProductStockQuantity(
                    productDto.Id, productDto.StockQuantity));

                return Ok();
            }
            catch (InvalidOperationException exception)
            {
                logger.LogError("an error occured: {message}", exception.Message);
                return new ObjectResult(new { exception.Message }) { StatusCode = 400 };
            }
            catch
            {
                return new ObjectResult("internal server error") { StatusCode = 500 };
            }
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
