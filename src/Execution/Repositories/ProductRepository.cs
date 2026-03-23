using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CaisseClaire.Execution.Entities;

namespace CaisseClaire.Execution.Repositories;

public class ProductRepository
{
    private readonly string _csvFilePath;
    private List<ProductEntity>? _cachedProducts;

    public ProductRepository(string csvFilePath = @"data\products.csv")
    {
        _csvFilePath = csvFilePath;
    }

    public List<ProductEntity> GetAllProducts()
    {
        if (_cachedProducts != null)
        {
            return _cachedProducts;
        }

        _cachedProducts = new List<ProductEntity>();

        if (!File.Exists(_csvFilePath))
        {
            return _cachedProducts;
        }

        var lines = File.ReadAllLines(_csvFilePath).Skip(1); // skip header
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(',');
            if (parts.Length >= 3)
            {
                if (decimal.TryParse(parts[2], out decimal price))
                {
                    _cachedProducts.Add(new ProductEntity
                    {
                        Code = parts[0].Trim(),
                        Name = parts[1].Trim(),
                        Price = price
                    });
                }
            }
        }

        return _cachedProducts;
    }

    public ProductEntity? GetProductByCode(string code)
    {
        return GetAllProducts().FirstOrDefault(p => p.Code == code);
    }

    public void AddProduct(ProductEntity product)
    {
        GetAllProducts().Add(product);
        SaveProducts();
    }

    public bool RemoveProduct(string code)
    {
        var list = GetAllProducts();
        var item = list.FirstOrDefault(p => p.Code == code);
        if (item != null)
        {
            list.Remove(item);
            SaveProducts();
            return true;
        }
        return false;
    }

    public void ReplaceAllProductsAndSave(IEnumerable<ProductEntity> newProducts)
    {
        _cachedProducts = newProducts.ToList();
        SaveProducts();
    }

    public void SaveProducts()
    {
        if (_cachedProducts == null) return;
        var lines = new List<string> { "Code,Name,Price" };
        foreach (var p in _cachedProducts)
        {
            lines.Add($"{p.Code},{p.Name},{p.Price}");
        }
        File.WriteAllLines(_csvFilePath, lines);
    }
}
