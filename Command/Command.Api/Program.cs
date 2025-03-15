using Command.Api;
using Command.Api.Handlers;
using Command.Domain.Aggregates;
using Command.Infrastructure;
using Confluent.Kafka;
using Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RavenDbConfig>(builder.Configuration.GetSection(nameof(RavenDbConfig)));
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));
builder.Services.Configure<KafkaConfig>(builder.Configuration.GetSection(nameof(KafkaConfig)));
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepositoryV3>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<Client>, EventSourcingHandler<Client>>();
builder.Services.AddScoped<IEventSourcingHandler<ClientContact>, EventSourcingHandler<ClientContact>>();
builder.Services.AddScoped<IEventSourcingHandler<Product>, EventSourcingHandler<Product>>();
builder.Services.AddScoped<IClientCommandHandler, ClientCommandHandler>();
builder.Services.AddScoped<IProductCommandHandler, ProductCommandHandler>();

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
