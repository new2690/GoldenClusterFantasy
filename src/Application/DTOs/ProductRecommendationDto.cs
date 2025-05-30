namespace Application.DTOs;

/// <summary>
/// DTO for product recommendations.
/// DTO برای پیشنهادات محصول.
/// </summary>
public class ProductRecommendationDto
{
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
    /// Recommended product details.
    /// جزئیات محصول پیشنهادی.
    /// </summary>
    public ProductDto? RecommendedProduct { get; set; }
}