namespace Command.Infrastructure;

public record CouchDbConfig(string Endpoint, string Username, string Password)
{
    public CouchDbConfig() : this("", "", "") { }
}
