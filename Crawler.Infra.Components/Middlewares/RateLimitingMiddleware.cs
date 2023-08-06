using Crawler.Infra.Components.Interfaces.RateLimiter;
using Microsoft.AspNetCore.Http;

namespace Crawler.Infra.Components.Middlewares;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRateLimiter _rateLimiter;

    public RateLimitingMiddleware(RequestDelegate next, IRateLimiter rateLimiter)
    {
        _next = next;
        _rateLimiter = rateLimiter;
    }

    public async Task Invoke(HttpContext context)
    {
        // Obter o endereço IP do cliente da requisição
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();

        if (ipAddress is null)
            throw new ArgumentNullException(nameof(ipAddress));

        // Verificar se o rate limit foi excedido
        bool isAllowed = await _rateLimiter.IsLimited(ipAddress);

        if (!isAllowed)
        {
            // Caso o rate limit seja excedido, retornar um erro 429 - Too Many Requests
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            await context.Response.WriteAsync("Rate limit excedido. Tente novamente mais tarde.");
            return;
        }

        await _next(context);

    }
}
