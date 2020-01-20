using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;
using DG.Tweening;

namespace DOTweenExtension.Editor {

    internal static class Controller {

        #region DOTween Extension Window

        public static void SetGameObject(UnityEngine.Object gameObject) => Model.SetGameObject(gameObject);

        private static void SetAllAnimation(Action<DOTweenAnimation> setter) {
            DOTweenAnimation[] animations = Model.CurrentAnimation;
            for (ushort index = 0; index < animations.Length; index++)
                setter(animations[index]);
            EditorUtility.SetDirty(Model.CurrentObject);
            AssetDatabase.SaveAssets();
        }

        public static void SetAllAnimationAutoPlay(bool isAutoPlay) =>
            SetAllAnimation((DOTweenAnimation tween) => tween.autoPlay = isAutoPlay);
        
        public static void SetAllAnimationDelay(float delay) =>
            SetAllAnimation((DOTweenAnimation tween) => tween.delay = delay);

        public static void SetAnimationAutoPlayByID(bool isAutoPlay, string id) =>
            SetAllAnimation((DOTweenAnimation tween) => {
                if (tween.id == id)
                    tween.autoPlay = isAutoPlay;
            });
        
        public static void SetAnimationDelayByID(float delay, string id) =>
            SetAllAnimation((DOTweenAnimation tween) => {
                if (tween.id == id)
                    tween.delay = delay;
            });
        #endregion

        #region Write Tween Configure File

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
                string path = Path.JsonPath + UGUIEditor.Tool.GetNameWithExtension(name, Const.JsonTextExtension);
                FileStream file = new FileStream(path, FileMode.Create);
                StreamWriter fileWriter = new StreamWriter(file);
                fileWriter.Write(m_stringbuilder.ToString());
                fileWriter.Close(); 
                fileWriter.Dispose();    
            }
        }
        #endregion

        public static void Clear() => Model.Clear();
    }
}