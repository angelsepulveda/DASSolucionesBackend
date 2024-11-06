namespace DASSolucionesBackend.General.Submodules.Address.Countries.Features.Delete;

internal sealed class DeleteCountryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/countries/{id:guid}", async (Guid id, ISender sender) =>
            {
                DeleteCountryCommand command = new(id);

                await sender.Send(command);

                return Results.NoContent();
                
            }).WithName("DeleteCountry") 
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Country")
            .WithDescription("Delete Country")
            .WithTags("Countries");
    }
}