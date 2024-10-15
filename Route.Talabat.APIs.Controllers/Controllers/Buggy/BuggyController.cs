using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Route.Talaat.APIs.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.APIs.Controllers.Controllers.Buggy
{
    public class BuggyController : BaseApiController
    {

        [HttpGet("notFound")] // Get: /api/buggy/notfound
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(new {StatusCode = 404 , Message = "Not Found"}); // 404
        }
        [HttpGet("servererror")] // Get: /api/buggy/notfound
        public IActionResult GetServerError() 
        {
            throw new Exception(); //500
        }

        [HttpGet("badrequest")] // Get: /api/buggy/badrequest/one
        public IActionResult GetBadRequest()
        {
            return BadRequest(new { StatusCode = 400, Message = "Bad Request" });
        }

        [HttpGet("badrequest/{id}")] // Get: /api/buggy/badrequest
        public IActionResult GetValidationError(int id) // => 4000
        { 
            return Ok();  //400
        }
       
        [HttpGet("unauthorized")] // Get: /api/buggy/unauthorized
        public IActionResult GetUnauthorizedError()
        {
            return Unauthorized(new { StatusCode = 401, Message = "Unauthorized" });
        }
        
        [HttpGet("forbidden")] // Get: /api/buggy/forbidden
        public IActionResult GEtForbiddenRequest()
        {
            return Forbid(); // 401
        }

        [Authorize]
        [HttpGet("authorized")] // Get: api/buggy/authorized
        public IActionResult GetAuthorizedRequest()
        {
            return Ok();
        }
    }
}
