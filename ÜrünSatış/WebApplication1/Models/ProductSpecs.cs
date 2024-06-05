namespace WebApplication1.Models
{
    public class ProductSpecs
    {
        public int Id { get; set; }
        public string ProductImage1 { get; set; }
        public string ProductImage2 { get; set; }
        public string ProductImage3 { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Product Product { get; set; }// Navigation Property

    }
}
