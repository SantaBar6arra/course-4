using Core;
using CouchDB.Driver;
using CouchDB.Driver.Options;

namespace Command.Infrastructure;

public class DataContext : CouchContext
{
    public CouchDatabase<EventModel> Events { get; set; }

    public DataContext(CouchOptions<DataContext> options) : base(options) { }
}
