using Route.Talaat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Specifications.Products
{
    public class IncludingBrandAndCategory : BaseSpecifications<Product,int>
    {
        public IncludingBrandAndCategory(string? sort , int? brandId,int? CategoryId)
            :base(
                 p => 
                 (!brandId.HasValue || p.BrandId == brandId)
                 &&
                 (!CategoryId.HasValue || p.CategoryId == CategoryId)
                 )
        {

            AddIncludes();

            AddOrderBy(p => p.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch(sort)
                {
                    case "nameDesc":
                        AddOrderByDesc(p => p.Name);
                        break;
                    case "priceAsc":
                        // or -> OrderBy = p => p.Price;
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        // or -> OrderByDesc = p => p.Price;
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                    
                }
            }
            
        }

        public IncludingBrandAndCategory(int id)
            :base(id)
        {
            AddIncludes();
        }



        private protected override void AddIncludes()
        {
            base.AddIncludes();
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }
    }
}
