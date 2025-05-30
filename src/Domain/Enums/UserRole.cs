namespace Domain.Enums;

/// <summary>
/// Defines the roles a user can have.
/// تعریف نقش‌هایی که یک کاربر می‌تواند داشته باشد.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Customer role.
    /// نقش مشتری.
    /// </summary>
    Customer,

    /// <summary>
    /// Vendor role.
    /// نقش فروشنده.
    /// </summary>
    Vendor,

    /// <summary>
    /// Admin role.
    /// نقش مدیر.
    /// </summary>
    Admin
}