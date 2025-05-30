using System;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Represents a transaction related to loyalty points.
/// نشان‌دهنده یک تراکنش مرتبط با امتیازات وفاداری.
/// </summary>
public class LoyaltyTransaction
{
    /// <summary>
    /// Unique identifier for the transaction.
    /// شناسه یکتا برای تراکنش.
    /// </summary>
    public int TransactionId { get; set; }

    /// <summary>
    /// Identifier of the user.
    /// شناسه کاربر.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Number of points (positive for earned, negative for spent).
    /// تعداد امتیازات (مثبت برای کسب‌شده، منفی برای مصرف‌شده).
    /// </summary>
    [Required]
    public int Points { get; set; }

    /// <summary>
    /// Type of transaction (e.g., Earned, Spent).
    /// نوع تراکنش (مثل کسب‌شده، مصرف‌شده).
    /// </summary>
    [Required]
    public LoyaltyTransactionType Type { get; set; }

    /// <summary>
    /// Timestamp when the transaction was created.
    /// زمان ایجاد تراکنش.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Navigation property to the user.
    /// ویژگی ناوبری به کاربر.
    /// </summary>
    public User User { get; set; } = null!;
}