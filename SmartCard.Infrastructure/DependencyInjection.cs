using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Infrastructure.Persistence;

namespace SmartCard.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DBConnection")));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<DataContext>());

        return services;
    }
}
