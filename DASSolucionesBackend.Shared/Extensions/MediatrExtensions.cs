using System.Reflection;
using DASSolucionesBackend.Shared.Behaviors;

namespace DASSolucionesBackend.Shared.Extensions;

public static class MediatrExtensions
{
    public static IServiceCollection AddMediatrWithAssemblies
        (this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assemblies);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(assemblies);

        return services;
    }
}