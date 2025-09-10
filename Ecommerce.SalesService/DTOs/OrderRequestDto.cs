namespace Ecommerce.SalesService.DTOs
{
    public class OrderRequestDto
    {
        public string CustomerId { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
