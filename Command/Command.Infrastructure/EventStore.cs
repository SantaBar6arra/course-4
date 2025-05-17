using Core;
using Microsoft.Extensions.Options;

namespace Command.Infrastructure;

public class EventStore(
    IEventStoreRepository eventStoreRepository,
    IEventProducer eventProducer,
    IOptions<KafkaConfig> kafkaConfig) : IEventStore
{
    private readonly KafkaConfig _kafkaConfig = kafkaConfig.Value;

    public async Task<IList<BaseEvent>> GetEventsAsync(Guid aggregateId, Type aggregateType)
    {
        var eventStream = await eventStoreRepository.FindEvents(aggregateId, aggregateType);

        if (eventStream is null or { Count: 0 })
            throw new AggregateNotFoundException($"incorrect {nameof(aggregateId)} provided!");

        return eventStream
            .OrderBy(@event => @event.Version)
            .Select(@event => @event.EventData)
            .ToList();
    }

    public async Task SaveEventsAsync(AggregateRoot aggregate, IEnumerable<BaseEvent> events, int expectedVersion)
    {
        var aggregateType = aggregate.GetType();
        var eventStream = await eventStoreRepository.FindEvents(aggregate.Id, aggregateType);

        if (expectedVersion is not -1 && eventStream[^1].Version != expectedVersion)
            throw new ConcurrencyException();

        var version = expectedVersion;

        foreach (var @event in events)
        {
            version++;
            @event.Version = version;

            var eventModel = new EventModel
            {
                TimeStamp = DateTime.UtcNow,
                Version = version,
                EventData = @event,
                EventType = @event.GetType().FullName!,
                AggregateId = aggregate.Id,
                AggregateType = aggregate.GetType().Name
            };

            await eventStoreRepository.SaveAsync(eventModel);

            var topic = _kafkaConfig.Topic ?? throw new Exception("KAFKA_TOPIC is not set");
            await eventProducer.ProduceAsync(topic, @event);
        }
    }
}
