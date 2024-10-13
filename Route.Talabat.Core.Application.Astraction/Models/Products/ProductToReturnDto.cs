using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talaat.Core.Application.Abstraction.Models.Products
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }

        #region Brand
        public int? BrandId { get; set; } // Foregin Key -> Product Brand
        public string? Brand { get; set; }

        #endregion

        #region Category 
        public int? CategoryId { get; set; } // Foregin Key -> Product Category
        public string? Category { get; set; }

        #endregion
    }
}
