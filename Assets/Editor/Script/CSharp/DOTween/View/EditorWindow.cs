using UnityEditor;
using UnityEngine;
using System;

namespace DOTweenExtension.Editor {

    internal class EditorWindow : BaseEditorWindow {

        private const string LabelTarget = "Target";
        private static Type TargetType = typeof(GameObject);
        private const string LabelID = "ID";
        private const string LabelSet = "Set";
        private const string LabelAutoPlay = "AutoPlay";
        private const string LabelDelay = "Delay";

        public static void Open(string title) => Open<EditorWindow>(title);

        private void OnGUI() => VerticalLayoutUI(EditorWindowUI);

        private void EditorWindowUI() {
            HorizontalLayoutUI(TitleUI);
            if (Model.IsNoAnimation)
                return;
            HorizontalLayoutUI(IDUI);
            HorizontalLayoutUI(SetAutoPlayUI);
            HorizontalLayoutUI(SetDelayUI);
        }

        private void TitleUI() {
            SpaceWithLabel(LabelTarget);
            Space();
            UnityEngine.Object gameObject = EditorGUILayout.ObjectField(Model.CurrentObject, TargetType, true);
            if (gameObject == null)
                return;
            Controller.SetGameObject(gameObject as GameObject);
        }

        private static string m_id;
        private static bool m_isAutoPlay;
        private static float m_delay;
        
        private void IDUI() {
            SpaceWithLabel(LabelID);
            m_id = TextField(m_id);
        }

        private void SetAutoPlayUI() {
            SpaceWithLabel(LabelAutoPlay);
            m_isAutoPlay = EditorGUILayout.Toggle(m_isAutoPlay);
            if (!SpaceWithButton(LabelSet))
                return;
            if (string.IsNullOrEmpty(m_id))
                Controller.SetAllAnimationAutoPlay(m_isAutoPlay);
            else
                Controller.SetAnimationAutoPlayByID(m_isAutoPlay, m_id);
        }
        private void SetDelayUI() {
            SpaceWithLabel(LabelDelay);
            m_delay = TextField(m_delay);
            if (!SpaceWithButton(LabelSet))
                return;
            if (string.IsNullOrEmpty(m_id))
                Controller.SetAllAnimationDelay(m_delay);
            else
                Controller.SetAnimationDelayByID(m_delay, m_id);
        }
    }
}