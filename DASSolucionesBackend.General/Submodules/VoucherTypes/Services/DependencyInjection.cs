using DASSolucionesBackend.General.Submodules.VoucherTypes.Contracts.Services;

namespace DASSolucionesBackend.General.Submodules.VoucherTypes.Services;

public static class DependencyInjection 
{
    public static IServiceCollection AddVoucherTypesServices(this IServiceCollection services)
    {
        services.AddScoped<IGetByIdVoucherTypeService, GetByIdVoucherTypeService>();

        return services;
    }
}