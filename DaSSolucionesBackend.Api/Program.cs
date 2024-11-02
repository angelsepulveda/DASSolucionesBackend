using Carter;
using DASSolucionesBackend.Shared.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//se agrega los contenedores de servicios
builder.Services.AddCarterWithAssemblies(typeof(GeneralModule).Assembly);
builder.Services.AddGeneralModule(builder.Configuration);

WebApplication app = builder.Build();

app.MapCarter();

app.UseGeneralModule();

app.Run();