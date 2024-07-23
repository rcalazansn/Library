using Library.Application.Http;

namespace Library.API.Configuration
{
    public static class ProvidersConfig
    {
        public static void AddProvidersConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var providers = configuration
                .GetSection("Providers")
                .Get<IEnumerable<Provider>>();

            ProviderBase.AddProviders(providers);
        }
    }
}
