using PSP.DataWebApi.ARM_Context.Services;
using PSP.DataWebApi.ARM_Context.Services.Interfaces;

namespace PSP.DataWebApi.ARM_Context.Infrastructure;

public static class ArmDI
{
    public static IServiceCollection AddArm(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IARMService, ARMService>();
        
        return services;
    }
}