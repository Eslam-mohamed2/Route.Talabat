namespace Route.Talabat.Core.Domain.Entities.Products
{
    public class ProductBrand : BaseEntity<int>
    {
        public required string Name { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
