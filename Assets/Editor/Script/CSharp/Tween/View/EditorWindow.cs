
namespace Tween {

    internal class EditorWindow : BaseEditorWindow {

        public const string WindowName = "Tween 编辑器窗口";

        public static void Open() => Open<EditorWindow>(WindowName);

        public static void CloseWindow() {
            Clear();
            GetWindow<EditorWindow>().Close();
        }

        public static void RefreshRepaint() => GetWindow<EditorWindow>().Repaint();

        public static void Clear() {

        }

        public static void InitData(string[] animationClipNames, int[] animationClipIndexs) {
            Clear();

        }

        private void OnGUI() {
            
        }
    }
}