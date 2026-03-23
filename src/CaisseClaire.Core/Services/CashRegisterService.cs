using CaisseClaire.Core.Interfaces;
using CaisseClaire.Core.Models;

namespace CaisseClaire.Core.Services;

/// <summary>
/// Implements cash register business logic.
/// </summary>
public class CashRegisterService : ICashRegisterService
{
    // TODO: Inject IProductRepository and ITransactionRepository via constructor
    private readonly List<Transaction> _transactions = new();
    private int _nextTransactionId = 1;

    public Task<Transaction> CreateTransactionAsync()
    {
        var transaction = new Transaction
        {
            Id = _nextTransactionId++,
            TransactionDate = DateTime.UtcNow,
            Status = TransactionStatus.Pending
        };

        _transactions.Add(transaction);
        return Task.FromResult(transaction);
    }

    public Task<Transaction> AddItemAsync(int transactionId, int productId, int quantity)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == transactionId)
            ?? throw new InvalidOperationException($"Transaction {transactionId} not found.");

        if (transaction.Status != TransactionStatus.Pending)
            throw new InvalidOperationException($"Transaction {transactionId} is not in Pending status.");

        // TODO: Look up product from repository to get name and price
        var item = new TransactionItem
        {
            Id = transaction.Items.Count + 1,
            ProductId = productId,
            ProductName = $"Product #{productId}",
            Quantity = quantity,
            UnitPrice = 0m // TODO: Get from product repository
        };

        transaction.Items.Add(item);
        return Task.FromResult(transaction);
    }

    public Task<Transaction> RemoveItemAsync(int transactionId, int itemId)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == transactionId)
            ?? throw new InvalidOperationException($"Transaction {transactionId} not found.");

        if (transaction.Status != TransactionStatus.Pending)
            throw new InvalidOperationException($"Transaction {transactionId} is not in Pending status.");

        var item = transaction.Items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            transaction.Items.Remove(item);
        }

        return Task.FromResult(transaction);
    }

    public Task<Transaction> CompleteTransactionAsync(int transactionId, string paymentMethod)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == transactionId)
            ?? throw new InvalidOperationException($"Transaction {transactionId} not found.");

        if (transaction.Status != TransactionStatus.Pending)
            throw new InvalidOperationException($"Transaction {transactionId} is not in Pending status.");

        transaction.PaymentMethod = paymentMethod;
        transaction.Status = TransactionStatus.Completed;

        return Task.FromResult(transaction);
    }

    public Task<Transaction> CancelTransactionAsync(int transactionId)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == transactionId)
            ?? throw new InvalidOperationException($"Transaction {transactionId} not found.");

        if (transaction.Status != TransactionStatus.Pending)
            throw new InvalidOperationException($"Cannot cancel a transaction that is not Pending.");

        transaction.Status = TransactionStatus.Cancelled;
        return Task.FromResult(transaction);
    }

    public Task<Transaction?> GetTransactionAsync(int transactionId)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == transactionId);
        return Task.FromResult(transaction);
    }

    public Task<IEnumerable<Transaction>> GetTransactionsAsync(DateTime from, DateTime to)
    {
        var results = _transactions
            .Where(t => t.TransactionDate >= from && t.TransactionDate <= to)
            .AsEnumerable();

        return Task.FromResult(results);
    }
}
