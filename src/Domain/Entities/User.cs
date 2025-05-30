using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Represents a registered user.
/// نشان‌دهنده یک کاربر ثبت‌نام‌شده.
/// </summary>
public class User
{
    /// <summary>
    /// Unique identifier for the user.
    /// شناسه یکتا برای کاربر.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Email address of the user.
    /// آدرس ایمیل کاربر.
    /// </summary>
    [Required]
    public string Email { get; set; } = null!;

    /// <summary>
    /// Hashed password for security.
    /// رمز عبور هش‌شده برای امنیت.
    /// </summary>
    [Required]
    public string PasswordHash { get; set; } = null!;

    /// <summary>
    /// First name of the user.
    /// نام کاربر.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name of the user.
    /// نام خانوادگی کاربر.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Role of the user (e.g., Customer, Vendor, Admin).
    /// نقش کاربر (مثل مشتری، فروشنده، مدیر).
    /// </summary>
    [Required]
    public string Role { get; set; } = null!;

    /// <summary>
    /// Timestamp when the user was created.
    /// زمان ایجاد کاربر.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when the user was last updated.
    /// زمان آخرین به‌روزرسانی کاربر.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }
}