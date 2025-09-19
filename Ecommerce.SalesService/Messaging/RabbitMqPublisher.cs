using Ecommerce.SalesService.Interfaces;
using Ecommerce.SalesService.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Ecommerce.SalesService.Messaging
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "order_created";
        private readonly string _username = "guest";
        private readonly string _password = "guest";

        public Task PublishOrderNotificationAsync(Order order)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();


            channel.QueueDeclare(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var message = JsonSerializer.Serialize(new
            {
                order.OrderId,
                order.CustomerId,
                order.Status,
                order.CreatedAt,
                Items = order.Items.Select(i => new
                {
                    i.ProductId,
                    i.Quantity,
                    i.UnitPrice
                })
            });

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: _queueName,
                basicProperties: null,
                body: body
            );

            Console.WriteLine($"[x] Sent order_created: {message}");

            return Task.CompletedTask;
        }
    }
}
