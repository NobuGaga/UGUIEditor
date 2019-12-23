using System.Collections.Generic;
using DOTweenExtension.Runtime;

namespace DOTweenExtension.Editor {

    internal static class Model {

        private static Dictionary<string, List<DOTweenAnimationJson>> m_dicObjectTweenList = new Dictionary<string, List<DOTweenAnimationJson>>();
        public static Dictionary<string, List<DOTweenAnimationJson>> ObjectTweenList => m_dicObjectTweenList;

        public static void AddDOTweenAnimation(string name, DOTweenAnimationJson json) {
            if (!m_dicObjectTweenList.ContainsKey(name))
                m_dicObjectTweenList.Add(name, new List<DOTweenAnimationJson>());
            m_dicObjectTweenList[name].Add(json);
        }

        public static void Clear() => m_dicObjectTweenList.Clear();
    }
}