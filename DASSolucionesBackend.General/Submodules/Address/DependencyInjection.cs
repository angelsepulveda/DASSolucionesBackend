using DASSolucionesBackend.General.Submodules.Address.Countries.Services;
using DASSolucionesBackend.General.Submodules.Address.Localities.Services;
using DASSolucionesBackend.General.Submodules.Address.Regions.Services;

namespace DASSolucionesBackend.General.Submodules.Address;

public static class DependencyInjection
{
    public static IServiceCollection AddAddressSubModule(this IServiceCollection services)
    {
        services.AddCountriesServices();
        services.AddRegionsServices();
        services.AddLocalitiesServices();
        
        return services;
    }
}