using System.Reflection;
using Core;

namespace Query.Api;

public static class Startup
{
    public static void RegisterEventHandlers(this IServiceCollection services, Assembly assembly)
    {
        var eventHandlers = assembly.GetTypes()
            .Where(type => type.GetInterfaces()
                .Any(interfaceType => interfaceType.IsGenericType
                                      && interfaceType.GetGenericTypeDefinition() == typeof(IEventHandler<>)));

        foreach (var eventHandler in eventHandlers)
        {
            services.Add(new ServiceDescriptor(
                typeof(IEventHandler),
                eventHandler,
                ServiceLifetime.Scoped));
        }
    }
}
