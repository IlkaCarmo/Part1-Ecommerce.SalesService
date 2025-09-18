using Ecommerce.SalesService.Data;
using Ecommerce.SalesService.Interfaces;
using Ecommerce.SalesService.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.SalesService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SalesDbContext _context;

        public OrderRepository(SalesDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }
    }
}
