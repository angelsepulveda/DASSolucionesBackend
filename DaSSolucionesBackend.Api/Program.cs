using Carter;
using DASSolucionesBackend.Shared.Exceptions.Handler;
using DASSolucionesBackend.Shared.Extensions;
using DASSolucionesBackend.Warehouses;
using Microsoft.OpenApi.Models;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

//se agrega los contenedores de servicios
builder.Services.AddCarterWithAssemblies(typeof(GeneralModule).Assembly, typeof(WarehousesModule).Assembly);
builder.Services.AddMediatrWithAssemblies(typeof(GeneralModule).Assembly, typeof(WarehousesModule).Assembly);
builder.Services.AddGeneralModule(builder.Configuration);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();


// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Cambia esto por la URL de tu frontend
            .AllowAnyHeader()
            .AllowAnyMethod(); // Permitir GET, POST, PUT, DELETE, etc.
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); });

WebApplication app = builder.Build();

app.UseSwagger();

// Enable middleware to serve Swagger UI
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

app.UseCors("AllowFrontend");
app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(options => { });

GeneralModule.UseGeneralModule(app);
app.Run();