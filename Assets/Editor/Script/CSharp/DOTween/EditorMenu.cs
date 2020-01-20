using UnityEditor;

namespace DOTweenExtension.Editor {

    internal static class Menu {

        private const string MenuTitle = "DOTween/";

        [MenuItem(MenuTitle + "Open " + Const.WindowTitle)]
        private static void OpenEditorWindow() => EditorWindow.Open(Const.WindowTitle);

        [MenuItem(MenuTitle + "导出 DOTweenAnimation 数据")]
        private static void Export() => Manager.Export();
    }
}