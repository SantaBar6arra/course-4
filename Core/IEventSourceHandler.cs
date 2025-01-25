namespace Core;

public interface IEventSourcingHandler<T> where T : AggregateRoot, new()
{
    Task SaveAsync(AggregateRoot root);
    Task<T> GetByIdAsync(Guid id);
}
