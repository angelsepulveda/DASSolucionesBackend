namespace DASSolucionesBackend.General.Submodules.Address.Regions.Features.Register;

internal sealed class RegisterRegionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/regions", async (RegisterRegionPayloadDto payloadDto, ISender sender) =>
            {
                RegisterRegionCommand command = new(payloadDto);

                RegisterRegionResult result = await sender.Send(command);

                return Results.Created($"/api/regions/{result.Id}", result.Id);
            }).WithName("RegisterRegion")
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Register Region")
            .WithDescription("Register a new Region")
            .WithTags("Regions");
    }
}