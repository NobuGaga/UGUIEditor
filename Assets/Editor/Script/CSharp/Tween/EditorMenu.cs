using UnityEditor;

namespace Tween {

    internal static class EditorMenu {

        public const string MenuTitle = "Tween 编辑器/";

        [MenuItem(MenuTitle + "打开 Tween 编辑器窗口")]
        private static void OpenWindow() => EditorWindow.Open();
    }
}