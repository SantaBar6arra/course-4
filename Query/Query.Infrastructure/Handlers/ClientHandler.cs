using Common.Events;
using Common.Models;
using Core;
using Query.Domain.Entities;

namespace Query.Infrastructure.Handlers;

public class ClientHandler(DataContext context)
    : IEventHandler<ClientCreated>
    , IEventHandler<ClientBaseDataUpdated>
    , IEventHandler<ClientAddressUpdated>
    , IEventHandler<ClientDeleted>
{
    public DataContext _context = context;

    public async Task On(ClientCreated @event)
    {
        var client = new Client()
        {
            Id = @event.Id,
            FullName = $"{@event.FirstName} {@event.LastName}",
            Status = @event.Status,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Country = @event.Address.Country,
            Region = @event.Address.Region,
            City = @event.Address.City,
            Street = @event.Address.Street,
            House = @event.Address.House,
            PostalCode = @event.Address.PostalCode
        };

        await _context.AddAsync(client);
        await _context.SaveChangesAsync();
    }

    public async Task On(ClientBaseDataUpdated @event)
    {
        var client = await _context.Clients.FindAsync([@event.Id])
            ?? throw new Exception("client not found!");

        client.FullName = $"{@event.FirstName} {@event.LastName}";
        client.Status = @event.Status;
        client.LastUpdatedAt = DateTime.UtcNow;

        _context.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task On(ClientAddressUpdated @event)
    {
        var client = await _context.Clients.FindAsync([@event.Id])
            ?? throw new Exception("client not found!");

        client.Country = @event.Address.Country;
        client.Region = @event.Address.Region;
        client.City = @event.Address.City;
        client.Street = @event.Address.Street;
        client.House = @event.Address.House;
        client.PostalCode = @event.Address.PostalCode;
        client.LastUpdatedAt = DateTime.UtcNow;

        _context.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task On(ClientDeleted @event)
    {
        var client = await _context.Clients.FindAsync([@event.Id])
            ?? throw new Exception("client not found!");

        client.Status = ClientStatus.Deleted;
        client.LastUpdatedAt = DateTime.UtcNow;

        _context.Update(client);
        await _context.SaveChangesAsync();
    }
}
