using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Tweening = DG.Tweening;
using Runtime = DOTweenExtension.Runtime;

namespace DOTweenExtension.Editor {

    internal static class Manager {

        private static Dictionary<string, List<Tweening.DOTweenAnimation>> m_dicObjectTweenList = new Dictionary<string, List<Tweening.DOTweenAnimation>>();

        public static void Export() {
            GameObject gameObject = Selection.activeGameObject;
            if (gameObject == null)
                return;
            Tweening.DOTweenAnimation animation = gameObject.GetComponent<Tweening.DOTweenAnimation>();
            if (animation == null)
                return;
            Runtime.DOTweenAnimationTranslator.ToJson(animation);
            // UGUIEditor.Tool.LoadAllPrefab(AddDOTweenAnimation);
        }

        private static void AddDOTweenAnimation(GameObject gameObject) {
            var animations = gameObject.GetComponents<Tweening.DOTweenAnimation>();
            if (animations == null || animations.Length == 0)
                return;
            string name = gameObject.name;
            if (!m_dicObjectTweenList.ContainsKey(name))
                m_dicObjectTweenList.Add(name, new List<Tweening.DOTweenAnimation>());
            foreach(Tweening.DOTweenAnimation animation in animations)
                m_dicObjectTweenList[name].Add(animation);
        }
    }
}