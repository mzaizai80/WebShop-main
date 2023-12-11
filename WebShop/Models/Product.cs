﻿public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = ""; // Initialize with an empty string or provide a default value.
    public decimal Price { get; set; }
    public string PictureUrl { get; set; } = "";
    public decimal CategoryId { get; set; }
}



//namespace WebShopTests
//{
//    public class Product
//    {
//        public Product()
//        {

//        }
//    }
//}