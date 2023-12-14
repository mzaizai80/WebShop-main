namespace WebShop.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<Category>? Subcategories { get; set; }


        public Category()
        {
            Subcategories = new List<Category>();
        }
    }
}