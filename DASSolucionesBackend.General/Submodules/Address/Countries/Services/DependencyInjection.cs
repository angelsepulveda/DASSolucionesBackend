using DASSolucionesBackend.General.Submodules.Address.Countries.Contracts.Services;

namespace DASSolucionesBackend.General.Submodules.Address.Countries.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddCountriesServices(this IServiceCollection services)
    {
        services.AddScoped<IGetByIdCountryService, GetByIdCountryService>();
        
        return services;
    }
}