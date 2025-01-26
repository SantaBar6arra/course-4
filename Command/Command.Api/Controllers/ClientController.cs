using Command.Api.Commands;
using Command.Api.Dtos;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Command.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(ICommandDispatcher dispatcher) : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] CreateClientDto client)
        {
            dispatcher.SendAsync(new CreateClient(
                client.FirstName, client.LastName, client.Status, client.Address, client.Contacts));

            return Ok();
        }
    }
}
