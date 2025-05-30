using System;
using System.Collections.Generic;

namespace Application.DTOs;

/// <summary>
/// DTO for advanced search queries.
/// DTO برای پرس‌وجوهای جستجوی پیشرفته.
/// </summary>
public class SearchQueryDto
{
    /// <summary>
    /// Search term for product name, description, or tags (e.g., "bagel" or "gluten-free").
    /// عبارت جستجو برای نام محصول، توضیحات یا تگ‌ها (مثل "بیگل" یا "بدون گلوتن").
    /// </summary>
    public string? Keyword { get; set; }

    /// <summary>
    /// Filter by brand name (e.g., "Soroush").
    /// فیلتر بر اساس نام برند (مثل "سروش").
    /// </summary>
    public string? Brand { get; set; }

    /// <summary>
    /// Filter by vendor name or ID (e.g., "Soroush" or vendor ID 1).
    /// فیلتر بر اساس نام یا شناسه فروشنده (مثل "سروش" یا شناسه 1).
    /// </summary>
    public string? Vendor { get; set; }

    /// <summary>
    /// Minimum price for filtering (inclusive).
    /// حداقل قیمت برای فیلتر (شامل).
    /// </summary>
    public decimal? MinPrice { get; set; }

    /// <summary>
    /// Maximum price for filtering (inclusive).
    /// حداکثر قیمت برای فیلتر (شامل).
    /// </summary>
    public decimal? MaxPrice { get; set; }

    /// <summary>
    /// Filter by product availability (true for in-stock only).
    /// فیلتر بر اساس موجود بودن محصول (true برای محصولات موجود).
    /// </summary>
    public bool? InStock { get; set; }

    /// <summary>
    /// Start date for filtering orders or products (e.g., created after this date).
    /// تاریخ شروع برای فیلتر سفارشات یا محصولات (مثل ایجاد شده پس از این تاریخ).
    /// </summary>
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    /// End date for filtering orders or products (e.g., created before this date).
    /// تاریخ پایان برای فیلتر سفارشات یا محصولات (مثل ایجاد شده پیش از این تاریخ).
    /// </summary>
    public DateTimeOffset? EndDate { get; set; }

    /// <summary>
    /// List of specific tags to filter (e.g., ["vegan", "organic"]).
    /// لیست تگ‌های خاص برای فیلتر (مثل ["وگان", "ارگانیک"]).
    /// </summary>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Sort field (e.g., "Price", "Name", "CreatedAt").
    /// فیلد مرتب‌سازی (مثل "Price", "Name", "CreatedAt").
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Sort direction ("asc" for ascending, "desc" for descending).
    /// جهت مرتب‌سازی ("asc" برای صعودی، "desc" برای نزولی).
    /// </summary>
    public string? SortDirection { get; set; } = "asc";

    /// <summary>
    /// Page number for pagination (starting from 1).
    /// شماره صفحه برای صفحه‌بندی (شروع از 1).
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Number of items per page.
    /// تعداد آیتم‌ها در هر صفحه.
    /// </summary>
    public int PageSize { get; set; } = 10;

       // New property for dynamic attribute filters
    // ویژگی جدید برای فیلترهای ویژگی پویا
    public Dictionary<int, string>? AttributeFilters { get; set; } // Key: AttributeId, Value: Desired value (e.g., {1: "500"} for Weight=500)

    /// <summary>
    /// Enable fuzzy search for keyword, brand, and tags (default: false).
    /// فعال کردن جستجوی فازی برای کلیدواژه، برند و تگ‌ها (پیش‌فرض: false).
    /// </summary>
    public bool EnableFuzzySearch { get; set; } = false;

    /// <summary>
    /// Maximum Levenshtein distance for fuzzy search (default: 2).
    /// حداکثر فاصله Levenshtein برای جستجوی فازی (پیش‌فرض: 2).
    /// </summary>
    public int MaxFuzzyDistance { get; set; } = 2;

}