using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSP.Infrastructure;

namespace PSP.RouteApplication;

public static class ApplicationDI
{
    public static IServiceCollection AddRouteApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        var assemply = typeof(ApplicationDI).Assembly;

        services.AddMediatR(x => x.RegisterServicesFromAssembly(assemply));
        services.AddValidatorsFromAssembly(assemply);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddAutoMapper(typeof(ApplicationDI));
        
        return services;
    }
}