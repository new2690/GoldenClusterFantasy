using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces;

/// <summary>
/// Interface for user-related operations.
/// رابط برای عملیات مرتبط با کاربر.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Creates a new user.
    /// ایجاد یک کاربر جدید.
    /// </summary>
    Task<UserDto> CreateUserAsync(CreateUserDto dto);

    /// <summary>
    /// Authenticates a user and returns a JWT token.
    /// احراز هویت کاربر و بازگشت توکن JWT.
    /// </summary>
    Task<string> AuthenticateAsync(string email, string password);

    /// <summary>
    /// Gets a user by ID.
    /// دریافت کاربر با شناسه.
    /// </summary>
    Task<UserDto> GetUserByIdAsync(int userId);
}