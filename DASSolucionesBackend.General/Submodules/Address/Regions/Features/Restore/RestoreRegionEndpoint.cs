namespace DASSolucionesBackend.General.Submodules.Address.Regions.Features.Restore;

internal sealed class RestoreRegionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/regions/{id:guid}", async (Guid id, ISender sender) =>
            {
                RestoreRegionCommand command = new(id);

                await sender.Send(command);

                return Results.NoContent();
                
            }).WithName("RestoreRegion")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Restore Region")
            .WithDescription("Restore Region")
            .WithTags("Regions");
    }
}