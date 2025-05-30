using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces;

/// <summary>
/// Interface for review-related operations.
/// رابط برای عملیات مرتبط با نظر.
/// </summary>
public interface IReviewService
{
    /// <summary>
    /// Creates a new review.
    /// ایجاد یک نظر جدید.
    /// </summary>
    Task<ReviewDto> CreateReviewAsync(CreateReviewDto dto, int userId);

    /// <summary>
    /// Gets reviews for a product.
    /// دریافت نظرات برای یک محصول.
    /// </summary>
    Task<IEnumerable<ReviewDto>> GetReviewsByProductIdAsync(int productId);
}