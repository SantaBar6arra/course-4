using System;
using Command.Api.Commands;
using Command.Api.Handlers;
using Command.Infrastructure;
using Core;

namespace Command.Api;

public static class Startup
{
    public static void RegisterCommandHandlers(this WebApplicationBuilder builder)
    {
        var (dispatcher, serviceProvider) = (new CommandDispatcher(), builder.Services.BuildServiceProvider());
        var clientCommandHandler = serviceProvider.GetRequiredService<IClientCommandHandler>();
        var productCommandHandler = serviceProvider.GetRequiredService<IProductCommandHandler>();

        dispatcher.Register<CreateClient>(clientCommandHandler.HandleAsync);
        dispatcher.Register<UpdateClient>(clientCommandHandler.HandleAsync);
        dispatcher.Register<DeleteClient>(clientCommandHandler.HandleAsync);
        dispatcher.Register<UpdateClientContact>(clientCommandHandler.HandleAsync);
        dispatcher.Register<DeleteClientContact>(clientCommandHandler.HandleAsync);

        dispatcher.Register<CreateProduct>(productCommandHandler.HandleAsync);
        dispatcher.Register<UpdateProductDetails>(productCommandHandler.HandleAsync);
        dispatcher.Register<UpdateProductStockQuantity>(productCommandHandler.HandleAsync);
        dispatcher.Register<DiscontinueProduct>(productCommandHandler.HandleAsync);
        dispatcher.Register<UnlockProduct>(productCommandHandler.HandleAsync);

        builder.Services.AddSingleton<ICommandDispatcher>(_ => dispatcher);
    }
}
