namespace CaisseClaire.Core.Exceptions;

/// <summary>
/// Base exception for CaisseClaire domain errors.
/// </summary>
public class CaisseClaireException : Exception
{
    public CaisseClaireException() { }

    public CaisseClaireException(string message) : base(message) { }

    public CaisseClaireException(string message, Exception innerException)
        : base(message, innerException) { }
}

/// <summary>
/// Thrown when a product is not found.
/// </summary>
public class ProductNotFoundException : CaisseClaireException
{
    public int ProductId { get; }

    public ProductNotFoundException(int productId)
        : base($"Product with ID {productId} was not found.")
    {
        ProductId = productId;
    }
}

/// <summary>
/// Thrown when a transaction operation is invalid.
/// </summary>
public class InvalidTransactionException : CaisseClaireException
{
    public int TransactionId { get; }

    public InvalidTransactionException(int transactionId, string reason)
        : base($"Transaction {transactionId}: {reason}")
    {
        TransactionId = transactionId;
    }
}

/// <summary>
/// Thrown when stock is insufficient for an operation.
/// </summary>
public class InsufficientStockException : CaisseClaireException
{
    public int ProductId { get; }
    public int RequestedQuantity { get; }
    public int AvailableQuantity { get; }

    public InsufficientStockException(int productId, int requested, int available)
        : base($"Insufficient stock for product {productId}: requested {requested}, available {available}.")
    {
        ProductId = productId;
        RequestedQuantity = requested;
        AvailableQuantity = available;
    }
}
