using WebShop.Models;

namespace WebShop.ViewModels
{
    public class HomeViewModel : Product
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public List<Product> Product { get; set; }
        public List<Product> Category { get; set; }
        //public Product product { get; set; }
        //public Category category { get; set; }
    }
}
