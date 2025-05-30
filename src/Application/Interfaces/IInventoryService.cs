using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces;

/// <summary>
/// Interface for inventory-related operations.
/// رابط برای عملیات مرتبط با موجودی.
/// </summary>
public interface IInventoryService
{
    /// <summary>
    /// Updates the inventory for a product.
    /// به‌روزرسانی موجودی برای یک محصول.
    /// </summary>
    Task<InventoryDto> UpdateInventoryAsync(int productId, int quantity);

    /// <summary>
    /// Gets the inventory for a product.
    /// دریافت موجودی برای یک محصول.
    /// </summary>
    Task<InventoryDto> GetInventoryAsync(int productId);
}