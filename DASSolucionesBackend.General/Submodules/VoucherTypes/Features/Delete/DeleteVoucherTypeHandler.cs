using DASSolucionesBackend.General.Submodules.VoucherTypes.Contracts.Services;
using DASSolucionesBackend.General.Submodules.VoucherTypes.Entities;

namespace DASSolucionesBackend.General.Submodules.VoucherTypes.Features.Delete;

public sealed record DeleteVoucherTypeCommand(Guid Id)
    : ICommand;

internal class DeleteVoucherTypeCommandHandler(
    IGeneralDbContext dbContext,
    IGetByIdVoucherTypeService getByIdVoucherTypeService ) : ICommandHandler<DeleteVoucherTypeCommand>
{
    public async Task<Unit> Handle(DeleteVoucherTypeCommand request, CancellationToken cancellationToken)
    {
        VoucherType voucherType =
            await getByIdVoucherTypeService.HandleAsync(request.Id, cancellationToken);

        voucherType.ChangeStatus(false);

        dbContext.VoucherTypes.Update(voucherType);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}