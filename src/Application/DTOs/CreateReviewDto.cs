using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

/// <summary>
/// Data transfer object for creating a review.
/// شیء انتقال داده برای ایجاد نظر.
/// </summary>
public class CreateReviewDto
{
    /// <summary>
    /// Identifier of the product.
    /// شناسه محصول.
    /// </summary>
    [Required]
    public int ProductId { get; set; }

    /// <summary>
    /// Rating given (1-5).
    /// امتیاز داده‌شده (۱ تا ۵).
    /// </summary>
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    /// <summary>
    /// Comment provided.
    /// نظر ارائه‌شده.
    /// </summary>
    public string? Comment { get; set; }
}