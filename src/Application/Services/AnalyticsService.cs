using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services;

/// <summary>
/// Service for analytics-related operations.
/// سرویس برای عملیات مرتبط با تحلیل.
/// </summary>
public class AnalyticsService : IAnalyticsService
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the AnalyticsService.
    /// سازنده‌ای برای ایجاد نمونه جدید از AnalyticsService.
    /// </summary>
    /// <param name="context">Database context / زمینه دیتابیس.</param>
    public AnalyticsService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Generates an analytics report.
    /// تولید گزارش تحلیلی.
    /// </summary>
    /// <param name="filter">Report filter / فیلتر گزارش.</param>
    /// <returns>The analytics report / گزارش تحلیلی.</returns>
    public async Task<AnalyticsReportDto> GenerateReportAsync(ReportFilterDto filter)
    {
        // Query activities
        // پرس‌وجو فعالیت‌ها
        var query = _context.UserActivities.AsQueryable();
        if (filter.StartDate.HasValue)
        {
            query = query.Where(a => a.CreatedAt >= filter.StartDate.Value);
        }
        if (filter.EndDate.HasValue)
        {
            query = query.Where(a => a.CreatedAt <= filter.EndDate.Value);
        }

        // Group by activity type
        // گروه‌بندی بر اساس نوع فعالیت
        var reportData = await query
            .GroupBy(a => a.ActivityType)
            .Select(g => new { ActivityType = g.Key, Count = g.Count() })
            .ToListAsync();

        // Create report
        // ایجاد گزارش
        var report = new AnalyticsReportDto
        {
            TotalActivities = reportData.Sum(r => r.Count),
            ActivityBreakdown = reportData.ToDictionary(r => r.ActivityType, r => r.Count)
        };

        return report;
    }
}