namespace DASSolucionesBackend.General.Submodules.Address.Countries.Features.Register;

internal sealed class RegisterCountryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/countries", async (RegisterCountryPayloadDto payloadDto, ISender sender) =>
            {
                RegisterCountryCommand command = new(payloadDto);

                RegisterCountryResult result = await sender.Send(command);

                return Results.Created($"/api/countries/{result.Id}", result.Id);
            }).WithName("RegisterCountry")
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Register Country")
            .WithDescription("Register a new Country")
            .WithTags("Countries");
    }
}