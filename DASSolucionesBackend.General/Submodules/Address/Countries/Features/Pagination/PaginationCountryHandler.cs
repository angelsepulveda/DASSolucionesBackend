using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Countries.Features.Pagination;

public sealed record PaginationCountryDto(Guid Id, string Name, string? Code, string? Demonym, bool Status);

public sealed record PaginationCountryQuery(PaginationRequest PaginationRequest)
    : IQuery<PaginationCountryResult>;

public sealed record PaginationCountryResult(PaginationResult<PaginationCountryDto> ResponseDto);

internal class
    PaginationCountryQueryHandler(IGeneralDbContext dbContext)
    : IQueryHandler<PaginationCountryQuery, PaginationCountryResult>
{
    public async Task<PaginationCountryResult> Handle(PaginationCountryQuery request, CancellationToken cancellationToken)
    {
        int pageIndex = request.PaginationRequest.PageIndex;
        int pageSize = request.PaginationRequest.PageSize;

        long totalCount = await dbContext.DocumentTypes.LongCountAsync(cancellationToken);

        List<Country> countries = await dbContext.Countries
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        List<PaginationCountryDto> documentTypeDtos = countries
            .Select(x => new PaginationCountryDto(x.Id, x.Name, x.Code, x.Demonym, x.Status)).ToList();

        return new PaginationCountryResult(
            new PaginationResult<PaginationCountryDto>(pageIndex, pageSize, totalCount, documentTypeDtos));
    }
}