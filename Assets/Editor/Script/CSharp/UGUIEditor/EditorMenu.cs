using UnityEditor;

namespace UGUIEditor {

    internal static class EditorMenu {

        public const string MenuTitle = "UI 编辑器/";

        [MenuItem(MenuTitle + "删除丢失组件")]
        private static void DeleteMissingComponent() {
            Tool.DeleteAllPrefabMissingComponent();
	    }

        [MenuItem(MenuTitle + "删除多余 Canvas Renderer 组件")]
        private static void DeleteNoNeedCanvasRenderer() {
            Tool.DeleteAllPrefabNoNeedCanvasRenderer();
	    }
    }
}