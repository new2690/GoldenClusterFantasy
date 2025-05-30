using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces;

/// <summary>
/// Interface for product-related operations.
/// رابط برای عملیات مرتبط با محصول.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Creates a new product.
    /// ایجاد یک محصول جدید.
    /// </summary>
    Task<ProductDto> CreateProductAsync(CreateProductDto dto, int vendorId);

    /// <summary>
    /// Gets a product by ID.
    /// دریافت محصول با شناسه.
    /// </summary>
    Task<ProductDto> GetProductByIdAsync(int productId);

    /// <summary>
    /// Gets all products.
    /// دریافت همه محصولات.
    /// </summary>
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
}