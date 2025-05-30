using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using System;
using System.Threading.Tasks;

namespace Application.Services;

/// <summary>
/// Service for chat-related operations.
/// سرویس برای عملیات مرتبط با چت.
/// </summary>
public class ChatService : IChatService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the ChatService.
    /// سازنده‌ای برای ایجاد نمونه جدید از ChatService.
    /// </summary>
    /// <param name="context">Database context / زمینه دیتابیس.</param>
    /// <param name="mapper">AutoMapper instance / نمونه AutoMapper.</param>
    public ChatService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Sends a chat message.
    /// ارسال پیام چت.
    /// </summary>
    /// <param name="senderId">Sender ID / شناسه فرستنده.</param>
    /// <param name="dto">Message data / داده‌های پیام.</param>
    /// <returns>The sent message / پیام ارسال‌شده.</returns>
    public async Task<ChatMessageDto> SendMessageAsync(int senderId, SendChatMessageDto dto)
    {
        // Validate sender exists
        // اعتبارسنجی وجود فرستنده
        var sender = await _context.Users.FindAsync(senderId);
        if (sender == null)
        {
            throw new ArgumentException("Sender not found / فرستنده یافت نشد.");
        }

        // Validate receiver if specified
        // اعتبارسنجی گیرنده اگر مشخص شده باشد
        if (dto.ReceiverId.HasValue)
        {
            var receiver = await _context.Users.FindAsync(dto.ReceiverId.Value);
            if (receiver == null)
            {
                throw new ArgumentException("Receiver not found / گیرنده یافت نشد.");
            }
        }

        // Create message
        // ایجاد پیام
        var message = _mapper.Map<ChatMessage>(dto);
        message.SenderId = senderId;
        message.CreatedAt = DateTimeOffset.UtcNow;

        // Add message to database
        // افزودن پیام به دیتابیس
        _context.ChatMessages.Add(message);
        await _context.SaveChangesAsync();

        // Map entity to DTO and return
        // نگاشت موجودیت به DTO و بازگشت
        return _mapper.Map<ChatMessageDto>(message);
    }
}