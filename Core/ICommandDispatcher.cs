namespace Core;

public interface ICommandDispatcher
{
    void Register<T>(Func<T, Task> handler) where T : BaseCommand;
    Task SendAsync(BaseCommand command);
}
