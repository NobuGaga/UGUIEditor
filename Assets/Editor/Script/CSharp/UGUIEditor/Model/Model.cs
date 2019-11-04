using UnityEngine;
using System.Collections.Generic;

namespace UGUIEditor {

    internal static class Model {

        private static Dictionary<EPrefabType, string> m_dicPrefabPath;

        static Model() {
            m_dicPrefabPath = new Dictionary<EPrefabType, string>(64);
            AddControlsTempleteUI(EPrefabType.FullScreenWindow, "Window");
            AddControlsTempleteUI(EPrefabType.Window, "Window");
        }

        private static void AddControlsTempleteUI(EPrefabType prefabType, string prefabName) {
            string path = EditorPath.Combine(EditorPath.ProjectPathStart, EditorPath.ControlsTempleteUIAssetPath);
            AddTempleteUI(prefabType, path, prefabName);
        }

        private static void AddModelTempleteUI(EPrefabType prefabType, string prefabName) {
            string path = EditorPath.Combine(EditorPath.ProjectPathStart, EditorPath.ModelTempleteUIAssetPath);
            AddTempleteUI(prefabType, path, prefabName);
        }

        private static void AddTempleteUI(EPrefabType prefabType, string path, string prefabName) {
            string prefabWithExt = Tool.GetNameWithExtension(prefabName, EditorConst.PrefabExtension);
            m_dicPrefabPath.Add(prefabType, EditorPath.Combine(path, prefabWithExt));
        }

        public static string GetPrefabPath(EPrefabType prefabType) {
            if (!m_dicPrefabPath.ContainsKey(prefabType)) {
                Debug.LogError("Prefab path is not add in Model Class");
                return string.Empty;
            }
            return m_dicPrefabPath[prefabType];
        }
    }
}