using DASSolucionesBackend.General.Submodules.VoucherTypes.Entities;

namespace DASSolucionesBackend.General.Submodules.VoucherTypes.Contracts.Services;

public interface IGetByIdVoucherTypeService
{
    Task<VoucherType> HandleAsync(Guid id, CancellationToken cancellationToken);
}