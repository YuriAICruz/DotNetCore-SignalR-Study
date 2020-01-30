using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebServerStudy.Core
{
    public class NotFoundMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            
            context.Response.Redirect("/");

            // Actual processing code goes here
            //context.Response.StatusCode = StatusCodes.Status200OK;
            //await context.Response.WriteAsync("Hello, world!");
        }
    }
}