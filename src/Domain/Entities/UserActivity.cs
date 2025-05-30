using System;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Represents an activity performed by a user.
/// نشان‌دهنده یک فعالیت انجام‌شده توسط کاربر.
/// </summary>
public class UserActivity
{
    /// <summary>
    /// Unique identifier for the activity.
    /// شناسه یکتا برای فعالیت.
    /// </summary>
    public int ActivityId { get; set; }

    /// <summary>
    /// Identifier of the user.
    /// شناسه کاربر.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Type of activity (e.g., Login, Purchase).
    /// نوع فعالیت (مثل ورود، خرید).
    /// </summary>
    [Required]
    public ActivityType ActivityType { get; set; }

    /// <summary>
    /// Additional details about the activity.
    /// جزئیات اضافی درباره فعالیت.
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// Timestamp when the activity was recorded.
    /// زمان ثبت فعالیت.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Navigation property to the user.
    /// ویژگی ناوبری به کاربر.
    /// </summary>
    public User User { get; set; } = null!;
}