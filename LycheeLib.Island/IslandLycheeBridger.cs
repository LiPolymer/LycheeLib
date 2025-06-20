using ClassIsland.Core;
using LycheeLib.Interface;
using LycheeLib.LyricsProviders;

namespace LycheeLib.Island;

public class IslandLycheeBridger : ILycheeLyrics {
    public IslandLycheeBridger() {
        //start:测试
        if (!Instance.HasProvider()) {
            try {
                if (Convert.ToInt64(Config.Instance!.PortOfLyricIsland) < 10000 || Convert.ToInt64(Config.Instance!.PortOfLyricIsland) > 65535) {
                    Config.Instance!.PortOfLyricIsland = "50063";
                }
            }
            catch {
                Config.Instance!.PortOfLyricIsland = "50063";
            }
            AppBase.Current.AppStopping += (_,_) => {
                Console.WriteLine("检测到Lychee LyricsIsland协议接口启动,5秒后将强制结束进程");
                new Thread(() => {
                    Thread.Sleep(5000);
                    Console.WriteLine("正在关闭...");
                    Environment.Exit(0);
                }).Start();
            };
            Instance.LoadProvider(new LyricIslandProvider($"http://127.0.0.1:{Config.Instance!.PortOfLyricIsland}/"));
        }
        //end
        Instance.OnLyricsChanged += lyrics => {
            Lyrics = lyrics;
            OnLyricsChanged?.Invoke(Lyrics);
        };
    }

    static readonly LycheeLyricsInstance Instance = new LycheeLyricsInstance();
    public List<string> Lyrics { private set; get; } = [];
    public event Action<List<string>>? OnLyricsChanged;
}