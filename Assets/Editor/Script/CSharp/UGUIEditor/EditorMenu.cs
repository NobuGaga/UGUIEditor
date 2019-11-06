﻿using UnityEditor;
using UnityEngine;

namespace UGUIEditor {

    internal static class EditorMenu {

        public const string MenuTitle = "UI 编辑器/";
        private const string UITitle = MenuTitle + "添加/";
        private const string ComponentTitle = UITitle + "组件/";
        private const string ToolTitle = MenuTitle + "工具/";

        [MenuItem(UITitle + "窗体/全屏窗体")]
        private static void AddFullScreenWindow() {
            Manager.AddFullScreenWindow();
	    }

        [MenuItem(UITitle + "窗体/普通窗体")]
        private static void AddWindow() {
            Manager.AddWindow();
	    }

        [MenuItem(ComponentTitle + "空节点")]
        private static void AddEmptyGameObject() {
            Manager.AddEmptyGameObject();
	    }

        [MenuItem(ComponentTitle + "图片/小图")]
        private static void AddImage() {
            Manager.AddImage();
	    }

        [MenuItem(ComponentTitle + "图片/大图")]
        private static void AddRawImage() {
            Manager.AddRawImage();
	    }

        [MenuItem(ComponentTitle + "文本/文本")]
        private static void AddText() {
            Manager.AddText();
	    }

        [MenuItem(ComponentTitle + "文本/输入框")]
        private static void AddInputText() {
            Manager.AddInputText();
	    }

        [MenuItem(ComponentTitle + "按钮/样式1")]
        private static void AddStyleOneButton() {
            Manager.AddStyleOneButton();
	    }

        [MenuItem(ComponentTitle + "拖动区域/水平")]
        private static void AddHorizontalScrollView() {
            Manager.AddHorizontalScrollView();
	    }

        [MenuItem(ComponentTitle + "拖动区域/垂直")]
        private static void AddVerticalScrollView() {
            Manager.AddVerticalScrollView();
	    }

        [MenuItem(ComponentTitle + "其他/复选框")]
        private static void AddToggle() {
            Manager.AddToggle();
	    }

        [MenuItem(ComponentTitle + "其他/进度条")]
        private static void AddProcess() {
            Manager.AddProcess();
	    }

        [MenuItem(ComponentTitle + "其他/拖动条")]
        private static void AddSlider() {
            Manager.AddSlider();
	    }

        [InitializeOnLoadMethod]
        private static void InitializeOnLoadMethod() {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
        }

        private static Rect m_cacheRect = new Rect(0, 0, 0, 0);    
        private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect) {
            if (Event.current == null || !selectionRect.Contains(Event.current.mousePosition) || 
                Event.current.button != 1 || Event.current.type > EventType.MouseUp)
                return;
            GameObject selectedGameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (selectedGameObject == null || !(selectedGameObject.transform is RectTransform))
                return;
            Vector2 mousePosition = Event.current.mousePosition;
            m_cacheRect.x = mousePosition.x;
            m_cacheRect.y = mousePosition.y;
            EditorUtility.DisplayPopupMenu(m_cacheRect, ComponentTitle, null);
            Event.current.Use();
        }

        [MenuItem(ToolTitle + "删除丢失组件")]
        private static void DeleteMissingComponent() {
            Tool.DeleteAllPrefabMissingComponent();
	    }

        [MenuItem(ToolTitle + "删除多余 Canvas Renderer 组件")]
        private static void DeleteNoNeedCanvasRenderer() {
            Tool.DeleteAllPrefabNoNeedCanvasRenderer();
	    }
    }
}