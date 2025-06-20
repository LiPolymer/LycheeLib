namespace LycheeLib;

public class LycheeLyricsInstance {
    public List<string> Lyrics { get; set; } = [];
    public event Action<List<string>>? OnLyricsChanged;
    ILyricsProvider? _lyricsProvider;

    public void LoadProvider(ILyricsProvider lyricsProvider) {
        if (_lyricsProvider != null) _lyricsProvider.OnLyricsChanged -= Invoked;
        _lyricsProvider = lyricsProvider;
        _lyricsProvider.OnLyricsChanged += Invoked;
    }

    void Invoked(List<string> lyrics) {
        OnLyricsChanged?.Invoke(lyrics);
    }
    
    public bool HasProvider() {
        return _lyricsProvider != null;
    }
}

public interface ILyricsProvider {
    public event Action<List<string>>? OnLyricsChanged;
}