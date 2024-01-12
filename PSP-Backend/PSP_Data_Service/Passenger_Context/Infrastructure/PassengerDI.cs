using PSP_Data_Service.Passenger_Context.Models;
using PSP_Data_Service.Passenger_Context.Repositories;
using PSP_Data_Service.Passenger_Context.Repositories.Interfaces;
using PSP_Data_Service.Passenger_Context.Services;
using PSP_Data_Service.Passenger_Context.Services.Interfaces;

namespace PSP_Data_Service.Passenger_Context.Infrastructure;

public static class PassengerDI
{
    public static IServiceCollection AddPassenger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PassengerDataContext>();
        
        services.AddTransient<IGenderTypeRepository, GenderTypeRepository>();

        services.AddScoped<IPassengerService, PassengerService>();
        services.AddTransient<IPassengerRepository, PassengerRepository>();
        
        services.AddScoped<IPassengerTypeService, PassengerTypeService>();
        services.AddTransient<IPassengerTypeRepository, PassengerTypeRepository>();
        
        services.AddScoped<IDocumentTypeService, DocumentTypeService>();
        services.AddTransient<IDocumentTypeRepository, DocumentTypeRepository>();
        
        services.AddScoped<IPassengerQuotaCountService, PassengerQuotaCountService>();
        services.AddTransient<IPassengerQuotaCountRepository, PassengerQuotaCountRepository>();
        
        return services;
    }
}