namespace LycheeLib.Interface;

public interface ILycheeLyrics {
    public List<string> Lyrics { get; }
    public event Action<List<string>>? OnLyricsChanged;
}