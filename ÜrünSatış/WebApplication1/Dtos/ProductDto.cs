namespace WebApplication1.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; } // Primary Key
        public int CategoryId { get; set; } // Foreign Key
        public int ProductSpecsId { get; set; } // Foreign Key
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }

    }
}
