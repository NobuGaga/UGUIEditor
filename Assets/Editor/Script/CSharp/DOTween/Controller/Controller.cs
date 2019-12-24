using UnityEngine;
using System.IO;
using System.Text;
using DG.Tweening;
using UGUIEditor;

namespace DOTweenExtension.Editor {

    internal static class Controller {

        private static StringBuilder m_stringbuilder = new StringBuilder();

        public static void AddDOTweenAnimation(GameObject gameObject) {
            string name = gameObject.name;
            var animations = gameObject.GetComponents<DOTweenAnimation>();
            if (animations != null && animations.Length != 0)
                foreach (var animation in animations)
                    Model.AddDOTweenAnimation(name, animation.ToJson());
            animations = gameObject.GetComponentsInChildren<DOTweenAnimation>(true);
            if (animations != null && animations.Length != 0)
                foreach (var animation in animations)
                    Model.AddDOTweenAnimation(name, animation.ToJson());
        }

        public static void WriteDOTweenFile() {
            foreach (var objectListPair in Model.ObjectTweenList) {
                var name = objectListPair.Key;
                var list = objectListPair.Value;
                m_stringbuilder.Clear();
                foreach (var json in list) {
                    m_stringbuilder.Append(JsonUtility.ToJson(json, true));
                    m_stringbuilder.Append(",\n");
                }
                m_stringbuilder.Remove(m_stringbuilder.Length - 2, 2);
                m_stringbuilder.Replace("\n", "\n\t");
                m_stringbuilder.Insert(0, "[\n\t");
                m_stringbuilder.Append("\n]");
                string path = Path.JsonPath + Tool.GetNameWithExtension(name, Const.JsonTextExtension);
                FileStream file = new FileStream(path, FileMode.Create);
                StreamWriter fileWriter = new StreamWriter(file);
                fileWriter.Write(m_stringbuilder.ToString());
                fileWriter.Close(); 
                fileWriter.Dispose();    
            }
        }

        public static void Clear() => Model.Clear();
    }
}