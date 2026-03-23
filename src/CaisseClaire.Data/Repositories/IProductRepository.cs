using CaisseClaire.Data.Entities;

namespace CaisseClaire.Data.Repositories;

/// <summary>
/// Defines the contract for product data access operations.
/// </summary>
public interface IProductRepository
{
    Task<ProductEntity?> GetByIdAsync(int id);

    Task<IEnumerable<ProductEntity>> GetAllAsync();

    Task<IEnumerable<ProductEntity>> GetByCategoryAsync(string category);

    Task<ProductEntity?> GetByBarcodeAsync(string barcode);

    Task<ProductEntity> AddAsync(ProductEntity product);

    Task<ProductEntity> UpdateAsync(ProductEntity product);

    Task<bool> DeleteAsync(int id);

    Task<IEnumerable<ProductEntity>> SearchAsync(string searchTerm);
}
