namespace WebShop.Models.Tests
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<Category> Subcategories { get; set; } = new List<Category>();
    }
}