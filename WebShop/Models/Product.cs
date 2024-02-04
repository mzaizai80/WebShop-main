using Microsoft.Build.Framework;

namespace WebShop.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = "";
        public string Description { get; set; } = "";
        public int CategoryId { get; set; }
    }
}
