using ClassIsland.Core;
using LycheeLib.Interface;
using LycheeLib.LyricsProviders;

namespace LycheeLib.Island;

public class IslandLycheeBridger : ILycheeLyrics {
    public IslandLycheeBridger() {
        //start:测试
        if (!Instance.HasProvider()) {
            if (!IsPortInRange(Config.Instance!.PortOfLyricIsland)) {
                Config.Instance.PortOfLyricIsland = "50063";
            }
            if (!IsPortInRange(Config.Instance.PortOfLxMusic)) {
                Config.Instance.PortOfLxMusic = "23330";
            }
            AppBase.Current.AppStopping += (_,_) => {
                Console.WriteLine("Lychee协议接口启动,未能正常关闭的Https客户端将阻止程序退出,5秒后将强制结束进程");
                new Thread(() => {
                    Thread.Sleep(5000);
                    Console.WriteLine("正在关闭...");
                    Environment.Exit(0);
                }).Start();
            };
            switch (Config.Instance.Provider) {
                case ProviderType.LyricIsland:
                    Instance.LoadProvider(new LyricIslandProvider($"http://127.0.0.1:{Config.Instance!.PortOfLyricIsland}/"));
                    break;
                case ProviderType.LxMusic:
                    Instance.LoadProvider(new LxMusicProvider(Convert.ToInt32(Config.Instance.PortOfLxMusic)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        //end
        Instance.OnLyricsChanged += lyrics => {
            Lyrics = lyrics;
            OnLyricsChanged?.Invoke(Lyrics);
        };
    }

    static bool IsPortInRange(string port) {
        try {
            return !(Convert.ToInt64(port) < 10000 || Convert.ToInt64(port) > 65535);
        }
        catch {
            return false;
        }
    }
    
    public static readonly LycheeLyricsInstance Instance = new LycheeLyricsInstance();
    public List<string> Lyrics { private set; get; } = [];
    public event Action<List<string>>? OnLyricsChanged;
}