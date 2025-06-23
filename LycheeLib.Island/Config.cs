using System.ComponentModel;
using System.IO;
using ClassIsland.Shared.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LycheeLib.Island;

public class Config : ObservableObject {
    // ReSharper disable once MemberCanBePrivate.Global
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

    //// ----------Data----------

    ProviderType _provider = ProviderType.LyricIsland;
    public ProviderType Provider {
        get => _provider;
        set {
            if (_provider == value) return;
            _provider = value;
            OnPropertyChanged();
            RestartNeeded?.Invoke();
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

    string _portOfLxMusic = "23330";
    public string PortOfLxMusic {
        get => _portOfLxMusic;
        set {
            if (_portOfLxMusic == value) return;
            _portOfLxMusic = value;
            OnPropertyChanged();
            RestartNeeded?.Invoke();
        }
    }
}