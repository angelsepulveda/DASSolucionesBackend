namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Exceptions;

public class DocumentTypeNotFoundException : NotFoundException
{
    public DocumentTypeNotFoundException(Guid id) 
        : base("Product", id)
    {
    }
}