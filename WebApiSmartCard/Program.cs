using WebApiSmartCard.Extensions;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de servicios (DI)
builder.Services.AddSmartCardServices(builder.Configuration);

// 2. Construcción de la app
var app = builder.Build();

// 3. Configuración del pipeline HTTP
app.UseSmartCardPipeline();

app.Run();