using WebShop.Models;

namespace WebShop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }

    //public class Product
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public decimal Price { get; set; }
    //    public string PictureUrl { get; set; }
    //    public string Product_Discription { get; set; }
    //    public int CategoryId { get; set; }
    //}

    //public class Category
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public List<Product> Products { get; set; }
    //}
}



//public Product product { get; set; }
//public Category category { get; set; }



//public class HomeViewModel
//{
//    public IEnumerable<CategoryViewModel> Categories { get; set; }
//    public IEnumerable<Product> Products { get; set; }
//}

//public class CategoryViewModel
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public IEnumerable<CategoryViewModel> Subcategories { get; set; }
//}