using UnityEngine;
using System.IO;

namespace UGUIEditor {
    internal static class EditorPath {

        private static readonly string FullProjectPath = Application.dataPath.Substring(0, Application.dataPath.IndexOf(ProjectPathStart));
        private const string ProjectPathStart = "Assets";
        public static readonly string UI = Combine(Application.dataPath, "Working/UI");
        public const string UIPrefabsFolder = "Prefabs";

        public static string FullPathToProjectPath(string fullPath) {
            int subIndex = fullPath.IndexOf(ProjectPathStart);
            return Tool.GetCacheString(fullPath.Substring(subIndex));
        }

        public static string Combine(string path1, string path2) {
#if UNITY_EDITOR_WIN
            path1 = CheckWindowsPathFlag(path1);
            path2 = CheckWindowsPathFlag(path2);
#endif
            return Path.Combine(path1, path2);
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