using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces;

/// <summary>
/// Interface for notification-related operations.
/// رابط برای عملیات مرتبط با اعلان.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Sends a notification.
    /// ارسال یک اعلان.
    /// </summary>
    Task<NotificationDto> SendNotificationAsync(CreateNotificationDto dto);
}