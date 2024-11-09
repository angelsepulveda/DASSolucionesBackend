using DASSolucionesBackend.General.Submodules.Address.Localities.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Localities.Features.Pagination;

public sealed record PaginationLocalityDto(
    Guid Id,
    string Name,
    string? Code,
    Guid CountryId,
    string Country,
    Guid RegionId,
    string Region,
    bool Status);

public sealed record PaginationLocalityQuery(PaginationRequest PaginationRequest)
    : IQuery<PaginationLocalityResult>;

public sealed record PaginationLocalityResult(PaginationResult<PaginationLocalityDto> ResponseDto);

internal class
    PaginationLocalityQueryHandler(IGeneralDbContext dbContext)
    : IQueryHandler<PaginationLocalityQuery, PaginationLocalityResult>
{
    public async Task<PaginationLocalityResult> Handle(PaginationLocalityQuery request,
        CancellationToken cancellationToken)
    {
        int pageIndex = request.PaginationRequest.PageIndex;
        int pageSize = request.PaginationRequest.PageSize;

        long totalCount = await dbContext.Regions.LongCountAsync(cancellationToken);

        List<Locality> localities = await dbContext.Localities
            .AsNoTracking()
            .Include(r => r.Country)
            .Include(p => p.Region)
            .OrderBy(p => p.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        List<PaginationLocalityDto> dtos = localities
            .Select(x => new PaginationLocalityDto(x.Id, x.Name, x.Code, x.CountryId, x.Country.Name, x.RegionId,
                x.Region.Name, x.Status)).ToList();

        return new PaginationLocalityResult(
            new PaginationResult<PaginationLocalityDto>(pageIndex, pageSize, totalCount, dtos));
    }
}