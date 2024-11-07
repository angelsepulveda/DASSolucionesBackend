using DASSolucionesBackend.General.Submodules.Address.Regions.Contracts.Services;

namespace DASSolucionesBackend.General.Submodules.Address.Regions.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddRegionsServices(this IServiceCollection services)
    {
        services.AddScoped<IGetByIdRegionService, GetByIdRegionService>();
        
        return services;
    }
}