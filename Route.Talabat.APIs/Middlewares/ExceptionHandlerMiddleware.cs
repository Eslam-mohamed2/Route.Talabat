using Castle.Components.DictionaryAdapter.Xml;
using Route.Talabat.APIs.Controllers.Errors;
using Route.Talabat.Core.Application.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Route.Talabat.APIs.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlerMiddleware(RequestDelegate next,ILogger<ExceptionHandlerMiddleware> logger,IWebHostEnvironment env) 
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

                #region Logging : TODO
                if (_env.IsDevelopment())
                {
                    _logger.LogError(ex, ex.Message);
                    //response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString());

                }
                else
                {
                    //response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, null, null);
                } 
                #endregion

                response = await HandleExceptionAsync(httpContext, ex);

                //Development mode

            }
        }

        private async Task<ApiResponse> HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            ApiResponse response;
            switch (ex)
            {
                case NotFoundException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    httpContext.Response.ContentType = "application/json";

                    response = new ApiResponse(404, ex.Message);
                    await httpContext.Response.WriteAsJsonAsync(response.ToString());
                    break;
                
                case BadRequestException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    httpContext.Response.ContentType = "application/json";

                    response = new ApiResponse(400, ex.Message);
                    await httpContext.Response.WriteAsJsonAsync(response.ToString());
                    break;
                
                
                case UnAuthorizedException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    httpContext.Response.ContentType = "application/json";

                    response = new ApiResponse(500, ex.Message);
                    await httpContext.Response.WriteAsJsonAsync(response.ToString());
                    break;

                default:
                    response = _env.IsDevelopment()?
                        new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message , ex.StackTrace?.ToString()) :
                        response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, null, null);

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

            return response;
        }
    }
}
