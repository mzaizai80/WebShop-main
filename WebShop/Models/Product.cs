using Microsoft.Build.Framework;

namespace WebShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string? PictureUrl { get; set; } = "";
        public string? Product_Discription { get; set; } = "";
        public int ReverseLookupOfCategoryIds { get; set; }
    }
}