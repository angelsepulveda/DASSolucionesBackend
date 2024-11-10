namespace DASSolucionesBackend.General.Submodules.VoucherTypes.Features.Delete;

internal sealed class DeleteVoucherTypeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/voucher-types/{id:guid}", async (Guid id, ISender sender) =>
            {
                DeleteVoucherTypeCommand command = new(id);

                await sender.Send(command);

                return Results.NoContent();
                
            }).WithName("DeleteVoucherType") 
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete VoucherType")
            .WithDescription("Delete VoucherType")
            .WithTags("VoucherTypes");
    }
}