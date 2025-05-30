using System;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

/// <summary>
/// Service for subscription-related operations.
/// سرویس برای عملیات مرتبط با اشتراک.
/// </summary>
public class SubscriptionService : ISubscriptionService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the SubscriptionService.
    /// سازنده‌ای برای ایجاد نمونه جدید از SubscriptionService.
    /// </summary>
    public SubscriptionService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new subscription.
    /// ایجاد یک اشتراک جدید.
    /// </summary>
    public async Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionDto dto)
    {
        var subscription = _mapper.Map<Subscription>(dto);
        subscription.Status = "Active";
        subscription.StartDate = DateTimeOffset.UtcNow;
        subscription.NextDeliveryDate = subscription.Plan == Domain.Enums.SubscriptionPlan.Weekly
            ? DateTimeOffset.UtcNow.AddDays(7)
            : DateTimeOffset.UtcNow.AddMonths(1);
        subscription.CreatedAt = DateTimeOffset.UtcNow;
        subscription.UpdatedAt = DateTimeOffset.UtcNow;

        _context.Subscriptions.Add(subscription);
        await _context.SaveChangesAsync();

        return _mapper.Map<SubscriptionDto>(subscription);
    }

    /// <summary>
    /// Gets a subscription by ID.
    /// دریافت اشتراک با شناسه.
    /// </summary>
    public async Task<SubscriptionDto> GetSubscriptionByIdAsync(int subscriptionId)
    {
        var subscription = await _context.Subscriptions.FindAsync(subscriptionId);
        if (subscription == null)
        {
            throw new KeyNotFoundException("Subscription not found / اشتراک یافت نشد.");
        }
        return _mapper.Map<SubscriptionDto>(subscription);
    }
}