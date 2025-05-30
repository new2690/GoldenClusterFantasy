using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

/// <summary>
/// Data transfer object for creating a user.
/// شیء انتقال داده برای ایجاد کاربر.
/// </summary>
public class CreateUserDto
{
    /// <summary>
    /// Email address of the user.
    /// آدرس ایمیل کاربر.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    /// <summary>
    /// Password for the user.
    /// رمز عبور کاربر.
    /// </summary>
    [Required]
    [MinLength(6)]
    public string Password { get; set; } = null!;

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
    [Required]
    public string Role { get; set; } = null!;
}