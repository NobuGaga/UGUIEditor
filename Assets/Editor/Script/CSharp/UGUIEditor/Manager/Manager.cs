using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace UGUIEditor {

    internal static class Manager {

        private static Scene m_scene;
        public static Transform WindowParent => Controller.WindowParent;
        
        public static void AddFullScreenWindow() {
            OpenEditorScene();
            Controller.OpenFullWindow(EPrefabType.FullScreenWindow);
	    }

        public static void AddWindow() {
            OpenEditorScene();
            Controller.AddGameObject(EPrefabType.Window);
	    }

        public static void AddEmptyGameObject() {
            Controller.CreateEmpty();
	    }

        public static void AddImage() {
            Controller.AddGameObject(EPrefabType.Image);
        }

        public static void AddRawImage() {
            Controller.AddGameObject(EPrefabType.RawImage);
        }

        public static void AddText() {
            Controller.AddGameObject(EPrefabType.Text);
        }

        public static void AddInputText() {
            Controller.AddGameObject(EPrefabType.InputText);
        }

        public static void AddStyleOneButton() {
            Controller.AddGameObject(EPrefabType.StyleOneButton);
        }

        public static void AddToggle() {
            Controller.AddGameObject(EPrefabType.Toggle);
        }

        public static void AddProcess() {
            Controller.AddGameObject(EPrefabType.ProcessBar);
        }

        public static void AddSlider() {
            Controller.AddGameObject(EPrefabType.Slider);
        }

        public static void AddHorizontalScrollView() {
            Controller.AddGameObject(EPrefabType.HorizontalScrollView);
        }

        public static void AddVerticalScrollView() {
            Controller.AddGameObject(EPrefabType.VerticalScrollView);
        }

        public static void AddStyleButtonLevelOneHight() {
            Controller.AddPrefabGameObject(EPrefabType.StyleButtonLevelOneHight);
	    }

        public static void AddStyleButtonLevelTwoHight() {
            Controller.AddPrefabGameObject(EPrefabType.StyleButtonLevelTwoHight);
	    }

        public static void AddStyleButtonLevelTwoLow() {
            Controller.AddPrefabGameObject(EPrefabType.StyleButtonLevelTwoLow);
	    }

        public static void AddStyleTabToggleVertical() {
            Controller.AddPrefabGameObject(EPrefabType.StyleTabToggleVertical);
	    }

        public static void AddStyleTabToggleHorizontal() {
            Controller.AddPrefabGameObject(EPrefabType.StyleTabToggleHorizontal);
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