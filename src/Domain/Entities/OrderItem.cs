using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Represents an item in an order.
/// نشان‌دهنده یک قلم در سفارش.
/// </summary>
public class OrderItem
{
    /// <summary>
    /// Unique identifier for the order item.
    /// شناسه یکتا برای قلم سفارش.
    /// </summary>
    public int OrderItemId { get; set; }

    /// <summary>
    /// Identifier of the order.
    /// شناسه سفارش.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Identifier of the product.
    /// شناسه محصول.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Quantity of the product ordered.
    /// تعداد محصول سفارش‌داده‌شده.
    /// </summary>
    [Required]
    public int Quantity { get; set; }

    /// <summary>
    /// Price per unit at the time of order.
    /// قیمت هر واحد در زمان سفارش.
    /// </summary>
    [Required]
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Navigation property to the order.
    /// ویژگی ناوبری به سفارش.
    /// </summary>
    public Order Order { get; set; } = null!;

    /// <summary>
    /// Navigation property to the product.
    /// ویژگی ناوبری به محصول.
    /// </summary>
    public Product Product { get; set; } = null!;
}