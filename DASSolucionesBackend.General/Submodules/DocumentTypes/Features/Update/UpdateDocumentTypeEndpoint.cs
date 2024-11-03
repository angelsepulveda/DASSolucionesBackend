using Microsoft.AspNetCore.Mvc;

namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Features.Update;

internal sealed class UpdateDocumentTypeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/document-types",
                async ([FromBody] UpdateDocumentTypePayloadDto payloadDto, ISender sender) =>
                {
                    UpdateDocumentTypeCommand query = new(payloadDto);

                    await sender.Send(query);

                    return Results.NoContent();
                }).WithName("UpdateDocumentType")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update DocumentType")
            .WithDescription("Update Document Type")
            .WithTags("DocumentTypes");
    }
}