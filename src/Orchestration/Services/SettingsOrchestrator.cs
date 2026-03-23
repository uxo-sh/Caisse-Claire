using CaisseClaire.Execution.Entities;
using CaisseClaire.Execution.Repositories;

namespace CaisseClaire.Orchestration.Services;

public class SettingsOrchestrator
{
    private readonly SettingsRepository _repository;
    private SettingsEntity _currentSettings;

    public SettingsOrchestrator(SettingsRepository repository)
    {
        _repository = repository;
        _currentSettings = _repository.GetSettings();
    }

    public SettingsEntity GetCurrentSettings() => _currentSettings;

    public void SaveSettings(string theme, string language, double fontSizeScale)
    {
        _currentSettings.Theme = theme;
        _currentSettings.Language = language;
        _currentSettings.FontSizeScale = fontSizeScale;
        _repository.SaveSettings(_currentSettings);
    }
}
