using DASSolucionesBackend.General.Submodules.DocumentTypes.Entities;

namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Contracts.Services;

public interface IGetByIdDocumentTypeService
{
    Task<DocumentType> HandleAsync(Guid id, CancellationToken cancellationToken);
}