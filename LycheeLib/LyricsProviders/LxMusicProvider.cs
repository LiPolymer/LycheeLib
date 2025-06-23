using System.Text.RegularExpressions;

namespace LycheeLib.LyricsProviders;

public partial class LxMusicProvider : ILyricsProvider {
    public LxMusicProvider(int port = 23330) {
        _port = port;
        new Thread(Listener).Start();
    }
    readonly int _port;
    public event Action<List<string>>? OnLyricsChanged;
    public string LastMessage { get; private set; } = string.Empty;
    public bool Status { get; private set; }
    bool _keepAlive = true;
    public void Shutdown() {
        _keepAlive = false;
    }
    readonly HttpClient _client = new HttpClient();
    string LxApiUrl { get => $"http://127.0.0.1:{_port.ToString()}/subscribe-player-status?filter=lyricLineAllText"; }
    void Listener() {
        while (_keepAlive) {
            try {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,LxApiUrl);
                request.Headers.Add("Accept","text/event-stream");
                using HttpResponseMessage response = _client.Send(request,HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                using Stream stream = response.Content.ReadAsStream();
                using StreamReader reader = new StreamReader(stream);
                LastMessage = "LXMusicAdapter: 已连接";
                Status = true;
                while (!reader.EndOfStream) {
                    string? line = reader.ReadLine();
                    if (line == null) continue;
                    if (!line.StartsWith("data:")) continue;
                    string data = LxExtractorRegex().Match(line).Groups[1].Value;
                    if (data.Contains("\\n")) {
                        string[] dataArray = data.Split("\\n");
                        OnLyricsChanged?.Invoke([dataArray[0],dataArray[1]]);
                    } else {
                        OnLyricsChanged?.Invoke([data,string.Empty]);
                    }
                }
            }
            catch (Exception e) {
                if (e.HResult == -2147467259) {
                    LastMessage = "LXMusicAdapter: 未连接";
                } else {
                    LastMessage = $"LXMusicAdapter: 出现错误: {e.Message}";
                    Status = false;
                }

            }
        }
    }

    [GeneratedRegex("\"(.*)\"$")]
    private static partial Regex LxExtractorRegex();
}