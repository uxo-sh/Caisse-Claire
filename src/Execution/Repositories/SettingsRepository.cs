using System.IO;
using System.Text.Json;
using CaisseClaire.Execution.Entities;

namespace CaisseClaire.Execution.Repositories;

public class SettingsRepository
{
    private readonly string _filePath;

    public SettingsRepository(string filePath = @"data\settings.json")
    {
        _filePath = filePath;
    }

    public SettingsEntity GetSettings()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<SettingsEntity>(json) ?? new SettingsEntity();
        }
        return new SettingsEntity();
    }

    public void SaveSettings(SettingsEntity settings)
    {
        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}
