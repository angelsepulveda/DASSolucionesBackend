using DASSolucionesBackend.General.Submodules.VoucherTypes.Entities;

namespace DASSolucionesBackend.General.Submodules.VoucherTypes.Features.Pagination;

public sealed record PaginationVoucherTypeDto(Guid Id, string Name, string? Code, string? Description, string Status);

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

        IQueryable<VoucherType> query = dbContext.VoucherTypes.AsQueryable();

        if (request.PaginationRequest.NumberFilter is not null &&
            !string.IsNullOrWhiteSpace(request.PaginationRequest.Search))
        {
            query = request.PaginationRequest.NumberFilter switch
            {
                1 => query.Where(x => x.Name.Contains(request.PaginationRequest.Search)),
                2 => query.Where(x => x.Code.Contains(request.PaginationRequest.Search)),
                3 => query.Where(x => x.Description.Contains(request.PaginationRequest.Search)),
                _ => query
            };
        }

        if (request.PaginationRequest.StateFilter is not null)
        {
            bool status = request.PaginationRequest.StateFilter == 1;
            query = query.Where(x => x.Status == status);
        }

        List<VoucherType> voucherTypes = await Shared.Data.Pagination.Ordering(request.PaginationRequest, query)
            .ToListAsync(cancellationToken: cancellationToken);

        long totalCount = await dbContext.VoucherTypes.LongCountAsync(cancellationToken);

        List<PaginationVoucherTypeDto> dtos = voucherTypes
            .Select(x =>
                new PaginationVoucherTypeDto(x.Id, x.Name, x.Code, x.Description, x.Status ? "Activo" : "Inactivo"))
            .ToList();

        return new PaginationVoucherTypeResult(
            new PaginationResult<PaginationVoucherTypeDto>(pageIndex, pageSize, totalCount, dtos));
    }
}