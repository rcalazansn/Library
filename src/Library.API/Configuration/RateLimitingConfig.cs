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
                    options.PermitLimit = 10; //max number of requests in a 10 second interval
                    options.Window = TimeSpan.FromSeconds(2);
                    options.QueueLimit = 2; //tolerance after reaching limit
                });
            });

            return services;
        }
    }
}
