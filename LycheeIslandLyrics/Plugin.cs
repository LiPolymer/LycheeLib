using ClassIsland.Core;
using ClassIsland.Core.Abstractions;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Controls.CommonDialog;
using ClassIsland.Core.Extensions.Registry;
using ClassIsland.Shared;
using LycheeLib.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LycheeIslandLyrics;

[PluginEntrance]
// ReSharper disable once UnusedType.Global
public class Plugin : PluginBase {
    public override void Initialize(HostBuilderContext context,IServiceCollection services) {
        services.AddComponent<LyricDebugger>();
        AppBase.Current.AppStarted += (_, _) => {
            Rendezvous.Load(IAppHost.GetService<ILycheeLyrics>());
        };
    }
}