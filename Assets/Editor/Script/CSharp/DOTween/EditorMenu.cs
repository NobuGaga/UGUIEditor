using UnityEditor;

namespace DOTweenExtension.Editor {

    internal static class Menu {

        private const string MenuTitle = "DOTween/";

        [MenuItem(MenuTitle + "导出 DOTweenAnimation 数据 #S")]
        private static void Export() => Manager.Export();
    }
}