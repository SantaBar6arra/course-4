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

        dispatcher.Register<CreateClient>(clientCommandHandler.HandleAsync);
        dispatcher.Register<UpdateClient>(clientCommandHandler.HandleAsync);
        dispatcher.Register<DeleteClient>(clientCommandHandler.HandleAsync);

        builder.Services.AddSingleton<ICommandDispatcher>(_ => dispatcher);
    }
}
