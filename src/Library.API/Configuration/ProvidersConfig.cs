namespace Library.API.Configuration
{
    public static class ProvidersConfig
    {
        public static void AddProvidersConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var providers = configuration
                .GetSection("Providers")
                .Get<IEnumerable<Provider>>();

            Console.WriteLine(providers);
        }
    }

    public class Provider
    {
        public string Name { get; set; }
        public string BaseAddress { get; set; }
        public string ApiKey { get; set; }
        public override string ToString()
            => $"{Name} - {BaseAddress}";
    }
}
