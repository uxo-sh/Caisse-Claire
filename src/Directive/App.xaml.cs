using System.Windows;
using CaisseClaire.Execution.Repositories;
using CaisseClaire.Orchestration.Services;
using CaisseClaire.Directive.ThemeManagement;

namespace CaisseClaire.Directive
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var settingsRepo = new SettingsRepository(@"data\settings.json");
            var orchestrator = new SettingsOrchestrator(settingsRepo);
            var currentSettings = orchestrator.GetCurrentSettings();

            ThemeManager.ApplySettings(currentSettings.Theme, currentSettings.Language, currentSettings.FontSizeScale);
        }
    }
}
