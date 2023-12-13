using WebShop.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public string PictureUrl { get; set; } = "";
}


//public List<Category> Categories { get; set; } = new List<Category>();
