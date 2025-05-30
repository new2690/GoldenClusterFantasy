using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Represents the inventory of a product.
/// نشان‌دهنده موجودی یک محصول.
/// </summary>
public class Inventory
{
    /// <summary>
    /// Identifier of the product.
    /// شناسه محصول.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Current stock quantity.
    /// مقدار موجودی فعلی.
    /// </summary>
    [Required]
    public int Quantity { get; set; }

    /// <summary>
    /// Minimum stock threshold for alerts.
    /// آستانه حداقل موجودی برای هشدارها.
    /// </summary>
    [Required]
    public int MinStockThreshold { get; set; }

    /// <summary>
    /// Timestamp when the inventory was last updated.
    /// زمان آخرین به‌روزرسانی موجودی.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>
    /// Navigation property to the product.
    /// ویژگی ناوبری به محصول.
    /// </summary>
    public Product Product { get; set; } = null!;
}