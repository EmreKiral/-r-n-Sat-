namespace WebApplication1.Dtos
{
    public class CouponDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Discount { get; set; }
        public bool IsActive { get; set; }
    }
}
