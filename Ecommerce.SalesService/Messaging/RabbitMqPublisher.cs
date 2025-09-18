using Ecommerce.SalesService.Interfaces;
using Ecommerce.SalesService.Models;

namespace Ecommerce.SalesService.Messaging
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        public Task PublishOrderNotificationAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
