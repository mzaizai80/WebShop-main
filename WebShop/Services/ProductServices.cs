using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class ProductService
{
    private readonly string _filePath = "data/products.json";

    public List<Product> GetAllProducts()
    {
        var json = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
    }
}


//namespace WebShop.Services
//{
//    public class ProductService
//    {
//        public object GetAllProducts()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
