using RabbitMQ.Client;

namespace Library.Core.RabbitMQ
{
    public static class RabbitExtensions
    {
        public static IBasicProperties SetBasicProperties(this IBasicProperties prop, string serviceName)
        {
            prop.ContentType = "application/json";
            prop.DeliveryMode = 2;
            prop.Headers = new Dictionary<string, object>()
            {
                { "Date", DateTime.UtcNow.ToLocalTime().ToString("R") },
                { "ServiceName", serviceName },
                { "Host", Environment.MachineName },
                { "SO", Environment.OSVersion.ToString() },
            };
            return prop;
        }
    }
}
