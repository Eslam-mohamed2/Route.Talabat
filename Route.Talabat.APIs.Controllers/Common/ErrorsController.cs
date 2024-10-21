using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Controllers.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.APIs.Controllers.Common
{
    [ApiController]
    [Route("Errors/{Code}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    class ErrorsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error(int Code)
        {
            if(Code == (int)HttpStatusCode.NotFound)
            {
                var response = new ApiResponse((int)HttpStatusCode.NotFound, $"The Requested End Point: {Request.Path} Is Not Found");
                return NotFound(Response);
            }
            return StatusCode(Code,new ApiResponse(Code));
        }
    }
}
