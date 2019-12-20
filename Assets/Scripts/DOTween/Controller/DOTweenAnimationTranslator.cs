using UnityEngine;
using DG.Tweening;

namespace DOTweenExtension.Runtime {

    public static class DOTweenAnimationTranslator {

        private static DOTweenAnimation m_animation = new DOTweenAnimation();
        private static DG.Tweening.TweenCallback m_callback;

        public static DOTweenAnimationJson ToJson(this DG.Tweening.DOTweenAnimation animation) {
            DOTweenAnimationJson json = default;
            if (animation.hasOnRewind) {
                animation.onRewind.GetPersistentEventCount();
                Debug.Log(animation.onRewind.ToString());
                // Debug.Log(m_callback.ToString());
            }
            return json;
        }

        private static void SetABSAnimationComponentData(ref DOTweenAnimationJson json) {
            
        }

        private static void SetUnityEvent(ref DOTweenAnimation json) {
            
        }

        public static void ToJson(DOTweenAnimationJson animation) {


        }
    }
}