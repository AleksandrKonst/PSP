using PSP.DataWebApi.Passenger_Context.Services;
using PSP.DataWebApi.Passenger_Context.Services.Interfaces;

namespace PSP.DataWebApi.Passenger_Context.Infrastructure;

public static class PassengerDI
{
    public static IServiceCollection AddPassenger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPassengerService, PassengerService>();
        
        services.AddScoped<IPassengerTypeService, PassengerTypeService>();
        
        services.AddScoped<IDocumentTypeService, DocumentTypeService>();
        
        services.AddScoped<IPassengerQuotaCountService, PassengerQuotaCountService>();
        
        return services;
    }
}