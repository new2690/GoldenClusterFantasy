using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Caching;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services;

/// <summary>
/// Service for advanced search operations.
/// سرویس برای عملیات جستجوی پیشرفته.
/// </summary>
public class SearchService : ISearchService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    /// <summary>
    /// Initializes a new instance of the SearchService.
    /// سازنده‌ای برای ایجاد نمونه جدید از SearchService.
    /// </summary>
    /// <param name="context">Database context / زمینه دیتابیس.</param>
    /// <param name="mapper">AutoMapper instance / نمونه AutoMapper.</param>
    /// <param name="cacheService">Cache service / سرویس کش.</param>
    public SearchService(AppDbContext context, IMapper mapper, ICacheService cacheService)
    {
        _context = context;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    /// <summary>
    /// Performs an advanced search based on the provided query.
    /// انجام جستجوی پیشرفته بر اساس پرس‌وجوی ارائه‌شده.
    /// </summary>
    /// <param name="query">Search query parameters / پارامترهای پرس‌وجوی جستجو.</param>
    /// <param name="userId">ID of the user performing the search (null for anonymous) / شناسه کاربر انجام‌دهنده جستجو (null برای ناشناس).</param>
    /// <returns>Search results / نتایج جستجو.</returns>
    public async Task<SearchResultDto> SearchAsync(SearchQueryDto query, int? userId)
    {
        // Generate cache key based on query and user
        // تولید کلید کش بر اساس پرس‌وجو و کاربر
        var cacheKey = $"Search_{userId ?? 0}_{JsonSerializer.Serialize(query)}";
        var cachedResult = await _cacheService.GetAsync<SearchResultDto>(cacheKey);
        if (cachedResult != null)
        {
            return cachedResult;
        }

        // Build query dynamically
        // ساخت پرس‌وجو به‌صورت پویا
            var dbQuery = _context.Products
            .Include(p => p.Vendor)
            .Include(p => p.Inventory)
            .Include(p => p.AttributeValues)
                .ThenInclude(pav => pav.Attribute)
            .AsQueryable();

        // Apply filters
        // اعمال فیلترها
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var keyword = query.Keyword.ToLower();
            if (query.EnableFuzzySearch)
            {
                // Fuzzy search using Levenshtein (implemented in SQL)
                dbQuery = dbQuery.Where(p =>
                    EF.Functions.Collate(p.Name, "SQL_Latin1_General_CP1_CI_AS").ToLower().Contains(keyword) ||
                    EF.Functions.Collate(p.Description, "SQL_Latin1_General_CP1_CI_AS").ToLower().Contains(keyword) ||
                    (p.Tags != null && EF.Functions.Collate(p.Tags, "SQL_Latin1_General_CP1_CI_AS").ToLower().Contains(keyword)) ||
                    EF.Functions.Levenshtein(p.Name.ToLower(), keyword) <= query.MaxFuzzyDistance ||
                    EF.Functions.Levenshtein(p.Description.ToLower(), keyword) <= query.MaxFuzzyDistance ||
                    (p.Tags != null && EF.Functions.Levenshtein(p.Tags.ToLower(), keyword) <= query.MaxFuzzyDistance));
            }
            else
            {
                dbQuery = dbQuery.Where(p =>
                    p.Name.ToLower().Contains(keyword) ||
                    p.Description.ToLower().Contains(keyword) ||
                    (p.Tags != null && p.Tags.ToLower().Contains(keyword)));
            }
        }

        if (!string.IsNullOrWhiteSpace(query.Brand))
        {
           var brand = query.Brand.ToLower();
            if (query.EnableFuzzySearch)
            {
                dbQuery = dbQuery.Where(p => p.Brand != null &&
                    (p.Brand.ToLower().Contains(brand) ||
                     EF.Functions.Levenshtein(p.Brand.ToLower(), brand) <= query.MaxFuzzyDistance));
            }
            else
            {
                dbQuery = dbQuery.Where(p => p.Brand != null && p.Brand.ToLower().Contains(brand));
            }
        }

        if (!string.IsNullOrWhiteSpace(query.Vendor))
        {
            var vendor = query.Vendor.ToLower();
            dbQuery = dbQuery.Where(p => p.Vendor != null &&
                (p.Vendor.VendorName.ToLower().Contains(vendor) ||
                 p.VendorId.ToString() == query.Vendor));
        }

        if (query.MinPrice.HasValue)
        {
            dbQuery = dbQuery.Where(p => p.Price >= query.MinPrice.Value);
        }

        if (query.MaxPrice.HasValue)
        {
            dbQuery = dbQuery.Where(p => p.Price <= query.MaxPrice.Value);
        }

        if (query.InStock.HasValue && query.InStock.Value)
        {
            dbQuery = dbQuery.Where(p => p.Inventory != null && p.Inventory.Quantity > 0);
        }

        if (query.StartDate.HasValue)
        {
            dbQuery = dbQuery.Where(p => p.CreatedAt >= query.StartDate.Value);
        }

        if (query.EndDate.HasValue)
        {
            dbQuery = dbQuery.Where(p => p.CreatedAt <= query.EndDate.Value);
        }

        if (query.Tags != null && query.Tags.Any())
        {
            foreach (var tag in query.Tags)
            {
                var tagLower = tag.ToLower();
                if (query.EnableFuzzySearch)
                {
                    dbQuery = dbQuery.Where(p => p.Tags != null &&
                        (p.Tags.ToLower().Contains(tagLower) ||
                         EF.Functions.Levenshtein(p.Tags.ToLower(), tagLower) <= query.MaxFuzzyDistance));
                }
                else
                {
                    dbQuery = dbQuery.Where(p => p.Tags != null && p.Tags.ToLower().Contains(tagLower));
                }
            }
        }

        if (query.AttributeFilters != null && query.AttributeFilters.Any())
        {
            foreach (var filter in query.AttributeFilters)
            {
                var attributeId = filter.Key;
                var value = filter.Value.ToLower();
                dbQuery = dbQuery.Where(p => p.AttributeValues.Any(pav =>
                    pav.AttributeId == attributeId &&
                    pav.Values.Any(v => v.ToLower() == value)));
            }
        }

        // Apply sorting
        // اعمال مرتب‌سازی
        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            switch (query.SortBy.ToLower())
            {
                case "price":
                    dbQuery = query.SortDirection.ToLower() == "asc"
                        ? dbQuery.OrderBy(p => p.Price)
                        : dbQuery.OrderByDescending(p => p.Price);
                    break;
                case "name":
                    dbQuery = query.SortDirection.ToLower() == "asc"
                        ? dbQuery.OrderBy(p => p.Name)
                        : dbQuery.OrderByDescending(p => p.Name);
                    break;
                case "createdat":
                    dbQuery = query.SortDirection.ToLower() == "asc"
                        ? dbQuery.OrderBy(p => p.CreatedAt)
                        : dbQuery.OrderByDescending(p => p.CreatedAt);
                    break;
                default:
                    dbQuery = dbQuery.OrderBy(p => p.ProductId);
                    break;
            }
        }
        else
        {
            dbQuery = dbQuery.OrderBy(p => p.ProductId);
        }

        // Get total count for pagination
        // دریافت تعداد کل برای صفحه‌بندی
        var totalCount = await dbQuery.CountAsync();

        // Apply pagination
        // اعمال صفحه‌بندی
        var skip = (query.PageNumber - 1) * query.PageSize;
        dbQuery = dbQuery.Skip(skip).Take(query.PageSize);

        // Execute query
        // اجرای پرس‌وجو
        var products = await dbQuery.ToListAsync();
        var productDtos = _mapper.Map<List<ProductDto>>(products);

        // Create result
        // ایجاد نتیجه
        var result = new SearchResultDto
        {
            Products = productDtos,
            TotalCount = totalCount,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };

        // Cache the result
        // کش کردن نتیجه
        await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5));

        // Save search history
        // ذخیره تاریخچه جستجو
        if (userId.HasValue || !string.IsNullOrWhiteSpace(query.Keyword))
        {
            var searchHistory = new SearchHistory
            {
                UserId = userId,
                Query = JsonSerializer.Serialize(query),
                CreatedAt = DateTimeOffset.UtcNow
            };
            _context.SearchHistory.Add(searchHistory);
            await _context.SaveChangesAsync();
        }

        return result;
    }

    /// <summary>
    /// Retrieves the search history for a user.
    /// دریافت تاریخچه جستجو برای یک کاربر.
    /// </summary>
    /// <param name="userId">User ID / شناسه کاربر.</param>
    /// <returns>List of search history records / لیست رکوردهای تاریخچه جستجو.</returns>
    public async Task<IEnumerable<SearchHistoryDto>> GetSearchHistoryAsync(int userId)
    {
        // Find search history for user
        // یافتن تاریخچه جستجو برای کاربر
        var history = await _context.SearchHistory
            .Where(sh => sh.UserId == userId)
            .OrderByDescending(sh => sh.CreatedAt)
            .ToListAsync();

        // Map to DTOs
        // نگاشت به DTOها
        return _mapper.Map<IEnumerable<SearchHistoryDto>>(history);
    }

    public async Task<IEnumerable<AttributeDto>> GetFilterableAttributesAsync()
    {
        var cacheKey = "FilterableAttributes";
        var cachedAttributes = await _cacheService.GetAsync<IEnumerable<AttributeDto>>(cacheKey);
        if (cachedAttributes != null)
            return cachedAttributes;

        var attributes = await _context.Attributes
            .Where(a => a.IsFilterable)
            .ToListAsync();
        var result = _mapper.Map<IEnumerable<AttributeDto>>(attributes);

        await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromHours(1));
        return result;
    }

    public async Task<IEnumerable<ProductRecommendationDto>> GetRecommendationsAsync(int? userId, int? productId, int limit = 5)
    {
        var cacheKey = $"Recommendations_{userId ?? 0}_{productId ?? 0}_{limit}";
        var cachedRecommendations = await _cacheService.GetAsync<IEnumerable<ProductRecommendationDto>>(cacheKey);
        if (cachedRecommendations != null)
            return cachedRecommendations;

        var recommendations = new List<ProductRecommendation>();

        if (productId.HasValue)
        {
            // Get recommendations based on product attributes
            var product = await _context.Products
                .Include(p => p.AttributeValues)
                .FirstOrDefaultAsync(p => p.ProductId == productId.Value);
            if (product != null)
            {
                var attributeValues = product.AttributeValues.SelectMany(pav => pav.Values).ToList();
                recommendations.AddRange(await _context.ProductRecommendations
                    .Include(pr => pr.RecommendedProduct)
                    .Where(pr => pr.ProductId == productId.Value)
                    .OrderByDescending(pr => pr.Score)
                    .Take(limit)
                    .ToListAsync());

                // If not enough recommendations, find similar products
                if (recommendations.Count < limit)
                {
                    var similarProducts = await _context.Products
                        .Include(p => p.AttributeValues)
                        .Where(p => p.ProductId != productId.Value &&
                                    p.AttributeValues.Any(pav => pav.Values.Any(v => attributeValues.Contains(v))))
                        .Take(limit - recommendations.Count)
                        .ToListAsync();

                    recommendations.AddRange(similarProducts.Select(sp => new ProductRecommendation
                    {
                        ProductId = productId.Value,
                        RecommendedProductId = sp.ProductId,
                        Score = 0.5, // Default score for attribute-based similarity
                        RecommendedProduct = sp
                    }));
                }
            }
        }

        if (userId.HasValue && recommendations.Count < limit)
        {
            // Get recommendations based on search history
            var userSearches = await _context.SearchHistory
                .Where(sh => sh.UserId == userId.Value)
                .OrderByDescending(sh => sh.CreatedAt)
                .Take(10)
                .ToListAsync();

            var searchKeywords = userSearches
                .Select(sh => JsonSerializer.Deserialize<SearchQueryDto>(sh.Query))
                .Where(q => q != null)
                .SelectMany(q => new[] { q!.Keyword }.Concat(q.Tags ?? new List<string>()))
                .Where(k => !string.IsNullOrWhiteSpace(k))
                .Distinct()
                .ToList();

            var searchBasedProducts = await _context.Products
                .Include(p => p.AttributeValues)
                .Where(p => searchKeywords.Any(k =>
                    p.Name.ToLower().Contains(k.ToLower()) ||
                    p.Description.ToLower().Contains(k.ToLower()) ||
                    (p.Tags != null && p.Tags.ToLower().Contains(k.ToLower())) ||
                    p.AttributeValues.Any(pav => pav.Values.Any(v => v.ToLower().Contains(k.ToLower())))))
                .Take(limit - recommendations.Count)
                .ToListAsync();

            recommendations.AddRange(searchBasedProducts.Select(sp => new ProductRecommendation
            {
                ProductId = productId ?? 0,
                RecommendedProductId = sp.ProductId,
                Score = 0.4, // Default score for search-based similarity
                RecommendedProduct = sp
            }));
        }

        // If still not enough, get popular products
        if (recommendations.Count < limit)
        {
            var popularProducts = await _context.Products
                .Include(p => p.AttributeValues)
                .OrderByDescending(p => p.OrderItems.Sum(oi => oi.Quantity))
                .Take(limit - recommendations.Count)
                .ToListAsync();

            recommendations.AddRange(popularProducts.Select(pp => new ProductRecommendation
            {
                ProductId = productId ?? 0,
                RecommendedProductId = pp.ProductId,
                Score = 0.3, // Default score for popularity
                RecommendedProduct = pp
            }));
        }

        var result = _mapper.Map<IEnumerable<ProductRecommendationDto>>(recommendations);
        await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(10));
        return result;
    }
}

// SearchAsync:
// از LINQ برای ساخت پویای کوئری استفاده می‌کنه.

// فیلترهای مختلف (Keyword، Brand، Vendor، Price، InStock، Date، Tags) رو به‌صورت ترکیبی اعمال می‌کنه.

// مرتب‌سازی (SortBy و SortDirection) و صفحه‌بندی (PageNumber و PageSize) رو پشتیبانی می‌کنه.

// نتایج در Redis کش می‌شن (برای 5 دقیقه) تا عملکرد بهبود پیدا کنه.

// تاریخچه جستجو برای کاربران احراز هویت‌شده یا جستجوهای با Keyword ذخیره می‌شه.

// GetSearchHistoryAsync:
// تاریخچه جستجوی کاربر رو برمی‌گردونه (برای تحلیل رفتار یا پیشنهادات).

