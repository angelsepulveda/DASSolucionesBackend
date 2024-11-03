using DASSolucionesBackend.General.Submodules.DocumentTypes.Entities;

namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Features.GetAll;

public record GetAllDocumentTypeQuery()
    : IQuery<GetAllDocumentTypeResult>;

public sealed record GetAllDocumentTypeResult(List<GetAllDocumentTypeReponseDto> DocumentTypes);

internal class GetAllDocumentTypeCommandHandler(IGeneralDbContext dbContext)
    : IQueryHandler<GetAllDocumentTypeQuery, GetAllDocumentTypeResult>
{
    public async Task<GetAllDocumentTypeResult> Handle(GetAllDocumentTypeQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<DocumentType> documentTypes = await dbContext.DocumentTypes.Where(x => x.Status)
            .ToListAsync(cancellationToken: cancellationToken);

        return new GetAllDocumentTypeResult(documentTypes.Select(x
                => new GetAllDocumentTypeReponseDto(x.Id, x.Name))
            .ToList());
    }
}