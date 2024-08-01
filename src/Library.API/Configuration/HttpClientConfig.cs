using Library.Application.Http;
using Library.Application.Http.ViaCep.HttpClientFactory;

namespace Library.API.Configuration
{
    public static class HttpClientConfig
    {
        public static IServiceCollection AddHttpClientConfig(this IServiceCollection services)
        {
            services.AddHttpClient(typeof(ViaCepClientFactory).Name , client =>
            {
                client.BaseAddress = new Uri(ProviderBase.GetUrl("ViaCEP"));
            });

           //       .AddPolicyHandler(GetRetryPolicy())
           //.AddPolicyHandler(GetCircuitBreakerPolicy());

            return services;
        }
    }
}
