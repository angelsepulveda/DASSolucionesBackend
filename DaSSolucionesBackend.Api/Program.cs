WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//se agrega los contenedores de servicios
builder.Services.AddGeneralModule(builder.Configuration);

WebApplication app = builder.Build();

app.UseGeneralModule();

app.Run();