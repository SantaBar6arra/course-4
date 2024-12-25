namespace Core;

public interface IEventStore
{
    Task SaveEventsAsync(AggregateRoot aggregate, IEnumerable<BaseEvent> events, int expectedVersion);
    Task<IList<BaseEvent>> GetEventsAsync(Guid aggregateId, Type aggregateType);
}
