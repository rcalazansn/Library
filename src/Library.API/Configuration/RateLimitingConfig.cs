using Microsoft.AspNetCore.RateLimiting;
using System.Net;

namespace Library.API.Configuration
{
    public static class RateLimitingConfig
    {
        public static IServiceCollection AddFixedRate(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = (int)HttpStatusCode.TooManyRequests;

                options.AddFixedWindowLimiter(policyName: "fixed", options =>
                {
                    options.PermitLimit = 5; //max number of requests in a 10 second interval
                    options.Window = TimeSpan.FromSeconds(10);
                });
            });

            return services;
        }
    }
}
