using System;
using System.Linq;
using System.Windows;
using MaterialDesignThemes.Wpf;

namespace CaisseClaire.Directive.ThemeManagement
{
    public static class ThemeManager
    {
        public static void ApplySettings(string theme, string language, double fontScale)
        {
            var paletteHelper = new PaletteHelper();
            var mdTheme = paletteHelper.GetTheme();
            mdTheme.SetBaseTheme(theme == "Dark" ? BaseTheme.Dark : BaseTheme.Light);
            paletteHelper.SetTheme(mdTheme);

            var dictionaries = Application.Current.Resources.MergedDictionaries;
            var langDict = dictionaries.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Strings_"));
            if (langDict != null)
            {
                dictionaries.Remove(langDict);
            }
            
            string langSource = language == "fr" ? "pack://application:,,,/Themes/Strings_fr.xaml" : "pack://application:,,,/Themes/Strings_en.xaml";
            dictionaries.Add(new ResourceDictionary { Source = new Uri(langSource, UriKind.Absolute) });

            Application.Current.Resources["GlobalFontSize"] = 14.0 * fontScale;
            Application.Current.Resources["TitleFontSize"] = 20.0 * fontScale;
        }
    }
}
