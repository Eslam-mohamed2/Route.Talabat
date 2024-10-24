using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Route.Talaat.APIs.Controllers.Base;
using Route.Talabat.APIs.Controllers.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.APIs.Controllers.Controllers.Buggy
{
    public class BuggyController : BaseApiController
    {

        [HttpGet("NotFound")] //GET: /api/Buggy/NotFound
        public IActionResult GetNotFoundError()
        {
            return NotFound(new ApiResponse(404)); //404
        }

        [HttpGet("ServerError")] //GET: /api/Buggy/ServerError
        public IActionResult GetServerError()
        {
            throw new Exception();
        }

        [HttpGet("BadRequest")] //GET: /api/Buggy/BadRequest
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400)); //400
        }

        [HttpGet("BadRequest/{id}")] //GET: /api/Buggy/BadRequest/five{string}
        public IActionResult GetValidationError(int id) // => 400
        {
            return Ok();
        }

        [HttpGet("UnAuthorized")] //GET: /api/Buggy/UnAuthorized
        public IActionResult GetUnAuthorized()
        {
            return Unauthorized(new ApiResponse(401)); //401
        }

        [HttpGet("Forbidden")] //GET: /api/Buggy/Forbidden
        public IActionResult GetForbidden()
        {
            return Forbid(); //401
        }

        [Authorize]
        [HttpGet("Authorize")] //GET: /api/Buggy/Authorize
        public IActionResult GetAuthorizeRequest()
        {
            return Ok();
        }
    }
}
