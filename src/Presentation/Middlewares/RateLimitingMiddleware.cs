using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Presentation.Middlewares;

/// <summary>
/// Middleware for rate limiting requests.
/// میدلور برای محدود کردن نرخ درخواست‌ها.
/// </summary>
public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMemoryCache _cache;
    private readonly int _requestLimit = 100; // Max requests per minute
    private readonly TimeSpan _timeWindow = TimeSpan.FromMinutes(1);

    /// <summary>
    /// Initializes a new instance of the RateLimitingMiddleware.
    /// سازنده‌ای برای ایجاد نمونه جدید از RateLimitingMiddleware.
    /// </summary>
    /// <param name="next">Next middleware in the pipeline / میدلور بعدی در خط لوله.</param>
    /// <param name="cache">Memory cache instance / نمونه کش حافظه.</param>
    public RateLimitingMiddleware(RequestDelegate next, IMemoryCache cache)
    {
        _next = next;
        _cache = cache;
    }

    /// <summary>
    /// Invokes the middleware.
    /// فراخوانی میدلور.
    /// </summary>
    /// <param name="context">HTTP context / زمینه HTTP.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        // Get client IP
        // دریافت آی‌پی کلاینت
        var clientIp = context.Connection.RemoteIpAddress?.ToString();
        var cacheKey = $"RateLimit_{clientIp}";

        // Check request count
        // بررسی تعداد درخواست‌ها
        if (!_cache.TryGetValue(cacheKey, out int requestCount))
        {
            requestCount = 0;
        }

        // Increment request count
        // افزایش تعداد درخواست‌ها
        requestCount++;

        // Update cache
        // به‌روزرسانی کش
        _cache.Set(cacheKey, requestCount, _timeWindow);

        // Check if limit exceeded
        // بررسی превыش از حد مجاز
        if (requestCount > _requestLimit)
        {
            context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
            await context.Response.WriteAsync("Rate limit exceeded. Try again later / حد نرخ درخواست‌ها превыش کرد. بعداً دوباره تلاش کنید.");
            return;
        }

        // Pass request to next middleware
        // انتقال درخواست به میدلور بعدی
        await _next(context);
    }
}