using Core;

namespace Command.Infrastructure.Handlers;

public class EventSourcingHandler<T>(IEventStore eventStore)
    : IEventSourcingHandler<T> where T : AggregateRoot, new()
{
    public async Task<T> GetByIdAsync(Guid aggregateId)
    {
        var aggregate = new T();
        var events = await eventStore.GetEventsAsync(aggregateId, typeof(T));

        if (events is null or { Count: 0 })
            return aggregate;

        aggregate.ReplayEvents(events);
        aggregate.Version = events.Select(evt => evt.Version).Max();

        return aggregate;
    }

    public async Task SaveAsync(AggregateRoot aggregate)
    {
        await eventStore.SaveEventsAsync(aggregate, aggregate.GetUncommittedChanges(), aggregate.Version);
        aggregate.MarkChangesAsCommitted();
    }
}
