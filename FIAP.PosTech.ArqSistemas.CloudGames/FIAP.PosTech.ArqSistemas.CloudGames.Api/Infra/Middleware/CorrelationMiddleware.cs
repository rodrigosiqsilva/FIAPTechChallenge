using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CorrelationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string _correlationIdHeader = "x-corretation-id";

        public CorrelationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ICorrelationIdGenerator correlationIdGenerator)
        {
            var correlationId = GetCorrelationId(httpContext, correlationIdGenerator);
            AddCorrelationIdHeaderToResponse(httpContext, correlationId);

            await _next(httpContext);
        }

        private static StringValues GetCorrelationId(HttpContext httpContext, ICorrelationIdGenerator correlationIdGenerator)
        {
            if (!httpContext.Request.Headers.TryGetValue(_correlationIdHeader, out var correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
            }
            correlationIdGenerator.Set(correlationId);
            return correlationId;
        }

        private static void AddCorrelationIdHeaderToResponse(HttpContext httpContext, StringValues correlationId)
            => httpContext.Response.OnStarting(() =>
            {
                httpContext.Response.Headers[_correlationIdHeader] = new[] { correlationId.ToString() };
                return Task.CompletedTask;
            });
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CorrelationMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorrelationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorrelationMiddleware>();
        }
    }
}
