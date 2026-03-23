using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using CaisseClaire.Orchestration.Services;
using CaisseClaire.Directive.ThemeManagement;

namespace CaisseClaire.Directive
{
    public partial class SettingsWindow : Window
    {
        private readonly SettingsOrchestrator _orchestrator;

        public SettingsWindow(SettingsOrchestrator orchestrator)
        {
            InitializeComponent();
            _orchestrator = orchestrator;

            LoadCurrentSettings();
        }

        private void LoadCurrentSettings()
        {
            var settings = _orchestrator.GetCurrentSettings();
            
            cmbTheme.SelectedIndex = settings.Theme == "Dark" ? 1 : 0;
            cmbLanguage.SelectedIndex = settings.Language == "fr" ? 1 : 0;
            sldFontScale.Value = settings.FontSizeScale;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string newTheme = cmbTheme.SelectedIndex == 1 ? "Dark" : "Light";
            string newLang = cmbLanguage.SelectedIndex == 1 ? "fr" : "en";
            double newScale = sldFontScale.Value;

            _orchestrator.SaveSettings(newTheme, newLang, newScale);

            // Apply immediately
            ThemeManager.ApplySettings(newTheme, newLang, newScale);

            lblMessage.Text = "Settings Saved!";
        }
    }
}
