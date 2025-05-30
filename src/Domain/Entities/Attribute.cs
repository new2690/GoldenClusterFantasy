namespace Domain.Entities;

/// <summary>
/// Represents a dynamic attribute for products (e.g., Weight, BowlCount).
/// نشان‌دهنده یک ویژگی پویا برای محصولات (مثل وزن، تعداد کاسه).
/// </summary>
public class Attribute
{
    /// <summary>
    /// Unique identifier for the attribute.
    /// شناسه یکتا برای ویژگی.
    /// </summary>
    public int AttributeId { get; set; }

    /// <summary>
    /// Name of the attribute (e.g., "Weight", "BowlCount").
    /// نام ویژگی (مثل "وزن"، "تعداد کاسه").
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Data type of the attribute (e.g., "Decimal", "Integer", "String").
    /// نوع داده ویژگی (مثل "اعشاری"، "عدد صحیح"، "رشته").
    /// </summary>
    public string DataType { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the attribute can be used in search filters.
    /// نشان‌دهنده این است که آیا ویژگی در فیلترهای جستجو قابل استفاده است.
    /// </summary>
    public bool IsFilterable { get; set; } = true;

    /// <summary>
    /// Timestamp when the attribute was created.
    /// زمان ایجاد ویژگی.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when the attribute was last updated.
    /// زمان آخرین به‌روزرسانی ویژگی.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }
}