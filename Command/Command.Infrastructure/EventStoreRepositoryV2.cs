using Core;
using CouchDB.Driver.Extensions;

namespace Command.Infrastructure;

public class EventStoreRepositoryV2(DataContext context) : IEventStoreRepository
{
    public async Task<IList<EventModel>> FindEvents(Guid id, Type type) => await context.Events
        .Where(@event => @event.AggregateId == id && @event.AggregateType == type.Name)
        .ToListAsync()
        .ConfigureAwait(false);

    public async Task SaveAsync(EventModel eventModel) => await context.Events
        .AddAsync(eventModel)
        .ConfigureAwait(false);
}
