using System.Windows;
using System.Windows.Controls;
using ClassIsland.Core.Attributes;
using ClassIsland.Shared;
using LycheeLib.Interface;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;

namespace LycheeIslandLyrics;

[ComponentInfo("82134843-6b3b-4ad3-9674-389845493492",
                  "Lychee测试",
                  PackIconKind.MusicNote,
                  "在主界面上显示来自音乐软件的歌词。")]
public partial class LyricDebugger {
    public LyricDebugger() {
        InitializeComponent();
    }
    
    void UpdateLyrics(List<string> lyrics) {
        this.Invoke(() => {
            LyricsText.Text = lyrics[0];
        });
    }
    void LyricDebugger_OnUnloaded(object sender,RoutedEventArgs e) {
        Rendezvous.OnLyricsChanged -= UpdateLyrics;
    }
    void LyricDebugger_OnLoaded(object sender,RoutedEventArgs e) {
        Rendezvous.OnLyricsChanged += UpdateLyrics;
    }
}