using ClassIsland.Core;
using ClassIsland.Core.Abstractions;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Extensions.Registry;
using ClassIsland.Shared;
using LycheeLib.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LycheeLib.Island;

[PluginEntrance]
public class Plugin : PluginBase {
    public override void Initialize(HostBuilderContext context,IServiceCollection services) {
        Config.ConfigFolder = PluginConfigFolder;
        services.AddSingleton<ILycheeLyrics, IslandLycheeBridger>();
        services.AddSettingsPage<SettingsPage>();
        Config.Load();

        AppBase.Current.AppStarted += (_,_) => {
            IAppHost.GetService<ILycheeLyrics>();
        };
    }
}