namespace DASSolucionesBackend.General.Submodules.Address.Localities.Features.Pagination;

internal sealed class PaginationLocalityEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/localities", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                PaginationLocalityQuery query = new(request);

                PaginationLocalityResult result = await sender.Send(query);

                return Results.Ok(result.ResponseDto);
            }).WithName("PaginationLocality")
            .Produces<PaginationResult<PaginationLocalityDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Pagination Locality")
            .WithDescription("Pagination Localities")
            .WithTags("Localities");
    }
}