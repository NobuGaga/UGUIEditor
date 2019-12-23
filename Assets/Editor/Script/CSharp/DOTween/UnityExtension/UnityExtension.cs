using UnityEngine;
using UnityEngine.Events;

namespace DOTweenExtension.Editor {

    internal static class UnityExtension {

        public static string ToJson(this UnityEvent unityEvent) => JsonUtility.ToJson(unityEvent);
        public static string ToJson(this Vector3 vector3) => JsonUtility.ToJson(vector3);
        public static string ToJson(this Vector2 vector2) => JsonUtility.ToJson(vector2);
        public static string ToJson(this Color color) => JsonUtility.ToJson(color);

        public static T ToJson<T>(this string eventString) => JsonUtility.FromJson<T>(eventString);
    }
}