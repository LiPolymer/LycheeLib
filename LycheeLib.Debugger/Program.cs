using LycheeLib.LyricsProviders;

namespace LycheeLib.Debugger;

static class Program {
    static void Main(string[] args) {
        Console.WriteLine("开始接收");
        LycheeLyricsInstance iInstance = new LycheeLyricsInstance();
        iInstance.LoadProvider(new LyricIslandProvider());
        LycheeLyricsInstance lInstance = new LycheeLyricsInstance();
        lInstance.LoadProvider(new LxMusicProvider());
        iInstance.OnLyricsChanged += lyrics => {
            Console.WriteLine("[I]" + lyrics[0]);
            Console.WriteLine(" - " + lyrics[1]);
        };
        lInstance.OnLyricsChanged += lyrics => {
            Console.WriteLine("[L]" + lyrics[0]);
            Console.WriteLine(" - " + lyrics[1]);
        };
    }
}