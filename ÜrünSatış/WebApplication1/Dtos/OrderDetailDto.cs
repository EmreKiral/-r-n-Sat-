namespace WebApplication1.Dtos
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int productId { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public string AppUserId { get; set; }

        public string UserName { get; set; }
        public decimal Total { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public UserDto User { get; set; }
    }
}
