using CouchDB.Driver.Types;
using Newtonsoft.Json;

namespace Core;

[JsonConverter(typeof(EventModelConverter))]
public class EventModel : CouchDocument
{
    public DateTime TimeStamp { get; set; }
    public Guid AggregateId { get; set; }
    public string AggregateType { get; set; } = string.Empty;
    public int Version { get; set; }
    public string EventType { get; set; } = string.Empty;
    public BaseEvent EventData { get; set; }
}
