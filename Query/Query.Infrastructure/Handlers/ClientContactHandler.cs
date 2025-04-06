using Common.Events;
using Core;
using Query.Domain.Entities;

namespace Query.Infrastructure.Handlers;

public class ClientContactHandler(DataContext context)
    : IEventHandler<ClientContactCreated>
    , IEventHandler<ClientContactUpdated>
    , IEventHandler<ClientContactDeleted>
{
    private readonly DataContext _context = context;

    public async Task On(ClientContactCreated @event)
    {
        var contact = new ClientContact()
        {
            Id = @event.Id,
            ClientId = @event.ClientId,
            Type = @event.ContactType,
            Value = @event.Value,
            IsDeleted = false
        };

        await _context.AddAsync(contact);
        await _context.SaveChangesAsync();
    }

    public async Task On(ClientContactUpdated @event)
    {
        var contact = await _context.ClientContacts.FindAsync([@event.Id])
            ?? throw new Exception("Client Contact not found!");

        if (contact.IsDeleted)
            throw new Exception("contact is deleted!");

        contact.Type = @event.ContactType;
        contact.Value = @event.Value;

        _context.Update(contact);
        await _context.SaveChangesAsync();
    }

    public async Task On(ClientContactDeleted @event)
    {
        var contact = await _context.ClientContacts.FindAsync([@event.Id])
            ?? throw new Exception("Client Contact not found!");

        contact.IsDeleted = true;

        _context.Update(contact);
        await _context.SaveChangesAsync();
    }
}
