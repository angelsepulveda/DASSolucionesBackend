using Carter;
using DASSolucionesBackend.Shared.Exceptions.Handler;
using DASSolucionesBackend.Shared.Extensions;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

//se agrega los contenedores de servicios
builder.Services.AddCarterWithAssemblies(typeof(GeneralModule).Assembly);
builder.Services.AddMediatrWithAssemblies(typeof(GeneralModule).Assembly);
builder.Services.AddGeneralModule(builder.Configuration);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

WebApplication app = builder.Build();

app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(options => { });

app.UseGeneralModule();
app.Run();