using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Entities.Identity
{
        public class ApplicationUser : IdentityUser  // There are 2 overlaods non Generic Option -> by Default the Id will be String 
        {

            public required string DisplayName { get; set; }

            public virtual Address? Address { get; set; }
        }
}
