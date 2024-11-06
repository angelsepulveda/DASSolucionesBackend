namespace DASSolucionesBackend.General.Submodules.Address.Countries.Features.Restore;

public class RestoreCountryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/countries/{id:guid}", async (Guid id, ISender sender) =>
            {
                RestoreCountryCommand command = new(id);

                await sender.Send(command);

                return Results.NoContent();
                
            }).WithName("RestoreCountry")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Restore Country")
            .WithDescription("Restore Country")
            .WithTags("Countries");
    }
}