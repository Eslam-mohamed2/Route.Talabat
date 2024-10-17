using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Entities.Identity
{
        public class ApplicationUser : IdentityUser<string>  // There are 2 overlaods non Generic Option -> by Default the Id will be String 
                                                             // the Second Option is the Generic Option 
        {
        private Address? address;

        public required string DisplayName { get; set; }

        public Address? Address { get => address; set => address = value; }
    }
}
