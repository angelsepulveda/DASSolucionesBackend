using DASSolucionesBackend.General.Data;
using DASSolucionesBackend.General.Submodules.Address;
using DASSolucionesBackend.General.Submodules.DocumentTypes.Services;
using DASSolucionesBackend.General.Submodules.VoucherTypes.Services;
using DASSolucionesBackend.Shared.Data;
using DASSolucionesBackend.Shared.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace DASSolucionesBackend.General;

public static class GeneralModule
{
    public static IServiceCollection AddGeneralModule(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException(nameof(configuration));

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<IGeneralDbContext, GeneralDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        services.AddGeneralServices();
        
        return services;
    }

    public static IApplicationBuilder UseGeneralModule(this IApplicationBuilder app)
    {
        app.UseMigration<GeneralDbContext>();

        return app;
    }
    
    private static IServiceCollection AddGeneralServices(this IServiceCollection services)
    {
        services.AddDocumentTypeServices();
        services.AddAddressSubModule();
        services.AddVoucherTypesServices();
        
        return services;
    }
}