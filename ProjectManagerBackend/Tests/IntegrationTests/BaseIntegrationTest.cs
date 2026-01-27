using Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Infrastructure;

namespace ProjectManager.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<TestWebAppFactory>
{
    private readonly IServiceScope serviceScope;
    protected readonly ISender sender;
    protected readonly AppDbContext dbContext;

    protected BaseIntegrationTest(TestWebAppFactory factory)
    {
        serviceScope = factory.Services.CreateScope();
        sender = serviceScope.ServiceProvider.GetRequiredService<ISender>();
        dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    }
}