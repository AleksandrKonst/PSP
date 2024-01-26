namespace PSP.DataWebApi.Contexts.Passenger_Context;

public static class PassengerDI
{
    public static IServiceCollection AddARM(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(PassengerDI));
        
        return services;
    }  
}