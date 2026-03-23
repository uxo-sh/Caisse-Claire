using CaisseClaire.Data.Entities;

namespace CaisseClaire.Data.Repositories;

/// <summary>
/// In-memory implementation of <see cref="IProductRepository"/>.
/// Replace with Entity Framework Core implementation when database is configured.
/// </summary>
public class ProductRepository : IProductRepository
{
    // TODO: Replace with DbContext injection when EF Core is configured
    private readonly List<ProductEntity> _products = new();
    private int _nextId = 1;

    public Task<ProductEntity?> GetByIdAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }

    public Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        return Task.FromResult(_products.AsEnumerable());
    }

    public Task<IEnumerable<ProductEntity>> GetByCategoryAsync(string category)
    {
        var results = _products.Where(p =>
            string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(results);
    }

    public Task<ProductEntity?> GetByBarcodeAsync(string barcode)
    {
        var product = _products.FirstOrDefault(p =>
            string.Equals(p.Barcode, barcode, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(product);
    }

    public Task<ProductEntity> AddAsync(ProductEntity product)
    {
        product.Id = _nextId++;
        product.CreatedAt = DateTime.UtcNow;
        _products.Add(product);
        return Task.FromResult(product);
    }

    public Task<ProductEntity> UpdateAsync(ProductEntity product)
    {
        var existing = _products.FirstOrDefault(p => p.Id == product.Id)
            ?? throw new InvalidOperationException($"Product {product.Id} not found.");

        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Price = product.Price;
        existing.StockQuantity = product.StockQuantity;
        existing.Category = product.Category;
        existing.Barcode = product.Barcode;
        existing.IsActive = product.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        return Task.FromResult(existing);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return Task.FromResult(false);

        _products.Remove(product);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<ProductEntity>> SearchAsync(string searchTerm)
    {
        var results = _products.Where(p =>
            p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            (p.Description?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false));
        return Task.FromResult(results);
    }
}
