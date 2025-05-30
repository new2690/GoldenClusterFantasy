using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

/// <summary>
/// AutoMapper profile for mapping entities to DTOs.
/// پروفایل AutoMapper برای نگاشت موجودیت‌ها به DTOها.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the MappingProfile.
    /// سازنده‌ای برای ایجاد نمونه جدید از MappingProfile.
    /// </summary>
    public MappingProfile()
    {
        // User mappings
        // نگاشت‌های کاربر
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();

        // Product mappings
        // نگاشت‌های محصول
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();

        // Order mappings
        // نگاشت‌های سفارش
        CreateMap<Order, OrderDto>();
        CreateMap<CreateOrderDto, Order>();
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<CreateOrderItemDto, OrderItem>();

        // Payment mappings
        // نگاشت‌های پرداخت
        CreateMap<Payment, PaymentDto>();
        CreateMap<CreatePaymentDto, Payment>();

        // Notification mappings
        // نگاشت‌های اعلان
        CreateMap<Notification, NotificationDto>();
        CreateMap<CreateNotificationDto, Notification>();

        // Subscription mappings
        // نگاشت‌های اشتراک
        CreateMap<Subscription, SubscriptionDto>();
        CreateMap<CreateSubscriptionDto, Subscription>();

        // Loyalty mappings
        // نگاشت‌های وفاداری
        CreateMap<LoyaltyPoint, LoyaltyPointDto>();
        CreateMap<LoyaltyTransaction, LoyaltyPointDto>();

        // Chat mappings
        // نگاشت‌های چت
        CreateMap<ChatMessage, ChatMessageDto>();
        CreateMap<SendChatMessageDto, ChatMessage>();

        // Analytics mappings
        // نگاشت‌های تحلیل
        CreateMap<UserActivity, UserActivityDto>();

        // Inventory mappings
        // نگاشت‌های موجودی
        CreateMap<Inventory, InventoryDto>();

        // Review mappings
        // نگاشت‌های نظر
        CreateMap<Review, ReviewDto>();
        CreateMap<CreateReviewDto, Review>();

         // New mappings for attributes
        CreateMap<Attribute, AttributeDto>();
        CreateMap<ProductAttributeValue, ProductAttributeValueDto>();

        // New mappings for search
        // نگاشت‌های جدید برای جستجو
        CreateMap<SearchHistory, SearchHistoryDto>();

            // New mapping for recommendations
        CreateMap<ProductRecommendation, ProductRecommendationDto>()
            .ForMember(dest => dest.RecommendedProduct, opt => opt.MapFrom(src => src.RecommendedProduct));
    
    }
}