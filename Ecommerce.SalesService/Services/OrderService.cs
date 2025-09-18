using Ecommerce.SalesService.DTOs;
using Ecommerce.SalesService.Interfaces;
using Ecommerce.SalesService.Models;

namespace Ecommerce.SalesService.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRabbitMqPublisher _rabbitMqPublisher;

        public OrderService(IOrderRepository orderRepository, IRabbitMqPublisher rabbitMqPublisher)
        {
            _orderRepository = orderRepository;
            _rabbitMqPublisher = rabbitMqPublisher;
        }
        public async Task<Order> CreateOrderAsync(OrderRequestDto request)
        {
            if (request.Items == null || !request.Items.Any())
                throw new ArgumentException("Order must contain at least one item.");

            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                CustomerId = request.CustomerId,
                Status = "Pending",
                Items = request.Items.Select(item => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = 0m 
                }).ToList()
            };
            await _orderRepository.CreateAsync(order);
            //await _rabbitMqPublisher.PublishOrderNotificationAsync(order);

            return order;
        }
        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

    }
}
