namespace DASSolucionesBackend.General.Submodules.Address.Localities.Features.Update;

internal sealed class UpdateLocalityEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/localities",
                async ([FromBody] UpdateLocalityPayloadDto payloadDto, ISender sender) =>
                {
                    UpdateLocalityCommand command = new(payloadDto);

                    await sender.Send(command);

                    return Results.NoContent();
                    
                }).WithName("UpdateLocality")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Locality")
            .WithDescription("Update Locality")
            .WithTags("Localities");
    }
}