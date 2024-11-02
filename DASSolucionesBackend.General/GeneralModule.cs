using System.Reflection;
using DASSolucionesBackend.General.Data;
using DASSolucionesBackend.Shared.Data;
using DASSolucionesBackend.Shared.Data.Interceptors;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DASSolucionesBackend.General;

public static class GeneralModule
{
    public static IServiceCollection AddGeneralModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        string connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException(nameof(configuration));
        
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        
        services.AddDbContext<GeneralDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });
        
        return services;
    }

    public static IApplicationBuilder UseGeneralModule(this IApplicationBuilder app)
    {
        app.UseMigration<GeneralDbContext>();
        
        return app;
    }
}