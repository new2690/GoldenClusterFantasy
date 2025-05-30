using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Represents a product (e.g., bread, pastry).
/// نشان‌دهنده یک محصول (مثل نان، شیرینی).
/// </summary>
public class Product
{
    /// <summary>
    /// Unique identifier for the product.
    /// شناسه یکتا برای محصول.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Identifier of the vendor who owns the product.
    /// شناسه فروشنده مالک محصول.
    /// </summary>
    public int VendorId { get; set; }

    /// <summary>
    /// Name of the product.
    /// نام محصول.
    /// </summary>
    [Required]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Description of the product.
    /// توضیحات محصول.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Price of the product in IRR.
    /// قیمت محصول به ریال.
    /// </summary>
    [Required]
    public decimal Price { get; set; }

    /// <summary>
    /// Timestamp when the product was created.
    /// زمان ایجاد محصول.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when the product was last updated.
    /// زمان آخرین به‌روزرسانی محصول.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }

// New properties for advanced search
    // ویژگی‌های جدید برای سرچ پیشرفته
    public string? Brand { get; set; } // Brand of the product / برند محصول
    public string? Tags { get; set; } // Comma-separated tags (e.g., "gluten-free,vegan") / تگ‌های جدا شده با کاما

    // Navigation properties
    /// Navigation property to the vendor.
    // ویژگی‌های ناوبری
    public Vendor? Vendor { get; set; }
    public Inventory? Inventory { get; set; }
    public List<ProductAttributeValue> AttributeValues { get; set; } = new List<ProductAttributeValue>(); // Added for dynamic attributes / اضافه شده برای ویژگی‌های پویا

}