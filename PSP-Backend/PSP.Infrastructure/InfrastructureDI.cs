using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.PassengerRepositories;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.Infrastructure;

public static class InfrastructureDI
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PSPContext>();
        
        services.AddTransient<IGenderTypeRepository, GenderTypeRepository>();
        services.AddTransient<IPassengerRepository, PassengerRepository>();
        services.AddTransient<IPassengerTypeRepository, PassengerTypeRepository>();
        services.AddTransient<IDocumentTypeRepository, DocumentTypeRepository>();
        services.AddTransient<IPassengerQuotaCountRepository, PassengerQuotaCountRepository>();
        
        return services;
    }
}