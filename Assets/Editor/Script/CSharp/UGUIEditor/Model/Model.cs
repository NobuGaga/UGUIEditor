using UnityEngine;
using System;
using System.Collections.Generic;

namespace UGUIEditor {

    internal static class Model {

        private static Dictionary<EPrefabType, string> m_dicPrefabPath;

        static Model() {
            Array prefabTypes = Enum.GetValues(typeof(EPrefabType));
            m_dicPrefabPath = new Dictionary<EPrefabType, string>(prefabTypes.Length);
            for (int index = 0; index < prefabTypes.Length; index++) {
                EPrefabType prefabType = (EPrefabType)prefabTypes.GetValue(index);
                AddTempleteUI(prefabType, prefabType.ToString());
            }
        }

        private static void AddTempleteUI(EPrefabType prefabType, string prefabName) {
            string path = EditorPath.Combine(EditorPath.ProjectPathStart, EditorPath.TempleteUIAssetPath);
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