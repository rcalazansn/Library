using Library.Core.RabbitMQ;

namespace Library.API.Configuration;

public static class RabbitMqConfig
{
    public static IServiceCollection AddRabbitMqConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqSetting>(configuration.GetSection("RabbitMQ"));

        return services;
    }
}