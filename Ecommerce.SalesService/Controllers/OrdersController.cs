using Ecommerce.SalesService.DTOs;
using Ecommerce.SalesService.Services;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce.SalesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDto orderRequest)
        {
            try
            {
                var order = await _orderService.CreateOrderAsync(orderRequest);

                return CreatedAtAction(
                    nameof(GetOrderById),
                    new { id = order.OrderId },
                    order
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }
    }
}
