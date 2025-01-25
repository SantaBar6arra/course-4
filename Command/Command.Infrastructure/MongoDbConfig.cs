namespace Command.Infrastructure;

public record MongoDbConfig(string ConnectionString, string Database, string Collection)
{
    public MongoDbConfig() : this(string.Empty, string.Empty, string.Empty)
    {

    }
}
