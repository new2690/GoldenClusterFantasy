namespace Application.DTOs;

/// <summary>
/// Data transfer object for product information.
/// شیء انتقال داده برای اطلاعات محصول.
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Unique identifier for the product.
    /// شناسه یکتا برای محصول.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Name of the product.
    /// نام محصول.
    /// </summary>
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
    public decimal Price { get; set; }

        public int VendorId { get; set; }
    public string? Brand { get; set; }
    public string? Tags { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // New property for dynamic attributes
    // ویژگی جدید برای ویژگی‌های پویا
    public List<ProductAttributeValueDto> AttributeValues { get; set; } = new List<ProductAttributeValueDto>();

}