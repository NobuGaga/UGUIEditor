using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using DOTweenExtension.Runtime;

namespace DOTweenExtension.Editor {

    internal static class Model {

        #region DOTween Extension Window
        
        private static GameObject m_curGameObject;
        public static GameObject CurrentObject => m_curGameObject;
        private static DOTweenAnimation[] m_curAnimation;
        public static DOTweenAnimation[] CurrentAnimation => m_curAnimation;
        
        public static bool IsNoAnimation => m_curGameObject == null || m_curAnimation == null || m_curAnimation.Length == 0;

        public static void SetGameObject(Object gameObject) {
            if (gameObject == m_curGameObject)
                return;
            m_curGameObject = gameObject as GameObject;
            if (gameObject == null) {
                m_curAnimation = null;
                return;
            }
            m_curAnimation = m_curGameObject.GetComponentsInChildren<DOTweenAnimation>(true);
        }
        #endregion

        #region Write Tween Configure File

        private static Dictionary<string, List<DOTweenAnimationJson>> m_dicObjectTweenList = new Dictionary<string, List<DOTweenAnimationJson>>();
        public static Dictionary<string, List<DOTweenAnimationJson>> ObjectTweenList => m_dicObjectTweenList;

        public static void AddDOTweenAnimation(string name, DOTweenAnimationJson json) {
            if (!m_dicObjectTweenList.ContainsKey(name))
                m_dicObjectTweenList.Add(name, new List<DOTweenAnimationJson>());
            m_dicObjectTweenList[name].Add(json);
        }
        #endregion

        public static void Clear() {
            m_curGameObject = null;
            m_dicObjectTweenList.Clear();  
        }
    }
}