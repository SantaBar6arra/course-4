using Core;
using Command.Domain.Aggregates;
using Command.Api.Commands;

namespace Command.Api.Handlers;

public interface IClientCommandHandler
{
    Task HandleAsync(CreateClient command);
    Task HandleAsync(UpdateClient command);
    Task HandleAsync(DeleteClient command);
    Task HandleAsync(UpdateClientContact command);
    Task HandleAsync(DeleteClientContact command);
}

public class ClientCommandHandler(
    IEventSourcingHandler<Client> clientsHandler,
    IEventSourcingHandler<ClientContact> clientContactsHandler) : IClientCommandHandler
{
    public async Task HandleAsync(CreateClient command)
    {
        var client = new Client(
            command.FirstName,
            command.LastName,
            command.ClientStatus,
            command.Address);
        await clientsHandler.SaveAsync(client);

        var clientContactsTask = command.Contacts.Select(contact =>
        {
            var clientContact = new ClientContact(client.Id, contact.Type, contact.Value);
            return clientContactsHandler.SaveAsync(clientContact);
        });

        await Task.WhenAll(clientContactsTask);
    }

    public async Task HandleAsync(UpdateClient command)
    {
        var client = await clientsHandler.GetByIdAsync(command.Id);
        client.UpdateBaseData(command.FirstName, command.LastName, command.ClientStatus);
        client.UpdateAddress(command.Address);
        await clientsHandler.SaveAsync(client);
    }

    public async Task HandleAsync(DeleteClient command)
    {
        var client = await clientsHandler.GetByIdAsync(command.Id);
        client.Delete();

        // look up postgres database for contacts
        // and call delete for each of them

        await clientsHandler.SaveAsync(client);
    }

    public async Task HandleAsync(UpdateClientContact command)
    {
        var clientContact = await clientContactsHandler.GetByIdAsync(command.Id);
        clientContact.Update(command.Type, command.Value);
        await clientContactsHandler.SaveAsync(clientContact);
    }

    public async Task HandleAsync(DeleteClientContact command)
    {
        var clientContact = await clientContactsHandler.GetByIdAsync(command.Id);
        clientContact.Delete();
        await clientContactsHandler.SaveAsync(clientContact);
    }
}
