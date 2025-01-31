namespace Core;

public interface IEventSourcingHandler<T> where T : AggregateRoot, new()
{
    Task SaveAsync(T root);
    Task<T> GetByIdAsync(Guid id);
}
