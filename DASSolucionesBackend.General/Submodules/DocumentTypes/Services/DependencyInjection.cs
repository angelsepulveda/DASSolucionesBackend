using DASSolucionesBackend.General.Submodules.DocumentTypes.Contracts.Services;

namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddDocumentTypeServices(this IServiceCollection services)
    {
        services.AddScoped<IGetByIdDocumentTypeService, GetByIdDocumentTypeService>();
        
        return services;
    }
}