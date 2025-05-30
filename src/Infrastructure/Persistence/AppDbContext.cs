using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

/// <summary>
/// Database context for the application.
/// زمینه دیتابیس برای برنامه.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the AppDbContext.
    /// سازنده‌ای برای ایجاد نمونه جدید از AppDbContext.
    /// </summary>
    /// <param name="options">Database context options / گزینه‌های زمینه دیتابیس.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Users table.
    /// جدول کاربران.
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Products table.
    /// جدول محصولات.
    /// </summary>
    public DbSet<Product> Products { get; set; } = null!;

    /// <summary>
    /// Orders table.
    /// جدول سفارشات.
    /// </summary>
    public DbSet<Order> Orders { get; set; } = null!;

    /// <summary>
    /// OrderItems table.
    /// جدول اقلام سفارش.
    /// </summary>
    public DbSet<OrderItem> OrderItems { get; set; } = null!;

    /// <summary>
    /// Payments table.
    /// جدول پرداخت‌ها.
    /// </summary>
    public DbSet<Payment> Payments { get; set; } = null!;

    /// <summary>
    /// Notifications table.
    /// جدول اعلانات.
    /// </summary>
    public DbSet<Notification> Notifications { get; set; } = null!;

    /// <summary>
    /// Subscriptions table.
    /// جدول اشتراک‌ها.
    /// </summary>
    public DbSet<Subscription> Subscriptions { get; set; } = null!;

    /// <summary>
    /// LoyaltyPoints table.
    /// جدول امتیازات وفاداری.
    /// </summary>
    public DbSet<LoyaltyPoint> LoyaltyPoints { get; set; } = null!;

    /// <summary>
    /// LoyaltyTransactions table.
    /// جدول تراکنش‌های امتیازات وفاداری.
    /// </summary>
    public DbSet<LoyaltyTransaction> LoyaltyTransactions { get; set; } = null!;

    /// <summary>
    /// Discounts table.
    /// جدول تخفیف‌ها.
    /// </summary>
    public DbSet<Discount> Discounts { get; set; } = null!;

    /// <summary>
    /// ChatMessages table.
    /// جدول پیام‌های چت.
    /// </summary>
    public DbSet<ChatMessage> ChatMessages { get; set; } = null!;

    /// <summary>
    /// UserActivities table.
    /// جدول فعالیت‌های کاربر.
    /// </summary>
    public DbSet<UserActivity> UserActivities { get; set; } = null!;

    /// <summary>
    /// Inventories table.
    /// جدول موجودی‌ها.
    /// </summary>
    public DbSet<Inventory> Inventories { get; set; } = null!;

    /// <summary>
    /// InventoryAlerts table.
    /// جدول هشدارهای موجودی.
    /// </summary>
    public DbSet<InventoryAlert> InventoryAlerts { get; set; } = null!;

    /// <summary>
    /// Reviews table.
    /// جدول نظرات.
    /// </summary>
    public DbSet<Review> Reviews { get; set; } = null!;

    // New DbSet for search history
    // مجموعه جدید برای تاریخچه جستجو
    public DbSet<SearchHistory> SearchHistory { get; set; }

    // New DbSets for dynamic attributes
    // مجموعه‌های جدید برای ویژگی‌های پویا
    public DbSet<Attribute> Attributes { get; set; }
    public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }

// New DbSet for recommendations
    // مجموعه جدید برای پیشنهادات
    public DbSet<ProductRecommendation> ProductRecommendations { get; set; }

    /// <summary>
    /// Configures the database schema.
    /// پیکربندی اسکیما دیتابیس.
    /// </summary>
    /// <param name="modelBuilder">Model builder / سازنده مدل.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        // پیکربندی موجودیت کاربر
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Configure OrderItem entity
        // پیکربندی موجودیت قلم سفارش
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        // Configure Discount entity
        // پیکربندی موجودیت تخفیف
        modelBuilder.Entity<Discount>()
            .HasIndex(d => d.Code)
            .IsUnique();

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Vendor)
            .WithMany()
            .HasForeignKey(p => p.VendorId);

        modelBuilder.Entity<Product>()
            .Property(p => p.Tags)
            .HasMaxLength(500); // Limit tags length / محدود کردن طول تگ‌ها

        // SearchHistory configuration
        // پیکربندی تاریخچه جستجو
        modelBuilder.Entity<SearchHistory>()
            .HasOne(sh => sh.User)
            .WithMany()
            .HasForeignKey(sh => sh.UserId)
            .IsRequired(false); // Allow anonymous searches / اجازه جستجوهای ناشناس

            // New configurations for attributes
        // پیکربندی‌های جدید برای ویژگی‌ها
        modelBuilder.Entity<Attribute>()
            .HasIndex(a => a.Name)
            .IsUnique();

        modelBuilder.Entity<ProductAttributeValue>()
            .HasOne(pav => pav.Product)
            .WithMany(p => p.AttributeValues)
            .HasForeignKey(pav => pav.ProductId);

        modelBuilder.Entity<ProductAttributeValue>()
            .HasOne(pav => pav.Attribute)
            .WithMany()
            .HasForeignKey(pav => pav.AttributeId);

            // Configure ProductAttributeValue to store Values as JSON
        // پیکربندی ProductAttributeValue برای ذخیره Values به‌صورت JSON
        modelBuilder.Entity<ProductAttributeValue>()
            .Property(pav => pav.Values)
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<IEnumerable<string>>(v, new JsonSerializerOptions()) ?? new List<string>());

        // New configuration for recommendations
        // پیکربندی جدید برای پیشنهادات
        modelBuilder.Entity<ProductRecommendation>()
            .HasOne(pr => pr.Product)
            .WithMany()
            .HasForeignKey(pr => pr.ProductId);

        modelBuilder.Entity<ProductRecommendation>()
            .HasOne(pr => pr.RecommendedProduct)
            .WithMany()
            .HasForeignKey(pr => pr.RecommendedProductId);
    }
}