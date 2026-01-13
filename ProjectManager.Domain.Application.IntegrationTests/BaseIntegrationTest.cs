using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Infrastructure;

namespace ProjectManager.Application.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope serviceScope;
    protected readonly ISender sender;
    protected readonly AppDbContext dbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        serviceScope = factory.Services.CreateScope();

        sender = serviceScope.ServiceProvider.GetRequiredService<ISender>();
        dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    }
}