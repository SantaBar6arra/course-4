using Confluent.Kafka;
using Core;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Query.Infrastructure;

public class EventConsumer : IEventConsumer
{
    private readonly ConsumerConfig _config;
    private readonly IList<IEventHandler> _eventHandlers;

    public EventConsumer(IOptions<ConsumerConfig> config, IEnumerable<IEventHandler> eventHandlers)
    {
        _config = config.Value;
        _eventHandlers = eventHandlers.ToList();
    }

    public async Task Consume(string topic)
    {
        using var consumer = new ConsumerBuilder<string, string>(_config)
            .SetKeyDeserializer(Deserializers.Utf8)
            .SetValueDeserializer(Deserializers.Utf8)
            .Build();

        var settings = new JsonSerializerSettings { Converters = { new EventJsonConverter() } };
        consumer.Subscribe(topic);

        while (true)
        {
            var consumeResult = consumer.Consume();
            if (consumeResult.Message is null)
                continue;

            Console.WriteLine(consumeResult.Message.Value);

            var @event = JsonConvert.DeserializeObject<BaseEvent>(consumeResult.Message.Value, settings)
                ?? throw new InvalidOperationException("could not deserialize event message!");

            await HandleEvent(@event);

            consumer.Commit(consumeResult);
        }
    }

    private async Task HandleEvent(BaseEvent @event)
    {
        var handlers = _eventHandlers
            .Select(handler => (handler, on: handler.GetType().GetMethod("On", [@event.GetType()])))
            .Where(handlerAndOnMethod => handlerAndOnMethod.on is not null)
            .ToArray();

        if (handlers.Length is 0)
            throw new InvalidOperationException("no 'On' method found for event type");

        foreach (var (handler, on) in handlers)
            await (Task)on.Invoke(handler, [@event]);
    }
}
