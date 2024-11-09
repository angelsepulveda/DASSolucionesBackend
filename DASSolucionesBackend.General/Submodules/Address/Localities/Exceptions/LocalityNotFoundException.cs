namespace DASSolucionesBackend.General.Submodules.Address.Localities.Exceptions;

public class LocalityNotFoundException : NotFoundException
{
    public LocalityNotFoundException(Guid id)
        : base("Locality", id)
    {
    }
}