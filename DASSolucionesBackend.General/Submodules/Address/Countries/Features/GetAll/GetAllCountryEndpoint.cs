namespace DASSolucionesBackend.General.Submodules.Address.Countries.Features.GetAll;

public class GetAllCountryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/countries/select", async (ISender sender) =>
            {
                GetAllCountryQuery query = new();

                GetAllCountryResult result = await sender.Send(query);

                return Results.Ok(result.Countries);

            }).WithName("GetAllCountry")
            .Produces<List<GetAllCountryReponseDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("GetAll Country")
            .WithDescription("GetAll Countries")
            .WithTags("Countries");
    }
}