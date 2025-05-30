using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Represents a chat message between users or in a group.
/// نشان‌دهنده یک پیام چت بین کاربران یا در گروه.
/// </summary>
public class ChatMessage
{
    /// <summary>
    /// Unique identifier for the message.
    /// شناسه یکتا برای پیام.
    /// </summary>
    public int MessageId { get; set; }

    /// <summary>
    /// Identifier of the sender user.
    /// شناسه کاربر فرستنده.
    /// </summary>
    public int SenderId { get; set; }

    /// <summary>
    /// Identifier of the receiver user (optional).
    /// شناسه کاربر گیرنده (اختیاری).
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

    /// <summary>
    /// Timestamp when the message was created.
    /// زمان ایجاد پیام.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Navigation property to the sender.
    /// ویژگی ناوبری به فرستنده.
    /// </summary>
    public User Sender { get; set; } = null!;

    /// <summary>
    /// Navigation property to the receiver.
    /// ویژگی ناوبری به گیرنده.
    /// </summary>
    public User? Receiver { get; set; }
}