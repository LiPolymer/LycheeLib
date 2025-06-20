using LycheeLib.Interface;

namespace LycheeIslandLyrics;

public static class Rendezvous {
    static ILycheeLyrics? _lyricsInterface;
    public static event Action<List<string>>? OnLyricsChanged;
    
    public static void Load(ILycheeLyrics lyricsInterface) {
        if (_lyricsInterface != null) return;
        Console.WriteLine("Rendezvousing...");
        _lyricsInterface = lyricsInterface;
        _lyricsInterface.OnLyricsChanged += lyrics => {
            Console.WriteLine("INVOKED");
            OnLyricsChanged?.Invoke(lyrics);
        };
    }
}