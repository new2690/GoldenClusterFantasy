using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Presentation.Hubs;

/// <summary>
/// SignalR Hub for real-time chat functionality.
/// هاب SignalR برای قابلیت چت بلادرنگ.
/// </summary>
[Authorize]
public class ChatHub : Hub
{
    private readonly IChatService _chatService;

    /// <summary>
    /// Initializes a new instance of the ChatHub.
    /// سازنده‌ای برای ایجاد نمونه جدید از ChatHub.
    /// </summary>
    /// <param name="chatService">Chat service / سرویس چت.</param>
    public ChatHub(IChatService chatService)
    {
        _chatService = chatService;
    }

    /// <summary>
    /// Sends a message to a specific user or group.
    /// ارسال پیام به یک کاربر خاص یا گروه.
    /// </summary>
    /// <param name="dto">Message data / داده‌های پیام.</param>
    public async Task SendMessage(SendChatMessageDto dto)
    {
        // Get sender ID from JWT
        // دریافت شناسه فرستنده از JWT
        var senderId = int.Parse(Context.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        // Save message using ChatService
        // ذخیره پیام با استفاده از ChatService
        var message = await _chatService.SendMessageAsync(senderId, dto);

        // Broadcast message to receiver or group
        // پخش پیام به گیرنده یا گروه
        if (dto.ReceiverId.HasValue)
        {
            await Clients.User(dto.ReceiverId.Value.ToString()).SendAsync("ReceiveMessage", message);
        }
        else if (!string.IsNullOrEmpty(dto.GroupName))
        {
            await Clients.Group(dto.GroupName).SendAsync("ReceiveMessage", message);
        }
    }

    /// <summary>
    /// Adds the user to a group.
    /// افزودن کاربر به یک گروه.
    /// </summary>
    /// <param name="groupName">Name of the group / نام گروه.</param>
    public async Task JoinGroup(string groupName)
    {
        // Add user to group
        // افزودن کاربر به گروه
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        // Notify group of new member
        // اطلاع‌رسانی به گروه درباره عضو جدید
        await Clients.Group(groupName).SendAsync("GroupNotification", $"{Context.User!.Identity!.Name} joined the group / {Context.User!.Identity!.Name} به گروه پیوست.");
    }

    /// <summary>
    /// Removes the user from a group.
    /// حذف کاربر از یک گروه.
    /// </summary>
    /// <param name="groupName">Name of the group / نام گروه.</param>
    public async Task LeaveGroup(string groupName)
    {
        // Remove user from group
        // حذف کاربر از گروه
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

        // Notify group of departure
        // اطلاع‌رسانی به گروه درباره خروج
        await Clients.Group(groupName).SendAsync("GroupNotification", $"{Context.User!.Identity!.Name} left the group / {Context.User!.Identity!.Name} از گروه خارج شد.");
    }
}