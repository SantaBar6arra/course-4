using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core;

public class EventModelConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(EventModel);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);
        var eventTypeName = jsonObject["eventType"].ToString()
            ?? throw new JsonSerializationException("Missing 'eventType' property in event JSON.");

        var eventType = GetEventType(eventTypeName)
            ?? throw new JsonSerializationException($"Unknown event type: {eventTypeName}");

        var eventData = jsonObject["eventData"].ToObject(eventType, serializer)
            ?? throw new JsonSerializationException($"Couldn't deserialize event data for {eventTypeName}"); ;

        return new EventModel // fuck it for now, too much time spent
        {
            TimeStamp = DateTime.Parse(jsonObject["timeStamp"].ToString()),
            Version = int.Parse(jsonObject["version"].ToString()),
            AggregateType = jsonObject["aggregateType"].ToString(),
            AggregateId = Guid.Parse(jsonObject["aggregateId"].ToString()),
            EventType = eventTypeName,
            EventData = (BaseEvent)eventData,
        };
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }

    private static Type? GetEventType(string eventTypeName)
    {
        var assemblyName = eventTypeName.Split('.').First();
        var type = AppDomain.CurrentDomain
            .GetAssemblies()
            .Single(assembly => assembly.GetName().Name == assemblyName)
            .DefinedTypes
            .SingleOrDefault(type => type.FullName == eventTypeName);

        // var type = Type.GetType(eventTypeName);
        return type != null && typeof(BaseEvent).IsAssignableFrom(type)
            ? type
            : null;
    }
}
