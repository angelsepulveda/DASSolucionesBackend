namespace DASSolucionesBackend.General.Submodules.Address.Regions.Features.Update;

internal sealed class UpdateRegionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/regions",
                async ([FromBody] UpdateRegionPayloadDto payloadDto, ISender sender) =>
                {
                    UpdateRegionCommand command = new(payloadDto);

                    await sender.Send(command);

                    return Results.NoContent();
                    
                }).WithName("UpdateRegion")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Region")
            .WithDescription("Update Region")
            .WithTags("Regions");
    }
}