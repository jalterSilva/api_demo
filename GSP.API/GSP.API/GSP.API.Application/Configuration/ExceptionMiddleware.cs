using GSP.API.Core.Helpers.ExtensionMethods;
using GSP.API.Core.Models.Application;
using GSP.API.Core.Models.System;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace GSP.API.Application.Configuration
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomException ex)
            {
                await HandleExceptionCustomizedAsync(httpContext, ex);
            }
            catch (Exception e)
            {
                var erro = e.Message;
                await HandleRequestExceptionAsync(httpContext);
            }
        }

        private Task HandleExceptionCustomizedAsync(HttpContext context, CustomException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            return context.Response.WriteAsync(ex.GetError().ObjectToJson());
        }

        private Task HandleRequestExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(new ResultModel(new ErrorModel("Internal Server Error")).ObjectToJson());
        }

    }

}
