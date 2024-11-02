namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Features.Update;

public sealed record UpdateDocumentTypePayloadDto(Guid Id, string Name, string? Code, string? Description);