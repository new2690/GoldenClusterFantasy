using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs;

/// <summary>
/// Data transfer object for creating a notification.
/// شیء انتقال داده برای ایجاد اعلان.
/// </summary>
public class CreateNotificationDto
{
    /// <summary>
    /// Identifier of the user.
    /// شناسه کاربر.
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// Type of notification.
    /// نوع اعلان.
    /// </summary>
    [Required]
    public NotificationType Type { get; set; }

    /// <summary>
    /// Content of the notification.
    /// محتوای اعلان.
    /// </summary>
    [Required]
    public string Content { get; set; } = null!;
}