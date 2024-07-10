using Library.Application.Command.AddBook;

namespace Library.API.Configuration
{
    public static class MediatrConfig
    {
        public static IServiceCollection AddMediatr(this IServiceCollection services)
        {
            services.AddMediatR(t => t.RegisterServicesFromAssembly(typeof(AddBookCommandHandler).Assembly));

            /*
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUsersQueryHandler).Assembly));
            */
            return services;
        }
    }
}
