using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers;

/// <summary>
/// Controller for loyalty points-related operations.
/// کنترلر برای عملیات مرتبط با امتیازات وفاداری.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class LoyaltyController : ControllerBase
{
    private readonly ILoyaltyService _loyaltyService;

    /// <summary>
    /// Initializes a new instance of the LoyaltyController.
    /// سازنده‌ای برای ایجاد نمونه جدید از LoyaltyController.
    /// </summary>
    /// <param name="loyaltyService">Loyalty service / سرویس وفاداری.</param>
    public LoyaltyController(ILoyaltyService loyaltyService)
    {
        _loyaltyService = loyaltyService;
    }

    /// <summary>
    /// Redeems loyalty points for a user.
    /// مصرف امتیازات وفاداری برای کاربر.
    /// </summary>
    /// <param name="dto">Redemption data / داده‌های مصرف.</param>
    /// <returns>The updated loyalty points / امتیازات وفاداری به‌روزشده.</returns>
    [Authorize]
    [HttpPost("redeem")]
    public async Task<IActionResult> RedeemPoints([FromBody] RedeemPointsDto dto)
    {
        // Call service to redeem points
        // فراخوانی سرویس برای مصرف امتیازات
        var points = await _loyaltyService.RedeemPointsAsync(dto);
        return Ok(points);
    }
}