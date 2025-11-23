
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mentorax.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/problem+json";
                var problem = new {
                    type = "https://httpstatuses.com/500",
                    title = "Internal Server Error",
                    status = 500,
                    detail = ex.Message
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
            }
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
