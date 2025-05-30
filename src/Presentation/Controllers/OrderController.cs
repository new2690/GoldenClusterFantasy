using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers;

/// <summary>
/// Controller for order-related operations.
/// کنترلر برای عملیات مرتبط با سفارش.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    /// <summary>
    /// Initializes a new instance of the OrderController.
    /// سازنده‌ای برای ایجاد نمونه جدید از OrderController.
    /// </summary>
    /// <param name="orderService">Order service / سرویس سفارش.</param>
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Creates a new order.
    /// ایجاد سفارش جدید.
    /// </summary>
    /// <param name="dto">Order creation data / داده‌های ایجاد سفارش.</param>
    /// <returns>The created order / سفارش ایجادشده.</returns>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
    {
        // Get user ID from JWT
        // دریافت شناسه کاربر از JWT
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        // Call service to create order
        // فراخوانی سرویس برای ایجاد سفارش
        var order = await _orderService.CreateOrderAsync(dto, userId);
        return CreatedAtAction(nameof(GetOrder), new { orderId = order.OrderId }, order);
    }

    /// <summary>
    /// Gets an order by ID.
    /// دریافت سفارش با شناسه.
    /// </summary>
    /// <param name="orderId">Order ID / شناسه سفارش.</param>
    /// <returns>The order / سفارش.</returns>
    [Authorize]
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder(int orderId)
    {
        // Call service to get order
        // فراخوانی سرویس برای دریافت سفارش
        var order = await _orderService.GetOrderByIdAsync(orderId);
        return Ok(order);
    }
}