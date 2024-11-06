using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Countries.Features.GetAll;

public sealed record GetAllCountryReponseDto(Guid Id, string Name);

public sealed record GetAllCountryQuery()
    : IQuery<GetAllCountryResult>;

public sealed record GetAllCountryResult(List<GetAllCountryReponseDto> Countries);

internal class GetAllCountryCommandHandler(IGeneralDbContext dbContext)
    : IQueryHandler<GetAllCountryQuery, GetAllCountryResult>
{
    public async Task<GetAllCountryResult> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Country> countries = await dbContext.Countries.Where(x => x.Status)
            .ToListAsync(cancellationToken: cancellationToken);

        return new GetAllCountryResult(countries.Select(x
                => new GetAllCountryReponseDto(x.Id, x.Name))
            .ToList());
    }
}