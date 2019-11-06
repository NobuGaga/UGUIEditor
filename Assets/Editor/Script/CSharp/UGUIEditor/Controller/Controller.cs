using UnityEditor;
using UnityEngine;

namespace UGUIEditor {

    internal static class Controller {

        private static Transform m_windowParent;
        private static GameObject m_lastWindow;

        public static void SetParent(Transform parent) {
            m_windowParent = parent;
            Selection.activeTransform = m_windowParent;
            // 切换新场景重置上一次窗口节点
            m_lastWindow = null;
        }

        public static void OpenFullWindow(EPrefabType prefabType) {
            if (m_lastWindow != null)
                Selection.activeGameObject = m_lastWindow;
            else
                m_lastWindow = AddGameObject(prefabType);
        }

        public static void CreateEmpty() {
            GameObject gameObject = ObjectFactory.CreateGameObject("GameObject");
            gameObject.name = "EmptyNode";
            RectTransform rect = gameObject.AddComponent<RectTransform>();
            rect.sizeDelta = Vector2.zero;
            gameObject.transform.SetParent(Selection.activeTransform);
            Normalize(gameObject);
            Selection.activeGameObject = gameObject;
        }

        public static GameObject AddGameObject(EPrefabType prefabType) {
            return AddPrefabGameObject(prefabType, false);
        }

        public static GameObject AddPrefabGameObject(EPrefabType prefabType) {
            return AddPrefabGameObject(prefabType, true);
        }

        private static GameObject AddPrefabGameObject(EPrefabType prefabType, bool isConnect) {
            string path = Model.GetPrefabPath(prefabType);
            if (string.IsNullOrEmpty(path))
                return null;
            GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (gameObject == null) {
                Debug.LogError("Load prefab is null. path = " + path);
                return null;
            }
            Transform parent;
            if (prefabType == EPrefabType.FullScreenWindow || prefabType == EPrefabType.Window)
                parent = m_windowParent;
            else
                parent = Selection.activeTransform;
            if (isConnect)
    	        gameObject = PrefabUtility.InstantiatePrefab(gameObject, parent) as GameObject;
            else
                gameObject = Object.Instantiate(gameObject, parent);
            gameObject.name = prefabType.ToString();
            Selection.activeGameObject = gameObject;
            Normalize(gameObject);
            return gameObject;
        }

        private static void Normalize(GameObject gameObject) {
            Transform transform =  gameObject.transform;
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            transform.localScale = Vector3.one;
            if (transform.parent == null)
                return;
            gameObject.layer = transform.parent.gameObject.layer;
        }
    }
}