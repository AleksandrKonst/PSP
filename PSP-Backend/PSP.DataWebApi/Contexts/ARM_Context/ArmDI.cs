namespace PSP.DataWebApi.Contexts.ARM_Context;

public static class ArmDI
{
    public static IServiceCollection AddPassenger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(ArmDI));
        
        return services;
    }
}