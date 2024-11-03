namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Features.Delete;

internal sealed class DeleteDocumentTypeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/document-types/{id:guid}", async (Guid id, ISender sender) =>
            {
                DeleteDocumentTypeCommand query = new(id);

                await sender.Send(query);

                return Results.NoContent();
                
            }).WithName("DeleteDocumentType") 
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Restore DocumentType")
            .WithDescription("Restore Document Type")
            .WithTags("DocumentTypes");
    }
}