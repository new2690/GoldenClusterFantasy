using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces;

/// <summary>
/// Interface for analytics-related operations.
/// رابط برای عملیات مرتبط با تحلیل.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Generates an analytics report.
    /// تولید یک گزارش تحلیلی.
    /// </summary>
    Task<AnalyticsReportDto> GenerateReportAsync(ReportFilterDto filter);
}