using System.Linq;
using System.Reflection;
using MelonLoader;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using VRC.UI;

// ReSharper disable InconsistentNaming

namespace PissEmoji
{
    public class Main : MelonMod
    {
        private static Sprite _pissSprite;

        public override void OnApplicationLateStart()
        {
            _pissSprite = LoadSprite("PissEmoji.piss_drops_border.png");

            //https://melonwiki.xyz/#/modders/xrefscanning?id=example always useful
            var initializeMethodInfo = typeof(EmojiManager).GetMethods()
                .First(mi => mi.Name.StartsWith("Method_Private_Void_") && XrefScanner.XrefScan(mi)
                    .Any(instance => instance.Type == XrefType.Global && instance.ReadAsObject() != null &&
                                     instance.ReadAsObject().ToString() == "UI.RecentlyUsedEmojiNames"));

            HarmonyInstance.Patch(typeof(EmojiManager).GetMethod(initializeMethodInfo.Name),
                typeof(Main).GetMethod(nameof(InitializePatch), BindingFlags.NonPublic | BindingFlags.Static)
                    ?.ToNewHarmonyMethod());
        }

        private static void InitializePatch(ref EmojiManager __instance)
        {
            foreach (var data in __instance.field_Private_List_1_EmojiData_0)
            {
                if (data.Name != "Splash") continue;
                data.SpawnablePrefab.GetComponent<Renderer>().material.color = Color.yellow;
                data.Image = _pissSprite;
            }
        }

        private Sprite LoadSprite(string manifestString)
        {
            var texture = new Texture2D(2, 2);
            using (var iconStream = Assembly.GetManifestResourceStream(manifestString))
            {
                if (iconStream != null)
                {
                    var buffer = new byte[iconStream.Length];
                    iconStream.Read(buffer, 0, buffer.Length);
                    // ReSharper disable once InvokeAsExtensionMethod
                    ImageConversion.LoadImage(texture, buffer);
                }
            }

            var rect = new Rect(0, 0, texture.width, texture.height);
            var pivot = new Vector2(0.5f, 0.5f);
            var border = Vector4.zero;
            var sprite = Sprite.CreateSprite_Injected(texture, ref rect, ref pivot, 50, 0, SpriteMeshType.Tight,
                ref border, false);
            sprite.hideFlags = HideFlags.DontUnloadUnusedAsset;
            return sprite;
        }
    }
}