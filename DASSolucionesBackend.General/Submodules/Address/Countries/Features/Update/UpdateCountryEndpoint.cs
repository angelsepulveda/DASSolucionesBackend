namespace DASSolucionesBackend.General.Submodules.Address.Countries.Features.Update;

internal sealed class UpdateCountryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/countries",
                async ([FromBody] UpdateCountryPayloadDto payloadDto, ISender sender) =>
                {
                    UpdateCountryCommand query = new(payloadDto);

                    await sender.Send(query);

                    return Results.NoContent();
                    
                }).WithName("UpdateCountry")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Country")
            .WithDescription("Update Country")
            .WithTags("Countries");
    }
}