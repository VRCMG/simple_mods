using System.Collections;
using System.Reflection;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable InconsistentNaming

namespace piss_emoji
{
    public class Main : MelonMod
    {
        private static Sprite _pissSprite;

        public override void OnApplicationLateStart()
        {
            HarmonyInstance.Patch(typeof(VRCPlayer).GetMethod(nameof(VRCPlayer.SpawnEmojiRPC)),
                typeof(Main).GetMethod(nameof(SpawnEmojiRPCPatch), BindingFlags.NonPublic | BindingFlags.Static)
                    ?.ToNewHarmonyMethod());
            _pissSprite = LoadSprite("piss_emoji.piss_drops_border.png");
            MelonCoroutines.Start(OnVRCUiManagerInit());
        }

        private static IEnumerator OnVRCUiManagerInit()
        {
            while (VRCUiManager.field_Private_Static_VRCUiManager_0 == null)
                yield return null;

            var pissEmojiIcon = GameObject.Find("UserInterface/QuickMenu/EmojiMenu/Page6/EmojiButton9");
            pissEmojiIcon.GetComponent<Image>().sprite = _pissSprite;
        }

        private static void SpawnEmojiRPCPatch(ref VRCPlayer __instance, int param_1)
        {
            if (param_1 != 56) return;
            var pissEmoji = __instance.field_Private_EmojiGenerator_0
                .field_Public_ArrayOf_GameObject_0[param_1].GetComponent<Renderer>();
            pissEmoji.material.color = Color.yellow;
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
