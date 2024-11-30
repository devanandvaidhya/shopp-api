using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class EmployeeMIddleware 
    {
        private readonly RequestDelegate _next;

        public EmployeeMIddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            return _next(httpContext);
        }

        //public Task InvokeAsync(HttpContext context, RequestDelegate next)
        //{
        //    throw new NotImplementedException();
        //}
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class EmployeeMIddlewareExtensions
    {
        public static IApplicationBuilder UseEmployeeMIddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EmployeeMIddleware>();
        }
    }
}
