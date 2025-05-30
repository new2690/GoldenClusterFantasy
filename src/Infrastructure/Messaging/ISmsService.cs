using System.Threading.Tasks;

namespace Infrastructure.Messaging;

/// <summary>
/// Interface for SMS sending operations.
/// رابط برای عملیات ارسال پیامک.
/// </summary>
public interface ISmsService
{
    /// <summary>
    /// Sends an SMS to the specified phone number.
    /// ارسال پیامک به شماره تلفن مشخص‌شده.
    /// </summary>
    /// <param name="phoneNumber">Recipient's phone number / شماره تلفن گیرنده.</param>
    /// <param name="message">SMS content / محتوای پیامک.</param>
    Task SendSmsAsync(string phoneNumber, string message);
}