using Domain.Enums;

namespace Application.DTOs;

/// <summary>
/// Data transfer object for payment information.
/// شیء انتقال داده برای اطلاعات پرداخت.
/// </summary>
public class PaymentDto
{
    /// <summary>
    /// Unique identifier for the payment.
    /// شناسه یکتا برای پرداخت.
    /// </summary>
    public int PaymentId { get; set; }

    /// <summary>
    /// Amount paid.
    /// مبلغ پرداخت‌شده.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Currency of the payment.
    /// ارز پرداخت.
    /// </summary>
    public string Currency { get; set; } = null!;

    /// <summary>
    /// Status of the payment.
    /// وضعیت پرداخت.
    /// </summary>
    public PaymentStatus Status { get; set; }
}