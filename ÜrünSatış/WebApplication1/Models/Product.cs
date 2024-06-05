namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; } // Primary Key
        public int CategoryId { get; set; } // Foreign Key
        public int ProductSpecsId { get; set; } // Foreign Key
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }


        public ProductSpecs ProductSpecs { get; set; } // Navigation Property
        public Category Category { get; set; }// Navigation Property
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
