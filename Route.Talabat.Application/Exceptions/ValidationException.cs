using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Exceptions
{
    internal class ValidationException : BadRequestException
    {
        public required IEnumerable<string> Errors { get; set; }
        public ValidationException(string message = "Invalid Account") : base(message)
        {
        }
    }
}
