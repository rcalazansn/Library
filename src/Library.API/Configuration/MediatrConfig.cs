using Library.Application.Command.AddBook;

namespace Library.API.Configuration
{
    public static class MediatrConfig
    {
        public static IServiceCollection AddMediatrConfig(this IServiceCollection services)
        {
            services.AddMediatR(_ => _.RegisterServicesFromAssembly(typeof(AddBookCommandHandler).Assembly));

            return services;
        }
    }
}
