using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace UGUIEditor {

    internal static class Manager {

        private static Scene m_scene;
        private static Transform m_uiParent;

        public static void AddFullScreenWindow() {
            OpenEditorScene();
            Controller.AddGameObject(EPrefabType.FullScreenWindow);
	    }

        public static void AddWindow() {
            OpenEditorScene();
            Controller.AddGameObject(EPrefabType.Window);
	    }

        private static void OpenEditorScene() {
            if (m_scene.isLoaded)
                return;
            m_scene = EditorSceneManager.OpenScene(EditorPath.Scene);
            GameObject[] gameObjects = m_scene.GetRootGameObjects();
            if (gameObjects == null)
                return;
            for (int index = 0; index < gameObjects.Length; index++)
                if (FindMainUINode(gameObjects[index]))
                    break;
        }

        private static bool FindMainUINode(GameObject gameObject) {
            if (gameObject.name == EditorConst.MainUINodeName) {
                Controller.SetParent(gameObject.transform);
                return true;
            }
            Transform[] transforms = gameObject.GetComponentsInChildren<Transform>(true);
            if (transforms == null)
                return false;
            for (int index = 0; index < transforms.Length; index++)
                if (transforms[index].name == EditorConst.MainUINodeName) {
                    Controller.SetParent(transforms[index]);
                    return true;
                }
            return false;
        }
    }
}