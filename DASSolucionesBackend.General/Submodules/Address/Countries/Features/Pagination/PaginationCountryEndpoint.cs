namespace DASSolucionesBackend.General.Submodules.Address.Countries.Features.Pagination;

public class PaginationCountryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/countries", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                PaginationCountryQuery query = new(request);

                PaginationCountryResult result = await sender.Send(query);

                return Results.Ok(result.ResponseDto);
                
            }).WithName("PaginationCountry")
            .Produces<PaginationResult<PaginationCountryDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Pagination Country")
            .WithDescription("Pagination Countries")
            .WithTags("Countries");
    }
}