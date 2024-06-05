namespace WebApplication1.Dtos
{
    public class CategoryWithProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
