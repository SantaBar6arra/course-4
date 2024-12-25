namespace Core;

public interface IEventConsumer
{
    Task Consume(string topic);
}
