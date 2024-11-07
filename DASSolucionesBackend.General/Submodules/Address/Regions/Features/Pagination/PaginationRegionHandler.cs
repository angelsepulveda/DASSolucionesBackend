using DASSolucionesBackend.General.Submodules.Address.Regions.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Regions.Features.Pagination;


public sealed record PaginationRegionDto(Guid Id, string Name, string? Code, string Country, bool Status);

public sealed record PaginationRegionQuery(PaginationRequest PaginationRequest)
    : IQuery<PaginationRegionResult>;

public sealed record PaginationRegionResult(PaginationResult<PaginationRegionDto> ResponseDto);

internal class
    PaginationRegionQueryHandler(IGeneralDbContext dbContext)
    : IQueryHandler<PaginationRegionQuery, PaginationRegionResult>
{
    public async Task<PaginationRegionResult> Handle(PaginationRegionQuery request, CancellationToken cancellationToken)
    {
        int pageIndex = request.PaginationRequest.PageIndex;
        int pageSize = request.PaginationRequest.PageSize;

        long totalCount = await dbContext.Regions.LongCountAsync(cancellationToken);

        List<Region> regions = await dbContext.Regions
            .AsNoTracking()
            .Include(r => r.Country)
            .OrderBy(p => p.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        List<PaginationRegionDto> dtos = regions
            .Select(x => new PaginationRegionDto(x.Id, x.Name, x.Code, x.Country.Name, x.Status)).ToList();

        return new PaginationRegionResult(
            new PaginationResult<PaginationRegionDto>(pageIndex, pageSize, totalCount, dtos));
    }
}