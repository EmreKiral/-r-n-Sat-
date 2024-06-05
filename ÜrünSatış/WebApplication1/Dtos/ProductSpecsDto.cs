namespace WebApplication1.Dtos
{
    public class ProductSpecsDto
    {
        public int Id { get; set; }
        public string ProductImage1 { get; set; }
        public string ProductImage2 { get; set; }
        public string ProductImage3 { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

    }
}
