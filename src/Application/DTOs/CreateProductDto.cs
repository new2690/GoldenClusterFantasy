using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

/// <summary>
/// Data transfer object for creating a product.
/// شیء انتقال داده برای ایجاد محصول.
/// </summary>
public class CreateProductDto
{
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
    /// Price of the product.
    /// قیمت محصول.
    /// </summary>
    [Required]
    public decimal Price { get; set; }

    /// <summary>
    /// Quantity in stock.
    /// مقدار موجودی.
    /// </summary>
    [Required]
    public int Quantity { get; set; }

    /// <summary>
    /// Minimum stock threshold for alerts.
    /// آستانه حداقل موجودی برای هشدارها.
    /// </summary>
    [Required]
    public int MinStockThreshold { get; set; }
}