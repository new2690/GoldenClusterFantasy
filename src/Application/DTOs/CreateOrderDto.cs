using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

/// <summary>
/// Data transfer object for creating an order.
/// شیء انتقال داده برای ایجاد سفارش.
/// </summary>
public class CreateOrderDto
{
    /// <summary>
    /// List of order items.
    /// لیست اقلام سفارش.
    /// </summary>
    [Required]
    public List<CreateOrderItemDto> Items { get; set; } = new();
}

/// <summary>
/// Data transfer object for an order item.
/// شیء انتقال داده برای یک قلم سفارش.
/// </summary>
public class CreateOrderItemDto
{
    /// <summary>
    /// Identifier of the product.
    /// شناسه محصول.
    /// </summary>
    [Required]
    public int ProductId { get; set; }

    /// <summary>
    /// Quantity of the product.
    /// تعداد محصول.
    /// </summary>
    [Required]
    public int Quantity { get; set; }
}