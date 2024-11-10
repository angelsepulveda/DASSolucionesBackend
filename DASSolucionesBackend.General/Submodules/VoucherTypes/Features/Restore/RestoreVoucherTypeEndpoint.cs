namespace DASSolucionesBackend.General.Submodules.VoucherTypes.Features.Restore;

internal sealed class RestoreVoucherTypeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/voucher-types/{id:guid}", async (Guid id, ISender sender) =>
            {
                RestoreVoucherTypeCommand command = new(id);

                await sender.Send(command);

                return Results.NoContent();
                
            }).WithName("RestoreVoucherType")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Restore VoucherType")
            .WithDescription("Restore VoucherType")
            .WithTags("VoucherTypes");
    }
}