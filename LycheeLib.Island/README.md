# LycheeLib
⌈R0.11⌋

这是一个用于歌词接口提供的前置插件

目前支持LyricsIsland协议(BetterNCM/SimMusic) 和 LXMusic协议

*暂时没有插件使用本前置*

*(ExtraIsland将在下个版本将其作为对于歌词功能的可选前置)*

如果您想要尝试本前置,请为您的项目引入Nuget包 [LycheeLib.Interface](https://www.nuget.org/packages/LycheeLib.Interface/)

完成后,在插件入口使用以下代码来进行初始化
```csharp
using LycheeLib.Interface;
//...
//其他必要代码
//...
[PluginEntrance]
public class Plugin : PluginBase 
{
    public override void Initialize(HostBuilderContext context,IServiceCollection services) 
    {
        //...
        AppBase.Current.AppStarted += (_, _) => {
            Rendezvous.Load(IAppHost.GetService<ILycheeLyrics>());
        };
        //...
    }
}
```
您可以在程序各处使用 `Rendezvous` 静态类进行歌词操作

该类的核心为事件 `Rendezvous.OnLyricsChanged` ,其传出一个 `List<string>` 参数

例如:
```csharp
Rendezvous.OnLyricsChanged += lyricsList => {
    Console.WriteLine(lyricList[0]); //这是主要歌词
    Console.WriteLine(lyricList[1]); //这是次要歌词
};
```