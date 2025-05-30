using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers;

/// <summary>
/// Controller for subscription-related operations.
/// کنترلر برای عملیات مرتبط با اشتراک.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    /// <summary>
    /// Initializes a new instance of the SubscriptionController.
    /// سازنده‌ای برای ایجاد نمونه جدید از SubscriptionController.
    /// </summary>
    /// <param name="subscriptionService">Subscription service / سرویس اشتراک.</param>
    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    /// <summary>
    /// Creates a new subscription.
    /// ایجاد اشتراک جدید.
    /// </summary>
    /// <param name="dto">Subscription creation data / داده‌های ایجاد اشتراک.</param>
    /// <returns>The created subscription / اشتراک ایجادشده.</returns>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionDto dto)
    {
        // Call service to create subscription
        // فراخوانی سرویس برای ایجاد اشتراک
        var subscription = await _subscriptionService.CreateSubscriptionAsync(dto);
        return CreatedAtAction(nameof(CreateSubscription), new { subscriptionId = subscription.SubscriptionId }, subscription);
    }
}