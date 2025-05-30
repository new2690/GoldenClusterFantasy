using System.Collections.Generic;

namespace Application.DTOs;

/// <summary>
/// DTO for search results.
/// DTO برای نتایج جستجو.
/// </summary>
public class SearchResultDto
{
    /// <summary>
    /// List of products matching the search criteria.
    /// لیست محصولات مطابق با معیارهای جستجو.
    /// </summary>
    public List<ProductDto> Products { get; set; } = new List<ProductDto>();

    /// <summary>
    /// Total number of matching products (for pagination).
    /// تعداد کل محصولات مطابق (برای صفحه‌بندی).
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Current page number.
    /// شماره صفحه فعلی.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Number of items per page.
    /// تعداد آیتم‌ها در هر صفحه.
    /// </summary>
    public int PageSize { get; set; }
}