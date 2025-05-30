using System;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Represents a notification sent to a user.
/// نشان‌دهنده یک اعلان ارسالی به کاربر.
/// </summary>
public class Notification
{
    /// <summary>
    /// Unique identifier for the notification.
    /// شناسه یکتا برای اعلان.
    /// </summary>
    public int NotificationId { get; set; }

    /// <summary>
    /// Identifier of the user.
    /// شناسه کاربر.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Type of notification (e.g., Email, SMS).
    /// نوع اعلان (مثل ایمیل، پیامک).
    /// </summary>
    [Required]
    public NotificationType Type { get; set; }

    /// <summary>
    /// Content of the notification.
    /// محتوای اعلان.
    /// </summary>
    [Required]
    public string Content { get; set; } = null!;

    /// <summary>
    /// Status of the notification (e.g., Pending, Sent).
    /// وضعیت اعلان (مثل در انتظار، ارسال‌شده).
    /// </summary>
    [Required]
    public string Status { get; set; } = null!;

    /// <summary>
    /// Timestamp when the notification was created.
    /// زمان ایجاد اعلان.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when the notification was last updated.
    /// زمان آخرین به‌روزرسانی اعلان.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>
    /// Navigation property to the user.
    /// ویژگی ناوبری به کاربر.
    /// </summary>
    public User User { get; set; } = null!;
}