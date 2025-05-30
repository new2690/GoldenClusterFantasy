-- Database schema for FancyBread online store
-- طرح دیتابیس برای فروشگاه آنلاین نان فانتزی و شیرینی FancyBread

-- Drop existing tables if they exist (for clean setup)
-- حذف جداول موجود در صورت وجود (برای راه‌اندازی تمیز)
IF OBJECT_ID('ProductAttributeValues') IS NOT NULL DROP TABLE ProductAttributeValues;
IF OBJECT_ID('Attributes') IS NOT NULL DROP TABLE Attributes;
IF OBJECT_ID('SearchHistory') IS NOT NULL DROP TABLE SearchHistory;
IF OBJECT_ID('Reviews') IS NOT NULL DROP TABLE Reviews;
IF OBJECT_ID('InventoryAlerts') IS NOT NULL DROP TABLE InventoryAlerts;
IF OBJECT_ID('Inventories') IS NOT NULL DROP TABLE Inventories;
IF OBJECT_ID('ChatMessages') IS NOT NULL DROP TABLE ChatMessages;
IF OBJECT_ID('LoyaltyTransactions') IS NOT NULL DROP TABLE LoyaltyTransactions;
IF OBJECT_ID('LoyaltyPoints') IS NOT NULL DROP TABLE LoyaltyPoints;
IF OBJECT_ID('Subscriptions') IS NOT NULL DROP TABLE Subscriptions;
IF OBJECT_ID('Notifications') IS NOT NULL DROP TABLE Notifications;
IF OBJECT_ID('Payments') IS NOT NULL DROP TABLE Payments;
IF OBJECT_ID('OrderItems') IS NOT NULL DROP TABLE OrderItems;
IF OBJECT_ID('Orders') IS NOT NULL DROP TABLE Orders;
IF OBJECT_ID('Products') IS NOT NULL DROP TABLE Products;
IF OBJECT_ID('Vendors') IS NOT NULL DROP TABLE Vendors;
IF OBJECT_ID('Users') IS NOT NULL DROP TABLE Users;

-- Create Users table
-- ایجاد جدول کاربران
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the user / شناسه یکتا برای کاربر
    Email NVARCHAR(255) NOT NULL, -- User's email address (unique) / آدرس ایمیل کاربر (یکتا)
    PasswordHash NVARCHAR(255) NOT NULL, -- Hashed password for security / رمز عبور هش‌شده برای امنیت
    FirstName NVARCHAR(100) NULL, -- User's first name / نام کاربر
    LastName NVARCHAR(100) NULL, -- User's last name / نام خانوادگی کاربر
    Role NVARCHAR(50) NOT NULL, -- User role (e.g., Customer, Admin, Vendor) / نقش کاربر (مثل مشتری، مدیر، فروشنده)
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the user was created / زمان ایجاد کاربر
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the user was last updated / زمان آخرین به‌روزرسانی کاربر
    CONSTRAINT UQ_Users_Email UNIQUE (Email) -- Ensure email is unique / اطمینان از یکتایی ایمیل
);

-- Create Vendors table
-- ایجاد جدول فروشندگان
CREATE TABLE Vendors (
    VendorId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the vendor / شناسه یکتا برای فروشنده
    VendorName NVARCHAR(100) NOT NULL, -- Name of the vendor / نام فروشنده
    ContactInfo NVARCHAR(500) NULL, -- Contact information (e.g., phone, email) / اطلاعات تماس (مثل تلفن، ایمیل)
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the vendor was created / زمان ایجاد فروشنده
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the vendor was last updated / زمان آخرین به‌روزرسانی فروشنده
    CONSTRAINT UQ_Vendors_VendorName UNIQUE (VendorName) -- Ensure vendor name is unique / اطمینان از یکتایی نام فروشنده
);

-- Create Products table
-- ایجاد جدول محصولات
CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the product / شناسه یکتا برای محصول
    Name NVARCHAR(100) NOT NULL, -- Name of the product (e.g., Bagel, Halim) / نام محصول (مثل بیگل، حلیم)
    Description NVARCHAR(1000) NULL, -- Description of the product / توضیحات محصول
    Price DECIMAL(18,2) NOT NULL, -- Price of the product / قیمت محصول
    VendorId INT NOT NULL, -- ID of the vendor who sells this product / شناسه فروشنده محصول
    Brand NVARCHAR(100) NULL, -- Brand of the product (e.g., Soroush) / برند محصول (مثل سروش)
    Tags NVARCHAR(500) NULL, -- Comma-separated tags (e.g., gluten-free, vegan) / تگ‌های جدا شده با کاما (مثل بدون گلوتن، وگان)
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the product was created / زمان ایجاد محصول
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the product was last updated / زمان آخرین به‌روزرسانی محصول
    FOREIGN KEY (VendorId) REFERENCES Vendors(VendorId), -- Foreign key to Vendors table / کلید خارجی به جدول فروشندگان
    CONSTRAINT CHK_Products_Price CHECK (Price >= 0) -- Ensure price is non-negative / اطمینان از غیرمنفی بودن قیمت
);

-- Create Orders table
-- ایجاد جدول سفارشات
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the order / شناسه یکتا برای سفارش
    UserId INT NOT NULL, -- ID of the user who placed the order / شناسه کاربری که سفارش را ثبت کرده
    TotalPrice DECIMAL(18,2) NOT NULL, -- Total price of the order / قیمت کل سفارش
    Status NVARCHAR(50) NOT NULL, -- Order status (e.g., Pending, Completed) / وضعیت سفارش (مثل در انتظار، تکمیل شده)
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the order was created / زمان ایجاد سفارش
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the order was last updated / زمان آخرین به‌روزرسانی سفارش
    FOREIGN KEY (UserId) REFERENCES Users(UserId), -- Foreign key to Users table / کلید خارجی به جدول کاربران
    CONSTRAINT CHK_Orders_TotalPrice CHECK (TotalPrice >= 0) -- Ensure total price is non-negative / اطمینان از غیرمنفی بودن قیمت کل
);

-- Create OrderItems table
-- ایجاد جدول آیتم‌های سفارش
CREATE TABLE OrderItems (
    OrderItemId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the order item / شناسه یکتا برای آیتم سفارش
    OrderId INT NOT NULL, -- ID of the order / شناسه سفارش
    ProductId INT NOT NULL, -- ID of the product / شناسه محصول
    Quantity INT NOT NULL, -- Quantity of the product ordered / تعداد محصول سفارش‌شده
    UnitPrice DECIMAL(18,2) NOT NULL, -- Price per unit at the time of order / قیمت واحد در زمان سفارش
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the order item was created / زمان ایجاد آیتم سفارش
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId), -- Foreign key to Orders table / کلید خارجی به جدول سفارشات
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId), -- Foreign key to Products table / کلید خارجی به جدول محصولات
    CONSTRAINT CHK_OrderItems_Quantity CHECK (Quantity > 0), -- Ensure quantity is positive / اطمینان از مثبت بودن تعداد
    CONSTRAINT CHK_OrderItems_UnitPrice CHECK (UnitPrice >= 0) -- Ensure unit price is non-negative / اطمینان از غیرمنفی بودن قیمت واحد
);

-- Create Payments table
-- ایجاد جدول پرداخت‌ها
CREATE TABLE Payments (
    PaymentId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the payment / شناسه یکتا برای پرداخت
    OrderId INT NOT NULL, -- ID of the order associated with the payment / شناسه سفارش مرتبط با پرداخت
    Amount DECIMAL(18,2) NOT NULL, -- Payment amount / مبلغ پرداخت
    Status NVARCHAR(50) NOT NULL, -- Payment status (e.g., Pending, Completed) / وضعیت پرداخت (مثل در انتظار، تکمیل شده)
    PaymentMethod NVARCHAR(50) NOT NULL, -- Payment method (e.g., CreditCard, Online) / روش پرداخت (مثل کارت اعتباری، آنلاین)
    TransactionId NVARCHAR(100) NULL, -- Transaction ID from payment gateway / شناسه تراکنش از درگاه پرداخت
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the payment was created / زمان ایجاد پرداخت
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the payment was last updated / زمان آخرین به‌روزرسانی پرداخت
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId), -- Foreign key to Orders table / کلید خارجی به جدول سفارشات
    CONSTRAINT CHK_Payments_Amount CHECK (Amount >= 0) -- Ensure amount is non-negative / اطمینان از غیرمنفی بودن مبلغ
);

-- Create Notifications table
-- ایجاد جدول اعلان‌ها
CREATE TABLE Notifications (
    NotificationId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the notification / شناسه یکتا برای اعلان
    UserId INT NOT NULL, -- ID of the user receiving the notification / شناسه کاربری که اعلان را دریافت می‌کند
    Message NVARCHAR(1000) NOT NULL, -- Notification message / پیام اعلان
    Type NVARCHAR(50) NOT NULL, -- Notification type (e.g., Email, SMS) / نوع اعلان (مثل ایمیل، پیامک)
    Status NVARCHAR(50) NOT NULL, -- Notification status (e.g., Sent, Pending) / وضعیت اعلان (مثل ارسال شده، در انتظار)
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the notification was created / زمان ایجاد اعلان
    FOREIGN KEY (UserId) REFERENCES Users(UserId) -- Foreign key to Users table / کلید خارجی به جدول کاربران
);

-- Create Subscriptions table
-- ایجاد جدول اشتراک‌ها
CREATE TABLE Subscriptions (
    SubscriptionId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the subscription / شناسه یکتا برای اشتراک
    UserId INT NOT NULL, -- ID of the user subscribed / شناسه کاربری که اشتراک دارد
    Plan NVARCHAR(100) NOT NULL, -- Subscription plan (e.g., Monthly, Yearly) / طرح اشتراک (مثل ماهانه، سالانه)
    Status NVARCHAR(50) NOT NULL, -- Subscription status (e.g., Active, Cancelled) / وضعیت اشتراک (مثل فعال، لغو شده)
    StartDate DATETIMEOFFSET NOT NULL, -- Start date of the subscription / تاریخ شروع اشتراک
    EndDate DATETIMEOFFSET NULL, -- End date of the subscription / تاریخ پایان اشتراک
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the subscription was created / زمان ایجاد اشتراک
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the subscription was last updated / زمان آخرین به‌روزرسانی اشتراک
    FOREIGN KEY (UserId) REFERENCES Users(UserId) -- Foreign key to Users table / کلید خارجی به جدول کاربران
);

-- Create LoyaltyPoints table
-- ایجاد جدول امتیازات وفاداری
CREATE TABLE LoyaltyPoints (
    LoyaltyPointId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the loyalty point / شناسه یکتا برای امتیاز وفاداری
    UserId INT NOT NULL, -- ID of the user earning points / شناسه کاربری که امتیاز کسب می‌کند
    Points INT NOT NULL, -- Number of loyalty points / تعداد امتیازات وفاداری
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the points were created / زمان ایجاد امتیازات
    FOREIGN KEY (UserId) REFERENCES Users(UserId), -- Foreign key to Users table / کلید خارجی به جدول کاربران
    CONSTRAINT CHK_LoyaltyPoints_Points CHECK (Points >= 0) -- Ensure points are non-negative / اطمینان از غیرمنفی بودن امتیازات
);

-- Create LoyaltyTransactions table
-- ایجاد جدول تراکنش‌های وفاداری
CREATE TABLE LoyaltyTransactions (
    LoyaltyTransactionId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the loyalty transaction / شناسه یکتا برای تراکنش وفاداری
    UserId INT NOT NULL, -- ID of the user / شناسه کاربر
    Points INT NOT NULL, -- Number of points added or subtracted / تعداد امتیازات اضافه یا کسر شده
    TransactionType NVARCHAR(50) NOT NULL, -- Type of transaction (e.g., Earned, Redeemed) / نوع تراکنش (مثل کسب شده، استفاده شده)
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the transaction was created / زمان ایجاد تراکنش
    FOREIGN KEY (UserId) REFERENCES Users(UserId), -- Foreign key to Users table / کلید خارجی به جدول کاربران
    CONSTRAINT CHK_LoyaltyTransactions_Points CHECK (Points != 0) -- Ensure points are non-zero / اطمینان از غیرصفر بودن امتیازات
);

-- Create ChatMessages table
-- ایجاد جدول پیام‌های چت
CREATE TABLE ChatMessages (
    ChatMessageId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the chat message / شناسه یکتا برای پیام چت
    SenderId INT NOT NULL, -- ID of the user sending the message / شناسه کاربری که پیام را ارسال می‌کند
    ReceiverId INT NULL, -- ID of the user receiving the message (null for group messages) / شناسه کاربری که پیام را دریافت می‌کند (null برای پیام‌های گروهی)
    GroupName NVARCHAR(100) NULL, -- Name of the group (null for direct messages) / نام گروه (null برای پیام‌های مستقیم)
    Message NVARCHAR(1000) NOT NULL, -- Message content / محتوای پیام
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the message was created / زمان ایجاد پیام
    FOREIGN KEY (SenderId) REFERENCES Users(UserId), -- Foreign key to Users table (sender) / کلید خارجی به جدول کاربران (فرستنده)
    FOREIGN KEY (ReceiverId) REFERENCES Users(UserId) -- Foreign key to Users table (receiver) / کلید خارجی به جدول کاربران (گیرنده)
);

-- Create Inventories table
-- ایجاد جدول موجودی‌ها
CREATE TABLE Inventories (
    InventoryId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the inventory / شناسه یکتا برای موجودی
    ProductId INT NOT NULL, -- ID of the product / شناسه محصول
    Quantity INT NOT NULL, -- Available quantity / مقدار موجود
    MinStockThreshold INT NOT NULL, -- Minimum stock threshold for alerts / حداقل آستانه موجودی برای هشدارها
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the inventory was created / زمان ایجاد موجودی
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the inventory was last updated / زمان آخرین به‌روزرسانی موجودی
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId), -- Foreign key to Products table / کلید خارجی به جدول محصولات
    CONSTRAINT CHK_Inventories_Quantity CHECK (Quantity >= 0), -- Ensure quantity is non-negative / اطمینان از غیرمنفی بودن مقدار
    CONSTRAINT CHK_Inventories_MinStockThreshold CHECK (MinStockThreshold >= 0) -- Ensure threshold is non-negative / اطمینان از غیرمنفی بودن آستانه
);

-- Create InventoryAlerts table
-- ایجاد جدول هشدارهای موجودی
CREATE TABLE InventoryAlerts (
    InventoryAlertId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the inventory alert / شناسه یکتا برای هشدار موجودی
    InventoryId INT NOT NULL, -- ID of the inventory / شناسه موجودی
    Message NVARCHAR(1000) NOT NULL, -- Alert message (e.g., Low stock warning) / پیام هشدار (مثل هشدار کمبود موجودی)
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the alert was created / زمان ایجاد هشدار
    FOREIGN KEY (InventoryId) REFERENCES Inventories(InventoryId) -- Foreign key to Inventories table / کلید خارجی به جدول موجودی‌ها
);

-- Create Reviews table
-- ایجاد جدول نظرات
CREATE TABLE Reviews (
    ReviewId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the review / شناسه یکتا برای نظر
    ProductId INT NOT NULL, -- ID of the product being reviewed / شناسه محصولی که نظر برای آن است
    UserId INT NOT NULL, -- ID of the user who wrote the review / شناسه کاربری که نظر را نوشته
    Rating INT NOT NULL, -- Rating (1 to 5) / امتیاز (1 تا 5)
    Comment NVARCHAR(1000) NULL, -- Review comment / نظر کاربر
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the review was created / زمان ایجاد نظر
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the review was last updated / زمان آخرین به‌روزرسانی نظر
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId), -- Foreign key to Products table / کلید خارجی به جدول محصولات
    FOREIGN KEY (UserId) REFERENCES Users(UserId), -- Foreign key to Users table / کلید خارجی به جدول کاربران
    CONSTRAINT CHK_Reviews_Rating CHECK (Rating BETWEEN 1 AND 5) -- Ensure rating is between 1 and 5 / اطمینان از اینکه امتیاز بین 1 تا 5 است
);

-- Create SearchHistory table
-- ایجاد جدول تاریخچه جستجو
CREATE TABLE SearchHistory (
    SearchId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the search record / شناسه یکتا برای رکورد جستجو
    UserId INT NULL, -- ID of the user who performed the search (null for anonymous users) / شناسه کاربری که جستجو را انجام داده (null برای کاربران ناشناس)
    Query NVARCHAR(1000) NOT NULL, -- Search query or parameters / پرس‌وجو یا پارامترهای جستجو
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the search was performed / زمان انجام جستجو
    FOREIGN KEY (UserId) REFERENCES Users(UserId) -- Foreign key to Users table / کلید خارجی به جدول کاربران
);

-- Create Attributes table
-- ایجاد جدول ویژگی‌ها (برای ویژگی‌های پویا مثل وزن یا تعداد کاسه)
CREATE TABLE Attributes (
    AttributeId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the attribute / شناسه یکتا برای ویژگی
    Name NVARCHAR(100) NOT NULL, -- Name of the attribute (e.g., Weight, BowlCount) / نام ویژگی (مثل وزن، تعداد کاسه)
    DataType NVARCHAR(50) NOT NULL, -- Data type of the attribute (e.g., Decimal, Integer, String) / نوع داده ویژگی (مثل اعشاری، عدد صحیح، رشته)
    IsFilterable BIT NOT NULL DEFAULT 1, -- Whether this attribute can be used in search filters / آیا این ویژگی در فیلترهای جستجو قابل استفاده است
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the attribute was created / زمان ایجاد ویژگی
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the attribute was last updated / زمان آخرین به‌روزرسانی ویژگی
    CONSTRAINT UQ_Attributes_Name UNIQUE (Name) -- Ensure attribute name is unique / اطمینان از یکتایی نام ویژگی
);

-- Create ProductAttributeValues table
-- ایجاد جدول مقادیر ویژگی محصولات (برای ذخیره مقادیر ویژگی‌های پویا)
CREATE TABLE ProductAttributeValues (
    ProductAttributeValueId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the product attribute value / شناسه یکتا برای مقدار ویژگی محصول
    ProductId INT NOT NULL, -- ID of the product / شناسه محصول
    AttributeId INT NOT NULL, -- ID of the attribute / شناسه ویژگی
    Value NVARCHAR(500) NOT NULL, -- Value of the attribute (e.g., "500" for weight, "2" for bowl count) / مقدار ویژگی (مثل "500" برای وزن، "2" برای تعداد کاسه)
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the value was created / زمان ایجاد مقدار
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), -- Timestamp when the value was last updated / زمان آخرین به‌روزرسانی مقدار
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId), -- Foreign key to Products table / کلید خارجی به جدول محصولات
    FOREIGN KEY (AttributeId) REFERENCES Attributes(AttributeId), -- Foreign key to Attributes table / کلید خارجی به جدول ویژگی‌ها
    CONSTRAINT UQ_ProductAttributeValues_ProductId_AttributeId UNIQUE (ProductId, AttributeId) -- Ensure unique attribute per product / اطمینان از یکتایی ویژگی برای هر محصول
);

-- Create indexes for performance optimization
-- ایجاد ایندکس‌ها برای بهینه‌سازی عملکرد
CREATE INDEX IX_Users_Email ON Users(Email); -- Index for fast email lookups / ایندکس برای جستجوی سریع ایمیل
CREATE INDEX IX_Products_Name ON Products(Name); -- Index for fast product name searches / ایندکس برای جستجوی سریع نام محصول
CREATE INDEX IX_Products_Brand ON Products(Brand); -- Index for fast brand searches / ایندکس برای جستجوی سریع برند
CREATE INDEX IX_Products_Tags ON Products(Tags); -- Index for fast tag searches / ایندکس برای جستجوی سریع تگ‌ها
CREATE INDEX IX_Products_Price ON Products(Price); -- Index for fast price filtering / ایندکس برای فیلتر سریع قیمت
CREATE INDEX IX_Orders_UserId ON Orders(UserId); -- Index for fast order lookups by user / ایندکس برای جستجوی سریع سفارشات بر اساس کاربر
CREATE INDEX IX_OrderItems_OrderId ON OrderItems(OrderId); -- Index for fast order item lookups / ایندکس برای جستجوی سریع آیتم‌های سفارش
CREATE INDEX IX_Payments_OrderId ON Payments(OrderId); -- Index for fast payment lookups / ایندکس برای جستجوی سریع پرداخت‌ها
CREATE INDEX IX_ProductAttributeValues_ProductId ON ProductAttributeValues(ProductId); -- Index for fast attribute value lookups by product / ایندکس برای جستجوی سریع مقادیر ویژگی بر اساس محصول
CREATE INDEX IX_ProductAttributeValues_AttributeId ON ProductAttributeValues(AttributeId); -- Index for fast attribute value lookups by attribute / ایندکس برای جستجوی سریع مقادیر ویژگی بر اساس ویژگی
CREATE INDEX IX_SearchHistory_UserId ON SearchHistory(UserId); -- Index for fast search history lookups / ایندکس برای جستجوی سریع تاریخچه جستجو

-- Alter ProductAttributeValues to support multiple values as JSON
-- تغییر جدول مقادیر ویژگی محصولات برای پشتیبانی از مقادیر چندگانه به‌صورت JSON
ALTER TABLE ProductAttributeValues
    DROP COLUMN Value;
ALTER TABLE ProductAttributeValues
    ADD Values NVARCHAR(MAX) NOT NULL; -- JSON array (e.g., ["Chocolate", "Vanilla"]) / آرایه JSON (مثل ["شکلاتی"، "وانیلی"])

-- Create Levenshtein Distance function for fuzzy search
-- ایجاد تابع فاصله Levenshtein برای جستجوی فازی
CREATE FUNCTION dbo.Levenshtein(@s1 NVARCHAR(500), @s2 NVARCHAR(500))
RETURNS INT
AS
BEGIN
    DECLARE @s1_len INT, @s2_len INT, @s1_len = LEN(@s1), s2_len = LEN(@s2);
    DECLARE @d TABLE (i INT, j INT, d INT);
    DECLARE i INT, @j INT = 0;

    IF @s1_len = 0 RETURN s2_len;
    IF @d2_len = s2 RETURN NULL_len;

    WHILE @i <= s1_len
    BEGIN
        SET @d = 0;
        WHILE j <= @s2_len
        BEGIN
            INSERT @d (i, j, d) VALUES (@i, @j, @i);
            SET @j = j + 1;
        END
        SET i = @i + 1;
    END

    SET @i = 1;
    WHILE i <= @s1_len
    BEGIN
        SET @j = 1;
        WHILE j <= @s2_len
        BEGIN
            DECLARE @cost INT = CASE WHEN SUBSTRING(@s1, i, 1) = SUBSTRING(@s2, j, 1) THEN 0 ELSE 1 END;
            DECLARE min1 INT, @min2 INT, @min3 INT;
            SELECT min1 INTO d FROM @d WHERE i = (@i-1) AND j = @j-1;
            SELECT min2 INTO @d FROM @d WHERE i = (@i-1 AND j = @j;
            SELECT min3 INTO @d FROM @d WHERE i = i AND j = (@j-1);
            UPDATE @d SET d = (SELECT MIN(min1 + @cost, min2 + 1, min3 + 1) FROM FROM @d WHERE i = @i AND j = @j;
            SET @j = j + @1;
        END
        SET i = @i + 1;
    END

    DECLARE result INT;
    SELECT result = d FROM @d WHERE i = s1_len AND j = @s2_len;
    RETURN @result, result;
END;

-- Create ProductRecommendations table for storing precomputed recommendations
-- ایجاد جدول پیشنهادات محصولات برای ذخیره پیشنهادات از پیش محاسبه‌شده
CREATE TABLE ProductRecommendations (
    ProductRecommendationId INT PRIMARY KEY IDENTITY(1,1),
    ProductId INT NOT NULL,
    RecommendedProductId INT NOT NULL,
    Score FLOAT NOT NULL, -- Recommendation score (0 to 1) / امتیاز پیشنهاد (0 تا 1)
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    FOREIGN KEY (RecommendedProductId) REFERENCES Products(ProductId)
);