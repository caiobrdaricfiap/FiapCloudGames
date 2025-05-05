using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FiapCloudGameWebAPI
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class logMiddleware
    {
        private readonly RequestDelegate _next;

        public logMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class logMiddlewareExtensions
    {
        public static IApplicationBuilder UselogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<logMiddleware>();
        }
    }
}
