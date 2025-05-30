using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Application.Services;

/// <summary>
/// Service for loyalty points-related operations.
/// سرویس برای عملیات مرتبط با امتیازات وفاداری.
/// </summary>
public class LoyaltyService : ILoyaltyService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the LoyaltyService.
    /// سازنده‌ای برای ایجاد نمونه جدید از LoyaltyService.
    /// </summary>
    /// <param name="context">Database context / زمینه دیتابیس.</param>
    /// <param name="mapper">AutoMapper instance / نمونه AutoMapper.</param>
    public LoyaltyService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Redeems loyalty points for a user.
    /// مصرف امتیازات وفاداری برای کاربر.
    /// </summary>
    /// <param name="dto">Redemption data / داده‌های مصرف.</param>
    /// <returns>The updated loyalty points / امتیازات وفاداری به‌روزشده.</returns>
    public async Task<LoyaltyPointDto> RedeemPointsAsync(RedeemPointsDto dto)
    {
        // Find loyalty points for user
        // یافتن امتیازات وفاداری برای کاربر
        var loyaltyPoints = await _context.LoyaltyPoints.FindAsync(dto.UserId);
        if (loyaltyPoints == null)
        {
            throw new ArgumentException("No loyalty points found / هیچ امتیاز وفاداری یافت نشد.");
        }

        // Validate sufficient points
        // اعتبارسنجی کافی بودن امتیازات
        if (loyaltyPoints.TotalPoints < dto.Points)
        {
            throw new ArgumentException("Insufficient points / امتیازات ناکافی.");
        }

        // Update points
        // به‌روزرسانی امتیازات
        loyaltyPoints.TotalPoints -= dto.Points;
        loyaltyPoints.UpdatedAt = DateTimeOffset.UtcNow;

        // Create transaction
        // ایجاد تراکنش
        var transaction = new LoyaltyTransaction
        {
            UserId = dto.UserId,
            Points = -dto.Points,
            Type = "Spent",
            CreatedAt = DateTimeOffset.UtcNow
        };
        _context.LoyaltyTransactions.Add(transaction);

        // Save changes
        // ذخیره تغییرات
        await _context.SaveChangesAsync();

        // Map entity to DTO and return
        // نگاشت موجودیت به DTO و بازگشت
        return _mapper.Map<LoyaltyPointDto>(loyaltyPoints);
    }
}