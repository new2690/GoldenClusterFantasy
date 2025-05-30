using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers;

/// <summary>
/// Controller for analytics-related operations.
/// کنترلر برای عملیات مرتبط با تحلیل.
/// </summary>
/// <remarks>
/// This controller handles requests related to generating analytics reports for user activities.
/// این کنترلر درخواست‌های مرتبط با تولید گزارش‌های تحلیلی برای فعالیت‌های کاربران را مدیریت می‌کند.
/// </remarks>
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;

    /// <summary>
    /// Initializes a new instance of the AnalyticsController.
    /// سازنده‌ای برای ایجاد نمونه جدید از AnalyticsController.
    /// </summary>
    /// <param name="analyticsService">Analytics service for generating reports / سرویس تحلیل برای تولید گزارش‌ها.</param>
    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    /// <summary>
    /// Generates an analytics report based on the provided filter.
    /// تولید گزارش تحلیلی بر اساس فیلتر ارائه‌شده.
    /// </summary>
    /// <param name="filter">Filter criteria for the report (e.g., date range) / معیارهای فیلتر برای گزارش (مثلاً بازه زمانی).</param>
    /// <returns>An analytics report with activity breakdown / گزارش تحلیلی با تفکیک فعالیت‌ها.</returns>
    /// <response code="200">Returns the generated analytics report / گزارش تحلیلی تولیدشده را برمی‌گرداند.</response>
    /// <response code="400">If the filter criteria are invalid / اگر معیارهای فیلتر نامعتبر باشند.</response>
    /// <response code="401">If the user is not authenticated / اگر کاربر احراز هویت نشده باشد.</response>
    /// <response code="403">If the user lacks the required role (Admin or SuperAdmin) / اگر کاربر نقش موردنیاز (ادمین یا سوپرادمین) را نداشته باشد.</response>
    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPost("generate-report")]
    [ProducesResponseType(typeof(AnalyticsReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GenerateReport([FromBody] ReportFilterDto filter)
    {
        // Validate filter
        // اعتبارسنجی فیلتر
        if (filter == null || (filter.StartDate.HasValue && filter.EndDate.HasValue && filter.StartDate > filter.EndDate))
        {
            return BadRequest("Invalid filter criteria. Start date must be before end date. / معیارهای فیلتر نامعتبر است. تاریخ شروع باید قبل از تاریخ پایان باشد.");
        }

        // Call service to generate report
        // فراخوانی سرویس برای تولید گزارش
        var report = await _analyticsService.GenerateReportAsync(filter);

        // Return the report
        // بازگشت گزارش
        return Ok(report);
    }
}

