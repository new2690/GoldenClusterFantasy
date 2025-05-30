using System.Threading.Tasks;

namespace Infrastructure.Messaging;

/// <summary>
/// Interface for email sending operations.
/// رابط برای عملیات ارسال ایمیل.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email to the specified recipient.
    /// ارسال ایمیل به گیرنده مشخص‌شده.
    /// </summary>
    /// <param name="toEmail">Recipient's email / ایمیل گیرنده.</param>
    /// <param name="subject">Email subject / موضوع ایمیل.</param>
    /// <param name="body">Email body / محتوای ایمیل.</param>
    Task SendEmailAsync(string toEmail, string subject, string body);
}