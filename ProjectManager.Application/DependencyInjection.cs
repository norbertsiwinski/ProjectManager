using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Application.Abstractions.Security;
using ProjectManager.Domain.Users;

namespace ProjectManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));

        services.AddAutoMapper(cfg => {}, AssemblyReference.Assembly);
        
        return services;
    }
}