using UnityEditor;

namespace UGUIEditor {

    internal static class EditorMenu {

        [MenuItem("UI 编辑器/删除多余 Canvas Renderer 组件")]
        private static void DeleteCanvasRenderer() {
            Tool.DeleteCanvasRenderer();
	    }
    }
}