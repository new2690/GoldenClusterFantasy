using System;

namespace Application.DTOs;

/// <summary>
/// Data transfer object for report filters.
/// شیء انتقال داده برای فیلترهای گزارش.
/// </summary>
public class ReportFilterDto
{
    /// <summary>
    /// Start date for the report.
    /// تاریخ شروع برای گزارش.
    /// </summary>
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    /// End date for the report.
    /// تاریخ پایان برای گزارش.
    /// </summary>
    public DateTimeOffset? EndDate { get; set; }
}