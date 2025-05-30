using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces;

/// <summary>
/// Interface for chat-related operations.
/// رابط برای عملیات مرتبط با چت.
/// </summary>
public interface IChatService
{
    /// <summary>
    /// Sends a chat message.
    /// ارسال یک پیام چت.
    /// </summary>
    Task<ChatMessageDto> SendMessageAsync(int senderId, SendChatMessageDto dto);
}