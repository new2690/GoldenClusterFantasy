namespace Application.DTOs;

/// <summary>
/// DTO for product attribute values.
/// DTO برای مقادیر ویژگی‌های محصول.
/// </summary>
public class ProductAttributeValueDto
{
    /// <summary>
    /// Unique identifier for the product attribute value.
    /// شناسه یکتا برای مقدار ویژگی محصول.
    /// </summary>
    public int ProductAttributeValueId { get; set; }

    /// <summary>
    /// ID of the product.
    /// شناسه محصول.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// ID of the attribute.
    /// شناسه ویژگی.
    /// </summary>
    public int AttributeId { get; set; }

    /// <summary>
    /// List of values (e.g., ["Chocolate", "Vanilla"] for flavors).
    /// لیست مقادیر (مثل ["شکلاتی"، "وانیلی"] برای طعم‌ها).
    /// </summary>
    public IEnumerable<string> Values { get; set; } = new List<string>();

    /// <summary>
    /// Attribute details.
    /// جزئیات ویژگی.
    /// </summary>
    public AttributeDto? Attribute { get; set; }
}