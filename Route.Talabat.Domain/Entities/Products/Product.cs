using Route.Talabat.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Entities.Products
{
    public class Product : BaseEntity<int>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }

        #region Brand
        public int? BrandId { get; set; } // Foregin Key -> Product Brand
        public virtual ProductBrand? Brand { get; set; }

        #endregion

        #region Category 
        public int? CategoryId { get; set; } // Foregin Key -> Product Category
        public virtual ProductCategory? Category { get; set; }

        #endregion
    }
}
