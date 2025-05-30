using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Represents the total loyalty points for a user.
/// نشان‌دهنده کل امتیازات وفاداری یک کاربر.
/// </summary>
public class LoyaltyPoint
{
    /// <summary>
    /// Identifier of the user.
    /// شناسه کاربر.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Total loyalty points accumulated.
    /// کل امتیازات وفاداری جمع‌شده.
    /// </summary>
    [Required]
    public int TotalPoints { get; set; }

    /// <summary>
    /// Timestamp when the points were last updated.
    /// زمان آخرین به‌روزرسانی امتیازات.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>
    /// Navigation property to the user.
    /// ویژگی ناوبری به کاربر.
    /// </summary>
    public User User { get; set; } = null!;
}