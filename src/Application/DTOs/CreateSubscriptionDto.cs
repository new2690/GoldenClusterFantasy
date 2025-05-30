using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs;

/// <summary>
/// Data transfer object for creating a subscription.
/// شیء انتقال داده برای ایجاد اشتراک.
/// </summary>
public class CreateSubscriptionDto
{
    /// <summary>
    /// Identifier of the user.
    /// شناسه کاربر.
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// Subscription plan.
    /// پلن اشتراک.
    /// </summary>
    [Required]
    public SubscriptionPlan Plan { get; set; }
}