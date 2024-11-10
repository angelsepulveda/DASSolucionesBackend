using DASSolucionesBackend.General.Submodules.VoucherTypes.Entities;

namespace DASSolucionesBackend.General.Submodules.VoucherTypes.Features.Pagination;

public sealed record PaginationVoucherTypeDto(Guid Id, string Name, string? Code, string? Description, bool Status);

public sealed record PaginationVoucherTypeQuery(PaginationRequest PaginationRequest)
    : IQuery<PaginationVoucherTypeResult>;

public sealed record PaginationVoucherTypeResult(PaginationResult<PaginationVoucherTypeDto> ResponseDto);

internal class
    PaginationVoucherTypeQueryHandler(IGeneralDbContext dbContext)
    : IQueryHandler<PaginationVoucherTypeQuery, PaginationVoucherTypeResult>
{
    public async Task<PaginationVoucherTypeResult> Handle(PaginationVoucherTypeQuery request,
        CancellationToken cancellationToken)
    {
        int pageIndex = request.PaginationRequest.PageIndex;
        int pageSize = request.PaginationRequest.PageSize;

        long totalCount = await dbContext.Regions.LongCountAsync(cancellationToken);

        List<VoucherType> voucherTypes = await dbContext.VoucherTypes
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        List<PaginationVoucherTypeDto> dtos = voucherTypes
            .Select(x => new PaginationVoucherTypeDto(x.Id, x.Name, x.Code, x.Description, x.Status)).ToList();

        return new PaginationVoucherTypeResult(
            new PaginationResult<PaginationVoucherTypeDto>(pageIndex, pageSize, totalCount, dtos));
    }
}