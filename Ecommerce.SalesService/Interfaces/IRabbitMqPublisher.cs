using Ecommerce.SalesService.Models;

namespace Ecommerce.SalesService.Interfaces

{
    public interface IRabbitMqPublisher
    {
        Task PublishOrderNotificationAsync(Order order);
    }
}
