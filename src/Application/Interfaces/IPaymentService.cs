using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces;

/// <summary>
/// Interface for payment-related operations.
/// رابط برای عملیات مرتبط با پرداخت.
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Processes a payment.
    /// پردازش یک پرداخت.
    /// </summary>
    Task<PaymentDto> ProcessPaymentAsync(CreatePaymentDto dto);
}