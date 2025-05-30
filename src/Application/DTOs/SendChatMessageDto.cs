using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

/// <summary>
/// Data transfer object for sending a chat message.
/// شیء انتقال داده برای ارسال پیام چت.
/// </summary>
public class SendChatMessageDto
{
    /// <summary>
    /// Identifier of the receiver (optional).
    /// شناسه گیرنده (اختیاری).
    /// </summary>
    public int? ReceiverId { get; set; }

    /// <summary>
    /// Name of the group (optional).
    /// نام گروه (اختیاری).
    /// </summary>
    public string? GroupName { get; set; }

    /// <summary>
    /// Content of the message.
    /// محتوای پیام.
    /// </summary>
    [Required]
    public string Content { get; set; } = null!;
}