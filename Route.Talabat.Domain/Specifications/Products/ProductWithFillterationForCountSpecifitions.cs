using Route.Talaat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Specifications.Products
{
    public class ProductWithFillterationForCountSpecifitions : BaseSpecifications<Product, int>
    {
        public ProductWithFillterationForCountSpecifitions(int? brandId, int? CategoryId,string? Search)
            : base(
                 p =>
                 (string.IsNullOrEmpty(Search) || p.NormalizedName.Contains(Search))
                 &&
                 (!brandId.HasValue || p.BrandId == brandId)
                 &&
                 (!CategoryId.HasValue || p.CategoryId == CategoryId)
                 )
        {
        }
    }
}
