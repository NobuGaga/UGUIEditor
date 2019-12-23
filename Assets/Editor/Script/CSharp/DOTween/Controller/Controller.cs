using UnityEngine;
using DG.Tweening;

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
            
        }

        public static void Clear() => Model.Clear();
    }
}