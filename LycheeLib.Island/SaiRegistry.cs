using ClassIsland.Core.Icons;
using ClassIsland.Shared;
using LycheeLib.Interface;
using LycheeLib.Island.Models;
using SuperAutoIsland.Interface.Metadata;
using SuperAutoIsland.Interface.Services;
using SuperAutoIsland.Interface.Services.Automations;

namespace LycheeLib.Island;

public static class SaiRegistry {
    public static void Register() {
        ISaiServer server = IAppHost.GetService<ISaiServer>();

        server.RegisterBlocks("LycheeLib", it => it
            .AddLabel("LycheeLib")
            .AddBlock<LyricsFetcherBlock>()
        );
    }
}

public class LyricsFetcherBlock : DataBlockBase {
    public override string Id { get => "lychee.data.fetch"; }
    public override string Name { get => "获取歌词"; }
    public override (string, string) Icon { get => ("音符",FluentIcons.MusicNote1Regular); }
    public override string DataOutput { get => "String"; }
    public override Type SettingsType { get => typeof(SaiLyricsFetcherConfig); }

    public override void GetFields(FieldsRegister it) => it
        .AddField("Line", BasicFields.Dropdown("", [
            ("主行", "0"),
            ("副行", "1")
        ], true));

    public override Task<object> Handler(object? data) {
        if (data is not SaiLyricsFetcherConfig config) {
            return Task.FromResult<object>("???");
        }

        ILycheeLyrics service = IAppHost.GetService<ILycheeLyrics>();
        return Task.FromResult<object>(service.Lyrics.Count <= config.Line ? "未载入" : service.Lyrics[config.Line]);
    }
}
