using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

namespace UGUIEditor {
    internal static class Tool {
        
        public static void DeleteAllPrefabMissingComponent() {
            LoadAllPrefab(DeletePrefabMissingComponent);
        }

        public static void DeleteAllPrefabNoNeedCanvasRenderer() {
            LoadAllPrefab(DeleteNoNeedCanvasRenderer);
        }

        private static void LoadAllPrefab(Action<GameObject> doSomething) {
            LoadPrefab(EditorPath.ControlsTempleteUI, doSomething);
            string[] modelFolders = Directory.GetDirectories(EditorPath.ModelTempleteUI);
            if (modelFolders != null)
                for (int index = 0; index < modelFolders.Length; index++)
                    LoadPrefab(modelFolders[index], doSomething);
            string[] windowFolders = Directory.GetDirectories(EditorPath.UI);
            if (windowFolders != null)
                for (int index = 0; index < windowFolders.Length; index++) {
                    string windowPrefabFolder = EditorPath.Combine(windowFolders[index], EditorPath.UIPrefabsFolder);
                    LoadPrefab(windowPrefabFolder, doSomething);
                }
            AssetDatabase.SaveAssets();
        }

        private static void LoadPrefab(string folderPath, Action<GameObject> doSomething) {
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
                doSomething(prefab);
            }
        }

        private static void DeletePrefabMissingComponent(GameObject prefab) {
            Transform[] trans = prefab.GetComponentsInChildren<Transform>(true);
            for (int index = 0; index < trans.Length; index++)
                DeleteMissingComponent(trans[index].gameObject);
            if (EditorUtility.IsDirty(prefab))
                EditorUtility.SetDirty(prefab);
        }

        private static void DeleteMissingComponent(GameObject node) {
            if (GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(node) > 0)
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(node);
        }

        private static void DeleteNoNeedCanvasRenderer(GameObject prefab) {
            CanvasRenderer[] canvasRenderers = prefab.GetComponentsInChildren<CanvasRenderer>(true);
            if (canvasRenderers == null)
                return;
            for (int index = 0; index < canvasRenderers.Length; index++) {
                if (CheckIsNeedCanvasRenderer(canvasRenderers[index]))
                    continue;
                GameObject.DestroyImmediate(canvasRenderers[index], true);
            }
            if (EditorUtility.IsDirty(prefab))
                EditorUtility.SetDirty(prefab);
        }

        private static bool CheckIsNeedCanvasRenderer(CanvasRenderer canvasRenderer) {
            Component[] components = canvasRenderer.GetComponents<Component>();
            for (int index = 0; index < components.Length; index++) {
                Component component = components[index];
                Type type = component.GetType();
                bool isNeed = component is Text || type.IsSubclassOf(EditorConfig.TextType) || component is Image || 
                         type.IsSubclassOf(EditorConfig.ImageType) || component is RawImage || 
                         type.IsSubclassOf(EditorConfig.RawImageType);
                if (isNeed)
                    return true;
            }
            return false;
        }

        public static string GetCacheString(string text) {
            return string.Intern(text);
        }
    }
}