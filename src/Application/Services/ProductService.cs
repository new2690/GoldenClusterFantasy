using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Caching;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

/// <summary>
/// Service for product-related operations.
/// سرویس برای عملیات مرتبط با محصول.
/// </summary>
public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    /// <summary>
    /// Initializes a new instance of the ProductService.
    /// سازنده‌ای برای ایجاد نمونه جدید از ProductService.
    /// </summary>
    public ProductService(AppDbContext context, IMapper mapper, ICacheService cacheService)
    {
        _context = context;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    /// <summary>
    /// Creates a new product.
    /// ایجاد یک محصول جدید.
    /// </summary>
    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto, int vendorId)
    {
        var product = _mapper.Map<Product>(dto);
        product.VendorId = vendorId;
        product.CreatedAt = DateTimeOffset.UtcNow;
        product.UpdatedAt = DateTimeOffset.UtcNow;

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var inventory = new Inventory
        {
            ProductId = product.ProductId,
            Quantity = dto.Quantity,
            MinStockThreshold = dto.MinStockThreshold,
            UpdatedAt = DateTimeOffset.UtcNow
        };
        _context.Inventories.Add(inventory);
        await _context.SaveChangesAsync();

        var productDto = _mapper.Map<ProductDto>(product);
        await _cacheService.SetAsync($"product_{product.ProductId}", productDto, TimeSpan.FromHours(1));
        return productDto;
    }

    /// <summary>
    /// Gets a product by ID.
    /// دریافت محصول با شناسه.
    /// </summary>
    public async Task<ProductDto> GetProductByIdAsync(int productId)
    {
        var cacheKey = $"product_{productId}";
        var cachedProduct = await _cacheService.GetAsync<ProductDto>(cacheKey);
        if (cachedProduct != null)
        {
            return cachedProduct;
        }

        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            throw new KeyNotFoundException("Product not found / محصول یافت نشد.");
        }

        var productDto = _mapper.Map<ProductDto>(product);
        await _cacheService.SetAsync(cacheKey, productDto, TimeSpan.FromHours(1));
        return productDto;
    }

    /// <summary>
    /// Gets all products.
    /// دریافت همه محصولات.
    /// </summary>
    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var cacheKey = "all_products";
        var cachedProducts = await _cacheService.GetAsync<IEnumerable<ProductDto>>(cacheKey);
        if (cachedProducts != null)
        {
            return cachedProducts;
        }

        var products = await _context.Products.ToListAsync();
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        await _cacheService.SetAsync(cacheKey, productDtos, TimeSpan.FromHours(1));
        return productDtos;
    }
}