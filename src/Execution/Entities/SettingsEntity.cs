namespace CaisseClaire.Execution.Entities;

public class SettingsEntity
{
    public string Theme { get; set; } = "Light";
    public string Language { get; set; } = "en";
    public double FontSizeScale { get; set; } = 1.0;
}
