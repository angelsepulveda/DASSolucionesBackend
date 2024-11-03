using DASSolucionesBackend.General.Submodules.DocumentTypes.Dto;
using DASSolucionesBackend.General.Submodules.DocumentTypes.Entities;

namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Features.Pagination;

public sealed record PaginationDocumentTypeQuery(PaginationRequest PaginationRequest)
    : IQuery<PaginationDocumentTypeResult>;

public sealed record PaginationDocumentTypeResult(PaginationResult<DocumentTypeDto> ResponseDto);

internal class
    PaginationDocumentTypeQueryHandler(IGeneralDbContext dbContext)
    : IQueryHandler<PaginationDocumentTypeQuery, PaginationDocumentTypeResult>
{
    public async Task<PaginationDocumentTypeResult> Handle(PaginationDocumentTypeQuery request,
        CancellationToken cancellationToken)
    {
        int pageIndex = request.PaginationRequest.PageIndex;
        int pageSize = request.PaginationRequest.PageSize;

        long totalCount = await dbContext.DocumentTypes.LongCountAsync(cancellationToken);

        List<DocumentType> documentTypes = await dbContext.DocumentTypes
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        List<DocumentTypeDto> documentTypeDtos = documentTypes
            .Select(x => new DocumentTypeDto(x.Id, x.Name, x.Code, x.Description, x.Status)).ToList();

        return new PaginationDocumentTypeResult(
            new PaginationResult<DocumentTypeDto>(pageIndex, pageSize, totalCount, documentTypeDtos));
    }
}