namespace Domain.Enums;

/// <summary>
/// Defines the type of a user activity.
/// تعریف نوع یک فعالیت کاربر.
/// </summary>
public enum ActivityType
{
    /// <summary>
    /// User login.
    /// ورود کاربر.
    /// </summary>
    Login,

    /// <summary>
    /// User purchase.
    /// خرید کاربر.
    /// </summary>
    Purchase,

    /// <summary>
    /// User review.
    /// نظر کاربر.
    /// </summary>
    Review
}