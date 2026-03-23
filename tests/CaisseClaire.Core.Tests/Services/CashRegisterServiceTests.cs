using Xunit;
using CaisseClaire.Core.Models;
using CaisseClaire.Core.Services;

namespace CaisseClaire.Core.Tests.Services;

public class CashRegisterServiceTests
{
    private readonly CashRegisterService _service;

    public CashRegisterServiceTests()
    {
        _service = new CashRegisterService();
    }

    [Fact]
    public async Task CreateTransaction_ShouldReturnPendingTransaction()
    {
        // Act
        var transaction = await _service.CreateTransactionAsync();

        // Assert
        Assert.NotNull(transaction);
        Assert.Equal(TransactionStatus.Pending, transaction.Status);
        Assert.Empty(transaction.Items);
    }

    [Fact]
    public async Task CompleteTransaction_ShouldSetStatusToCompleted()
    {
        // Arrange
        var transaction = await _service.CreateTransactionAsync();

        // Act
        var completed = await _service.CompleteTransactionAsync(transaction.Id, "Cash");

        // Assert
        Assert.Equal(TransactionStatus.Completed, completed.Status);
        Assert.Equal("Cash", completed.PaymentMethod);
    }

    [Fact]
    public async Task CancelTransaction_ShouldSetStatusToCancelled()
    {
        // Arrange
        var transaction = await _service.CreateTransactionAsync();

        // Act
        var cancelled = await _service.CancelTransactionAsync(transaction.Id);

        // Assert
        Assert.Equal(TransactionStatus.Cancelled, cancelled.Status);
    }

    [Fact]
    public async Task GetTransaction_WithInvalidId_ShouldReturnNull()
    {
        // Act
        var result = await _service.GetTransactionAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddItem_ToCompletedTransaction_ShouldThrow()
    {
        // Arrange
        var transaction = await _service.CreateTransactionAsync();
        await _service.CompleteTransactionAsync(transaction.Id, "Card");

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.AddItemAsync(transaction.Id, 1, 2));
    }
}
