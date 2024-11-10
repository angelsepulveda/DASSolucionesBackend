using DASSolucionesBackend.Shared.Data;
using DASSolucionesBackend.Shared.Data.Interceptors;
using DASSolucionesBackend.Warehouses.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DASSolucionesBackend.Warehouses;

public static class WarehousesModule
{
    public static IServiceCollection AddWareHousesModule(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException(nameof(configuration));

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<IWareHousesDbContext, WareHousesDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        services.AddGeneralServices();
        
        return services;
    }

    public static IApplicationBuilder UseGeneralModule(this IApplicationBuilder app)
    {
        app.UseMigration<WareHousesDbContext>();

        return app;
    }
    
    private static IServiceCollection AddGeneralServices(this IServiceCollection services)
    {
        return services;
    }
}