using System.ComponentModel;
using System.IO;
using ClassIsland.Shared.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LycheeLib.Island;

public class Config : ObservableObject {
    public Config() {
        PropertyChanged += Save;
    }

    public static string ConfigFolder = string.Empty;
    static string ConfigPath { get => $"{ConfigFolder}/config.json"; }
    
    public static Config? Instance;
    
    public event Action? RestartNeeded;
    
    public static void Load() {
        try {
            Instance = ConfigureFileHelper.LoadConfig<Config>(ConfigPath);
        }
        catch (Exception ex) {
            Console.WriteLine($"[Lychee]加载配置文件失败: {ex.Message}");
            File.Delete(ConfigPath);
            Instance = new Config();
            Instance.Save();
        }
    }
    
    void Save(object? sender,PropertyChangedEventArgs propertyChangedEventArgs) {
        Save();
    }
    public void Save() {
        try {
            ConfigureFileHelper.SaveConfig(ConfigPath,this);
        }
        catch (Exception ex) {
            Console.WriteLine($"[Lychee]写入配置文件失败: {ex.Message}");
        }
    }

    string _portOfLyricIsland = "50063";
    public string PortOfLyricIsland {
        get => _portOfLyricIsland;
        set {
           if (_portOfLyricIsland == value) return;
           _portOfLyricIsland = value;
           OnPropertyChanged();
           RestartNeeded?.Invoke();
        }
    }
}