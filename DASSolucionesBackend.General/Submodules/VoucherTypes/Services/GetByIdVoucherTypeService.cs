using DASSolucionesBackend.General.Submodules.VoucherTypes.Contracts.Services;
using DASSolucionesBackend.General.Submodules.VoucherTypes.Entities;
using DASSolucionesBackend.General.Submodules.VoucherTypes.Exceptions;

namespace DASSolucionesBackend.General.Submodules.VoucherTypes.Services;

internal sealed class GetByIdVoucherTypeService(IGeneralDbContext dbContext) : IGetByIdVoucherTypeService
{
    public async Task<VoucherType> HandleAsync(Guid id, CancellationToken cancellationToken)
    {
        VoucherType? voucharType =
            await dbContext.VoucherTypes.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

        if (voucharType is null) throw new VoucherTypeNotFoundException(id);

        return voucharType;
    }
}