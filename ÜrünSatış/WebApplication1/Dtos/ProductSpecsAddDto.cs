using WebApplication1.Models;

namespace WebApplication1.Dtos
{
    public class ProductSpecsAddDto
    {
        public string ProductImage1 { get; set; }
        public string ProductImage2 { get; set; }
        public string ProductImage3 { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
