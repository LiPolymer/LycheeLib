using System.Windows;
using ClassIsland.Core.Abstractions;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Extensions.Registry;
using LycheeLib.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LycheeLib.Island;

[PluginEntrance]
public class Plugin : PluginBase {
    public override void Initialize(HostBuilderContext context,IServiceCollection services) {
        Config.ConfigFolder = PluginConfigFolder;
        services.AddSingleton<ILycheeLyrics, IslandLycheeBridger>();
        services.AddSettingsPage<SettingsPage>();
        Config.Load();
    }
}