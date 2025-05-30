namespace Domain.Entities;

/// <summary>
/// Represents a product recommendation.
/// نشان‌دهنده یک پیشنهاد محصول.
/// </summary>
public class ProductRecommendation
{
    /// <summary>
    /// Unique identifier for the recommendation.
    /// شناسه یکتا برای پیشنهاد.
    /// </summary>
    public int ProductRecommendationId { get; set; }

    /// <summary>
    /// ID of the source product.
    /// شناسه محصول منبع.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// ID of the recommended product.
    /// شناسه محصول پیشنهادی.
    /// </summary>
    public int RecommendedProductId { get; set; }

    /// <summary>
    /// Recommendation score (0 to 1).
    /// امتیاز پیشنهاد (0 تا 1).
    /// </summary>
    public double Score { get; set; }

    /// <summary>
    /// Timestamp when the recommendation was created.
    /// زمان ایجاد پیشنهاد.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when the recommendation was last updated.
    /// زمان آخرین به‌روزرسانی پیشنهاد.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }

    // Navigation properties
    // ویژگی‌های ناوبری
    public Product? Product { get; set; }
    public Product? RecommendedProduct { get; set; }
}