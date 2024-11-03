using DASSolucionesBackend.General.Submodules.DocumentTypes.Dto;

namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Features.Pagination;

internal sealed class PaginationDocumentTypeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/document-types", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                PaginationDocumentTypeQuery query = new(request);

                PaginationDocumentTypeResult result = await sender.Send(query);

                return Results.Ok(result.ResponseDto);
                
            }).WithName("PaginationDocumentType")
            .Produces<PaginationResult<DocumentTypeDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Pagination DocumentType")
            .WithDescription("Pagination Document Types")
            .WithTags("DocumentTypes");
    }
}