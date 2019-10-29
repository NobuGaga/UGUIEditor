using UnityEditor;

namespace UGUIEditor {

    internal static class EditorMenu {

        public const string MenuTitile = "UI 编辑器/";

        [MenuItem(MenuTitile + "删除多余 Canvas Renderer 组件")]
        private static void DeleteCanvasRenderer() {
            Tool.DeleteCanvasRenderer();
	    }
    }
}