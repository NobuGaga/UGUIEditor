using UnityEditor;
using UnityEngine;

namespace UGUIEditor {

    internal static class Controller {

        private static Transform m_parent;

        public static void SetParent(Transform parent) {
            m_parent = parent;
            Selection.activeTransform = m_parent;
        }

        public static void AddGameObject(EPrefabType prefabType) {
            AddPrefabGameObject(prefabType, false);
        }

        public static void AddPrefabGameObject(EPrefabType prefabType) {
            AddPrefabGameObject(prefabType, true);
        }

        private static void AddPrefabGameObject(EPrefabType prefabType, bool isConnect) {
            string path = Model.GetPrefabPath(prefabType);
            if (string.IsNullOrEmpty(path))
                return;
            GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (gameObject == null) {
                Debug.LogError("Load prefab is null. path = " + path);
                return;
            }
            if (isConnect)
    	        gameObject = PrefabUtility.InstantiatePrefab(gameObject, m_parent) as GameObject;
            else
                gameObject = Object.Instantiate(gameObject, m_parent);
            gameObject.name = prefabType.ToString();
            Transform transform =  gameObject.transform;
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            transform.localScale = Vector3.one;
            Selection.activeTransform = transform;
        }
    }
}