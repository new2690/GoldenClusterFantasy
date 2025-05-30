namespace Application.DTOs;

/// <summary>
/// Data transfer object for loyalty points information.
/// شیء انتقال داده برای اطلاعات امتیازات وفاداری.
/// </summary>
public class LoyaltyPointDto
{
    /// <summary>
    /// Identifier of the user.
    /// شناسه کاربر.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Total loyalty points.
    /// کل امتیازات وفاداری.
    /// </summary>
    public int TotalPoints { get; set; }
}