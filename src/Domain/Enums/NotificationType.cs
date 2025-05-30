namespace Domain.Enums;

/// <summary>
/// Defines the type of a notification.
/// تعریف نوع یک اعلان.
/// </summary>
public enum NotificationType
{
    /// <summary>
    /// Email notification.
    /// اعلان ایمیلی.
    /// </summary>
    Email,

    /// <summary>
    /// SMS notification.
    /// اعلان پیامکی.
    /// </summary>
    SMS,

    /// <summary>
    /// Push notification.
    /// اعلان پوش.
    /// </summary>
    Push
}