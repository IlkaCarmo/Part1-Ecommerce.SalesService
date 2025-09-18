namespace Ecommerce.SalesService.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CustomerId { get; set; }
        public List<OrderItem> Items { get; set; }
        public string Status { get; set; }  

    }
}
