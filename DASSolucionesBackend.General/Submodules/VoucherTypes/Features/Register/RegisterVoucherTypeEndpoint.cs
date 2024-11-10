namespace DASSolucionesBackend.General.Submodules.VoucherTypes.Features.Register;

internal sealed class RegisterVoucherTypeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/voucher-types", async (RegisterVoucherTypePayloadDto payloadDto, ISender sender) =>
            {
                RegisterVoucherTypeCommand command = new(payloadDto);

                RegisterVoucherTypeResult result = await sender.Send(command);

                return Results.Created($"/api/voucher-types/{result.Id}", result.Id);
            }).WithName("RegisterVoucherType")
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Register VoucherType")
            .WithDescription("Register a new VoucherType")
            .WithTags("VoucherTypes");
    }
}