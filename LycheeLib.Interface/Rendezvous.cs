namespace LycheeLib.Interface;

public static class Rendezvous {
    static ILycheeLyrics? _lyricsInterface;
    public static event Action<List<string>>? OnLyricsChanged;
    
    public static void Load(ILycheeLyrics lyricsInterface) {
        if (_lyricsInterface != null) return;
        _lyricsInterface = lyricsInterface;
        _lyricsInterface.OnLyricsChanged += lyrics => {
            OnLyricsChanged?.Invoke(lyrics);
        };
    }
}