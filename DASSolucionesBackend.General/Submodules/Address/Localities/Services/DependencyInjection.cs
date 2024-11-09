using DASSolucionesBackend.General.Submodules.Address.Localities.Contracts.Services;

namespace DASSolucionesBackend.General.Submodules.Address.Localities.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddLocalitiesServices(this IServiceCollection services)
    {
        services.AddScoped<IGetByIdLocalityService, GetByIdLocalityService>();

        return services;
    }
}