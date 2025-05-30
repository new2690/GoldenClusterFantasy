using System;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Represents a user's subscription plan.
/// نشان‌دهنده پلن اشتراک کاربر.
/// </summary>
public class Subscription
{
    /// <summary>
    /// Unique identifier for the subscription.
    /// شناسه یکتا برای اشتراک.
    /// </summary>
    public int SubscriptionId { get; set; }

    /// <summary>
    /// Identifier of the user.
    /// شناسه کاربر.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Subscription plan (e.g., Weekly, Monthly).
    /// پلن اشتراک (مثل هفتگی، ماهانه).
    /// </summary>
    [Required]
    public SubscriptionPlan Plan { get; set; }

    /// <summary>
    /// Status of the subscription (e.g., Active, Cancelled).
    /// وضعیت اشتراک (مثل فعال، لغوشده).
    /// </summary>
    [Required]
    public string Status { get; set; } = null!;

    /// <summary>
    /// Start date of the subscription.
    /// تاریخ شروع اشتراک.
    /// </summary>
    public DateTimeOffset StartDate { get; set; }

    /// <summary>
    /// Date of the next delivery.
    /// تاریخ تحویل بعدی.
    /// </summary>
    public DateTimeOffset NextDeliveryDate { get; set; }

    /// <summary>
    /// Timestamp when the subscription was created.
    /// زمان ایجاد اشتراک.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when the subscription was last updated.
    /// زمان آخرین به‌روزرسانی اشتراک.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>
    /// Navigation property to the user.
    /// ویژگی ناوبری به کاربر.
    /// </summary>
    public User User { get; set; } = null!;
}