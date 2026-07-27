using ClassIsland.Core.Icons;
using ClassIsland.Shared;
using LycheeLib.Interface;
using LycheeLib.Island.Models;
using SuperAutoIsland.Interface;
using SuperAutoIsland.Interface.MetaData;
using SuperAutoIsland.Interface.MetaData.ArgsType;
using SuperAutoIsland.Interface.Services;

namespace LycheeLib.Island;

public static class SaiRegistry {
    public static void Register() {
        ISaiServer server = IAppHost.GetService<ISaiServer>();
        
        server.RegisterBlocks("LycheeLib", new RegisterData
        {
            Actions = [],
            Rules = [],
            Data = [
                new BlockMetadata {
                    Id = "lychee.data.fetch",
                    Name = "获取歌词",
                    Icon = ("音符", FluentIcons.MusicNote1Regular),
                    Args = new Dictionary<string,MetaArgsBase> {
                        ["Line"] = new DropDownMetaArgs {
                            Name = "",
                            Type = MetaType.dropdown,
                            Options = [
                                ("主行", "0"),
                                ("副行", "1")
                            ]
                        }
                    },
                    DropdownUseNumbers = true,
                    InlineBlock = true,
                    InlineField = true
                }
            ]
        });
        
        server.RegisterDataGetter<SaiLyricsFetcherConfig>("lychee.data.fetch",data => {
            if (data is not SaiLyricsFetcherConfig config) {
                return Task.FromResult("???");
            }

            ILycheeLyrics service = IAppHost.GetService<ILycheeLyrics>();
            return Task.FromResult(service.Lyrics.Count <= config.Line ? "未载入" : service.Lyrics[config.Line]);
        });
    }
}