using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CaisseClaire.Core.Models;

namespace CaisseClaire.UI.ViewModels;

/// <summary>
/// Main ViewModel for the cash register interface.
/// Implements INotifyPropertyChanged for WPF data binding.
/// </summary>
public class MainViewModel : INotifyPropertyChanged
{
    private string _searchText = string.Empty;
    private decimal _total;
    private string _statusMessage = "Prêt";

    public MainViewModel()
    {
        // Initialize with sample data
        Products = new ObservableCollection<Product>
        {
            new() { Id = 1, Name = "Baguette", Price = 1.20m, Category = "Boulangerie" },
            new() { Id = 2, Name = "Croissant", Price = 1.10m, Category = "Boulangerie" },
            new() { Id = 3, Name = "Pain au chocolat", Price = 1.30m, Category = "Boulangerie" },
            new() { Id = 4, Name = "Lait 1L", Price = 1.05m, Category = "Boissons" },
            new() { Id = 5, Name = "Eau 1.5L", Price = 0.65m, Category = "Boissons" },
        };

        CartItems = new ObservableCollection<TransactionItem>();
    }

    public ObservableCollection<Product> Products { get; }

    public ObservableCollection<TransactionItem> CartItems { get; }

    public string SearchText
    {
        get => _searchText;
        set { _searchText = value; OnPropertyChanged(); }
    }

    public decimal Total
    {
        get => _total;
        set { _total = value; OnPropertyChanged(); }
    }

    public string StatusMessage
    {
        get => _statusMessage;
        set { _statusMessage = value; OnPropertyChanged(); }
    }

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}
