namespace WebShop.Models
{
    public class ProductCategoryRelation
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        
        public ProductCategoryRelation(int productId, int categoryId)
        {
        ProductId = productId;
        CategoryId = categoryId;
        }
    }
}
