using System;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

/// <summary>
/// Service for order-related operations.
/// سرویس برای عملیات مرتبط با سفارش.
/// </summary>
public class OrderService : IOrderService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the OrderService.
    /// سازنده‌ای برای ایجاد نمونه جدید از OrderService.
    /// </summary>
    public OrderService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new order.
    /// ایجاد یک سفارش جدید.
    /// </summary>
    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto, int userId)
    {
        var order = new Order
        {
            UserId = userId,
            PaymentStatus = PaymentStatus.Pending,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        decimal totalPrice = 0;
        foreach (var item in dto.Items)
        {
            var product = await _context.Products.FindAsync(item.ProductId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product {item.ProductId} not found / محصول {item.ProductId} یافت نشد.");
            }

            var inventory = await _context.Inventories.FindAsync(item.ProductId);
            if (inventory == null || inventory.Quantity < item.Quantity)
            {
                throw new InvalidOperationException($"Insufficient stock for product {item.ProductId} / موجودی کافی برای محصول {item.ProductId} نیست.");
            }

            inventory.Quantity -= item.Quantity;
            inventory.UpdatedAt = DateTimeOffset.UtcNow;

            var orderItem = new OrderItem
            {
                Order = order,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            };
            order.OrderItems.Add(orderItem);
            totalPrice += item.Quantity * product.Price;
        }

        order.TotalPrice = totalPrice;
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return _mapper.Map<OrderDto>(order);
    }

    /// <summary>
    /// Gets an order by ID.
    /// دریافت سفارش با شناسه.
    /// </summary>
    public async Task<OrderDto> GetOrderByIdAsync(int orderId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);
        if (order == null)
        {
            throw new KeyNotFoundException("Order not found / سفارش یافت نشد.");
        }
        return _mapper.Map<OrderDto>(order);
    }
}