namespace Library.API.Configuration
{
    public static class OutputCacheConfig
    {
        public static IServiceCollection AddOutputCacheConfig(this IServiceCollection services)
        {
            services.AddOutputCache(options =>
            {
                options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromSeconds(5)));

                options.AddPolicy("Expire10Seconds", builder => builder.Expire(TimeSpan.FromSeconds(10)));
            });

            return services;
        }
    }
}
