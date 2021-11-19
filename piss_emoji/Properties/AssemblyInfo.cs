using MelonLoader;
using Main = piss_emoji.Main;

[assembly: MelonInfo(typeof(Main), "Piss Emoji", "1.0.1", "abbey", "https://github.com/abbeybabbey/vrc_piss_emoji")]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonPlatform(MelonPlatformAttribute.CompatiblePlatforms.WINDOWS_X64)]
[assembly: MelonPlatformDomain(MelonPlatformDomainAttribute.CompatibleDomains.IL2CPP)]
[assembly: VerifyLoaderVersion(0, 4, 3, true)]
[assembly: MelonProcess("VRChat.exe")]
[assembly: MelonGameVersion("1151")]