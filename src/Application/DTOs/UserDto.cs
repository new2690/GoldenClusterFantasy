namespace Application.DTOs;

/// <summary>
/// Data transfer object for user information.
/// شیء انتقال داده برای اطلاعات کاربر.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Unique identifier for the user.
    /// شناسه یکتا برای کاربر.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Email address of the user.
    /// آدرس ایمیل کاربر.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// First name of the user.
    /// نام کاربر.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name of the user.
    /// نام خانوادگی کاربر.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Role of the user.
    /// نقش کاربر.
    /// </summary>
    public string Role { get; set; } = null!;
}