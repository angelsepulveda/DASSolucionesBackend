using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Countries.Features.Pagination;

public sealed record PaginationCountryDto(Guid Id, string Name, string? Code, string? Demonym, string Status);

public sealed record PaginationCountryQuery(PaginationRequest PaginationRequest)
    : IQuery<PaginationCountryResult>;

public sealed record PaginationCountryResult(PaginationResult<PaginationCountryDto> ResponseDto);

internal class
    PaginationCountryQueryHandler(IGeneralDbContext dbContext)
    : IQueryHandler<PaginationCountryQuery, PaginationCountryResult>
{
    public async Task<PaginationCountryResult> Handle(PaginationCountryQuery request,
        CancellationToken cancellationToken)
    {
        int pageIndex = request.PaginationRequest.PageIndex;
        int pageSize = request.PaginationRequest.PageSize;

        IQueryable<Country> query = dbContext.Countries.AsQueryable();

        if (request.PaginationRequest.NumberFilter is not null &&
            !string.IsNullOrWhiteSpace(request.PaginationRequest.Search))
        {
            query = request.PaginationRequest.NumberFilter switch
            {
                1 => query.Where(x => x.Name.Contains(request.PaginationRequest.Search)),
                2 => query.Where(x => x.Code.Contains(request.PaginationRequest.Search)),
                3 => query.Where(x => x.Demonym.Contains(request.PaginationRequest.Search)),
                _ => query
            };
        }

        if (request.PaginationRequest.StateFilter is not null)
        {
            bool status = request.PaginationRequest.StateFilter == 1;
            query = query.Where(x => x.Status == status);
        }

        List<Country> countries = await Shared.Data.Pagination.Ordering(request.PaginationRequest, query)
            .ToListAsync(cancellationToken: cancellationToken);

        long totalCount = await dbContext.Countries.LongCountAsync(cancellationToken);

        List<PaginationCountryDto> countriesDtos = countries
            .Select(x => new PaginationCountryDto(x.Id, x.Name, x.Code, x.Demonym,
                x.Status ? "Activo" : "Inactivo"))
            .ToList();

        return new PaginationCountryResult(
            new PaginationResult<PaginationCountryDto>(pageIndex, pageSize, totalCount, countriesDtos));
    }
}