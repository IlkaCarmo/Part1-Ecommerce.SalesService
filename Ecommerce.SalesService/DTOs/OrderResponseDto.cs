namespace Ecommerce.SalesService.DTOs
{
    public class OrderResponseDto
    {
        public Guid OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public List<OrderItemResponseDto> Items { get; set; }
    }
}
