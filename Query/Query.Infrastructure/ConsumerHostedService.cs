using Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Query.Infrastructure;

public class ConsumerHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ConsumerHostedService> _logger;
    private readonly KafkaConfig _kafkaConfig;

    public ConsumerHostedService(
        IServiceProvider serviceProvider,
        ILogger<ConsumerHostedService> logger,
        IOptions<KafkaConfig> options)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _kafkaConfig = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting consumer hosted background service");

        using var scope = _serviceProvider.CreateScope();
        var eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();

        if (string.IsNullOrEmpty(_kafkaConfig.Topic))
            throw new Exception("Could not find Kafka.Topic");

        await Task.Run(() => eventConsumer.Consume(_kafkaConfig.Topic), stoppingToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping consumer hosted service");
        return Task.CompletedTask;
    }
}
