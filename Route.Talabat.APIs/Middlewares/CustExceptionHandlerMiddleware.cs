using Castle.Components.DictionaryAdapter.Xml;
using Route.Talabat.APIs.Controllers.Errors;
using Route.Talabat.APIs.Controllers.Exceptions;
using System.Net;
using System.Text.Json;

namespace Route.Talabat.APIs.Middlewares
{
    public class CustExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public CustExceptionHandlerMiddleware(RequestDelegate next,ILogger<CustExceptionHandlerMiddleware> logger,IWebHostEnvironment env) 
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex) 
            {
                ApiResponse response;
                switch (ex)
                {
                    case NotFoundException:
                        httpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
                        httpContext.Response.ContentType = "application/json";

                         response = new ApiResponse(404, ex.Message);
                        await httpContext.Response.WriteAsJsonAsync(response.ToString());
                        break;

                    default:
                        if (_env.IsDevelopment())
                        {
                            _logger.LogError(ex, ex.Message);
                            response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString());

                        }
                        else
                        {
                            response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, null, null);
                        }
                        //Production Mode
                        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        httpContext.Response.ContentType = "application/json";
                        await httpContext.Response.WriteAsJsonAsync(response.ToString());
                        break;
                }

                //Development mode
               
            }
        }
    }
}
