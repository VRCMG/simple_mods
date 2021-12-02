using MelonLoader;
using Main = WingStateSaver.Main;

[assembly: MelonInfo(typeof(Main), "WingStateSaver", "1.0.1", "abbey", "https://github.com/abbeybabbey/simple_mods")]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonPlatform(MelonPlatformAttribute.CompatiblePlatforms.WINDOWS_X64)]
[assembly: MelonPlatformDomain(MelonPlatformDomainAttribute.CompatibleDomains.IL2CPP)]
[assembly: VerifyLoaderVersion(0, 4, 3, true)]
[assembly: MelonProcess("VRChat.exe")]
[assembly: MelonGameVersion("1156")]