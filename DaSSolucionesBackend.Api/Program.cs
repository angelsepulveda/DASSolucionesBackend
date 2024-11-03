using Carter;
using DASSolucionesBackend.Shared.Exceptions.Handler;
using DASSolucionesBackend.Shared.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//se agrega los contenedores de servicios
builder.Services.AddCarterWithAssemblies(typeof(GeneralModule).Assembly);
builder.Services.AddMediatrWithAssemblies(typeof(GeneralModule).Assembly);
builder.Services.AddGeneralModule(builder.Configuration);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

WebApplication app = builder.Build();

app.MapCarter();

app.UseGeneralModule();

app.UseExceptionHandler(options => {});

app.Run();