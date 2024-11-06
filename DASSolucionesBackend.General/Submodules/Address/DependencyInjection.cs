using DASSolucionesBackend.General.Submodules.Address.Countries.Services;

namespace DASSolucionesBackend.General.Submodules.Address;

public static class DependencyInjection
{
    public static IServiceCollection AddAddressSubModule(this IServiceCollection services)
    {
        services.AddCountriesServices();
        
        return services;
    }
}