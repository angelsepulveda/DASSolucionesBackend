namespace DASSolucionesBackend.General.Submodules.Address.Regions.Features.Pagination;

internal sealed class PaginationRegionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/regions", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                PaginationRegionQuery query = new(request);

                PaginationRegionResult result = await sender.Send(query);

                return Results.Ok(result.ResponseDto);
                
            }).WithName("PaginationRegion")
            .Produces<PaginationResult<PaginationRegionDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Pagination Region")
            .WithDescription("Pagination Regions")
            .WithTags("Regions");
    }
}