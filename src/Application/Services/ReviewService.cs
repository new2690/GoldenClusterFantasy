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
/// Service for review-related operations.
/// سرویس برای عملیات مرتبط با نظر.
/// </summary>
public class ReviewService : IReviewService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the ReviewService.
    /// سازنده‌ای برای ایجاد نمونه جدید از ReviewService.
    /// </summary>
    /// <param name="context">Database context / زمینه دیتابیس.</param>
    /// <param name="mapper">AutoMapper instance / نمونه AutoMapper.</param>
    public ReviewService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new review.
    /// ایجاد نظر جدید.
    /// </summary>
    /// <param name="dto">Review creation data / داده‌های ایجاد نظر.</param>
    /// <param name="userId">User ID / شناسه کاربر.</param>
    /// <returns>The created review / نظر ایجادشده.</returns>
    public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto dto, int userId)
    {
        // Validate product exists
        // اعتبارسنجی وجود محصول
        var product = await _context.Products.FindAsync(dto.ProductId);
        if (product == null)
        {
            throw new ArgumentException("Product not found / محصول یافت نشد.");
        }

        // Validate user exists
        // اعتبارسنجی وجود کاربر
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            throw new ArgumentException("User not found / کاربر یافت نشد.");
        }

        // Validate rating
        // اعتبارسنجی امتیاز
        if (dto.Rating < 1 || dto.Rating > 5)
        {
            throw new ArgumentException("Rating must be between 1 and 5 / امتیاز باید بین ۱ تا ۵ باشد.");
        }

        // Create review
        // ایجاد نظر
        var review = _mapper.Map<Review>(dto);
        review.UserId = userId;
        review.CreatedAt = DateTimeOffset.UtcNow;

        // Add review to database
        // افزودن نظر به دیتابیس
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        // Map entity to DTO and return
        // نگاشت موجودیت به DTO و بازگشت
        return _mapper.Map<ReviewDto>(review);
    }
}