# GoldenClusterFantasy
مستندات جامع پروژه نان فانتزی خوشه طلایی (فروش انواع نان فانتزی و شیرینی)
مستندات کامل پروژه GoldenClusterFantasy
پروژه GoldenClusterFantasy یک سیستم بک‌اند پیشرفته برای یک فروشگاه آنلاین فروش نان فانتزی، شیرینی، و محصولات متنوع (مثل حلیم) است که با استفاده از Clean Architecture، .NET 9.0.5، و تکنولوژی‌های مدرن طراحی شده است. این پروژه با هدف ارائه یک پلتفرم مقیاس‌پذیر، انعطاف‌پذیر، و قابل نگهداری توسعه یافته و شامل قابلیت‌های پیشرفته‌ای مثل سرچ پیشرفته با ویژگی‌های پویا، چت بلادرنگ، مدیریت موجودی، پرداخت آنلاین، اعلانات (ایمیل و پیامک)، و تست‌های واحد است. این مستندات تمام لایه‌ها، کلاس‌ها، قابلیت‌ها، تکنولوژی‌ها، و ساختارهای پروژه را به‌صورت کامل، منظم، و با جزئیات شرح می‌دهد.
1. نمای کلی پروژه
1.1 هدف پروژه
هدف پروژه، ایجاد یک پلتفرم فروش آنلاین با تمرکز بر نان فانتزی و شیرینی است که:
امکان جستجوی پیشرفته با فیلترهای ترکیبی (برند، قیمت، فروشنده، ویژگی‌های پویا مثل وزن یا تعداد کاسه) را فراهم می‌کند.

از محصولات متنوع (مثل حلیم با ویژگی‌های خاص) پشتیبانی می‌کند.

قابلیت‌هایی مثل احراز هویت، چت بلادرنگ، مدیریت سفارشات، پرداخت، و اعلانات را ارائه می‌دهد.

برای گسترش در آینده (اضافه کردن محصولات جدید یا ویژگی‌ها) آماده است.

1.2 معماری پروژه
پروژه از Clean Architecture استفاده می‌کند که شامل چهار لایه اصلی است:
Domain: شامل موجودیت‌ها و منطق اصلی کسب‌وکار.

Application: شامل منطق برنامه، سرویس‌ها، و DTOها.

Infrastructure: شامل پیاده‌سازی‌های زیرساختی مثل دیتابیس، کش، و سرویس‌های خارجی.

Presentation: شامل APIها، SignalR Hub، و میدلورها.

1.3 تکنولوژی‌ها
تکنولوژی

نسخه

توضیحات

.NET

9.0.5

فریم‌ورک اصلی برای توسعه بک‌اند

Entity Framework Core

9.0.0

برای دسترسی به دیتابیس SQL Server

AutoMapper

13.0.1

برای نگاشت موجودیت‌ها به DTOها

StackExchange.Redis

2.8.16

برای کش با Redis

RabbitMQ.Client

6.8.1

برای صف پیام

Kavenegar.Core

1.2.5

برای ارسال پیامک

Microsoft.AspNetCore.SignalR

1.1.0

برای چت بلادرنگ

Swashbuckle.AspNetCore

6.9.0

برای مستندسازی API با Swagger

xUnit

2.9.2

برای تست‌های واحد

Moq

4.20.70

برای شبیه‌سازی در تست‌ها

Microsoft.IdentityModel.Tokens

8.1.2

برای احراز هویت JWT

BCrypt.Net-Next

4.0.3

برای هش کردن رمز عبور

1.4 ساختار پروژه

GoldenClusterFantasy/
├── src/
│   ├── Domain/
│   │   └── Domain.csproj
│   ├── Application/
│   │   └── Application.csproj
│   ├── Infrastructure/
│   │   └── Infrastructure.csproj
│   ├── Presentation/
│   │   └── Presentation.csproj
├── tests/
│   ├── Application.Tests/
│   │   └── Application.Tests.csproj
├── GoldenClusterFantasy.sln

2. لایه‌ها و جزئیات
2.1 لایه Domain
لایه Domain هسته پروژه است و شامل موجودیت‌ها و منطق کسب‌وکار است. این لایه هیچ وابستگی به سایر لایه‌ها ندارد.
2.1.1 کلاس‌ها و توضیحات
کلاس

توضیحات

User

نشان‌دهنده کاربر سیستم (مشتری، فروشنده، مدیر). شامل ویژگی‌هایی مثل 
UserId
، 
Email
، 
PasswordHash
، 
Role
 (Customer, Vendor, Admin, SuperAdmin).

Product

نشان‌دهنده محصول (مثل نان، شیرینی، حلیم). شامل 
ProductId
، 
Name
، 
Price
، 
VendorId
، 
Brand
، 
Tags
، و 
AttributeValues
 برای ویژگی‌های پویا (مثل وزن).

Vendor

نشان‌دهنده فروشنده. شامل 
VendorId
، 
VendorName
، و ارتباط با محصولات.

Order

نشان‌دهنده سفارش. شامل 
OrderId
، 
UserId
، 
TotalPrice
، و لیست 
OrderItems
.

OrderItem

آیتم‌های سفارش. شامل 
OrderItemId
، 
ProductId
، 
Quantity
، و 
UnitPrice
.

Payment

پرداخت. شامل 
PaymentId
، 
OrderId
، 
Amount
، و 
Status
 (Pending, Completed, Failed).

Notification

اعلان (ایمیل یا پیامک). شامل 
NotificationId
، 
UserId
، 
Message
، و 
Type
 (Email, SMS).

Subscription

اشتراک کاربر. شامل 
SubscriptionId
، 
UserId
، و 
EndDate
.

LoyaltyPoint

امتیاز وفاداری کاربر. شامل 
LoyaltyPointId
، 
UserId
، و 
Points
.

LoyaltyTransaction

تراکنش امتیاز وفاداری. شامل 
LoyaltyTransactionId
، 
LoyaltyPointId
، و 
Amount
.

ChatMessage

پیام چت بلادرنگ. شامل 
ChatMessageId
، 
SenderId
، 
ReceiverId
، و 
Content
.

UserActivity

فعالیت کاربر (مثل ورود یا خرید). شامل 
UserActivityId
، 
UserId
، و 
ActivityType
.

Inventory

موجودی محصول. شامل 
ProductId
، 
Quantity
، و 
MinStockThreshold
.

InventoryAlert

هشدار کمبود موجودی. شامل 
InventoryAlertId
، 
ProductId
، و 
AlertDate
.

Review

نظر کاربر درباره محصول. شامل 
ReviewId
، 
ProductId
، 
UserId
، و 
Rating
.

SearchHistory

تاریخچه جستجوی کاربر. شامل 
SearchId
، 
UserId
، 
Query
، و 
CreatedAt
.

Attribute

ویژگی پویا (مثل وزن). شامل 
AttributeId
، 
Name
، 
DataType
 (Decimal, Integer, String)، و 
IsFilterable
.

ProductAttributeValue

مقدار ویژگی برای محصول (مثل "500" برای وزن). شامل 
ProductAttributeValueId
، 
ProductId
، 
AttributeId
، و 
Value
.

2.1.2 ساختار فایل‌ها
src/Domain/Entities/: شامل تمام کلاس‌های موجودیت.

src/Domain/Domain.csproj: فایل پروژه بدون وابستگی خارجی.

2.2 لایه Application
لایه Application شامل منطق برنامه، سرویس‌ها، DTOها، و نگاشت‌ها است. این لایه از Domain استفاده می‌کند و به Infrastructure وابسته نیست.
2.2.1 کلاس‌ها و توضیحات
DTOها (src/Application/DTOs/)
DTO

توضیحات

UserDto

برای انتقال اطلاعات کاربر (مثل 
Email
، 
FirstName
).

CreateUserDto

برای ایجاد کاربر جدید (مثل 
Email
، 
Password
).

ProductDto

برای انتقال اطلاعات محصول (شامل 
AttributeValues
).

CreateProductDto

برای ایجاد محصول جدید.

OrderDto

برای انتقال اطلاعات سفارش.

CreateOrderDto

برای ایجاد سفارش جدید.

OrderItemDto

برای آیتم‌های سفارش.

CreateOrderItemDto

برای ایجاد آیتم سفارش.

PaymentDto

برای انتقال اطلاعات پرداخت.

CreatePaymentDto

برای ایجاد پرداخت جدید.

NotificationDto

برای انتقال اطلاعات اعلان.

CreateNotificationDto

برای ایجاد اعلان جدید.

SubscriptionDto

برای انتقال اطلاعات اشتراک.

CreateSubscriptionDto

برای ایجاد اشتراک جدید.

LoyaltyPointDto

برای انتقال اطلاعات امتیاز وفاداری.

ChatMessageDto

برای انتقال اطلاعات پیام چت.

SendChatMessageDto

برای ارسال پیام چت جدید.

UserActivityDto

برای انتقال اطلاعات فعالیت کاربر.

InventoryDto

برای انتقال اطلاعات موجودی.

ReviewDto

برای انتقال اطلاعات نظر.

CreateReviewDto

برای ایجاد نظر جدید.

SearchQueryDto

برای پارامترهای جستجوی پیشرفته (مثل 
Keyword
، 
AttributeFilters
).

SearchResultDto

برای نتایج جستجو (شامل 
Products
 و 
TotalCount
).

SearchHistoryDto

برای تاریخچه جستجو.

AttributeDto

برای انتقال اطلاعات ویژگی پویا.

ProductAttributeValueDto

برای انتقال مقادیر ویژگی محصول.

رابط‌ها (src/Application/Interfaces/)
رابط

توضیحات

IUserService

عملیات کاربر (ایجاد، احراز هویت).

IProductService

عملیات محصول (ایجاد، به‌روزرسانی).

IOrderService

عملیات سفارش (ایجاد، پیگیری).

IPaymentService

عملیات پرداخت.

INotificationService

ارسال اعلان (ایمیل، پیامک).

ISubscriptionService

مدیریت اشتراک.

ILoyaltyService

مدیریت امتیازات وفاداری.

IChatService

مدیریت پیام‌های چت.

IInventoryService

مدیریت موجودی.

IReviewService

مدیریت نظرات.

IAnalyticsService

تولید گزارش‌های تحلیلی.

ISearchService

جستجوی پیشرفته و مدیریت تاریخچه جستجو.

ICacheService

عملیات کش (Redis).

IMessageQueueService

مدیریت صف پیام (RabbitMQ).

IEmailService

ارسال ایمیل.

ISmsService

ارسال پیامک.

سرویس‌ها (src/Application/Services/)
سرویس

توضیحات

UserService

مدیریت کاربران (ایجاد، احراز هویت با JWT).

ProductService

مدیریت محصولات (ایجاد، به‌روزرسانی، شامل ویژگی‌های پویا).

OrderService

مدیریت سفارشات (ایجاد، محاسبه قیمت، کاهش موجودی).

PaymentService

پردازش پرداخت‌ها.

NotificationService

ارسال اعلانات از طریق صف RabbitMQ.

SubscriptionService

مدیریت اشتراک‌ها.

LoyaltyService

مدیریت امتیازات وفاداری.

ChatService

مدیریت پیام‌های چت بلادرنگ.

InventoryService

مدیریت موجودی و هشدارهای کمبود.

ReviewService

مدیریت نظرات کاربران.

AnalyticsService

تولید گزارش‌های تحلیلی (مثل فروش در بازه زمانی).

SearchService

جستجوی پیشرفته با فیلترهای پویا و ذخیره تاریخچه.

نگاشت‌ها (src/Application/Mappings/)
کلاس

توضیحات

MappingProfile

نگاشت‌های AutoMapper برای تبدیل موجودیت‌ها به DTOها و بالعکس.

2.2.2 قابلیت‌ها
احراز هویت با JWT: کاربران با ایمیل و رمز عبور توکن دریافت می‌کنند.

جستجوی پیشرفته: فیلترهای ترکیبی (برند، قیمت، فروشنده، ویژگی‌های پویا مثل وزن)، مرتب‌سازی، و صفحه‌بندی.

مدیریت ویژگی‌های پویا: پشتیبانی از ویژگی‌های دلخواه (مثل وزن برای حلیم یا طعم برای کیک).

کش با Redis: برای بهبود عملکرد جستجو و ویژگی‌های پراستفاده.

صف پیام با RabbitMQ: برای اعلانات غیرهمزمان.

اعلانات: ارسال ایمیل (SMTP) و پیامک (کاوه‌نگار).

2.3 لایه Infrastructure
لایه Infrastructure شامل پیاده‌سازی‌های زیرساختی مثل دیتابیس، کش، و سرویس‌های خارجی است.
2.3.1 کلاس‌ها و توضیحات
Persistence (src/Infrastructure/Persistence/)
کلاس

توضیحات

AppDbContext

زمینه دیتابیس EF Core. شامل تمام DbSetها و پیکربندی‌های مدل.

DatabaseSchema.sql

اسکریپت SQL برای ایجاد جداول و ایندکس‌ها.

Caching (src/Infrastructure/Caching/)
کلاس

توضیحات

RedisCacheService

پیاده‌سازی ICacheService برای ذخیره و بازیابی داده‌ها در Redis.

Messaging (src/Infrastructure/Messaging/)
کلاس

توضیحات

RabbitMQService

پیاده‌سازی IMessageQueueService برای ارسال و دریافت پیام‌ها در RabbitMQ.

سرویس‌های خارجی
کلاس

توضیحات

SmtpEmailService

پیاده‌سازی IEmailService برای ارسال ایمیل با SMTP.

KavenegarSmsService

پیاده‌سازی ISmsService برای ارسال پیامک با API کاوه‌نگار.

2.3.2 قابلیت‌ها
دیتابیس SQL Server: ذخیره داده‌ها با EF Core.

کش Redis: برای نتایج جستجو و ویژگی‌های پراستفاده.

صف RabbitMQ: برای پردازش غیرهمزمان اعلانات.

ایمیل و پیامک: ارسال اعلانات به کاربران.

ایندکس‌های دیتابیس: برای بهبود عملکرد کوئری‌ها (مثل ایندکس روی Name، Brand، Tags).

2.4 لایه Presentation
لایه Presentation شامل APIها، SignalR Hub، و میدلورها است.
2.4.1 کلاس‌ها و توضیحات
کنترلرها (src/Presentation/Controllers/)
کنترلر

توضیحات

UserController

مدیریت کاربران (ثبت‌نام، ورود).

ProductController

مدیریت محصولات (ایجاد، به‌روزرسانی).

OrderController

مدیریت سفارشات (ایجاد، پیگیری).

PaymentController

مدیریت پرداخت‌ها.

NotificationController

مدیریت اعلانات.

SubscriptionController

مدیریت اشتراک‌ها.

LoyaltyController

مدیریت امتیازات وفاداری.

InventoryController

مدیریت موجودی.

ReviewController

مدیریت نظرات.

AnalyticsController

تولید گزارش‌های تحلیلی.

SearchController

جستجوی پیشرفته و تاریخچه جستجو.

هاب‌ها (src/Presentation/Hubs/)
هاب

توضیحات

ChatHub

هاب SignalR برای چت بلادرنگ (ارسال پیام به کاربر یا گروه).

میدلورها (src/Presentation/Middlewares/)
میدلور

توضیحات

ErrorHandlingMiddleware

مدیریت استثناهای سراسری و بازگشت پاسخ‌های خطا (مثل 400، 500).

RateLimitingMiddleware

محدود کردن نرخ درخواست‌ها (100 درخواست در دقیقه برای هر IP).

سایر فایل‌ها
فایل

توضیحات

Program.cs

نقطه ورود برنامه. ثبت سرویس‌ها، پیکربندی JWT، CORS، Swagger، و SignalR.

appsettings.json

تنظیمات پروژه (رشته‌های اتصال، JWT، SMTP، کاوه‌نگار، RabbitMQ).

2.4.2 قابلیت‌ها
APIهای RESTful: برای تمام عملیات (کاربران، محصولات، سفارشات، و غیره).

Swagger: مستندسازی APIها.

SignalR: چت بلادرنگ با پشتیبانی از گروه‌ها.

احراز هویت JWT: برای endpointهای محافظت‌شده.

CORS: برای دسترسی کلاینت‌ها.

مدیریت خطا: با ErrorHandlingMiddleware.

محدودیت نرخ: با RateLimitingMiddleware.

2.5 لایه Tests
لایه Tests شامل تست‌های واحد برای لایه Application است.
2.5.1 کلاس‌ها و توضیحات
کلاس

توضیحات

UserServiceTests

تست‌های واحد برای UserService (ایجاد کاربر، احراز هویت).

OrderServiceTests

تست‌های واحد برای OrderService (ایجاد سفارش، بررسی موجودی).

SearchServiceTests

تست‌های واحد برای SearchService (جستجوی پیشرفته، فیلترهای پویا).

2.5.2 قابلیت‌ها
تست‌های واحد با xUnit: برای اطمینان از صحت منطق برنامه.

شبیه‌سازی با Moq: برای حذف وابستگی‌ها.

دیتابیس در حافظه: برای تست‌های سریع و مستقل.

3. قابلیت‌ها و امکانات
3.1 احراز هویت و مجوزها
JWT: احراز هویت با توکن JWT (ایجاد توکن در /api/User/authenticate).

نقش‌ها: Customer، Vendor، Admin، SuperAdmin.

مجوزها: با [Authorize(Roles = "Admin")] برای endpointهای خاص.

هش رمز عبور: با BCrypt.

3.2 جستجوی پیشرفته
فیلترهای ترکیبی:
Keyword (جستجو در نام، توضیحات، تگ‌ها).

Brand، Vendor، Price (Min/Max)، InStock، Date (Start/End)، Tags.

AttributeFilters: فیلترهای پویا (مثل {1: "500"} برای وزن=500 گرم).

مرتب‌سازی: بر اساس Price، Name، CreatedAt (صعودی/نزولی).

صفحه‌بندی: با PageNumber و PageSize.

کش: نتایج در Redis برای 5 دقیقه.

تاریخچه جستجو: ذخیره و بازیابی برای کاربران.

3.3 ویژگی‌های پویا
Attributes: تعریف ویژگی‌های دلخواه (مثل Weight، BowlCount).

ProductAttributeValues: ذخیره مقادیر برای هر محصول.

فیلترپذیری: با IsFilterable در Attribute.

گسترش‌پذیری: اضافه کردن ویژگی‌های جدید بدون تغییر کد.

3.4 چت بلادرنگ
SignalR: با ChatHub در /chatHub.

قابلیت‌ها: ارسال پیام به کاربر خاص یا گروه، پیوستن/خروج از گروه.

احراز هویت: فقط برای کاربران احراز هویت‌شده.

3.5 مدیریت سفارشات
ایجاد سفارش: با محاسبه قیمت و کاهش موجودی.

پیگیری: مشاهده جزئیات سفارش.

اعلان: ارسال اعلان پس از ثبت سفارش.

3.6 پرداخت
وضعیت‌ها: Pending، Completed، Failed.

پردازش: شبیه‌سازی پرداخت (قابل اتصال به درگاه واقعی).

3.7 اعلانات
ایمیل: با SMTP (مثل Gmail).

پیامک: با API کاوه‌نگار.

غیرهمزمان: با RabbitMQ.

3.8 مدیریت موجودی
موجودی: بررسی و کاهش هنگام سفارش.

هشدار: اعلان برای کمبود موجودی.

3.9 نظرات و امتیازات
نظرات: ثبت نظر و امتیاز برای محصولات.

وفاداری: کسب امتیاز برای خرید یا نظر.

3.10 گزارش‌های تحلیلی
AnalyticsController: تولید گزارش فروش بر اساس فیلترهای زمانی.

4. ساختار دیتابیس
4.1 جداول
جدول

توضیحات

Users

کاربران (مشتری، فروشنده، مدیر).

Products

محصولات (شامل Brand، Tags، AttributeValues).

Vendors

فروشندگان.

Orders

سفارشات.

OrderItems

آیتم‌های سفارش.

Payments

پرداخت‌ها.

Notifications

اعلانات.

Subscriptions

اشتراک‌ها.

LoyaltyPoints

امتیازات وفاداری.

LoyaltyTransactions

تراکنش‌های امتیاز.

ChatMessages

پیام‌های چت.

UserActivities

فعالیت‌های کاربر.

Inventories

موجودی محصولات.

InventoryAlerts

هشدارهای موجودی.

Reviews

نظرات.

SearchHistory

تاریخچه جستجو.

Attributes

ویژگی‌های پویا.

ProductAttributeValues

مقادیر ویژگی‌ها.

4.2 ایندکس‌ها
Users: ایندکس روی Email (یکتا).

Products: ایندکس روی Name، Brand، Tags، Price.

ProductAttributeValues: ایندکس روی ProductId، AttributeId.

5. راه‌اندازی و اجرا
5.1 پیش‌نیازها
.NET 9.0.5 SDK

SQL Server (یا Express)

Redis (docker run -d -p 6379:6379 redis)

RabbitMQ (docker run -d -p 5672:5672 -p 15672:15672 rabbitmq:3-management)

حساب کاوه‌نگار (API Key)

حساب ایمیل SMTP (مثل Gmail با App Password)

5.2 راه‌اندازی
کلون پروژه:
bash

git clone https://github.com/your-repo/GoldenClusterFantasy.git
cd GoldenClusterFantasy

نصب پکیج‌ها:
bash

dotnet restore

پیکربندی دیتابیس:
به‌روزرسانی appsettings.json با رشته اتصال.

اعمال مهاجرت‌ها:
bash

dotnet ef migrations add InitialCreate
dotnet ef database update

پیکربندی Redis، RabbitMQ، SMTP، و کاوه‌نگار در appsettings.json.

اجرا:
bash

dotnet run --project src/Presentation

5.3 دیپلوی
ساخت پروژه:
bash

dotnet publish -c Release -o ./publish

دیپلوی در IIS، Kestrel، یا Docker.

تنظیم متغیرهای محیطی برای مقادیر حساس.

6. مستندات API
6.1 نمونه درخواست‌ها
جستجوی پیشرفته:
json

POST /api/Search
{
  "Keyword": "Halim",
  "AttributeFilters": {
    "1": "500",
    "2": "2"
  },
  "PageNumber": 1,
  "PageSize": 10
}

دریافت ویژگی‌های قابل فیلتر:
json

GET /api/Search/attributes

ایجاد کاربر:
json

POST /api/User
{
  "Email": "test@example.com",
  "Password": "Password123",
  "FirstName": "Test",
  "LastName": "User"
}

6.2 Swagger
در محیط توسعه: https://localhost:5001/swagger

7. نکات امنیتی
JWT: کلید در محیط تولید امن باشد.

HTTPS: الزامی برای محیط تولید.

Rate Limiting: جلوگیری از حملات DDoS.

اسرار: استفاده از متغیرهای محیطی یا Azure Key Vault.


پیشنهادات هوشمند: با تحلیل SearchHistory و ProductAttributeValues.

8. جمع‌بندی
این پروژه یک پلتفرم کامل، مقیاس‌پذیر، و آینده‌نگرانه برای فروشگاه آنلاین است که تمام نیازهای کنونی (جستجوی پیشرفته، ویژگی‌های پویا، چت، پرداخت) و آینده (اضافه کردن محصولات جدید مثل حلیم) را پوشش می‌دهد. تمام لایه‌ها، کلاس‌ها، و قابلیت‌ها با جزئیات شرح داده شدند. برای هرگونه اصلاح یا اضافه شدن ویژگی، لطفاً اطلاع دهید!



آپدیت قابلیت‌های جدید:
برای اضافه کردن سه قابلیت درخواستی (جستجوی فازی، ویژگی‌های پیچیده با مقادیر چندگانه، و پیشنهادات هوشمند) به سیستم سرچ پیشرفته فروشگاه آنلاین نان فانتزی و شیرینی، تغییرات لازم رو در کدهای موجود اعمال می‌کنم و کدهای جدید رو با مستندات کامل ارائه می‌دم. این پیاده‌سازی انعطاف‌پذیر، مقیاس‌پذیر، و آینده‌نگرانه خواهد بود تا نیاز به تغییرات اساسی در آینده به حداقل برسه. برای هر قابلیت، بهترین رویکرد رو انتخاب می‌کنم و توضیح می‌دم که چرا مناسب است.
انتخاب رویکردها
جستجوی فازی:
الگوریتم انتخاب‌شده: از Levenshtein Distance برای جستجوی فازی در SQL Server با EF Core استفاده می‌کنیم، چون:
ساده و کارآمد برای حجم داده‌های متوسط.

نیازی به ابزارهای خارجی مثل Elasticsearch نداره (کاهش پیچیدگی و هزینه).

برای پشتیبانی از اشتباهات تایپی (مثل "بیگل" به جای "Bagel") عالی عمل می‌کنه.

گسترش‌پذیری: اگر در آینده حجم داده‌ها زیاد شد، می‌تونیم به Elasticsearch مهاجرت کنیم (ساختار کد آماده این انتقال خواهد بود).

ویژگی‌ها: پشتیبانی از اشتباهات تایپی در Keyword، Brand، و Tags با حداکثر فاصله Levenshtein (مثلاً 2).

ویژگی‌های پیچیده با مقادیر چندگانه:
رویکرد: جدول ProductAttributeValues رو گسترش می‌دیم تا مقادیر چندگانه (مثل لیست طعم‌ها برای کیک: ["شکلاتی"، "وانیلی"]) رو به‌صورت JSON یا آرایه ذخیره کنه.

مزایا: انعطاف‌پذیری بالا برای ویژگی‌هایی مثل طعم، مواد تشکیل‌دهنده، یا گزینه‌های سفارشی‌سازی.

پیاده‌سازی: از ستون JSON در SQL Server برای ذخیره مقادیر چندگانه استفاده می‌کنیم، چون EF Core از JSON Columns پشتیبانی می‌کنه و کوئری‌ها رو ساده می‌کنه.

پیشنهادات هوشمند:
رویکرد: تحلیل SearchHistory و ProductAttributeValues با الگوریتم Collaborative Filtering ساده برای پیشنهاد محصولات مرتبط یا پرطرفدار.

ویژگی‌ها:
پیشنهاد محصولات بر اساس جستجوهای مشابه کاربران دیگر.

پیشنهاد محصولات پرطرفدار با ویژگی‌های مشابه (مثل محصولات با طعم شکلاتی).

کش کردن پیشنهادات در Redis برای عملکرد بهتر.

گسترش‌پذیری: آماده برای ادغام با الگوریتم‌های پیشرفته‌تر (مثل یادگیری ماشین با ML.NET) در آینده.

تغییرات لازم در کدها و جداول
1. تغییرات در دیتابیس (لایه Infrastructure)
تغییر جدول ProductAttributeValues:
ستون Value به Values تغییر نام می‌ده و نوعش به NVARCHAR(MAX) با فرمت JSON می‌شه برای پشتیبانی از مقادیر چندگانه.

اضافه کردن تابع Levenshtein:
یک تابع SQL برای محاسبه فاصله Levenshtein ایجاد می‌کنیم تا در کوئری‌های فازی استفاده بشه.

جدول جدید ProductRecommendations (اختیاری):
برای ذخیره پیشنهادات از پیش محاسبه‌شده (بهبود عملکرد).

تغییرات SQL
توضیحات:
ProductAttributeValues: ستون Values از نوع JSON برای ذخیره آرایه مقادیر (مثل ["شکلاتی"، "وانیلی"]) استفاده می‌شه.

Levenshtein Function: تابع SQL برای محاسبه فاصله ویرایش بین دو رشته، با حداکثر فاصله 2 برای جستجوی فازی.

ProductRecommendations: پیشنهادات محصولات رو ذخیره می‌کنه تا در زمان اجرا نیازی به محاسبات سنگین نباشه.


آپدیت مستندات
1. جستجوی فازی
ویژگی‌ها:
پشتیبانی از اشتباهات تایپی در Keyword، Brand، و Tags (مثل "Bagle" به جای "Bagel").

استفاده از فاصله Levenshtein با حداکثر فاصله قابل تنظیم (پیش‌فرض: 2).

فعال‌سازی با EnableFuzzySearch و تنظیم فاصله با MaxFuzzyDistance.

مثال درخواست:
json

{
  "Keyword": "Bagle",
  "EnableFuzzySearch": true,
  "MaxFuzzyDistance": 2,
  "PageNumber": 1,
  "PageSize": 10
}

مزایا:
ساده و بدون نیاز به ابزارهای خارجی.

مناسب برای حجم داده‌های متوسط.

گسترش‌پذیری:
برای داده‌های بزرگ، می‌تونید به Elasticsearch مهاجرت کنید (نیاز به پکیج NEST و پیکربندی).

2. ویژگی‌های پیچیده با مقادیر چندگانه
ویژگی‌ها:
پشتیبانی از مقادیر چندگانه در ProductAttributeValues (مثل طعم‌های ["شکلاتی"، "وانیلی"]).

ذخیره به‌صورت JSON برای انعطاف‌پذیری.

فیلتر کردن بر اساس هر مقدار (مثل "محصولات با طعم شکلاتی").

مثال اضافه کردن کیک:
ویژگی طعم:
sql

INSERT INTO Attributes (Name, DataType, IsFilterable, CreatedAt, UpdatedAt)
VALUES ('Flavor', 'String', 1, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET());

محصول کیک:
sql

INSERT INTO Products (Name, Description, Price, VendorId, Brand, Tags, CreatedAt, UpdatedAt)
VALUES ('Cake', 'Chocolate and Vanilla Cake', 200000, 1, 'Soroush', 'dessert', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET());

مقادیر طعم:
sql

INSERT INTO ProductAttributeValues (ProductId, AttributeId, Values, CreatedAt, UpdatedAt)
VALUES (1, 1, '["Chocolate", "Vanilla"]', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET());

جستجو:
json

{
  "Keyword": "Cake",
  "AttributeFilters": {
    "1": "Chocolate"
  },
  "PageNumber": 1,
  "PageSize": 10
}

3. پیشنهادات هوشمند
ویژگی‌ها:
پیشنهاد بر اساس ویژگی‌های مشترک محصولات (مثل طعم شکلاتی).

پیشنهاد بر اساس تاریخچه جستجوی کاربر.

پیشنهاد محصولات پرطرفدار به‌عنوان fallback.

کش کردن پیشنهادات برای 10 دقیقه.

مثال درخواست:
GET /api/Search/recommendations?productId=1&limit=5

پاسخ:
json

[
  {
    "RecommendedProductId": 2,
    "Score": 0.5,
    "RecommendedProduct": {
      "ProductId": 2,
      "Name": "Cookie",
      "Price": 50000,
      "AttributeValues": [
        {
          "AttributeId": 1,
          "Values": ["Chocolate"]
        }
      ],
      ...
    }
  }
]

گسترش‌پذیری:
می‌تونید از ML.NET برای الگوریتم‌های پیشرفته‌تر (مثل Matrix Factorization) استفاده کنید.

تحلیل آفلاین SearchHistory برای به‌روزرسانی جدول ProductRecommendations.

بهینه‌سازی‌ها
جستجوی فازی: تابع Levenshtein در SQL برای داده‌های کوچک مناسبه، اما برای مقیاس بزرگ، Elasticsearch توصیه می‌شه.

ویژگی‌های چندگانه: استفاده از JSON مقیاس‌پذیره، اما برای کوئری‌های پیچیده، می‌تونید ایندکس‌های JSON اضافه کنید:
sql

CREATE INDEX IX_ProductAttributeValues_Values ON ProductAttributeValues USING GIN (Values);

پیشنهادات: کش Redis و جدول ProductRecommendations عملکرد رو بهبود می‌دن. برای داده‌های بزرگ، می‌تونید پیشنهادات رو به‌صورت آفلاین محاسبه کنید.

جمع‌بندی
تغییرات اعمال‌شده
دیتابیس:
تغییر ProductAttributeValues برای پشتیبانی از مقادیر چندگانه با JSON.

اضافه شدن تابع Levenshtein برای جستجوی فازی.

جدول جدید ProductRecommendations.

موجودیت‌ها:
به‌روزرسانی ProductAttributeValue برای Values به‌صورت آرایه.

اضافه شدن ProductRecommendation.

EF Core:
پیکربندی JSON برای ProductAttributeValue.Values.

اضافه شدن DbSet و پیکربندی برای ProductRecommendations.

لایه Application:
به‌روزرسانی DTOها: ProductAttributeValueDto، SearchQueryDto.

DTO جدید: ProductRecommendationDto.

به‌روزرسانی SearchService برای جستجوی فازی، مقادیر چندگانه، و پیشنهادات.

لایه Presentation:
اضافه شدن endpoint /api/Search/recommendations.


چرا این پیاده‌سازی کامل است؟
جستجوی فازی: Levenshtein ساده و کارآمد برای پشتیبانی از اشتباهات تایپی.

ویژگی‌های چندگانه: ذخیره JSON انعطاف‌پذیر و قابل گسترش برای هر نوع ویژگی.

پیشنهادات هوشمند: ترکیب ویژگی‌ها، تاریخچه، و محبوبیت با کش برای عملکرد بالا.

آینده‌نگری: آماده برای Elasticsearch، ML.NET، و تحلیل آفلاین.

پیشنهادات
تست:
درخواست‌های نمونه (مثل JSONهای بالا) رو تست کنید و بازخورد بدید.

اگر ویژگی خاصی (مثل فیلترهای پیچیده‌تر یا پیشنهادات پیشرفته‌تر) نیازه، می‌تونم اضافه کنم.

بهینه‌سازی:
اضافه کردن Elasticsearch برای جستجوی فازی در مقیاس بزرگ.

پیاده‌سازی تحلیل آفلاین با ML.NET برای پیشنهادات پیشرفته‌تر.

