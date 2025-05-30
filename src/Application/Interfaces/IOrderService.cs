using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces;

/// <summary>
/// Interface for order-related operations.
/// رابط برای عملیات مرتبط با سفارش.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Creates a new order.
    /// ایجاد یک سفارش جدید.
    /// </summary>
    Task<OrderDto> CreateOrderAsync(CreateOrderDto dto, int userId);

    /// <summary>
    /// Gets an order by ID.
    /// دریافت سفارش با شناسه.
    /// </summary>
    Task<OrderDto> GetOrderByIdAsync(int orderId);
}