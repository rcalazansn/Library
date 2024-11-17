using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Library.Core.RabbitMQ
{
    public class RabbitPublisherBase
    {
        private string _serviceName { get; set; }
        private IConnection _connection { get; set; }
        private ConnectionFactory _factory { get; set; }

        public RabbitPublisherBase
        (
            string serviceName,
            string userName,
            string password,
            string hostname,
            int port,
            string virtualHost = "/"
        )
        {
            _serviceName = serviceName;

            _factory = new ConnectionFactory
            {
                HostName = hostname,
                UserName = userName,
                Password = password,
                Port = port,
                VirtualHost = virtualHost
            };

            _connection = _factory.CreateConnection();
        }
        public bool BuildPublish<T>(T request, string queueName, bool ttlDeadLetter = false)
        {
            try
            {
                BuildQueues(request, queueName, false, ttlDeadLetter);

                return true;
            }
            catch (OperationInterruptedException ex)
            {
                Console.WriteLine($"OperationInterruptedException{ex}");

                return false;
            }
            catch (BrokerUnreachableException ex)
            {
                Console.WriteLine($"BrokerUnreachableException{ex}");

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception{ex}");

                return false;
            }
        }
        private void BuildQueues<T>(T body, string queueName, bool retry, bool ttlDeadLetter)
        {
            ushort prefetchCount = 1;

            using var model = _connection.CreateModel();
            model.BasicQos(0, prefetchCount, false);
            model.ConfirmSelect();

            //Deadletter
            var argsDeadletter = new Dictionary<string, object>();
            if (ttlDeadLetter)
                argsDeadletter.Add("x-message-ttl", 10000); //10 seconds

            model.ExchangeDeclare($"{queueName}.deadletter.exchange", ExchangeType.Fanout, true, false, null);
            model.QueueDeclare($"{queueName}.deadletter", true, false, false, arguments: argsDeadletter);
            model.QueueBind($"{queueName}.deadletter", $"{queueName}.deadletter.exchange", string.Empty, null);

            //Main
            var argumentsMain = new Dictionary<string, object>() { { "x-dead-letter-exchange", $"{queueName}.deadletter.exchange" } };
            model.QueueDeclare($"{queueName}", true, false, false, argumentsMain);

            var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve };
            var json = JsonSerializer.Serialize(body, opt);

            var bytes = Encoding.UTF8.GetBytes(json);

            model.BasicPublish(
                exchange: "",
                routingKey: queueName,
                basicProperties: model.CreateBasicProperties().SetBasicProperties(_serviceName),
                body: bytes);

            model.WaitForConfirmsOrDie(TimeSpan.FromSeconds(5));  //Ack on publication
        }
    }
}
