namespace WebApplication1.Dtos
{
    public class ProductAddDto
    {
        public int CategoryId { get; set; } // Foreign Key
        public int ProductSpecsId { get; set; } // Foreign Key
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
    }
}
