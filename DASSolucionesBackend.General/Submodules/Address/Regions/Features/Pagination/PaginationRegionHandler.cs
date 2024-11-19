using DASSolucionesBackend.General.Submodules.Address.Regions.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Regions.Features.Pagination;

public sealed record PaginationRegionDto(Guid Id, string Name, string? Code, string Country, string Status);

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

        IQueryable<Region> query = dbContext.Regions.AsQueryable();

        query = query.Include(x => x.Country);

        if (request.PaginationRequest.NumberFilter is not null &&
            !string.IsNullOrWhiteSpace(request.PaginationRequest.Search))
        {
            query = request.PaginationRequest.NumberFilter switch
            {
                1 => query.Where(x => x.Name.Contains(request.PaginationRequest.Search)),
                2 => query.Where(x => x.Code.Contains(request.PaginationRequest.Search)),
                3 => query.Where(x => x.Country.Name.Contains(request.PaginationRequest.Search)),
                _ => query
            };
        }

        if (request.PaginationRequest.StateFilter is not null)
        {
            bool status = request.PaginationRequest.StateFilter == 1;
            query = query.Where(x => x.Status == status);
        }

        List<Region> regions = await Shared.Data.Pagination.Ordering(request.PaginationRequest, query)
            .ToListAsync(cancellationToken: cancellationToken);

        long totalCount = await dbContext.Regions.LongCountAsync(cancellationToken);

        List<PaginationRegionDto> dtos = regions
            .Select(x =>
                new PaginationRegionDto(x.Id, x.Name, x.Code, x.Country.Name, x.Status ? "Activo" : "Inactivo"))
            .ToList();

        return new PaginationRegionResult(
            new PaginationResult<PaginationRegionDto>(pageIndex, pageSize, totalCount, dtos));
    }
}