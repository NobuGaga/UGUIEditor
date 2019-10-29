using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

namespace UGUIEditor {
    internal static class Tool {
        
        public static void DeleteCanvasRenderer() {
            LoadPrefabAndCheckIsNeedCanvasRenderer(EditorPath.TempleteUI);
            string[] windowFolders = Directory.GetDirectories(EditorPath.UI);
            if (windowFolders == null)
                return;
            for (int windowIndex = 0; windowIndex < windowFolders.Length; windowIndex++) {
                string windowPrefabFolder = EditorPath.Combine(windowFolders[windowIndex], EditorPath.UIPrefabsFolder);
                LoadPrefabAndCheckIsNeedCanvasRenderer(windowPrefabFolder);
            }
            AssetDatabase.SaveAssets();
        }

        private static void LoadPrefabAndCheckIsNeedCanvasRenderer(string folderPath) {
            if (!Directory.Exists(folderPath))
                return;
            string[] files = Directory.GetFiles(folderPath);
            if (files == null)
                return;
            for (int fileIndex = 0; fileIndex < files.Length; fileIndex++) {
                string fullPath = files[fileIndex];
                if (!fullPath.EndsWith(EditorConst.PrefabExtension))
                    continue;
                string projectPath = EditorPath.FullPathToProjectPath(fullPath);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(projectPath);
                if (prefab == null)
                    return;
                CanvasRenderer[] canvasRenderers = prefab.GetComponentsInChildren<CanvasRenderer>();
                if (canvasRenderers == null)
                    return;
                bool isDirty = false;
                for (int index = 0; index < canvasRenderers.Length; index++) {
                    if (CheckIsNeedCanvasRenderers(canvasRenderers[index]))
                        return;
                    GameObject.DestroyImmediate(canvasRenderers[index], true);
                    isDirty = true;
                }
                if (isDirty)
                    EditorUtility.SetDirty(prefab);
            }
        }

        private static bool CheckIsNeedCanvasRenderers(CanvasRenderer canvasRenderer) {
            Component[] components = canvasRenderer.GetComponents<Component>();
            bool isNeed = false;
            for (int index = 0; index < components.Length; index++) {
                Component component = components[index];
                Type type = component.GetType();
                isNeed = component is Text || type.IsSubclassOf(EditorConfig.TextType) || component is Image || 
                         type.IsSubclassOf(EditorConfig.ImageType) || component is RawImage || 
                         type.IsSubclassOf(EditorConfig.RawImageType);
                if (isNeed)
                    break;
            }
            return isNeed;
        }

        public static string GetCacheString(string text) {
            return string.Intern(text);
        }
    }
}