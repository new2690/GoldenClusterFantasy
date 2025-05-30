namespace Application.DTOs;

/// <summary>
/// Data transfer object for analytics report.
/// شیء انتقال داده برای گزارش تحلیلی.
/// </summary>
public class AnalyticsReportDto
{
    /// <summary>
    /// Total number of orders.
    /// تعداد کل سفارشات.
    /// </summary>
    public int TotalOrders { get; set; }

    /// <summary>
    /// Total revenue.
    /// کل درآمد.
    /// </summary>
    public decimal TotalRevenue { get; set; }

    /// <summary>
    /// Number of active users.
    /// تعداد کاربران فعال.
    /// </summary>
    public int ActiveUsers { get; set; }
}