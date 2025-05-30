using Domain.Enums;

namespace Application.DTOs;

/// <summary>
/// Data transfer object for user activity information.
/// شیء انتقال داده برای اطلاعات فعالیت کاربر.
/// </summary>
public class UserActivityDto
{
    /// <summary>
    /// Unique identifier for the activity.
    /// شناسه یکتا برای فعالیت.
    /// </summary>
    public int ActivityId { get; set; }

    /// <summary>
    /// Type of activity.
    /// نوع فعالیت.
    /// </summary>
    public ActivityType ActivityType { get; set; }

    /// <summary>
    /// Details of the activity.
    /// جزئیات فعالیت.
    /// </summary>
    public string? Details { get; set; }
}