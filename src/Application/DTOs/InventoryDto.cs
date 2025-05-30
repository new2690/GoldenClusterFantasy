namespace Application.DTOs;

/// <summary>
/// Data transfer object for inventory information.
/// شیء انتقال داده برای اطلاعات موجودی.
/// </summary>
public class InventoryDto
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
    public int Quantity { get; set; }

    /// <summary>
    /// Minimum stock threshold.
    /// آستانه حداقل موجودی.
    /// </summary>
    public int MinStockThreshold { get; set; }
}