using DASSolucionesBackend.General.Submodules.VoucherTypes.Contracts.Services;
using DASSolucionesBackend.General.Submodules.VoucherTypes.Entities;

namespace DASSolucionesBackend.General.Submodules.VoucherTypes.Features.Restore;

public sealed record RestoreVoucherTypeCommand(Guid Id)
    : ICommand;

internal class RestoreVoucherTypeCommandHandler(
    IGeneralDbContext dbContext,
    IGetByIdVoucherTypeService getByIdVoucherTypeService) : ICommandHandler<RestoreVoucherTypeCommand>
{
    public async Task<Unit> Handle(RestoreVoucherTypeCommand request, CancellationToken cancellationToken)
    {
         VoucherType voucherType =
            await getByIdVoucherTypeService.HandleAsync(request.Id, cancellationToken);

        voucherType.ChangeStatus(true);

        dbContext.VoucherTypes.Update(voucherType);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}