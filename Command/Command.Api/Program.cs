using Command.Api;
using Command.Api.Handlers;
using Command.Domain.Aggregates;
using Command.Infrastructure;
using Confluent.Kafka;
using Core;
using CouchDB.Driver.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCouchContext<DataContext>(contextBuilder =>
{
    var couchDbConfig = builder.Configuration.GetSection(nameof(CouchDbConfig)).Get<CouchDbConfig>()
        ?? throw new NullReferenceException("no couchDb data in the config!");

    contextBuilder
        .UseEndpoint(couchDbConfig.Endpoint)
        .EnsureDatabaseExists()
        .UseBasicAuthentication(couchDbConfig.Username, couchDbConfig.Password);
});
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));
builder.Services.Configure<KafkaConfig>(builder.Configuration.GetSection(nameof(KafkaConfig)));
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepositoryV2>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<Client>, EventSourcingHandler<Client>>();
builder.Services.AddScoped<IClientCommandHandler, ClientCommandHandler>();

builder.RegisterCommandHandlers();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
