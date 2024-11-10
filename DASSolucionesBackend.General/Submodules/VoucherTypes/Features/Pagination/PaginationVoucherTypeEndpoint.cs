namespace DASSolucionesBackend.General.Submodules.VoucherTypes.Features.Pagination;

internal sealed class PaginationVoucherTypeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/voucher-types", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                PaginationVoucherTypeQuery query = new(request);

                PaginationVoucherTypeResult result = await sender.Send(query);

                return Results.Ok(result.ResponseDto);
            }).WithName("PaginationVoucherType")
            .Produces<PaginationResult<PaginationVoucherTypeDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Pagination VoucherType")
            .WithDescription("Pagination VoucherTypes")
            .WithTags("VoucherTypes");
    }
}