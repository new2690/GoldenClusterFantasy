using System;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

/// <summary>
/// Service for payment-related operations.
/// سرویس برای عملیات مرتبط با پرداخت.
/// </summary>
public class PaymentService : IPaymentService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the PaymentService.
    /// سازنده‌ای برای ایجاد نمونه جدید از PaymentService.
    /// </summary>
    public PaymentService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Processes a payment.
    /// پردازش یک پرداخت.
    /// </summary>
    public async Task<PaymentDto> ProcessPaymentAsync(CreatePaymentDto dto)
    {
        var order = await _context.Orders.FindAsync(dto.OrderId);
        if (order == null)
        {
            throw new KeyNotFoundException("Order not found / سفارش یافت نشد.");
        }

        var payment = new Payment
        {
            OrderId = dto.OrderId,
            Amount = dto.Amount,
            Currency = dto.Currency,
            Status = PaymentStatus.Successful, // Simplified for demo
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        order.PaymentStatus = PaymentStatus.Successful;
        order.UpdatedAt = DateTimeOffset.UtcNow;

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return _mapper.Map<PaymentDto>(payment);
    }
}