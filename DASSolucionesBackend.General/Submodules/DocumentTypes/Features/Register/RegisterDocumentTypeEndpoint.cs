namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Features.Register;

internal sealed class RegisterDocumentTypeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/document-types", async (RegisterDocumentTypePayloadDto payloadDto, ISender sender) =>
            {
                RegisterDocumentTypeCommand command = new(payloadDto);

                RegisterDocumentTypeResult result = await sender.Send(command);

                return Results.Created($"/api/document-types/{result.Id}", result.Id);
            }).WithName("RegisterDocumentType")
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Register DocumentType")
            .WithDescription("Register a new DocumentType")
            .WithTags("DocumentTypes");
    }
}