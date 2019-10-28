using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

namespace UGUIEditor {
    internal static class Tool {
        
        public static void DeleteCanvasRenderer() {
            if (!Directory.Exists(EditorPath.UI))
                return;
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
                    GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(projectPath);
                    if (gameObject == null)
                        continue;
                    CanvasRenderer[] canvasRenderers = gameObject.GetComponentsInChildren<CanvasRenderer>();
                    if (canvasRenderers == null)
                        continue;
                    int canvasRendererIndex = 0;
                    for (int index = 0; index < canvasRenderers.Length; index++) {
                        gameObject = canvasRenderers[index].gameObject;
                        if (CheckIsNeedCanvasRenderers(gameObject, ref canvasRendererIndex))
                            continue;
                        RemoveCanvasRenderFromPrefab(gameObject, canvasRendererIndex);
                    }
                }
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static bool CheckIsNeedCanvasRenderers(GameObject gameObject, ref int canvasRendererIndex) {
            Component[] components = gameObject.GetComponents<Component>();
            bool isNeed = false;
            for (int index = 0; index < components.Length; index++) {
                Component component = components[index];
                Type type = component.GetType();
                if (component is CanvasRenderer)
                    canvasRendererIndex = index;
                isNeed = component is Text || type.IsSubclassOf(EditorConfig.TextType) || component is Image || 
                         type.IsSubclassOf(EditorConfig.ImageType);
                if (isNeed)
                    break;
            }
            return isNeed;
        }

        private static void RemoveCanvasRenderFromPrefab(GameObject gameObject, int canvasRendererIndex) {
            SerializedObject serializeObj = new SerializedObject(gameObject);
            SerializedProperty property = serializeObj.FindProperty("m_Component");
            property.DeleteArrayElementAtIndex(canvasRendererIndex);
            serializeObj.ApplyModifiedProperties();
        }

        public static string GetCacheString(string text) {
            return string.Intern(text);
        }
    }
}