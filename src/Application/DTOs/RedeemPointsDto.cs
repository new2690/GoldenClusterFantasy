using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

/// <summary>
/// Data transfer object for redeeming loyalty points.
/// شیء انتقال داده برای بازخرید امتیازات وفاداری.
/// </summary>
public class RedeemPointsDto
{
    /// <summary>
    /// Identifier of the user.
    /// شناسه کاربر.
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// Number of points to redeem.
    /// تعداد امتیازات برای بازخرید.
    /// </summary>
    [Required]
    public int Points { get; set; }
}