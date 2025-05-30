using System;
using System.Threading.Tasks;

namespace Infrastructure.Caching;

/// <summary>
/// Interface for caching operations.
/// رابط برای عملیات کش.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Gets an item from the cache.
    /// دریافت یک مورد از کش.
    /// </summary>
    /// <typeparam name="T">Type of the item / نوع مورد.</typeparam>
    /// <param name="key">Cache key / کلید کش.</param>
    /// <returns>The cached item or null / مورد کش‌شده یا نال.</returns>
    Task<T?> GetAsync<T>(string key);

    /// <summary>
    /// Sets an item in the cache.
    /// تنظیم یک مورد در کش.
    /// </summary>
    /// <typeparam name="T">Type of the item / نوع مورد.</typeparam>
    /// <param name="key">Cache key / کلید کش.</param>
    /// <param name="value">Item to cache / مورد برای کش.</param>
    /// <param name="expiration">Expiration time / زمان انقضا.</param>
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
}