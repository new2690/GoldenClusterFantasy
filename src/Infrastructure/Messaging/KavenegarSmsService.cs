using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Infrastructure.Messaging;

/// <summary>
/// Service for sending SMS using Kavenegar API.
/// سرویس برای ارسال پیامک با استفاده از API کاوه‌نگار.
/// </summary>
public class KavenegarSmsService : ISmsService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    /// <summary>
    /// Initializes a new instance of the KavenegarSmsService.
    /// سازنده‌ای برای ایجاد نمونه جدید از KavenegarSmsService.
    /// </summary>
    /// <param name="configuration">Configuration for Kavenegar settings / پیکربندی برای تنظیمات کاوه‌نگار.</param>
    /// <param name="httpClient">HTTP client for API calls / کلاینت HTTP برای فراخوانی API.</param>
    public KavenegarSmsService(IConfiguration configuration, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiKey = configuration["Kavenegar:ApiKey"];
    }

    /// <summary>
    /// Sends an SMS to the specified phone number.
    /// ارسال پیامک به شماره تلفن مشخص‌شده.
    /// </summary>
    /// <param name="phoneNumber">Recipient's phone number / شماره تلفن گیرنده.</param>
    /// <param name="message">SMS content / محتوای پیامک.</param>
    public async Task SendSmsAsync(string phoneNumber, string message)
    {
        // Prepare the API request
        // آماده‌سازی درخواست API
        var requestUrl = $"https://api.kavenegar.com/v1/{_apiKey}/sms/send.json";
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("receptor", phoneNumber),
            new KeyValuePair<string, string>("message", message)
        });

        // Send the request to Kavenegar API
        // ارسال درخواست به API کاوه‌نگار
        var response = await _httpClient.PostAsync(requestUrl, content);

        // Check if the request was successful
        // بررسی موفقیت درخواست
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Failed to send SMS: {errorContent}");
        }
    }
}