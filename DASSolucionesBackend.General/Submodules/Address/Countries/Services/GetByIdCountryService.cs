using DASSolucionesBackend.General.Submodules.Address.Countries.Contracts.Services;
using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;
using DASSolucionesBackend.General.Submodules.Address.Countries.Exceptions;

namespace DASSolucionesBackend.General.Submodules.Address.Countries.Services;

internal sealed class GetByIdCountryService(IGeneralDbContext dbContext) : IGetByIdCountryService
{
    public async Task<Country> HandleAsync(Guid id, CancellationToken cancellationToken)
    {
        Country? country =
            await dbContext.Countries.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

        if (country is null) throw new CountryNotFoundException(id);
        
        return country;
    }
}