namespace WebApplication1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string AppUserId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public AppUser AppUser { get; set; }
    }
}
