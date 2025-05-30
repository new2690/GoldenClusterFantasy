using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers;

/// <summary>
/// Controller for payment-related operations.
/// کنترلر برای عملیات مرتبط با پرداخت.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    /// <summary>
    /// Initializes a new instance of the PaymentController.
    /// سازنده‌ای برای ایجاد نمونه جدید از PaymentController.
    /// </summary>
    /// <param name="paymentService">Payment service / سرویس پرداخت.</param>
    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    /// <summary>
    /// Initiates a payment for an order.
    /// شروع پرداخت برای یک سفارش.
    /// </summary>
    /// <param name="dto">Payment creation data / داده‌های ایجاد پرداخت.</param>
    /// <returns>The created payment / پرداخت ایجادشده.</returns>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> InitiatePayment([FromBody] CreatePaymentDto dto)
    {
        // Call service to initiate payment
        // فراخوانی سرویس برای شروع پرداخت
        var payment = await _paymentService.InitiatePaymentAsync(dto);
        return CreatedAtAction(nameof(VerifyPayment), new { paymentId = payment.PaymentId }, payment);
    }

    /// <summary>
    /// Verifies a payment.
    /// تأیید پرداخت.
    /// </summary>
    /// <param name="paymentId">Payment ID / شناسه پرداخت.</param>
    /// <param name="transactionId">Transaction ID / شناسه تراکنش.</param>
    /// <returns>The verified payment / پرداخت تأییدشده.</returns>
    [Authorize]
    [HttpPost("{paymentId}/verify")]
    public async Task<IActionResult> VerifyPayment(int paymentId, [FromBody] string transactionId)
    {
        // Call service to verify payment
        // فراخوانی سرویس برای تأیید پرداخت
        var payment = await _paymentService.VerifyPaymentAsync(paymentId, transactionId);
        return Ok(payment);
    }
}