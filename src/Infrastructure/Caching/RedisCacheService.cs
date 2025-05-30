using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Caching;

/// <summary>
/// Service for caching using Redis.
/// سرویس برای کش با استفاده از Redis.
/// </summary>
public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    /// <summary>
    /// Initializes a new instance of the RedisCacheService.
    /// سازنده‌ای برای ایجاد نمونه جدید از RedisCacheService.
    /// </summary>
    /// <param name="cache">Distributed cache instance / نمونه کش توزیع‌شده.</param>
    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    /// <summary>
    /// Gets an item from the cache.
    /// دریافت یک مورد از کش.
    /// </summary>
    /// <typeparam name="T">Type of the item / نوع مورد.</typeparam>
    /// <param name="key">Cache key / کلید کش.</param>
    /// <returns>The cached item or null / مورد کش‌شده یا نال.</returns>
    public async Task<T?> GetAsync<T>(string key)
    {
        // Retrieve the serialized data from Redis
        // دریافت داده سریال‌شده از Redis
        var data = await _cache.GetStringAsync(key);
        if (data == null)
        {
            // Return null if no data is found
            // بازگشت نال اگر داده‌ای یافت نشد
            return default;
        }

        // Deserialize the data to the specified type
        // تبدیل داده به نوع مشخص‌شده
        return JsonSerializer.Deserialize<T>(data);
    }

    /// <summary>
    /// Sets an item in the cache.
    /// تنظیم یک مورد در کش.
    /// </summary>
    /// <typeparam name="T">Type of the item / نوع مورد.</typeparam>
    /// <param name="key">Cache key / کلید کش.</param>
    /// <param name="value">Item to cache / مورد برای کش.</param>
    /// <param name="expiration">Expiration time / زمان انقضا.</param>
    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        // Serialize the value to JSON
        // سریال‌سازی مقدار به JSON
        var data = JsonSerializer.Serialize(value);

        // Set cache options with expiration
        // تنظیم گزینه‌های کش با زمان انقضا
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromHours(1)
        };

        // Store the serialized data in Redis
        // ذخیره داده سریال‌شده در Redis
        await _cache.SetStringAsync(key, data, options);
    }
}