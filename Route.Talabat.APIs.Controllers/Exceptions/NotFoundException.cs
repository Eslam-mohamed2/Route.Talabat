using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.APIs.Controllers.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException()
            : base("Not Found")
        {

        }
    }
}
