using System.Text.Json;
using Microsoft.Extensions.Options;
using Confluent.Kafka;
using Core;

namespace Command.Infrastructure;

public class EventProducer(IOptions<ProducerConfig> config) : IEventProducer
{
    private readonly ProducerConfig _config = config.Value;

    public async Task ProduceAsync<T>(string topic, T @event) where T : BaseEvent
    {
        using var producer = new ProducerBuilder<string, string>(_config)
            .SetKeySerializer(Serializers.Utf8)
            .SetValueSerializer(Serializers.Utf8)
            .Build();

        var message = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = JsonSerializer.Serialize(@event, @event.GetType())
        };

        var produceResult = await producer.ProduceAsync(topic, message);

        if (produceResult.Status is PersistenceStatus.NotPersisted)
            throw new Exception(
                $"event {@event.GetType().Name} was not persisted to topic {topic}, reason: {produceResult.Message}");
    }
}
