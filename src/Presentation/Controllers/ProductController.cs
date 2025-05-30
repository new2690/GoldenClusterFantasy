using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers;

/// <summary>
/// Controller for product-related operations.
/// کنترلر برای عملیات مرتبط با محصول.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    /// <summary>
    /// Initializes a new instance of the ProductController.
    /// سازنده‌ای برای ایجاد نمونه جدید از ProductController.
    /// </summary>
    /// <param name="productService">Product service / سرویس محصول.</param>
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Creates a new product.
    /// ایجاد محصول جدید.
    /// </summary>
    /// <param name="dto">Product creation data / داده‌های ایجاد محصول.</param>
    /// <returns>The created product / محصول ایجادشده.</returns>
    [Authorize(Roles = "Vendor")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
    {
        // Get vendor ID from JWT
        // دریافت شناسه فروشنده از JWT
        var vendorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        // Call service to create product
        // فراخوانی سرویس برای ایجاد محصول
        var product = await _productService.CreateProductAsync(dto, vendorId);
        return CreatedAtAction(nameof(GetProduct), new { productId = product.ProductId }, product);
    }

    /// <summary>
    /// Gets a product by ID.
    /// دریافت محصول با شناسه.
    /// </summary>
    /// <param name="productId">Product ID / شناسه محصول.</param>
    /// <returns>The product / محصول.</returns>
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProduct(int productId)
    {
        // Call service to get product
        // فراخوانی سرویس برای دریافت محصول
        var product = await _productService.GetProductByIdAsync(productId);
        return Ok(product);
    }

    /// <summary>
    /// Gets all products.
    /// دریافت همه محصولات.
    /// </summary>
    /// <returns>List of products / لیست محصولات.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        // Call service to get all products
        // فراخوانی سرویس برای دریافت همه محصولات
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }
}