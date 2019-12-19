using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

namespace DOTweenExtension.Editor {

    internal static class Manager {

        private static Dictionary<string, List<DOTweenAnimation>> m_dicObjectTweenList = new Dictionary<string, List<DOTweenAnimation>>();

        public static void Export() {
            foreach (var pair in m_dicObjectTweenList) {
                var list = pair.Value;
                list.Clear();
            }
            UGUIEditor.Tool.LoadAllPrefab(AddDOTweenAnimation);
        }

        private static void AddDOTweenAnimation(GameObject gameObject) {
            var animations = gameObject.GetComponents<DOTweenAnimation>();
            if (animations == null || animations.Length == 0)
                return;
            string name = gameObject.name;
            if (!m_dicObjectTweenList.ContainsKey(name))
                m_dicObjectTweenList.Add(name, new List<DOTweenAnimation>());
            foreach(DOTweenAnimation animation in animations)
                m_dicObjectTweenList[name].Add(animation);
        }
    }
}