namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Features.GetAll;

internal sealed class GetAllDocumentTypeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/document-type/select", async (ISender sender) =>
            {
                GetAllDocumentTypeQuery query = new();

                GetAllDocumentTypeResult result = await sender.Send(query);

                return Results.Ok(result.DocumentTypes);

            }).WithName("GetAllDocumentType")
            .Produces<List<GetAllDocumentTypeReponseDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("GetAll DocumentType")
            .WithDescription("GetAll Document Types")
            .WithTags("DocumentTypes");

    }
}