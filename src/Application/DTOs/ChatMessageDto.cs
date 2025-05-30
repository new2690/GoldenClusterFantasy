namespace Application.DTOs;

/// <summary>
/// Data transfer object for chat message information.
/// شیء انتقال داده برای اطلاعات پیام چت.
/// </summary>
public class ChatMessageDto
{
    /// <summary>
    /// Unique identifier for the message.
    /// شناسه یکتا برای پیام.
    /// </summary>
    public int MessageId { get; set; }

    /// <summary>
    /// Identifier of the sender.
    /// شناسه فرستنده.
    /// </summary>
    public int SenderId { get; set; }

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
    public string Content { get; set; } = null!;
}