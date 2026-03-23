namespace CaisseClaire.Core.Models;

/// <summary>
/// Represents a sales transaction.
/// </summary>
public class Transaction
{
    public int Id { get; set; }

    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

    public List<TransactionItem> Items { get; set; } = new();

    public decimal TotalAmount => Items.Sum(i => i.Subtotal);

    public string? PaymentMethod { get; set; }

    public string? CashierName { get; set; }

    public string? Notes { get; set; }

    public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
}

/// <summary>
/// Represents a single item within a transaction.
/// </summary>
public class TransactionItem
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Subtotal => Quantity * UnitPrice;
}

/// <summary>
/// Status of a transaction.
/// </summary>
public enum TransactionStatus
{
    Pending,
    Completed,
    Cancelled,
    Refunded
}
