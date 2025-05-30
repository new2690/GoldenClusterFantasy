using Application.DTOs;
using System.Threading.Tasks;

namespace Application.Interfaces;

/// <summary>
/// Interface for advanced search operations.
/// رابط برای عملیات جستجوی پیشرفته.
/// </summary>
public interface ISearchService
{
    /// <summary>
    /// Performs an advanced search based on the provided query.
    /// انجام جستجوی پیشرفته بر اساس پرس‌وجوی ارائه‌شده.
    /// </summary>
    /// <param name="query">Search query parameters / پارامترهای پرس‌وجوی جستجو.</param>
    /// <param name="userId">ID of the user performing the search (null for anonymous) / شناسه کاربر انجام‌دهنده جستجو (null برای ناشناس).</param>
    /// <returns>Search results / نتایج جستجو.</returns>
    Task<SearchResultDto> SearchAsync(SearchQueryDto query, int? userId);

    /// <summary>
    /// Retrieves the search history for a user.
    /// دریافت تاریخچه جستجو برای یک کاربر.
    /// </summary>
    /// <param name="userId">User ID / شناسه کاربر.</param>
    /// <returns>List of search history records / لیست رکوردهای تاریخچه جستجو.</returns>
    Task<IEnumerable<SearchHistoryDto>> GetSearchHistoryAsync(int userId);

    // New method for getting available attributes
    // متد جدید برای دریافت ویژگی‌های در دسترس
    /// <summary>
    /// Retrieves all filterable attributes.
    /// دریافت تمام ویژگی‌های قابل فیلتر.
    /// </summary>
    /// <returns>List of filterable attributes / لیست ویژگی‌های قابل فیلتر.</returns>
    Task<IEnumerable<AttributeDto>> GetFilterableAttributesAsync();

        /// <summary>
    /// Retrieves product recommendations based on search history and attributes.
    /// دریافت پیشنهادات محصول بر اساس تاریخچه جستجو و ویژگی‌ها.
    /// </summary>
    /// <param name="userId">User ID (null for anonymous) / شناسه کاربر (null برای ناشناس).</param>
    /// <param name="productId">Optional product ID for specific recommendations / شناسه محصول اختیاری برای پیشنهادات خاص.</param>
    /// <param name="limit">Maximum number of recommendations / حداکثر تعداد پیشنهادات.</param>
    /// <returns>List of recommended products / لیست محصولات پیشنهادی.</returns>
    Task<IEnumerable<ProductRecommendationDto>> GetRecommendationsAsync(int? userId, int? productId, int limit = 5);

}