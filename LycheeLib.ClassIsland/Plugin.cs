using System.Windows;
using ClassIsland.Core.Abstractions;
using ClassIsland.Core.Attributes;
using LycheeLib.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LycheeLib.ClassIsland;

[PluginEntrance]
public class Plugin : PluginBase {
    public override void Initialize(HostBuilderContext context,IServiceCollection services) {
        services.AddSingleton<ILycheeLyrics, IslandLycheeBridger>();
    }
}