using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Middlewares;

/// <summary>
/// Middleware for handling exceptions globally.
/// میدلور برای مدیریت خطاها به‌صورت سراسری.
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the ErrorHandlingMiddleware.
    /// سازنده‌ای برای ایجاد نمونه جدید از ErrorHandlingMiddleware.
    /// </summary>
    /// <param name="next">Next middleware in the pipeline / میدلور بعدی در خط لوله.</param>
    /// <param name="logger">Logger instance / نمونه لاگر.</param>
    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the middleware.
    /// فراخوانی میدلور.
    /// </summary>
    /// <param name="context">HTTP context / زمینه HTTP.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Pass request to next middleware
            // انتقال درخواست به میدلور بعدی
            await _next(context);
        }
        catch (Exception ex)
        {
            // Handle the exception
            // مدیریت خطا
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Handles the exception and returns a response.
    /// مدیریت خطا و بازگشت پاسخ.
    /// </summary>
    /// <param name="context">HTTP context / زمینه HTTP.</param>
    /// <param name="exception">The exception / خطا.</param>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Log the exception
        // ثبت خطا
        _logger.LogError(exception, "An unexpected error occurred / خطای غیرمنتظره رخ داد.");

        // Set response details
        // تنظیم جزئیات پاسخ
        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "An unexpected error occurred. Please try again later / خطای غیرمنتظره رخ داد. لطفاً بعداً دوباره تلاش کنید.",
            Detailed = exception.Message // Remove in production for security / در تولید برای امنیت حذف شود
        };

        // Set response status and content
        // تنظیم وضعیت و محتوای پاسخ
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // Serialize and write response
        // سریال‌سازی و نوشتن پاسخ
        var jsonResponse = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(jsonResponse);
    }
}