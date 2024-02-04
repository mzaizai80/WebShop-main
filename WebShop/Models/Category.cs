using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public List<int> ProductIds { get; set; } = new List<int>();
    }
}
