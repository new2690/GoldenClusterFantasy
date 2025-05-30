using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Represents a user review for a product.
/// نشان‌دهنده نظر کاربر برای یک محصول.
/// </summary>
public class Review
{
    /// <summary>
    /// Unique identifier for the review.
    /// شناسه یکتا برای نظر.
    /// </summary>
    public int ReviewId { get; set; }

    /// <summary>
    /// Identifier of the product.
    /// شناسه محصول.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Identifier of the user.
    /// شناسه کاربر.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Rating given (1-5).
    /// امتیاز داده‌شده (۱ تا ۵).
    /// </summary>
    [Required]
    public int Rating { get; set; }

    /// <summary>
    /// Comment provided by the user.
    /// نظر ارائه‌شده توسط کاربر.
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Timestamp when the review was created.
    /// زمان ایجاد نظر.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Navigation property to the product.
    /// ویژگی ناوبری به محصول.
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Navigation property to the user.
    /// ویژگی ناوبری به کاربر.
    /// </summary>
    public User User { get; set; } = null!;
}