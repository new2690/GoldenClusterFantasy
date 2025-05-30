using System;

namespace Application.DTOs;

/// <summary>
/// DTO for search history records.
/// DTO برای رکوردهای تاریخچه جستجو.
/// </summary>
public class SearchHistoryDto
{
    /// <summary>
    /// Unique identifier for the search record.
    /// شناسه یکتا برای رکورد جستجو.
    /// </summary>
    public int SearchId { get; set; }

    /// <summary>
    /// ID of the user who performed the search (null for anonymous users).
    /// شناسه کاربری که جستجو را انجام داده (null برای کاربران ناشناس).
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Search query or parameters.
    /// پرس‌وجو یا پارامترهای جستجو.
    /// </summary>
    public string Query { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp when the search was performed.
    /// زمان انجام جستجو.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
}