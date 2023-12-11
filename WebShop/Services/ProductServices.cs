using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using WebShop.Models;

public class ProductService
{
    private readonly string _productsFilePath  = "data/products.json";
    private readonly string _categoriesFilePath  = "data/categories.json";


    public List<Product> GetAllProducts()
    {
        var json = File.ReadAllText(_productsFilePath );
        return JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
    }
    
    
    public List<Category> GetAllCategories()
    {
        var json = File.ReadAllText(_categoriesFilePath );
        return JsonConvert.DeserializeObject<List<Category>>(json) ?? new List<Category>();
    }
}

