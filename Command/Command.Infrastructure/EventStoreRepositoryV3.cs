using Core;
using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace Command.Infrastructure;

public class EventStoreRepositoryV3 : IEventStoreRepository
{
    private readonly RavenDbConfig ravenDbConfig;
    private readonly SessionOptions sessionOptions;

    public EventStoreRepositoryV3(IOptions<RavenDbConfig> options)
    {
        ravenDbConfig = options.Value;
        sessionOptions = new SessionOptions
        {
            Database = ravenDbConfig.Database,
            TransactionMode = TransactionMode.ClusterWide
        };
    }

    public async Task<IList<EventModel>> FindEvents(Guid id, Type type)
    {
        using var store = new DocumentStore() { Urls = [ravenDbConfig.ServerUrl] }.Initialize();
        using var session = store.OpenAsyncSession(sessionOptions);

        var events = await session
            .Query<EventModel>()
            .Where(item => item.AggregateId == id && item.AggregateType == type.Name)
            .ToListAsync();

        return events;
    }

    public async Task SaveAsync(EventModel eventModel)
    {
        using var store = new DocumentStore() { Urls = [ravenDbConfig.ServerUrl] }.Initialize();
        using var session = store.OpenAsyncSession(sessionOptions);

        await session.StoreAsync(eventModel);
        await session.SaveChangesAsync();
    }
}
