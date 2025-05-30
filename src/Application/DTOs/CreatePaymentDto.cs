using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

/// <summary>
/// Data transfer object for creating a payment.
/// شیء انتقال داده برای ایجاد پرداخت.
/// </summary>
public class CreatePaymentDto
{
    /// <summary>
    /// Identifier of the order.
    /// شناسه سفارش.
    /// </summary>
    [Required]
    public int OrderId { get; set; }

    /// <summary>
    /// Amount to be paid.
    /// مبلغ برای پرداخت.
    /// </summary>
    [Required]
    public decimal Amount { get; set; }

    /// <summary>
    /// Currency of the payment.
    /// ارز پرداخت.
    /// </summary>
    [Required]
    public string Currency { get; set; } = null!;
}