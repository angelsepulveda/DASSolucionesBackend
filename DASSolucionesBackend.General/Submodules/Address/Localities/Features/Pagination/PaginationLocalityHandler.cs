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
    string Status);

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

        IQueryable<Locality> query = dbContext.Localities.AsQueryable();

        query = query.Include(x => x.Country).Include(x => x.Region);

        if (request.PaginationRequest.NumberFilter is not null &&
            !string.IsNullOrWhiteSpace(request.PaginationRequest.Search))
        {
            query = request.PaginationRequest.NumberFilter switch
            {
                1 => query.Where(x => x.Name.Contains(request.PaginationRequest.Search)),
                2 => query.Where(x => x.Code.Contains(request.PaginationRequest.Search)),
                3 => query.Where(x => x.Region.Name.Contains(request.PaginationRequest.Search)),
                4 => query.Where(x => x.Country.Name.Contains(request.PaginationRequest.Search)),
                _ => query
            };
        }

        if (request.PaginationRequest.StateFilter is not null)
        {
            bool status = request.PaginationRequest.StateFilter == 1;
            query = query.Where(x => x.Status == status);
        }

        List<Locality> localities = await Shared.Data.Pagination.Ordering(request.PaginationRequest, query)
            .ToListAsync(cancellationToken: cancellationToken);
        
        long totalCount = await dbContext.Localities.LongCountAsync(cancellationToken);

        List<PaginationLocalityDto> dtos = localities
            .Select(x => new PaginationLocalityDto(x.Id, x.Name, x.Code, x.CountryId, x.Country.Name, x.RegionId,
                x.Region.Name, x.Status ? "Activo" : "Inactivo")).ToList();

        return new PaginationLocalityResult(
            new PaginationResult<PaginationLocalityDto>(pageIndex, pageSize, totalCount, dtos));
    }
}