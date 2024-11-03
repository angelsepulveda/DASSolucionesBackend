namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Dto;

public sealed record DocumentTypeDto(Guid Id,string Name,string? Code,string? Description, bool Status);