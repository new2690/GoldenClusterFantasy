using Domain.Enums;

namespace Application.DTOs;

/// <summary>
/// Data transfer object for notification information.
/// شیء انتقال داده برای اطلاعات اعلان.
/// </summary>
public class NotificationDto
{
    /// <summary>
    /// Unique identifier for the notification.
    /// شناسه یکتا برای اعلان.
    /// </summary>
    public int NotificationId { get; set; }

    /// <summary>
    /// Type of notification.
    /// نوع اعلان.
    /// </summary>
    public NotificationType Type { get; set; }

    /// <summary>
    /// Content of the notification.
    /// محتوای اعلان.
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// Status of the notification.
    /// وضعیت اعلان.
    /// </summary>
    public string Status { get; set; } = null!;
}