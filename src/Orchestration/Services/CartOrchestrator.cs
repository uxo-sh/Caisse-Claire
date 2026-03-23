using System;
using System.Linq;
using CaisseClaire.Orchestration.Models;
using CaisseClaire.Execution.Repositories;

namespace CaisseClaire.Orchestration.Services;

public class CartOrchestrator
{
    private readonly ProductRepository _productRepository;
    private CartState _currentState;
    private int _otherCounter = 1;

    public CartOrchestrator(ProductRepository productRepository)
    {
        _productRepository = productRepository;
        _currentState = new CartState();
    }

    public CartState GetCurrentState()
    {
        return _currentState;
    }

    public CartState AddToCart(string productCode, int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }

        var product = _productRepository.GetProductByCode(productCode);
        if (product == null)
        {
            throw new Exception($"Product with code '{productCode}' not found.");
        }

        var existingItem = _currentState.Items.FirstOrDefault(i => i.ProductCode == productCode);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            _currentState.Items.Add(new CartItem
            {
                ProductCode = product.Code,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = quantity
            });
        }

        return _currentState;
    }

    public CartState UpdateClientMoney(decimal amountGiven)
    {
        if (amountGiven < 0)
        {
            throw new ArgumentException("Client money cannot be negative.");
        }

        _currentState.ClientMoney = amountGiven;
        return _currentState;
    }

    public CartState RemoveFromCart(string productCode)
    {
        var existingItem = _currentState.Items.FirstOrDefault(i => i.ProductCode == productCode);
        if (existingItem != null)
        {
            _currentState.Items.Remove(existingItem);
        }
        return _currentState;
    }

    public CartState AddUnknownProduct(decimal price, int quantity)
    {
        if (price <= 0)
        {
            throw new ArgumentException("Price must be greater than zero.");
        }
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }

        _currentState.Items.Add(new CartItem
        {
            ProductCode = $"OTH-{_otherCounter}",
            ProductName = $"other_{_otherCounter}",
            UnitPrice = price,
            Quantity = quantity
        });

        _otherCounter++;
        return _currentState;
    }

    public CartState ClearCart()
    {
        _currentState = new CartState();
        return _currentState;
    }
}
