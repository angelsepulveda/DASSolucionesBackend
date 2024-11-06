namespace DASSolucionesBackend.General.Submodules.Address.Countries.Exceptions;

public class CountryNotFoundException : NotFoundException
{
    public CountryNotFoundException(Guid id) 
        : base("Country", id)
    {
    }
}