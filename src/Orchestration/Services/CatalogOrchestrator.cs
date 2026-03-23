using System.Collections.Generic;
using CaisseClaire.Execution.Repositories;
using CaisseClaire.Execution.Services;
using CaisseClaire.Execution.Entities;

namespace CaisseClaire.Orchestration.Services;

public class CatalogOrchestrator
{
    private readonly ProductRepository _repository;
    private readonly PdfExportService _pdfService;

    public CatalogOrchestrator(ProductRepository repository, PdfExportService pdfService)
    {
        _repository = repository;
        _pdfService = pdfService;
    }

    public List<ProductEntity> GetAllProducts()
    {
        return new List<ProductEntity>(_repository.GetAllProducts());
    }

    public void ReplaceCatalog(IEnumerable<ProductEntity> products)
    {
        _repository.ReplaceAllProductsAndSave(products);
    }

    public void AddProduct(string name, decimal price)
    {
        var products = GetAllProducts();
        int maxCode = 0;
        foreach (var p in products)
        {
            if (int.TryParse(p.Code, out int c) && c > maxCode)
            {
                maxCode = c;
            }
        }
        string newCode = (maxCode + 1).ToString("000");

        _repository.AddProduct(new ProductEntity
        {
            Code = newCode,
            Name = name,
            Price = price
        });
    }

    public void RemoveProduct(string code)
    {
        _repository.RemoveProduct(code);
    }

    public void ExportCatalogToPdf(string outputPath)
    {
        var products = GetAllProducts();
        _pdfService.ExportCatalogToPdf(products, outputPath);
    }
}
