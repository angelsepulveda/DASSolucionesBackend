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

        IQueryable<DocumentType> query = dbContext.DocumentTypes.AsQueryable();

        if (request.PaginationRequest.NumberFilter is not null &&
            !string.IsNullOrWhiteSpace(request.PaginationRequest.Search))
        {
            query = request.PaginationRequest.NumberFilter switch
            {
                1 => query.Where(x => x.Name.Contains(request.PaginationRequest.Search)),
                2 => query.Where(x => x.Code.Contains(request.PaginationRequest.Search)),
                3 => query.Where(x => x.Description.Contains(request.PaginationRequest.Search)),
                _ => query
            };
        }

        if (request.PaginationRequest.StateFilter is not null)
        {
            bool status = request.PaginationRequest.StateFilter == 1;
            query = query.Where(x => x.Status == status);
        }

        List<DocumentType> documentTypes = await Shared.Data.Pagination.Ordering(request.PaginationRequest, query)
            .ToListAsync(cancellationToken: cancellationToken);

        long totalCount = await dbContext.DocumentTypes.LongCountAsync(cancellationToken);

        List<DocumentTypeDto> documentTypeDtos = documentTypes
            .Select(x => new DocumentTypeDto(x.Id, x.Name, x.Code, x.Description, x.Status ? "Activo" : "Inactivo"))
            .ToList();

        return new PaginationDocumentTypeResult(
            new PaginationResult<DocumentTypeDto>(pageIndex, pageSize, totalCount, documentTypeDtos));
    }
}