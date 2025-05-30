using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces;

/// <summary>
/// Interface for subscription-related operations.
/// رابط برای عملیات مرتبط با اشتراک.
/// </summary>
public interface ISubscriptionService
{
    /// <summary>
    /// Creates a new subscription.
    /// ایجاد یک اشتراک جدید.
    /// </summary>
    Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionDto dto);

    /// <summary>
    /// Gets a subscription by ID.
    /// دریافت اشتراک با شناسه.
    /// </summary>
    Task<SubscriptionDto> GetSubscriptionByIdAsync(int subscriptionId);
}