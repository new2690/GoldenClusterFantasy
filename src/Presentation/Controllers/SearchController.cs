using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers;

/// <summary>
/// Controller for advanced search operations.
/// کنترلر برای عملیات جستجوی پیشرفته.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    /// <summary>
    /// Initializes a new instance of the SearchController.
    /// سازنده‌ای برای ایجاد نمونه جدید از SearchController.
    /// </summary>
    /// <param name="searchService">Search service / سرویس جستجو.</param>
    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    /// <summary>
    /// Performs an advanced search based on the provided query.
    /// انجام جستجوی پیشرفته بر اساس پرس‌وجوی ارائه‌شده.
    /// </summary>
    /// <param name="query">Search query parameters / پارامترهای پرس‌وجوی جستجو.</param>
    /// <returns>Search results / نتایج جستجو.</returns>
    [HttpPost]
    public async Task<IActionResult> Search([FromBody] SearchQueryDto query)
    {
        // Get user ID from JWT (if authenticated)
        // دریافت شناسه کاربر از JWT (اگر احراز هویت شده باشد)
        int? userId = User.Identity?.IsAuthenticated == true
            ? int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
            : null;

        // Call service to perform search
        // فراخوانی سرویس برای انجام جستجو
        var result = await _searchService.SearchAsync(query, userId);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves the search history for the authenticated user.
    /// دریافت تاریخچه جستجو برای کاربر احراز هویت‌شده.
    /// </summary>
    /// <returns>List of search history records / لیست رکوردهای تاریخچه جستجو.</returns>
    [Authorize]
    [HttpGet("history")]
    public async Task<IActionResult> GetSearchHistory()
    {
        // Get user ID from JWT
        // دریافت شناسه کاربر از JWT
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        // Call service to get search history
        // فراخوانی سرویس برای دریافت تاریخچه جستجو
        var history = await _searchService.GetSearchHistoryAsync(userId);
        return Ok(history);
    }

    /// <summary>
    /// Retrieves all filterable attributes for search.
    /// دریافت تمام ویژگی‌های قابل فیلتر برای جستجو.
    /// </summary>
    [HttpGet("attributes")]
    public async Task<IActionResult> GetFilterableAttributes()
    {
        var attributes = await _searchService.GetFilterableAttributesAsync();
        return Ok(attributes);
    }

    /// <summary>
    /// Retrieves product recommendations for a user or product.
    /// دریافت پیشنهادات محصول برای یک کاربر یا محصول.
    /// </summary>
    /// <param name="productId">Optional product ID / شناسه محصول اختیاری.</param>
    /// <param name="limit">Maximum number of recommendations (default: 5) / حداکثر تعداد پیشنهادات (پیش‌فرض: 5).</param>
    /// <returns>List of recommended products / لیست محصولات پیشنهادی.</returns>
    [HttpGet("recommendations")]
    public async Task<IActionResult> GetRecommendations([FromQuery] int? productId, [FromQuery] int limit = 5)
    {
        int? userId = User.Identity?.IsAuthenticated == true
            ? int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
            : null;

        var recommendations = await _searchService.GetRecommendationsAsync(userId, productId, limit);
        return Ok(recommendations);
    }
}

// توضیحات:
// Search: جستجوی پیشرفته رو با پارامترهای ورودی انجام می‌ده و نتایج رو با صفحه‌بندی برمی‌گردونه.

// GetSearchHistory: تاریخچه جستجوی کاربر احراز هویت‌شده رو برمی‌گردونه.

// متد Search برای همه کاربران (حتی ناشناس) در دسترسه، اما GetSearchHistory نیاز به احراز هویت داره

