﻿using MelonLoader;
using Main = PissEmoji.Main;

[assembly: MelonInfo(typeof(Main), "Piss Emoji", "1.0.2", "abbey", "https://github.com/abbeybabbey/simple_mods")]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonPlatform(MelonPlatformAttribute.CompatiblePlatforms.UNIVERSAL)]
[assembly: MelonPlatformDomain(MelonPlatformDomainAttribute.CompatibleDomains.IL2CPP)]
[assembly: VerifyLoaderVersion(0, 4, 3, true)]
[assembly: MelonProcess("VRChat.exe")]
[assembly: MelonGameVersion("1156")]