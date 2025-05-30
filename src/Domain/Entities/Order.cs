using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Represents a customer order.
/// نشان‌دهنده یک سفارش مشتری.
/// </summary>
public class Order
{
    /// <summary>
    /// Unique identifier for the order.
    /// شناسه یکتا برای سفارش.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Identifier of the user who placed the order.
    /// شناسه کاربری که سفارش داده.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Total price of the order in IRR.
    /// قیمت کل سفارش به ریال.
    /// </summary>
    [Required]
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Status of the payment.
    /// وضعیت پرداخت.
    /// </summary>
    [Required]
    public PaymentStatus PaymentStatus { get; set; }

    /// <summary>
    /// Timestamp when the order was created.
    /// زمان ایجاد سفارش.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when the order was last updated.
    /// زمان آخرین به‌روزرسانی سفارش.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>
    /// Navigation property to the user.
    /// ویژگی ناوبری به کاربر.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Collection of order items.
    /// مجموعه اقلام سفارش.
    /// </summary>
    public List<OrderItem> OrderItems { get; set; } = new();
}