using System;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Represents a payment for an order.
/// نشان‌دهنده یک پرداخت برای سفارش.
/// </summary>
public class Payment
{
    /// <summary>
    /// Unique identifier for the payment.
    /// شناسه یکتا برای پرداخت.
    /// </summary>
    public int PaymentId { get; set; }

    /// <summary>
    /// Identifier of the order.
    /// شناسه سفارش.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Amount paid in IRR.
    /// مبلغ پرداخت‌شده به ریال.
    /// </summary>
    [Required]
    public decimal Amount { get; set; }

    /// <summary>
    /// Currency of the payment (e.g., IRR).
    /// ارز پرداخت (مثل ریال).
    /// </summary>
    [Required]
    public string Currency { get; set; } = null!;

    /// <summary>
    /// Status of the payment.
    /// وضعیت پرداخت.
    /// </summary>
    [Required]
    public PaymentStatus Status { get; set; }

    /// <summary>
    /// Timestamp when the payment was created.
    /// زمان ایجاد پرداخت.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when the payment was last updated.
    /// زمان آخرین به‌روزرسانی پرداخت.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>
    /// Navigation property to the order.
    /// ویژگی ناوبری به سفارش.
    /// </summary>
    public Order Order { get; set; } = null!;
}