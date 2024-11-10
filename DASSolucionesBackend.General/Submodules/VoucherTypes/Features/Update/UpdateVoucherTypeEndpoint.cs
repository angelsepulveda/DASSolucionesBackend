namespace DASSolucionesBackend.General.Submodules.VoucherTypes.Features.Update;

internal sealed class UpdateVoucherTypeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/voucher-types",
                async ([FromBody] UpdateVoucherTypePayloadDto payloadDto, ISender sender) =>
                {
                    UpdateVoucherTypeCommand command = new(payloadDto);

                    await sender.Send(command);

                    return Results.NoContent();
                    
                }).WithName("UpdateVoucherType")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update VoucherType")
            .WithDescription("Update VoucherType")
            .WithTags("VoucherTypes");
    }
}