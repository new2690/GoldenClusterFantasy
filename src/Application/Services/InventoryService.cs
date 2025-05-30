using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Application.Services;

/// <summary>
/// Service for inventory-related operations.
/// سرویس برای عملیات مرتبط با موجودی.
/// </summary>
public class InventoryService : IInventoryService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the InventoryService.
    /// سازنده‌ای برای ایجاد نمونه جدید از InventoryService.
    /// </summary>
    /// <param name="context">Database context / زمینه دیتابیس.</param>
    /// <param name="mapper">AutoMapper instance / نمونه AutoMapper.</param>
    public InventoryService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets inventory for a product.
    /// دریافت موجودی برای محصول.
    /// </summary>
    /// <param name="productId">Product ID / شناسه محصول.</param>
    /// <returns>The inventory / موجودی.</returns>
    public async Task<InventoryDto> GetInventoryByProductIdAsync(int productId)
    {
        // Find inventory
        // یافتن موجودی
        var inventory = await _context.Inventories.FindAsync(productId);
        if (inventory == null)
        {
            throw new ArgumentException("Inventory not found / موجودی یافت نشد.");
        }

        // Map entity to DTO and return
        // نگاشت موجودیت به DTO و بازگشت
        return _mapper.Map<InventoryDto>(inventory);
    }
}