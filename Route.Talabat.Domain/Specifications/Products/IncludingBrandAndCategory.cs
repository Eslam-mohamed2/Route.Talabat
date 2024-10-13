using Route.Talaat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Specifications.Products
{
    public class IncludingBrandAndCategory : BaseSpecifications<Product,int>
    {
        public IncludingBrandAndCategory()
            :base()
        {
            AddIncludes();
        }

        public IncludingBrandAndCategory(int id)
            :base(id)
        {
            AddIncludes();
        }



        private void AddIncludes()
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }
    }
}
