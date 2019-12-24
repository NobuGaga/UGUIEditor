
namespace DOTweenExtension.Editor {

    internal static class Manager {

        public static void Export() {
            Controller.Clear();
            UGUIEditor.Tool.LoadAllPrefab(Controller.AddDOTweenAnimation);
            Controller.WriteDOTweenFile();
        }
    }
}