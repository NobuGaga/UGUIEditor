using UnityEngine;
using System.IO;
using DG.Tweening;
using UGUIEditor;
using DOTweenExtension.Runtime;

namespace DOTweenExtension.Editor {

    internal static class Controller {

        public static void AddDOTweenAnimation(GameObject gameObject) {
            var animations = gameObject.GetComponents<DOTweenAnimation>();
            if (animations == null || animations.Length == 0)
                return;
            foreach (var animation in animations)
                Model.AddDOTweenAnimation(gameObject.name, animation.ToJson());
        }

        public static void WriteDOTweenFile() {
            foreach (var objectListPair in Model.ObjectTweenList) {
                var name = objectListPair.Key;
                var list = objectListPair.Value;
                DOTweenAnimationJsons data = default;
                data.jsons = list.ToArray();
                FileStream file = new FileStream(Path.JsonPath + Tool.GetNameWithExtension(name, Const.JsonTextExtension), FileMode.Create);
                StreamWriter fileWriter = new StreamWriter(file);
                fileWriter.Write(JsonUtility.ToJson(data));
                fileWriter.Close();
                fileWriter.Dispose();    
            }
        }

        public static void Clear() => Model.Clear();
    }
}