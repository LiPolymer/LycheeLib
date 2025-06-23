using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using ClassIsland.Core.Abstractions.Services;
using ClassIsland.Core.Attributes;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;

namespace LycheeLib.Island;

[SettingsPageInfo("lycheeLib.main","LycheeLib",PackIconKind.MusicNote,PackIconKind.MusicNotePlus)]
public partial class SettingsPage {
    public SettingsPage(ILessonsService lessonService) {
        Settings = Config.Instance!;
        _lessonService =  lessonService;
        Settings.RestartNeeded += RequestRestart;
        InitializeComponent();
        UpdateMessage();
    }
    readonly ILessonsService _lessonService;
    [GeneratedRegex("[^0-9]+")]
    private static partial Regex NumberRegex();
    void TextBoxNumberCheck(object sender,TextCompositionEventArgs e) {
        Regex re = NumberRegex();
        e.Handled = re.IsMatch(e.Text);
    }
    public Config Settings { get; set; }
    public List<ProviderType> ProviderTypes { get; } = [
        ProviderType.LyricIsland,
        ProviderType.LxMusic
    ];

    
    void UpdateMessage(object? sender,EventArgs e) {
        UpdateMessage();
    }
    void UpdateMessage() {
        this.BeginInvoke(() => {
            MessageZone.Background = IslandLycheeBridger.Instance.Status ? new SolidColorBrush(Color.FromArgb(0x15,0x00,0xf0,0xff)) 
                : new SolidColorBrush(Color.FromArgb(0x15,0xff,0x00,0x00));
            ErrorMessage.Content = IslandLycheeBridger.Instance.LastMessage; 
        });
    }
    void SettingsPage_OnUnloaded(object sender,RoutedEventArgs e) {
        _lessonService.PostMainTimerTicked -= UpdateMessage;
    }
    void SettingsPage_OnLoaded(object sender,RoutedEventArgs e) {
        _lessonService.PostMainTimerTicked += UpdateMessage;
    }
}

public class EnumDescriptionConverter : IValueConverter {
    object IValueConverter.Convert(object? value,Type targetType,object? parameter,CultureInfo culture) {
        Enum myEnum = (Enum)value!;
        string description = GetEnumDescription(myEnum);
        return description;
    }

    object IValueConverter.ConvertBack(object? value,Type targetType,object? parameter,CultureInfo culture) {
        return string.Empty;
    }

    static string GetEnumDescription(Enum? enumObj) {
        FieldInfo? fieldInfo = enumObj!.GetType().GetField(enumObj.ToString());

        object[] attribArray = fieldInfo!.GetCustomAttributes(false);

        if (attribArray.Length == 0) {
            return enumObj.ToString();
        }
        DescriptionAttribute? attrib = attribArray[0] as DescriptionAttribute;
        return attrib!.Description;
    }
}
