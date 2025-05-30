using Domain.Enums;

namespace Application.DTOs;

/// <summary>
/// Data transfer object for subscription information.
/// شیء انتقال داده برای اطلاعات اشتراک.
/// </summary>
public class SubscriptionDto
{
    /// <summary>
    /// Unique identifier for the subscription.
    /// شناسه یکتا برای اشتراک.
    /// </summary>
    public int SubscriptionId { get; set; }

    /// <summary>
    /// Subscription plan.
    /// پلن اشتراک.
    /// </summary>
    public SubscriptionPlan Plan { get; set; }

    /// <summary>
    /// Status of the subscription.
    /// وضعیت اشتراک.
    /// </summary>
    public string Status { get; set; } = null!;
}