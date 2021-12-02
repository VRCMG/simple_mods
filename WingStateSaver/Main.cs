using System.Collections;
using System.Linq;
using MelonLoader;
using UnityEngine;
using UnityEngine.Events;
using VRC.UI.Elements;

namespace WingStateSaver
{
    public class Main : MelonMod
    {
        private static MelonPreferences_Entry<bool> _saveWingsStates;
        private static MelonPreferences_Entry<bool> _leftWingOpened;
        private static MelonPreferences_Entry<bool> _rightWingOpened;

        public override void OnApplicationStart()
        {
            var category = MelonPreferences.CreateCategory("WingStateSaver", "WingStateSaver");
            _saveWingsStates = category.CreateEntry("SaveWingStates", true);
            _leftWingOpened = category.CreateEntry("LeftWingOpened", false);
            _rightWingOpened = category.CreateEntry("RightWingOpened", false);

            MelonCoroutines.Start(OnQuickMenuInitiated());
        }

        private static IEnumerator OnQuickMenuInitiated()
        {
            while (Object.FindObjectOfType<VRC.UI.Elements.QuickMenu>() == null)
                yield return null;

            if (!_saveWingsStates.Value) yield break;

            var wings = Object.FindObjectsOfType<Wing>();

            if (wings == null) yield break;
            var leftWing = wings.FirstOrDefault(w => w.field_Public_WingPanel_0 == Wing.WingPanel.Left);
            var rightWing = wings.FirstOrDefault(w => w.field_Public_WingPanel_0 == Wing.WingPanel.Right);

            if (leftWing == null || rightWing == null) yield break;

            if (_leftWingOpened.Value) leftWing.field_Public_Button_0.Press();
            if (_rightWingOpened.Value) rightWing.field_Public_Button_0.Press();

            leftWing.field_Public_Button_0.m_OnClick.AddListener((UnityAction) LeftWingClick);

            rightWing.field_Public_Button_0.m_OnClick.AddListener((UnityAction) RightWingClick);
        }

        private static void LeftWingClick()
        {
            MelonPreferences.SetEntryValue(_leftWingOpened.Category.Identifier, _leftWingOpened.Identifier,
                !_leftWingOpened.Value);
        }

        private static void RightWingClick()
        {
            MelonPreferences.SetEntryValue(_rightWingOpened.Category.Identifier, _rightWingOpened.Identifier,
                !_rightWingOpened.Value);
        }
    }
}