using Carter;
using DASSolucionesBackend.Shared.Exceptions.Handler;
using DASSolucionesBackend.Shared.Extensions;
using Microsoft.OpenApi.Models;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

//se agrega los contenedores de servicios
builder.Services.AddCarterWithAssemblies(typeof(GeneralModule).Assembly);
builder.Services.AddMediatrWithAssemblies(typeof(GeneralModule).Assembly);
builder.Services.AddGeneralModule(builder.Configuration);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

WebApplication app = builder.Build();

app.UseSwagger();

// Enable middleware to serve Swagger UI
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});


app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(options => { });

app.UseGeneralModule();
app.Run();