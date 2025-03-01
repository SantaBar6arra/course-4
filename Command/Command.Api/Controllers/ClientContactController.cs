using Command.Api.Commands;
using Command.Api.Dtos;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Command.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientContactController(ICommandDispatcher dispatcher) : ControllerBase
    {
        private readonly ICommandDispatcher _dispatcher = dispatcher;

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateClientContactDto clientContact)
        {
            await _dispatcher.SendAsync(new UpdateClientContact(
                clientContact.Id, clientContact.Type, clientContact.Value));

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid clientContactId)
        {
            await _dispatcher.SendAsync(new DeleteClientContact(clientContactId));

            return Ok();
        }
    }
}
