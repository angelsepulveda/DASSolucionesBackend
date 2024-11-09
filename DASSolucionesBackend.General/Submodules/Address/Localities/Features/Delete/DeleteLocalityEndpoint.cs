namespace DASSolucionesBackend.General.Submodules.Address.Localities.Features.Delete;

internal sealed class DeleteLocalityEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/localities/{id:guid}", async (Guid id, ISender sender) =>
            {
                DeleteLocalityCommand command = new(id);

                await sender.Send(command);

                return Results.NoContent();
            }).WithName("DeleteLocality")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Locality")
            .WithDescription("Delete Locality")
            .WithTags("Localities");
    }
}