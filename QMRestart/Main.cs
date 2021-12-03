using System;
using System.Collections;
using System.Diagnostics;
using MelonLoader;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VRC.UI.Elements;
using Object = UnityEngine.Object;

namespace QMRestart
{
    public class Main : MelonMod
    {
        private static GameObject _exitButtonGameObject;
        private static Sprite _restartSprite;

        public override void OnApplicationStart()
        {
            _restartSprite = LoadSprite("QMRestart.update-arrow.png");
            MelonCoroutines.Start(OnQuickMenuInitiated());
        }

        private static IEnumerator OnQuickMenuInitiated()
        {
            while (Object.FindObjectOfType<VRC.UI.Elements.QuickMenu>() == null)
                yield return null;

            _exitButtonGameObject =
                GameObject.Find(
                    "UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Settings/QMHeader_H1/RightItemContainer/Button_QM_Exit");
            var restartVRChatButton = Object.Instantiate(_exitButtonGameObject,
                GameObject.Find(
                        "UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Settings/QMHeader_H1/RightItemContainer")
                    .transform);
            restartVRChatButton.name = "Button_QM_Restart";
            restartVRChatButton.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 =
                "Restart VRChat";
            restartVRChatButton.transform.GetChild(0).GetComponent<Image>().sprite = _restartSprite;

            static void Restart()
            {
                ModalDialog("Restart", "Really restart VRChat?", () =>
                {
                    Application.Quit();
                    Process.Start(Environment.CurrentDirectory + "\\VRChat.exe", Environment.CommandLine);
                });
            }

            restartVRChatButton.GetComponent<Button>().onClick.AddListener((UnityAction) Restart);
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

        private static void ModalDialog(string title, string body, Action acceptAction, Action declineAction = null)
        {
            Object.FindObjectOfType<UIMenu>()
                .Method_Public_Void_String_String_Action_Action_PDM_0(title, body, acceptAction, declineAction);
        }
    }
}