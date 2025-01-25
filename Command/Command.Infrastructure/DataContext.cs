using Core;
using CouchDB.Driver;

namespace Command.Infrastructure;

public class DataContext : CouchContext
{
    public CouchDatabase<EventModel> Events { get; set; }
}
