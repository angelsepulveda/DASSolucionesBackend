namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Features.Delete;

internal sealed class DeleteDocumentTypeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/document-types/{id:guid}", async (Guid id, ISender sender) =>
            {
                DeleteDocumentTypeCommand command = new(id);

                await sender.Send(command);

                return Results.Ok(true);

            }).WithName("DeleteDocumentType") 
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete DocumentType")
            .WithDescription("Delete Document Type")
            .WithTags("DocumentTypes");
    }
}