namespace Application.DTOs;

/// <summary>
/// Data transfer object for review information.
/// شیء انتقال داده برای اطلاعات نظر.
/// </summary>
public class ReviewDto
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
    /// Rating given.
    /// امتیاز داده‌شده.
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Comment provided.
    /// نظر ارائه‌شده.
    /// </summary>
    public string? Comment { get; set; }
}