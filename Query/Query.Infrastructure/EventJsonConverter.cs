using Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Query.Infrastructure;

public class EventJsonConverter : JsonConverter<BaseEvent>
{
    public override BaseEvent? ReadJson(
        JsonReader reader, Type objectType, BaseEvent? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var document = JObject.Load(reader);
        var typeName = document["Type"]?.ToString();

        if (string.IsNullOrEmpty(typeName))
            throw new JsonException("Could not detect the 'Type' property!");

        var eventType = GetEventType(typeName)
            ?? throw new JsonException("could not get event type!");

        var json = document.ToString();
        return JsonConvert.DeserializeObject(json, eventType) as BaseEvent
            ?? throw new JsonException("cannot deserialize an event!");
    }

    public override void WriteJson(JsonWriter writer, BaseEvent? value, JsonSerializer serializer)
    {
        throw new NotImplementedException(); // we wont use it!
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
