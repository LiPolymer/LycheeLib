using LycheeLib.Interface;
using LycheeLib.LyricsProviders;

namespace LycheeLib.ClassIsland;

public class IslandLycheeBridger : ILycheeLyrics {
    public IslandLycheeBridger() {
        //start:测试
        if (!Instance.HasProvider()) {
            Instance.LoadProvider(new LyricIslandProvider());
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