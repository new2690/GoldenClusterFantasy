using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces;

/// <summary>
/// Interface for loyalty-related operations.
/// رابط برای عملیات مرتبط با وفاداری.
/// </summary>
public interface ILoyaltyService
{
    /// <summary>
    /// Redeems loyalty points.
    /// بازخرید امتیازات وفاداری.
    /// </summary>
    Task<LoyaltyPointDto> RedeemPointsAsync(RedeemPointsDto dto);

    /// <summary>
    /// Gets loyalty points for a user.
    /// دریافت امتیازات وفاداری برای کاربر.
    /// </summary>
    Task<LoyaltyPointDto> GetLoyaltyPointsAsync(int userId);
}