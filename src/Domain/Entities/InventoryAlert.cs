using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Represents an alert for low stock levels.
/// نشان‌دهنده یک هشدار برای سطوح پایین موجودی.
/// </summary>
public class InventoryAlert
{
    /// <summary>
    /// Unique identifier for the alert.
    /// شناسه یکتا برای هشدار.
    /// </summary>
    public int AlertId { get; set; }

    /// <summary>
    /// Identifier of the product.
    /// شناسه محصول.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Current stock level at the time of alert.
    /// سطح موجودی فعلی در زمان هشدار.
    /// </summary>
    [Required]
    public int CurrentStock { get; set; }

    /// <summary>
    /// Alert message.
    /// پیام هشدار.
    /// </summary>
    [Required]
    public string Message { get; set; } = null!;

    /// <summary>
    /// Timestamp when the alert was created.
    /// زمان ایجاد هشدار.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Navigation property to the product.
    /// ویژگی ناوبری به محصول.
    /// </summary>
    public Product Product { get; set; } = null!;
}