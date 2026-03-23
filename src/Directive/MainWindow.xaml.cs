using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CaisseClaire.Execution.Repositories;
using CaisseClaire.Execution.Services;
using CaisseClaire.Orchestration.Services;
using CaisseClaire.Orchestration.Models;

namespace CaisseClaire.Directive
{
    public partial class MainWindow : Window
    {
        private readonly CartOrchestrator _cartOrchestrator;
        private readonly CatalogOrchestrator _catalogOrchestrator;
        private readonly SettingsOrchestrator _settingsOrchestrator;

        public MainWindow()
        {
            InitializeComponent();
            
            // Manual DI
            var productRepository = new ProductRepository(@"data\products.csv");
            var settingsRepository = new SettingsRepository(@"data\settings.json");
            var pdfService = new PdfExportService();
            _cartOrchestrator = new CartOrchestrator(productRepository);
            _catalogOrchestrator = new CatalogOrchestrator(productRepository, pdfService);
            _settingsOrchestrator = new SettingsOrchestrator(settingsRepository);
            
            UpdateUI();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            lblError.Text = "";
            try
            {
                var code = txtProductCode.Text.Trim();
                if (string.IsNullOrEmpty(code))
                {
                    lblError.Text = "Please enter a product code.";
                    return;
                }

                if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
                {
                    lblError.Text = "Quantity must be a valid positive integer.";
                    return;
                }

                _cartOrchestrator.AddToCart(code, quantity);
                txtProductCode.Clear();
                txtQuantity.Text = "1";
                txtProductCode.Focus();

                UpdateUI();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void txtProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnAddItem_Click(sender, e);
            }
        }

        private void btnAddCustom_Click(object sender, RoutedEventArgs e)
        {
            lblError.Text = "";
            try
            {
                if (!decimal.TryParse(txtCustomPrice.Text, out decimal price) || price <= 0)
                {
                    lblError.Text = "Price must be a valid positive number.";
                    return;
                }
                if (!int.TryParse(txtCustomQuantity.Text, out int qty) || qty <= 0)
                {
                    lblError.Text = "Quantity must be a valid positive integer.";
                    return;
                }

                _cartOrchestrator.AddUnknownProduct(price, qty);
                txtCustomPrice.Clear();
                txtCustomQuantity.Text = "1";
                UpdateUI();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgCart.SelectedItem is CartItem selected)
            {
                _cartOrchestrator.RemoveFromCart(selected.ProductCode);
                UpdateUI();
            }
            else
            {
                lblError.Text = "Please select an item in the cart to remove.";
            }
        }

        private void txtClientMoney_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblError.Text = "";
            try
            {
                var text = txtClientMoney.Text.Trim();
                decimal amountGiven = 0;
                
                if (!string.IsNullOrEmpty(text))
                {
                    if (!decimal.TryParse(text, out amountGiven) || amountGiven < 0)
                    {
                        lblError.Text = "Invalid client money format.";
                        return;
                    }
                }

                _cartOrchestrator.UpdateClientMoney(amountGiven);
                UpdateUI();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void btnManageCatalog_Click(object sender, RoutedEventArgs e)
        {
            var catalogWindow = new CatalogWindow(_catalogOrchestrator);
            catalogWindow.Owner = this;
            catalogWindow.ShowDialog();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(_settingsOrchestrator);
            settingsWindow.Owner = this;
            settingsWindow.ShowDialog();
        }

        private void UpdateUI()
        {
            var state = _cartOrchestrator.GetCurrentState();

            dgCart.ItemsSource = null;
            dgCart.ItemsSource = state.Items;

            lblTotal.Text = $"{state.TotalAmount:N0} MGA";
            lblClientGiven.Text = $"{state.ClientMoney:N0} MGA";
            lblChange.Text = $"{state.ChangeToReturn:N0} MGA";

            if (state.ClientMoney < state.TotalAmount && state.ClientMoney > 0)
            {
                lblChange.Foreground = System.Windows.Media.Brushes.Red;
                lblChange.Text = "Insufficient funds";
                lblError.Text = $"Missing {state.TotalAmount - state.ClientMoney:N0} MGA";
            }
            else
            {
                lblChange.Foreground = System.Windows.Media.Brushes.Green;
            }
        }
    }
}