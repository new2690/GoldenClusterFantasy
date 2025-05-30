namespace Domain.Enums;

/// <summary>
/// Defines the status of an order.
/// تعریف وضعیت یک سفارش.
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Order is pending.
    /// سفارش در انتظار است.
    /// </summary>
    Pending,

    /// <summary>
    /// Order is confirmed.
    /// سفارش تأیید شده است.
    /// </summary>
    Confirmed,

    /// <summary>
    /// Order is shipped.
    /// سفارش ارسال شده است.
    /// </summary>
    Shipped,

    /// <summary>
    /// Order is delivered.
    /// سفارش تحویل داده شده است.
    /// </summary>
    Delivered,

    /// <summary>
    /// Order is cancelled.
    /// سفارش لغو شده است.
    /// </summary>
    Cancelled
}