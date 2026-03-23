using CaisseClaire.Core.Models;

namespace CaisseClaire.Core.Interfaces;

/// <summary>
/// Defines the contract for cash register operations.
/// </summary>
public interface ICashRegisterService
{
    /// <summary>
    /// Creates a new transaction.
    /// </summary>
    Task<Transaction> CreateTransactionAsync();

    /// <summary>
    /// Adds a product to the current transaction.
    /// </summary>
    Task<Transaction> AddItemAsync(int transactionId, int productId, int quantity);

    /// <summary>
    /// Removes an item from a transaction.
    /// </summary>
    Task<Transaction> RemoveItemAsync(int transactionId, int itemId);

    /// <summary>
    /// Completes the transaction and processes payment.
    /// </summary>
    Task<Transaction> CompleteTransactionAsync(int transactionId, string paymentMethod);

    /// <summary>
    /// Cancels a pending transaction.
    /// </summary>
    Task<Transaction> CancelTransactionAsync(int transactionId);

    /// <summary>
    /// Retrieves a transaction by its ID.
    /// </summary>
    Task<Transaction?> GetTransactionAsync(int transactionId);

    /// <summary>
    /// Retrieves all transactions within a date range.
    /// </summary>
    Task<IEnumerable<Transaction>> GetTransactionsAsync(DateTime from, DateTime to);
}
