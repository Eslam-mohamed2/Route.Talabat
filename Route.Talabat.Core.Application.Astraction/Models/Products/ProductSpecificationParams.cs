using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Abstraction.Models.Products
{
    public class ProductSpecificationParams
    {
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value; }
        }


        public int pageIndex { get; set; } = 1;

        private  const int maxPageSize = 10;
        private int PageSize = 5;

        public int pageSize
        {
            get { return PageSize; }
            set { PageSize = value > maxPageSize ? maxPageSize : value; }
        }
    }
}
