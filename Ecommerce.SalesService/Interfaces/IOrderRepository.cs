using Ecommerce.SalesService.DTOs;
using Ecommerce.SalesService.Models;

namespace Ecommerce.SalesService.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);
        Task<Order?> GetByIdAsync(Guid id);
    }
}
