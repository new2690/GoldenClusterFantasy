using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers;

/// <summary>
/// Controller for user-related operations.
/// کنترلر برای عملیات مرتبط با کاربر.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Initializes a new instance of the UserController.
    /// سازنده‌ای برای ایجاد نمونه جدید از UserController.
    /// </summary>
    /// <param name="userService">User service / سرویس کاربر.</param>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Creates a new user.
    /// ایجاد کاربر جدید.
    /// </summary>
    /// <param name="dto">User creation data / داده‌های ایجاد کاربر.</param>
    /// <returns>The created user / کاربر ایجادشده.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        // Call service to create user
        // فراخوانی سرویس برای ایجاد کاربر
        var user = await _userService.CreateUserAsync(dto);
        return CreatedAtAction(nameof(GetUser), new { userId = user.UserId }, user);
    }

    /// <summary>
    /// Authenticates a user and returns a JWT token.
    /// احراز هویت کاربر و بازگشت توکن JWT.
    /// </summary>
    /// <param name="request">Email and password / ایمیل و رمز عبور.</param>
    /// <returns>JWT token / توکن JWT.</returns>
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request)
    {
        // Call service to authenticate user
        // فراخوانی سرویس برای احراز هویت کاربر
        var token = await _userService.AuthenticateAsync(request.Email, request.Password);
        return Ok(new { Token = token });
    }

    /// <summary>
    /// Gets a user by ID.
    /// دریافت کاربر با شناسه.
    /// </summary>
    /// <param name="userId">User ID / شناسه کاربر.</param>
    /// <returns>The user / کاربر.</returns>
    [Authorize]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(int userId)
    {
        // Call service to get user
        // فراخوانی سرویس برای دریافت کاربر
        var user = await _userService.GetUserByIdAsync(userId);
        return Ok(user);
    }

    /// <summary>
    /// Updates a user's role.
    /// به‌روزرسانی نقش کاربر.
    /// </summary>
    /// <param name="userId">User ID / شناسه کاربر.</param>
    /// <param name="role">New role / نقش جدید.</param>
    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPut("{userId}/role")]
    public async Task<IActionResult> UpdateUserRole(int userId, [FromBody] string role)
    {
        // Call service to update role
        // فراخوانی سرویس برای به‌روزرسانی نقش
        await _userService.UpdateUserRoleAsync(userId, role);
        return NoContent();
    }
}

/// <summary>
/// Request model for authentication.
/// مدل درخواست برای احراز هویت.
/// </summary>
public class AuthenticateRequest
{
    /// <summary>
    /// User's email.
    /// ایمیل کاربر.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User's password.
    /// رمز عبور کاربر.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}