using Library.Application.Command.AddBook;

namespace Library.API.Configuration
{
    public static class MediatrConfig
    {
        public static IServiceCollection AddMediatr(this IServiceCollection services)
        {
            services.AddMediatR(t => t.RegisterServicesFromAssembly(typeof(AddBookCommandHandler).Assembly));

            return services;
        }
    }
}
