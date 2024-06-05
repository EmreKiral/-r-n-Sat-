namespace WebApplication1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Product> Products { get; set; } // Navigation Property 
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
