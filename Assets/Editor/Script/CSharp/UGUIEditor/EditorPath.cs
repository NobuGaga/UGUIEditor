using UnityEngine;
using System.IO;

namespace UGUIEditor {

    public static class EditorPath {

        public const string ProjectPathStart = "Assets";
        public static readonly string Scene = Combine(ProjectPathStart, "Main.unity");
        public static readonly string UI = Combine(Application.dataPath, "Working/UI");
        public const string UIPrefabsFolder = "Prefabs";
        public const string TempleteUIAssetPath = "Resources/TempleteUI";
        public static readonly string ParticleEffect = Combine(Application.dataPath, "fx/ui");
        public static readonly string TempleteUI = Combine(Application.dataPath, TempleteUIAssetPath);

        public static string FullPathToProjectPath(string fullPath) {
            int subIndex = fullPath.IndexOf(ProjectPathStart);
            return Tool.GetCacheString(fullPath.Substring(subIndex));
        }

        public static string Combine(string path1, string path2) {
#if UNITY_EDITOR_WIN
            path1 = CheckWindowsPathFlag(path1);
            path2 = CheckWindowsPathFlag(path2);
            return CheckWindowsPathFlag(Path.Combine(path1, path2));
#else
            return Path.Combine(path1, path2);
#endif
        }

#if UNITY_EDITOR_WIN
        private static string CheckWindowsPathFlag(string path) {
            if (path.Contains("\\"))
                return path.Replace('\\', '/');
            return path;
        }
#endif
    }
}