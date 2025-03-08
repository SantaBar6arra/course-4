namespace Command.Infrastructure;

public record RavenDbConfig(string ServerUrl, string Database)
{
    public RavenDbConfig() : this(string.Empty, string.Empty)
    {

    }
}
