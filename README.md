# GoldenClusterFantasy
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

