using System.Text.Json;
using SmartCard.Application;
using SmartCard.Infrastructure;

namespace WebApiSmartCard.Extensions
{   
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSmartCardServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Configuración JSON GLOBAL (case-insensitive, camelCase)
            services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.PropertyNameCaseInsensitive = true;
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

            // Capas de aplicación e infraestructura
            services.AddApplication();
            services.AddInfrastructure(configuration);

            // Controllers y OpenAPI / Swagger
            services.AddControllers();
            services.AddOpenApi();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // MediatR
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            return services;
        }
    }
}


