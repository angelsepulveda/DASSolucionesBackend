namespace DASSolucionesBackend.General.Submodules.Address.Localities.Features.Register;

internal sealed class RegiterLocalityEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/localities", async (RegisterLocalityPayloadDto payloadDto, ISender sender) =>
            {
                RegisterLocalityCommand command = new(payloadDto);

                RegisterLocalityResult result = await sender.Send(command);

                return Results.Created($"/api/localities/{result.Id}", result.Id);
            }).WithName("RegisterLocality")
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Register Locality")
            .WithDescription("Register a new Locality")
            .WithTags("Localities");
    }
}