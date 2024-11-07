namespace DASSolucionesBackend.General.Submodules.Address.Regions.Exceptions;

public class RegionNotFoundException: NotFoundException
{
    public RegionNotFoundException(Guid id) 
        : base("Country", id)
    {
    }
}