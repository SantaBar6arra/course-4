namespace Query.Infrastructure;

public record KafkaConfig(string Topic)
{
    public KafkaConfig() : this(string.Empty)
    {

    }
}
