namespace CaisseClaire.Data.Entities;

/// <summary>
/// Database entity representing a product.
/// Maps to the Products table in the database.
/// </summary>
public class ProductEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public string? Category { get; set; }

    public string? Barcode { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}
