namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Features.Restore;

internal sealed class RestoreDocumentTypeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/document-types/{id:guid}", async (Guid id, ISender sender) =>
            {
                RestoreDocumentTypeCommand query = new(id);

                await sender.Send(query);

                return Results.NoContent();
                
            }).WithName("RestoreDocumentType")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Restore DocumentType")
            .WithDescription("Restore Document Type")
            .WithTags("DocumentTypes");
    }
}