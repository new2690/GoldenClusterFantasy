using Domain.Enums;

namespace Application.DTOs;

/// <summary>
/// Data transfer object for order information.
/// شیء انتقال داده برای اطلاعات سفارش.
/// </summary>
public class OrderDto
{
    /// <summary>
    /// Unique identifier for the order.
    /// شناسه یکتا برای سفارش.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Total price of the order.
    /// قیمت کل سفارش.
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Status of the payment.
    /// وضعیت پرداخت.
    /// </summary>
    public PaymentStatus PaymentStatus { get; set; }
}