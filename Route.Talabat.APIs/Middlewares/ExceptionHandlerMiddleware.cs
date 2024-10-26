using Azure;
using Route.Talabat.APIs.Controllers.Errors;
using Route.Talabat.Core.Application.Exceptions;
using System.Net;

namespace Route.Talabat.APIs.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Process the request
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                #region Logging
                if (_environment.IsDevelopment())
                {
                    _logger.LogError(ex, ex.Message);
                }
                else
                {
                    // Production mode logging (e.g., to a file or database)
                }
                #endregion

                // Handle the exception
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            ApiResponse response;

            httpContext.Response.ContentType = "application/json"; // Set ContentType once

            switch (ex)
            {
                case NotFoundException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = new ApiResponse((int)HttpStatusCode.NotFound, ex.Message);
                    break;

                case BadRequestException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new ApiResponse((int)HttpStatusCode.BadRequest, ex.Message);
                    break;

                case UnAuthorizedException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response = new ApiResponse((int)HttpStatusCode.Unauthorized, ex.Message);
                    break;

                default:
                    // Handle other exceptions
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = _environment.IsDevelopment()
                        ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString())
                        : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                    break;
            }

            await httpContext.Response.WriteAsync(response.ToString()); // Send the response
        }
    }

}
