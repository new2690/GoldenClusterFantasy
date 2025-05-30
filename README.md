# GoldenClusterFantasy
مستندات جامع پروژه GoldenClusterFantasy
این مستند، شرح کاملی از پروژه GoldenClusterFantasy، یک پلتفرم فروش آنلاین نان فانتزی، شیرینی و محصولات متنوع (با قابلیت گسترش برای محصولاتی مثل حلیم) ارائه می‌دهد. هدف، ایجاد سیستمی انعطاف‌پذیر، مقیاس‌پذیر و با عملکرد بالا برای مدیریت فروشگاه آنلاین است که از Clean Architecture، اصول SOLID و فناوری‌های مدرن استفاده می‌کند. این مستند تمام لایه‌ها، قابلیت‌ها، ساختارها، فناوری‌ها و جزئیات پیاده‌سازی را به‌صورت منظم و با توضیحات دو زبانه (فارسی و انگلیسی) پوشش می‌دهد.
فهرست مطالب
مقدمه (#مقدمه)

معماری پروژه (#معماری-پروژه)
لایه Domain (#لایه-domain)

لایه Application (#لایه-application)

لایه Infrastructure (#لایه-infrastructure)

لایه Presentation (#لایه-presentation)

لایه Tests (#لایه-tests)

قابلیت‌ها و امکانات (#قابلیت‌ها-و-امکانات)
مدیریت کاربران (#مدیریت-کاربران)

مدیریت محصولات (#مدیریت-محصولات)

مدیریت سفارشات (#مدیریت-سفارشات)

پرداخت آنلاین (#پرداخت-آنلاین)

اعلانات (#اعلانات)

اشتراک‌ها (#اشتراک‌ها)

برنامه وفاداری (#برنامه-وفاداری)

چت بلادرنگ (#چت-بلادرنگ)

تحلیل و گزارش‌گیری (#تحلیل-و-گزارش‌گیری)

مدیریت موجودی (#مدیریت-موجودی)

نظرات کاربران (#نظرات-کاربران)

جستجوی پیشرفته (#جستجوی-پیشرفته)

فناوری‌ها و ابزارها (#فناوری‌ها-و-ابزارها)

ساختار پروژه (#ساختار-پروژه)

نصب و راه‌اندازی (#نصب-و-راه‌اندازی)

دیپلوی و مقیاس‌پذیری (#دیپلوی-و-مقیاس‌پذیری)

تست‌ها (#تست‌ها)

گسترش‌پذیری و نگهداری (#گسترش‌پذیری-و-نگهداری)

نتیجه‌گیری (#نتیجه‌گیری)

مقدمه
GoldenClusterFantasy یک پلتفرم فروش آنلاین است که برای عرضه نان فانتزی، شیرینی و محصولات متنوع (مثل حلیم) طراحی شده. این سیستم قابلیت‌هایی مثل جستجوی پیشرفته، چت بلادرنگ، مدیریت موجودی، پرداخت آنلاین و اعلانات را ارائه می‌دهد. هدف اصلی، ایجاد تجربه کاربری روان، عملکرد بالا و انعطاف‌پذیری برای اضافه کردن محصولات جدید با ویژگی‌های متفاوت (مثل وزن یا تعداد کاسه برای حلیم) است.
ویژگی‌های کلیدی:
معماری تمیز (Clean Architecture) برای جداسازی مسئولیت‌ها.

جستجوی پیشرفته با فیلترهای پویا (مثل برند، قیمت، ویژگی‌های محصول).

پشتیبانی از ویژگی‌های پویا برای محصولات (Dynamic Attributes).

استفاده از فناوری‌های مدرن مثل SignalR، Redis و RabbitMQ.

مستندات دو زبانه (فارسی و انگلیسی) برای توسعه‌دهندگان.

مخاطبان این مستند:
توسعه‌دهندگان برای درک ساختار و گسترش پروژه.

مدیران فنی برای ارزیابی فناوری‌ها و قابلیت‌ها.

تیم‌های عملیاتی برای نصب، دیپلوی و نگهداری.

معماری پروژه
پروژه از معماری Clean Architecture استفاده می‌کند که شامل لایه‌های زیر است:
لایه Domain
لایه Domain هسته مرکزی پروژه است و شامل موجودیت‌ها (Entities) و منطق اصلی کسب‌وکار (Business Logic) می‌شود. این لایه مستقل از سایر لایه‌ها بوده و هیچ وابستگی خارجی ندارد.
ویژگی‌ها:
موجودیت‌ها: تعریف مدل‌های اصلی مثل User، Product، Order و غیره.

Enumها: برای مقادیر ثابت مثل OrderStatus یا PaymentStatus.

انعطاف‌پذیری: پشتیبانی از ویژگی‌های پویا (Attributes) برای محصولات متنوع.

فایل‌های کلیدی:
Domain/Entities/User.cs: مدل کاربر با ویژگی‌هایی مثل Email، PasswordHash و Role.

Domain/Entities/Product.cs: مدل محصول با پشتیبانی از Brand، Tags و AttributeValues.

Domain/Entities/Attribute.cs: مدل ویژگی‌های پویا (مثل وزن یا تعداد کاسه).

Domain/Entities/ProductAttributeValue.cs: مقادیر ویژگی‌ها برای هر محصول.

Domain/Enums/OrderStatus.cs: وضعیت‌های سفارش (مثل Pending، Completed).

مثال کد:
csharp

namespace Domain.Entities;

/// <summary>
/// Represents a product in the system.
/// نشان‌دهنده یک محصول در سیستم.
/// </summary>
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Brand { get; set; }
    public List<ProductAttributeValue> AttributeValues { get; set; } = new List<ProductAttributeValue>();
}

لایه Application
لایه Application منطق برنامه‌نویسی را پیاده‌سازی می‌کند و رابط بین لایه‌های Presentation و Domain است. شامل سرویس‌ها، DTOها و نگاشت‌ها (Mappings) است.
ویژگی‌ها:
DTOs: برای انتقال داده‌ها بین لایه‌ها (مثل ProductDto، SearchQueryDto).

Interfaces: تعریف رابط‌ها برای سرویس‌ها (مثل ISearchService).

Services: پیاده‌سازی منطق کسب‌وکار (مثل SearchService).

AutoMapper: برای نگاشت موجودیت‌ها به DTOها.

فایل‌های کلیدی:
Application/DTOs/SearchQueryDto.cs: DTO برای جستجوی پیشرفته با فیلترهای پویا.

Application/Interfaces/ISearchService.cs: رابط سرویس جستجو.

Application/Services/SearchService.cs: پیاده‌سازی جستجوی پیشرفته.

Application/Mappings/MappingProfile.cs: نگاشت‌های AutoMapper.

مثال کد:
csharp

namespace Application.Services;

/// <summary>
/// Service for advanced search operations.
/// سرویس برای عملیات جستجوی پیشرفته.
/// </summary>
public class SearchService : ISearchService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public SearchService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SearchResultDto> SearchAsync(SearchQueryDto query, int? userId)
    {
        // Dynamic query building with LINQ
        // ساخت پرس‌وجوی پویا با LINQ
        var dbQuery = _context.Products.AsQueryable();
        // اعمال فیلترها و جستجو
    }
}

لایه Infrastructure
لایه Infrastructure خدمات زیرساختی مثل دسترسی به دیتابیس، کش، صف پیام و ارسال ایمیل/پیامک را فراهم می‌کند.
ویژگی‌ها:
دیتابیس: استفاده از Entity Framework Core با SQL Server.

کش: استفاده از Redis برای کش نتایج جستجو.

صف پیام: استفاده از RabbitMQ برای اعلانات غیرهمزمان.

ارسال ایمیل: استفاده از SMTP (مثل Gmail).

ارسال پیامک: استفاده از API کاوه‌نگار.

فایل‌های کلیدی:
Infrastructure/Persistence/AppDbContext.cs: زمینه دیتابیس EF Core.

Infrastructure/Caching/RedisCacheService.cs: سرویس کش Redis.

Infrastructure/Messaging/RabbitMQService.cs: سرویس صف RabbitMQ.

Infrastructure/Persistence/DatabaseSchema.sql: اسکریپت‌های دیتابیس.

مثال کد:
csharp

namespace Infrastructure.Persistence;

/// <summary>
/// Database context for the application.
/// زمینه دیتابیس برای برنامه.
/// </summary>
public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Attribute> Attributes { get; set; }
    // سایر DbSetها
}

لایه Presentation
لایه Presentation رابط کاربری API و ارتباطات بلادرنگ را مدیریت می‌کند.
ویژگی‌ها:
کنترلرها: APIهای RESTful با ASP.NET Core.

SignalR Hub: برای چت بلادرنگ.

میان‌افزارها: مدیریت خطا و محدودیت نرخ درخواست.

Swagger: مستندسازی API.

فایل‌های کلیدی:
Presentation/Controllers/SearchController.cs: کنترلر جستجوی پیشرفته.

Presentation/Hubs/ChatHub.cs: هاب SignalR برای چت.

Presentation/Middlewares/ErrorHandlingMiddleware.cs: مدیریت خطاهای سراسری.

Presentation/Program.cs: پیکربندی برنامه.

مثال کد:
csharp

namespace Presentation.Controllers;

/// <summary>
/// Controller for advanced search operations.
/// کنترلر برای عملیات جستجوی پیشرفته.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpPost]
    public async Task<IActionResult> Search([FromBody] SearchQueryDto query)
    {
        var result = await _searchService.SearchAsync(query, null);
        return Ok(result);
    }
}

لایه Tests
لایه Tests شامل تست‌های واحد برای اطمینان از صحت عملکرد سرویس‌ها است.
ویژگی‌ها:
تست‌های واحد: با استفاده از xUnit و Moq.

دیتابیس در حافظه: برای تست‌های مستقل با EF Core In-Memory.

فایل‌های کلیدی:
tests/Application.Tests/SearchServiceTests.cs: تست‌های سرویس جستجو.

tests/Application.Tests/UserServiceTests.cs: تست‌های سرویس کاربر.

مثال کد:
csharp

namespace Application.Tests;

public class SearchServiceTests
{
    [Fact]
    public async Task SearchAsync_WithAttributeFilters_ReturnsFilteredProducts()
    {
        // Arrange, Act, Assert
    }
}

قابلیت‌ها و امکانات
مدیریت کاربران
ثبت‌نام و ورود: کاربران می‌تونن با ایمیل و رمز عبور ثبت‌نام و وارد سیستم بشن (با JWT).

نقش‌ها: پشتیبانی از نقش‌های Customer، Vendor، Admin و SuperAdmin.

مدیریت پروفایل: ویرایش اطلاعات کاربر (نام، ایمیل، و غیره).

فناوری‌ها: JWT، BCrypt برای هش رمز عبور.
مدیریت محصولات
ایجاد و ویرایش محصول: اضافه کردن محصولات با ویژگی‌های پویا (مثل وزن برای حلیم).

ویژگی‌های پویا: پشتیبانی از ویژگی‌هایی مثل وزن، تعداد کاسه، طعم و غیره.

مدیریت برند و تگ‌ها: فیلتر محصولات بر اساس برند و تگ.

فناوری‌ها: EF Core، SQL Server.
مدیریت سفارشات
ایجاد سفارش: کاربران می‌تونن محصولات رو به سبد خرید اضافه و سفارش ثبت کنن.

پیگیری سفارش: نمایش وضعیت سفارش (Pending، Shipped، Delivered).

مدیریت موجودی: کاهش خودکار موجودی پس از ثبت سفارش.

فناوری‌ها: EF Core، RabbitMQ برای اعلانات.
پرداخت آنلاین
پردازش پرداخت: ادغام با درگاه‌های پرداخت (مثل زرین‌پال، در این پروژه شبیه‌سازی شده).

پیگیری تراکنش: ذخیره تاریخچه تراکنش‌ها.

فناوری‌ها: EF Core.
اعلانات
ایمیل و پیامک: ارسال اعلانات برای تأیید سفارش، پرداخت و غیره.

غیرهمزمان: استفاده از RabbitMQ برای پردازش اعلانات.

فناوری‌ها: SMTP، کاوه‌نگار، RabbitMQ.
اشتراک‌ها
اشتراک ماهیانه: کاربران می‌تونن برای دریافت محصولات به‌صورت دوره‌ای اشتراک کنن.

مدیریت اشتراک: فعال‌سازی، لغو و تمدید اشتراک.

فناوری‌ها: EF Core.
برنامه وفاداری
امتیازات وفاداری: کاربران با خرید امتیاز جمع می‌کنن.

استفاده از امتیازات: تخفیف با استفاده از امتیازات.

فناوری‌ها: EF Core.
چت بلادرنگ
چت با فروشندگان: کاربران می‌تونن با فروشندگان چت کنن.

چت گروهی: پشتیبانی از گروه‌های چت.

فناوری‌ها: SignalR.
تحلیل و گزارش‌گیری
گزارش‌های تحلیلی: گزارش فروش، موجودی و رفتار کاربران.

دسترسی محدود: فقط برای Admins و SuperAdmins.

فناوری‌ها: EF Core.
مدیریت موجودی
پیگیری موجودی: هشدار برای موجودی کم.

به‌روزرسانی خودکار: کاهش موجودی پس از سفارش.

فناوری‌ها: EF Core.
نظرات کاربران
ثبت نظر: کاربران می‌تونن برای محصولات نظر ثبت کنن.

نمایش نظرات: نمایش نظرات تأییدشده.

فناوری‌ها: EF Core.
جستجوی پیشرفته
فیلترهای ترکیبی: جستجو بر اساس نام، برند، فروشنده، قیمت، تاریخ، تگ‌ها و ویژگی‌های پویا.

ویژگی‌های پویا: پشتیبانی از فیلترهایی مثل وزن (500 گرم) یا تعداد کاسه (2).

صفحه‌بندی و مرتب‌سازی: نتایج با صفحه‌بندی و مرتب‌سازی (قیمت، نام، تاریخ).

کش: نتایج در Redis برای 5 دقیقه کش می‌شن.

تاریخچه جستجو: ذخیره جستجوهای کاربران برای تحلیل.

فناوری‌ها: EF Core، Redis، LINQ.
مثال درخواست جستجو:
json

{
  "Keyword": "Halim",
  "Brand": "Soroush",
  "AttributeFilters": {
    "1": "500", // Weight=500g
    "2": "2"    // BowlCount=2
  },
  "MinPrice": 40000,
  "PageNumber": 1,
  "PageSize": 10
}

فناوری‌ها و ابزارها
زبان برنامه‌نویسی: C# (.NET 9.0.5).

فریم‌ورک: ASP.NET Core برای API و SignalR.

دیتابیس: SQL Server با EF Core.

کش: Redis (StackExchange.Redis).

صف پیام: RabbitMQ.

ارسال ایمیل: SMTP (مثال با Gmail).

ارسال پیامک: کاوه‌نگار.

مستندسازی API: Swagger (Swashbuckle).

تست: xUnit، Moq، EF Core In-Memory.

نگاشت: AutoMapper.

احراز هویت: JWT (Microsoft.IdentityModel.Tokens).

ابزارهای توسعه: Visual Studio 2022، VS Code.

مدیریت پروژه: Git، GitHub.

ساختار پروژه

GoldenClusterFantasy/
├── src/
│   ├── Domain/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   ├── Domain.csproj
│   ├── Application/
│   │   ├── DTOs/
│   │   ├── Interfaces/
│   │   ├── Mappings/
│   │   ├── Services/
│   │   ├── Application.csproj
│   ├── Infrastructure/
│   │   ├── Persistence/
│   │   ├── Caching/
│   │   ├── Messaging/
│   │   ├── Infrastructure.csproj
│   ├── Presentation/
│   │   ├── Controllers/
│   │   ├── Hubs/
│   │   ├── Middlewares/
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   ├── Presentation.csproj
├── tests/
│   ├── Application.Tests/
│   │   ├── Application.Tests.csproj
├── GoldenClusterFantasy.sln

توضیحات:
src: شامل پروژه‌های اصلی.

tests: شامل پروژه تست‌ها.

sln: فایل سولوشن برای مدیریت پروژه‌ها.

نصب و راه‌اندازی
پیش‌نیازها
.NET 9.0.5 SDK

SQL Server (یا Express)

Redis (docker run -d -p 6379:6379 redis)

RabbitMQ (docker run -d -p 5672:5672 -p 15672:15672 rabbitmq:3-management)

حساب کاوه‌نگار (API Key)

حساب ایمیل SMTP (مثل Gmail با App Password)

Visual Studio 2022 یا VS Code

مراحل نصب
کلون پروژه:
bash

git clone https://github.com/your-repo/GoldenClusterFantasy.git
cd GoldenClusterFantasy

نصب پکیج‌ها:
bash

dotnet restore

پیکربندی دیتابیس:
به‌روزرسانی رشته اتصال در appsettings.json.

اعمال مهاجرت‌ها:
bash

dotnet ef migrations add InitialCreate
dotnet ef database update

پیکربندی Redis و RabbitMQ:
تنظیمات در appsettings.json.

پیکربندی ایمیل و پیامک:
اطلاعات SMTP و کاوه‌نگار در appsettings.json.

اجرا:
bash

dotnet run --project src/Presentation

API در https://localhost:5001.

Swagger در https://localhost:5001/swagger.

دیپلوی و مقیاس‌پذیری
دیپلوی
ساخت پروژه:
bash

dotnet publish -c Release -o ./publish

دیپلوی در سرور:
استفاده از IIS، Kestrel یا Docker.

تنظیم متغیرهای محیطی (رشته‌های اتصال، کلیدها).

مانیتورینگ:
استفاده از Serilog یا Application Insights.

مقیاس‌پذیری
دیتابیس: استفاده از Replica Sets در SQL Server.

کش: مقیاس‌پذیری Redis با Cluster Mode.

صف پیام: استفاده از چندین نود RabbitMQ.

API: استفاده از Kubernetes برای مقیاس‌پذیری افقی.

تست‌ها
تست‌های واحد: برای سرویس‌های کلیدی (مثل SearchService، UserService).

پوشش تست: تمرکز بر سناریوهای اصلی (جستجو، ثبت کاربر، سفارش).

ابزارها: xUnit، Moq، EF Core In-Memory.

مثال تست:
csharp

[Fact]
public async Task SearchAsync_WithAttributeFilters_ReturnsFilteredProducts()
{
    // Arrange, Act, Assert
}

گسترش‌پذیری و نگهداری
گسترش‌پذیری
اضافه کردن محصولات جدید:
تعریف ویژگی‌های جدید در Attributes (مثل وزن).

اضافه کردن محصول در Products.

ثبت مقادیر ویژگی‌ها در ProductAttributeValues.

جستجو با AttributeFilters.

ویژگی‌های جدید:
اضافه کردن فیلترهای جدید به SearchQueryDto.

گسترش SearchService برای پشتیبانی از فیلترها.

ادغام با Elasticsearch:
برای جستجوی سریع‌تر در حجم داده بالا.

نگهداری
لاگ‌نویسی: استفاده از Serilog برای ردیابی خطاها.

مستندات: به‌روزرسانی مستندات با تغییرات جدید.

تست‌های خودکار: اجرای تست‌ها در CI/CD.

نتیجه‌گیری
GoldenClusterFantasy یک پلتفرم قدرتمند و انعطاف‌پذیر برای فروش آنلاین نان فانتزی، شیرینی و محصولات متنوع است. با استفاده از Clean Architecture، فناوری‌های مدرن و جستجوی پیشرفته با ویژگی‌های پویا، این سیستم آماده پاسخگویی به نیازهای فعلی و گسترش در آینده است. مستندات ارائه‌شده تمام جنبه‌های پروژه را پوشش می‌دهد و راهنمایی کاملی برای توسعه‌دهندگان، مدیران فنی و تیم‌های عملیاتی فراهم می‌کند.
اقدامات پیشنهادی بعدی:
تست کامل APIها و جستجوی پیشرفته.

دیپلوی در محیط تولید با مانیتورینگ.

گسترش با ویژگی‌های جدید مثل پیشنهادات هوشمند


آپدیت قابلیت‌های جدید
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

