using Library.API.Filters;
using Library.API.Middlewares;

namespace Library.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services)
        {
            services.AddControllersWithViews(opt =>
            {
                opt.Filters.Add<NotificationAsyncResultFilter>();
            });

            services.AddTransient<ErrorHandlerMiddleware>();

            return services;
        }

        public static IApplicationBuilder UseApiConfig(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();

            return app;
        }
    }
}
