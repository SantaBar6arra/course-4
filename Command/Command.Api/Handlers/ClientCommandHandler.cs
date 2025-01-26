using Core;
using Command.Domain.Aggregates;
using Command.Api.Commands;

namespace Command.Api.Handlers;

public interface IClientCommandHandler
{
    Task HandleAsync(CreateClient command);
    Task HandleAsync(UpdateClient command);
    Task HandleAsync(DeleteClient command);
}

public class ClientCommandHandler(IEventSourcingHandler<Client> eventSourcingHandler)
    : IClientCommandHandler
{
    public async Task HandleAsync(CreateClient command)
    {
        var client = new Client(
            command.FirstName,
            command.LastName,
            command.ClientStatus,
            command.Address);
        await eventSourcingHandler.SaveAsync(client);

        var clientContactsTask = command.Contacts.Select(contact =>
        {
            var clientContact = new ClientContact(client.Id, contact.Type, contact.Value);
            return eventSourcingHandler.SaveAsync(clientContact);
        });

        await Task.WhenAll(clientContactsTask);
    }

    public async Task HandleAsync(UpdateClient command)
    {
        var client = await eventSourcingHandler.GetByIdAsync(command.Id);
        client.UpdateBaseData(command.FirstName, command.LastName, command.ClientStatus);
        client.UpdateAddress(command.Address);
        await eventSourcingHandler.SaveAsync(client);
    }

    public async Task HandleAsync(DeleteClient command)
    {
        var client = await eventSourcingHandler.GetByIdAsync(command.Id);
        client.Delete();
        await eventSourcingHandler.SaveAsync(client);
    }
}
