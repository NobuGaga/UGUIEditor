using UnityEditor;

namespace UGUIEditor {

    internal static class EditorMenu {

        public const string MenuTitle = "UI 编辑器/";
        private const string UITitle = "添加/";
        private const string ToolTitle = "工具/";

        [MenuItem(MenuTitle + UITitle + "全屏窗体")]
        private static void AddFullScreenWindow() {
            Manager.AddFullScreenWindow();
	    }

        [MenuItem(MenuTitle + UITitle + "普通窗体")]
        private static void AddWindow() {
            Manager.AddWindow();
	    }

        [MenuItem(MenuTitle + ToolTitle + "删除丢失组件")]
        private static void DeleteMissingComponent() {
            Tool.DeleteAllPrefabMissingComponent();
	    }

        [MenuItem(MenuTitle + ToolTitle + "删除多余 Canvas Renderer 组件")]
        private static void DeleteNoNeedCanvasRenderer() {
            Tool.DeleteAllPrefabNoNeedCanvasRenderer();
	    }
    }
}