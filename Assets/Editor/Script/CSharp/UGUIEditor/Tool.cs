using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

namespace UGUIEditor {

    public static class Tool {
        
        public static void DeleteAllPrefabMissingComponent() => LoadAllPrefab(DeletePrefabMissingComponent);

        public static void DeleteAllPrefabNoNeedCanvasRenderer() => LoadAllPrefab(DeleteNoNeedCanvasRenderer);

        public static void UnpackAllPrefabInstance() => LoadAllPrefab(UnpackPrefabInstance);

        public static void LoadAllPrefab(Action<GameObject> doSomething) {
            LoadPrefab(EditorPath.TempleteUI, doSomething);
            string[] windowFolders = Directory.GetDirectories(EditorPath.UI);
            if (windowFolders != null)
                for (int index = 0; index < windowFolders.Length; index++) {
                    string windowPrefabFolder = EditorPath.Combine(windowFolders[index], EditorPath.UIPrefabsFolder);
                    LoadPrefab(windowPrefabFolder, doSomething);
                }
            AssetDatabase.SaveAssets();
        }

        private static void LoadAllParticlePrefab(Action<GameObject> doSomething) {
            LoadPrefab(EditorPath.ParticleEffectAssetPath, doSomething);
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

        private static void UnpackPrefabInstance(GameObject prefab) {
            Transform[] transforms = prefab.GetComponentsInChildren<Transform>(true);
            if (transforms == null || transforms.Length == 0)
                return;
            for (int index = 0; index < transforms.Length; index++) {
                if (transforms[index].name == prefab.name)
                    continue;
                GameObject node = transforms[index].gameObject;
                if (!PrefabUtility.IsAnyPrefabInstanceRoot(node))
                    continue;
                GameObject prefabRoot = PrefabUtility.InstantiatePrefab(prefab, Manager.WindowParent) as GameObject;
                string path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(prefab);
                PrefabUtility.UnpackPrefabInstance(prefabRoot, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
                PrefabUtility.SaveAsPrefabAsset(prefabRoot, path);
                UnityEngine.Object.DestroyImmediate(prefabRoot);
                break;
            }
        }

        #pragma warning disable 0618
        
        private const ParticleSystemScalingMode UseParticleScale = ParticleSystemScalingMode.Local;
        private static void SetParticleScaleMode(GameObject prefab) {
            ParticleSystem[] particles = prefab.GetComponentsInChildren<ParticleSystem>(true);
            if (particles == null || particles.Length == 0)
                return;
            foreach (ParticleSystem particle in particles)
                particle.scalingMode = UseParticleScale;
        }

        #pragma warning restore 0618

        public static string GetNameWithExtension(string name, string extension) =>
            GetCacheString(string.Format("{0}.{1}", GetCacheString(name), GetCacheString(extension)));

        public static string GetCacheString(string text) => string.Intern(text);
    }
}