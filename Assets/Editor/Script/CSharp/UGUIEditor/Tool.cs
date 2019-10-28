using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

namespace UGUIEditor {
    internal static class Tool {
        
        public static void DeleteCanvasRenderer() {
            string[] windowFolders = Directory.GetDirectories(EditorPath.UI);
            if (windowFolders == null)
                return;
            for (int windowIndex = 0; windowIndex < windowFolders.Length; windowIndex++) {
                string windowPrefabFolder = EditorPath.Combine(windowFolders[windowIndex], EditorPath.UIPrefabsFolder);
                if (!Directory.Exists(windowPrefabFolder))
                    continue;
                string[] files = Directory.GetFiles(windowPrefabFolder);
                if (files == null)
                    continue;
                for (int fileIndex = 0; fileIndex < files.Length; fileIndex++) {
                    string fullPath = files[fileIndex];
                    if (!fullPath.EndsWith(EditorConst.PrefabExtension))
                        continue;
                    string projectPath = EditorPath.FullPathToProjectPath(fullPath);
                    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(projectPath);
                    if (prefab == null)
                        continue;
                    CanvasRenderer[] canvasRenderers = prefab.GetComponentsInChildren<CanvasRenderer>();
                    if (canvasRenderers == null)
                        continue;
                    bool isDirty = false;
                    for (int index = 0; index < canvasRenderers.Length; index++) {
                        if (CheckIsNeedCanvasRenderers(canvasRenderers[index]))
                            continue;
                        GameObject.DestroyImmediate(canvasRenderers[index], true);
                        isDirty = true;
                    }
                    if (isDirty)
                        EditorUtility.SetDirty(prefab);
                }
            }
            AssetDatabase.SaveAssets();
        }

        private static bool CheckIsNeedCanvasRenderers(CanvasRenderer canvasRenderer) {
            Component[] components = canvasRenderer.GetComponents<Component>();
            bool isNeed = false;
            for (int index = 0; index < components.Length; index++) {
                Component component = components[index];
                Type type = component.GetType();
                isNeed = component is Text || type.IsSubclassOf(EditorConfig.TextType) || component is Image || 
                         type.IsSubclassOf(EditorConfig.ImageType);
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