namespace Core;

public interface IEventHandler;

public interface IEventHandler<in T> : IEventHandler where T : BaseEvent
{
    Task On(T @event);
}
