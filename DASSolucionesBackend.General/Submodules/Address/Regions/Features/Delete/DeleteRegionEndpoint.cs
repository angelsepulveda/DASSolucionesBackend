namespace DASSolucionesBackend.General.Submodules.Address.Regions.Features.Delete;

internal sealed class DeleteRegionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/regions/{id:guid}", async (Guid id, ISender sender) =>
            {
                DeleteRegionCommand command = new(id);

                await sender.Send(command);

                return Results.NoContent();
                
            }).WithName("DeleteRegion") 
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Region")
            .WithDescription("Delete Region")
            .WithTags("Regions");
    }
}