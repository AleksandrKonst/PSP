using Infrastructure.Data.Context;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureDI
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PSPContext>();
        
        services.AddTransient<IFlightRepository, FlightRepository>();
        
        return services;
    }
}