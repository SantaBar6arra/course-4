namespace Core;

public class EventModel
{
    public string Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public Guid AggregateId { get; set; }
    public string AggregateType { get; set; } = string.Empty;
    public int Version { get; set; }
    public string EventType { get; set; } = string.Empty;
    public BaseEvent EventData { get; set; }
}
