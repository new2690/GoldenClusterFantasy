namespace Domain.Entities;

/// <summary>
/// Represents the value of a dynamic attribute for a specific product.
/// نشان‌دهنده مقدار یک ویژگی پویا برای یک محصول خاص.
/// </summary>
public class ProductAttributeValue
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
    /// JSON array of values (e.g., ["Chocolate", "Vanilla"] for flavors).
    /// آرایه JSON از مقادیر (مثل ["شکلاتی"، "وانیلی"] برای طعم‌ها).
    /// </summary>
    public IEnumerable<string> Values { get; set; } = new List<string>(); // Changed to JSON array / تغییر به آرایه JSON


    /// <summary>
    /// Timestamp when the value was created.
    /// زمان ایجاد مقدار.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when the value was last updated.
    /// زمان آخرین به‌روزرسانی مقدار.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }

    // Navigation properties
    // ویژگی‌های ناوبری
    public Product? Product { get; set; }
    public Attribute? Attribute { get; set; }
}