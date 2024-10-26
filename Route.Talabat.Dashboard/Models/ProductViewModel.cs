using Route.Talaat.Core.Domain.Entities.Products;
using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.Dashboard.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is Required !")]
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? PictureUrl { get; set; }

        [Required(ErrorMessage = "Price is Required !")]
        [Range(1, 100_000)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product Category Id Is Required!!")]
        public int ProductCategoryId { get; set; }
        public ProductCategory? ProductCategory { get; set; }

        [Required(ErrorMessage = "Product Brand Id Is Required!!")]
        public int ProductBrandeId { get; set; }
        public ProductBrand? ProductBrand { get; set; }
    }
}
