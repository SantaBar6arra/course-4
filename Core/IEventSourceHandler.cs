namespace Core;

public interface IEventSourcingHandler<T> // todo consider "where" T is aggregateRoot or what
{
    Task SaveAsync(AggregateRoot root);
    Task<T> GetByIdAsync(Guid id);
}
