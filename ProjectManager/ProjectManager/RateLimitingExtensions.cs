using Microsoft.AspNetCore.RateLimiting;

namespace ProjectManager;

public static class RateLimitingExtensions
{
    public static IServiceCollection AddRateLimiting(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddRateLimiter(rateLimiterOptions =>
        {
            rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            rateLimiterOptions.AddFixedWindowLimiter("Fixed", options =>
            {
                options.PermitLimit = 5;
                options.Window = TimeSpan.FromSeconds(10);
                options.QueueLimit = 0;
                options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
            });
        });

        return serviceCollection;
    }
}