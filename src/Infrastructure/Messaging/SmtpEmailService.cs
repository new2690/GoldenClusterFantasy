using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Messaging;

/// <summary>
/// Service for sending emails using SMTP.
/// سرویس برای ارسال ایمیل با استفاده از SMTP.
/// </summary>
public class SmtpEmailService : IEmailService
{
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;

    /// <summary>
    /// Initializes a new instance of the SmtpEmailService.
    /// سازنده‌ای برای ایجاد نمونه جدید از SmtpEmailService.
    /// </summary>
    /// <param name="configuration">Configuration for SMTP settings / پیکربندی برای تنظیمات SMTP.</param>
    public SmtpEmailService(IConfiguration configuration)
    {
        // Read SMTP settings from configuration
        // خواندن تنظیمات SMTP از پیکربندی
        _smtpHost = configuration["Smtp:Host"];
        _smtpPort = int.Parse(configuration["Smtp:Port"]);
        _smtpUser = configuration["Smtp:User"];
        _smtpPass = configuration["Smtp:Pass"];
    }

    /// <summary>
    /// Sends an email to the specified recipient.
    /// ارسال ایمیل به گیرنده مشخص‌شده.
    /// </summary>
    /// <param name="toEmail">Recipient's email / ایمیل گیرنده.</param>
    /// <param name="subject">Email subject / موضوع ایمیل.</param>
    /// <param name="body">Email body / محتوای ایمیل.</param>
    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        // Create a new email message
        // ایجاد پیام ایمیل جدید
        using var message = new MailMessage
        {
            From = new MailAddress(_smtpUser, "FancyBread"),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        message.To.Add(toEmail);

        // Configure SMTP client
        // پیکربندی کلاینت SMTP
        using var client = new SmtpClient(_smtpHost, _smtpPort)
        {
            Credentials = new NetworkCredential(_smtpUser, _smtpPass),
            EnableSsl = true
        };

        // Send the email
        // ارسال ایمیل
        await client.SendMailAsync(message);
    }
}