namespace DASSolucionesBackend.General.Submodules.Address.Localities.Features.Restore;

internal sealed class RestoreLocalityEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/localities/{id:guid}", async (Guid id, ISender sender) =>
            {
                RestoreLocalityCommand command = new(id);

                await sender.Send(command);

                return Results.NoContent();
                
            }).WithName("RestoreLocality")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Restore Locality")
            .WithDescription("Restore Locality")
            .WithTags("Localities");
    }
}