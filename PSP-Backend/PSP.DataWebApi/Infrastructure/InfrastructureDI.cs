using Infrastructure.Data.Context;
using Infrastructure.Repositories.FlightRepositories;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Infrastructure.Repositories.PassengerRepositories;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureDI
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PSPContext>();
        
        services.AddTransient<IGenderTypeRepository, GenderTypeRepository>();
        services.AddTransient<IPassengerRepository, PassengerRepository>();
        services.AddTransient<IPassengerTypeRepository, PassengerTypeRepository>();
        services.AddTransient<IDocumentTypeRepository, DocumentTypeRepository>();

        services.AddTransient<IAirlineRepository, AirlineRepository>();
        services.AddTransient<IAirportRepository, AirportRepository>();
        services.AddTransient<ICityRepository, CityRepository>();
        services.AddTransient<ICouponEventRepository, CouponEventRepository>();
        services.AddTransient<IFareRepository, FareRepository>();
        services.AddTransient<IFlightRepository, FlightRepository>();
        services.AddTransient<IQuotaCategoryRepository, QuotaCategoryRepository>();
        services.AddTransient<ISubsidizedRouteRepository, SubsidizedRouteRepository>();
        services.AddTransient<ITicketTypeRepository, TicketTypeRepository>();
        
        return services;
    }
}