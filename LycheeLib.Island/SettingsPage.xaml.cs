using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Attributes;
using MaterialDesignThemes.Wpf;

namespace LycheeLib.Island;

[SettingsPageInfo("lycheeLib.main","LycheeLib",PackIconKind.MusicNote,PackIconKind.MusicNotePlus)]
public partial class SettingsPage {
    public SettingsPage() {
        Settings = Config.Instance!;
        Settings.RestartNeeded += RequestRestart;
        InitializeComponent();
        MessageZone.Visibility = IslandLycheeBridger.Instance.Status ? Visibility.Collapsed : Visibility.Visible;
        ErrorMessage.Content = IslandLycheeBridger.Instance.LastMessage;
    }
    
    [GeneratedRegex("[^0-9]+")]
    private static partial Regex NumberRegex();
    void TextBoxNumberCheck(object sender,TextCompositionEventArgs e) {
        Regex re = NumberRegex();
        e.Handled = re.IsMatch(e.Text);
    }
    
    public Config Settings { get; set; }
}