using System;
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using CaisseClaire.Execution.Entities;
using CaisseClaire.Orchestration.Services;
using CaisseClaire.Directive.ViewModels;

namespace CaisseClaire.Directive
{
    public partial class CatalogWindow : Window
    {
        private readonly CatalogOrchestrator _catalogOrchestrator;
        private ObservableCollection<ProductViewModel> _catalogItems;

        public CatalogWindow(CatalogOrchestrator catalogOrchestrator)
        {
            InitializeComponent();
            _catalogOrchestrator = catalogOrchestrator;
            _catalogItems = new ObservableCollection<ProductViewModel>();
            LoadCatalog();
        }

        private void LoadCatalog()
        {
            var products = _catalogOrchestrator.GetAllProducts();
            _catalogItems.Clear();
            foreach(var p in products)
            {
                _catalogItems.Add(new ProductViewModel(p));
            }
            dgCatalog.ItemsSource = _catalogItems;
            EvaluateDeletionState();
        }

        private void EvaluateDeletionState()
        {
            bool hasMarked = _catalogItems.Any(i => i.IsMarkedForDeletion);
            btnConfirmDeletion.Visibility = hasMarked ? Visibility.Visible : Visibility.Collapsed;
        }

        private void btnMarkForDeletion_Click(object sender, RoutedEventArgs e)
        {
            lblMessage.Text = "";
            if (dgCatalog.SelectedItem is ProductViewModel selected)
            {
                selected.IsMarkedForDeletion = !selected.IsMarkedForDeletion;
                EvaluateDeletionState();
            }
            else
            {
                lblMessage.Foreground = System.Windows.Media.Brushes.Red;
                lblMessage.Text = "Please select a product.";
            }
        }

        private void btnConfirmDeletion_Click(object sender, RoutedEventArgs e)
        {
            lblMessage.Text = "";
            var toDelete = _catalogItems.Where(i => i.IsMarkedForDeletion).ToList();
            if (toDelete.Any())
            {
                foreach (var item in toDelete)
                {
                    _catalogItems.Remove(item);
                }
                SaveCurrentOrder(); // Extract logic to avoid null parameters
                lblMessage.Foreground = System.Windows.Media.Brushes.Green;
                lblMessage.Text = $"{toDelete.Count} product(s) deleted.";
                EvaluateDeletionState();
            }
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            SaveCurrentOrder();
            lblMessage.Foreground = System.Windows.Media.Brushes.Green;
            lblMessage.Text = "Catalog order saved.";
        }

        private void SaveCurrentOrder()
        {
            var entities = _catalogItems.Select(vm => vm.GetEntity()).ToList();
            _catalogOrchestrator.ReplaceCatalog(entities);
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            lblMessage.Text = "";
            try
            {
                var name = txtAddName.Text.Trim();
                if (string.IsNullOrEmpty(name))
                {
                    lblMessage.Text = "Name cannot be empty.";
                    return;
                }

                if (!decimal.TryParse(txtAddPrice.Text, out decimal price) || price < 0)
                {
                    lblMessage.Text = "Invalid price.";
                    return;
                }

                _catalogOrchestrator.AddProduct(name, price);
                
                txtAddName.Clear();
                txtAddPrice.Clear();
                LoadCatalog();
                
                lblMessage.Foreground = System.Windows.Media.Brushes.Green;
                lblMessage.Text = "Product added successfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Foreground = System.Windows.Media.Brushes.Red;
                lblMessage.Text = ex.Message;
            }
        }

        private void btnExportPdf_Click(object sender, RoutedEventArgs e)
        {
            lblMessage.Text = "";
            try
            {
                var dialog = new Microsoft.Win32.SaveFileDialog
                {
                    FileName = "CatalogExport",
                    DefaultExt = ".pdf",
                    Filter = "PDF documents (.pdf)|*.pdf"
                };

                if (dialog.ShowDialog() == true)
                {
                    _catalogOrchestrator.ExportCatalogToPdf(dialog.FileName);
                    lblMessage.Foreground = System.Windows.Media.Brushes.Green;
                    lblMessage.Text = $"Exported to {dialog.FileName}";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Foreground = System.Windows.Media.Brushes.Red;
                lblMessage.Text = "Export failed: " + ex.Message;
            }
        }
    }
}
