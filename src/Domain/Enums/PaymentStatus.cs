namespace Domain.Enums;

/// <summary>
/// Defines the status of a payment.
/// تعریف وضعیت یک پرداخت.
/// </summary>
public enum PaymentStatus
{
    /// <summary>
    /// Payment is pending.
    /// پرداخت در انتظار است.
    /// </summary>
    Pending,

    /// <summary>
    /// Payment is successful.
    /// پرداخت موفق است.
    /// </summary>
    Successful,

    /// <summary>
    /// Payment has failed.
    /// پرداخت ناموفق است.
    /// </summary>
    Failed
}