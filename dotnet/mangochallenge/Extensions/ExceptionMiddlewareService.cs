using LoggerService;
//using Microsoft.AspNetCore.Http.HttpContext; // Microsoft.AspNetCore.Http.HttpContext
using System;
using System.Net;
using Entities.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

/*
-using GlobalErrorHandling.Models;
-using LoggerService;
using Microsoft.AspNetCore.Http;
-using System;
-using System.Net;
-using System.Threading.Tasks;
*/

namespace Services.ExceptionMiddlewareService
{
    public class ExceptionMiddlewareService
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public ExceptionMiddlewareService(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware."
            }.ToString());
        }
    }

}