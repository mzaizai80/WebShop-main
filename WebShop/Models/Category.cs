namespace WebShop.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public ICollection<Product> Products{ get; set; }
        public List<int>? Subcategories { get; set; } = new List<int>();

        public Category()
        {
            Subcategories = new List<int>();
        }
    }
}