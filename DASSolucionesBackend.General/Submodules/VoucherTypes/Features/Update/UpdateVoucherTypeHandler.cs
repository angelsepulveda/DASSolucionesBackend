using DASSolucionesBackend.General.Submodules.VoucherTypes.Contracts.Services;
using DASSolucionesBackend.General.Submodules.VoucherTypes.Entities;

namespace DASSolucionesBackend.General.Submodules.VoucherTypes.Features.Update;

public sealed record UpdateVoucherTypePayloadDto(Guid Id, string Name,string? Code, string? Description);

public sealed record UpdateVoucherTypeCommand(UpdateVoucherTypePayloadDto PayloadDto)
    : ICommand;

internal class UpdateVoucherTypeCommandHandler(
    IGeneralDbContext dbContext,
    IGetByIdVoucherTypeService getByIdVoucherTypeService) : ICommandHandler<UpdateVoucherTypeCommand>
{
    public async Task<Unit> Handle(UpdateVoucherTypeCommand request, CancellationToken cancellationToken)
    {
        VoucherType voucherType =
            await getByIdVoucherTypeService.HandleAsync(request.PayloadDto.Id, cancellationToken);

        voucherType.Update(request.PayloadDto.Name, request.PayloadDto.Code, request.PayloadDto.Description);

        dbContext.VoucherTypes.Update(voucherType);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}