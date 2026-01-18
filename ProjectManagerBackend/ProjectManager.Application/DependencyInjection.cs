using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Application.Users;

namespace ProjectManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));

        services.AddAutoMapper(cfg => {}, AssemblyReference.Assembly);

        services.AddScoped<IUserContext, UserContext>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Behaviors.ValidationPipelineBehavior<,>));
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);

        return services;
    }
} 