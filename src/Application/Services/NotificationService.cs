using System;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Messaging;
using Infrastructure.Persistence;

namespace Application.Services;

/// <summary>
/// Service for notification-related operations.
/// سرویس برای عملیات مرتبط با اعلان.
/// </summary>
public class NotificationService : INotificationService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ISmsService _smsService;
    private readonly IMessageQueueService _messageQueueService;

    /// <summary>
    /// Initializes a new instance of the NotificationService.
    /// سازنده‌ای برای ایجاد نمونه جدید از NotificationService.
    /// </summary>
    public NotificationService(
        AppDbContext context,
        IMapper mapper,
        IEmailService emailService,
        ISmsService smsService,
        IMessageQueueService messageQueueService)
    {
        _context = context;
        _mapper = mapper;
        _emailService = emailService;
        _smsService = smsService;
        _messageQueueService = messageQueueService;
    }

    /// <summary>
    /// Sends a notification.
    /// ارسال یک اعلان.
    /// </summary>
    public async Task<NotificationDto> SendNotificationAsync(CreateNotificationDto dto)
    {
        var notification = _mapper.Map<Notification>(dto);
        notification.Status = "Pending";
        notification.CreatedAt = DateTimeOffset.UtcNow;
        notification.UpdatedAt = DateTimeOffset.UtcNow;

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();

        // Queue notification for processing
        await _messageQueueService.PublishAsync("notification_queue", _mapper.Map<NotificationDto>(notification));

        // Simplified: Process immediately for demo
        if (dto.Type == Domain.Enums.NotificationType.Email)
        {
            await _emailService.SendEmailAsync(dto.UserId, "Notification", dto.Content);
        }
        else if (dto.Type == Domain.Enums.NotificationType.SMS)
        {
            await _smsService.SendSmsAsync(dto.UserId, dto.Content);
        }

        notification.Status = "Sent";
        notification.UpdatedAt = DateTimeOffset.UtcNow;
        await _context.SaveChangesAsync();

        return _mapper.Map<NotificationDto>(notification);
    }
}