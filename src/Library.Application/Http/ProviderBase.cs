namespace Library.Application.Http
{
    public static class ProviderBase
    {
        public static IEnumerable<Provider> Providers { get; private set; }
        public static void AddProviders(IEnumerable<Provider> providers)
            => Providers = providers;
        public static string GetUrl(string providerName)
        {
            if (string.IsNullOrEmpty(providerName))
                return string.Empty;

            var provider = Providers
                .Where(_ => _.Name.ToUpper().Equals(providerName.ToUpper()))
                .SingleOrDefault();

            if (provider == null)
                return string.Empty;

            return provider.BaseAddress ?? string.Empty;
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
