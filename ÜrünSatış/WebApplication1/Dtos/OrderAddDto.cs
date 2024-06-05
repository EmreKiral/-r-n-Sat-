namespace WebApplication1.Dtos
{
    public class OrderAddDto
    {
        public int ProductId { get; set; }
        public string AppUserId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
