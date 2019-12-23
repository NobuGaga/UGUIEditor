using UnityEditor;
using UnityEngine;

namespace UGUIEditor {

    internal static class EditorMenu {

        public const string MenuTitle = "UI 编辑器/";
        private const string UITitle = MenuTitle + "添加/";
        private const string WindowTitle = UITitle + "窗体/";
        private const string StyleWindowTitle = WindowTitle + "样式/";
        private const string ComponentTitle = UITitle + "组件/";
        private const string ToolTitle = MenuTitle + "工具/";
        private const string StyleComponentTitle = ComponentTitle + "样式/";

        [MenuItem(WindowTitle + "全屏窗体 #N")]
        private static void AddFullScreenWindow() {
            Manager.AddFullScreenWindow();
	    }

        [MenuItem(WindowTitle + "普通窗体 #W")]
        private static void AddWindow() {
            Manager.AddWindow();
	    }

        [MenuItem(StyleWindowTitle + "小型弹窗")]
        private static void AddStyleSmallWindow() {
            Manager.AddStyleSmallWindow();
	    }

        [MenuItem(StyleWindowTitle + "中型弹窗")]
        private static void AddStyleMiddleWindow() {
            Manager.AddStyleMiddleWindow();
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

        [MenuItem(ComponentTitle + "按钮/图文按钮")]
        private static void AddStyleOneButton() {
            Manager.AddStyleOneButton();
	    }

        [MenuItem(ComponentTitle + "滚动列表/水平")]
        private static void AddHorizontalScrollView() {
            Manager.AddHorizontalScrollView();
	    }

        [MenuItem(ComponentTitle + "滚动列表/垂直")]
        private static void AddVerticalScrollView() {
            Manager.AddVerticalScrollView();
	    }

        [MenuItem(ComponentTitle + "滚动列表/垂直网格")]
        private static void AddVerticalGridScrollView() {
            Manager.AddVerticalGridScrollView();
	    }

        [MenuItem(ComponentTitle + "滚动列表/item模板")]
        private static void AddScrollItem() {
            Manager.AddScrollItem();
	    }

        [MenuItem(ComponentTitle + "其他/复选框")]
        private static void AddToggle() {
            Manager.AddToggle();
	    }

        [MenuItem(ComponentTitle + "其他/进度条")]
        private static void AddProcess() {
            Manager.AddProcess();
	    }

        [MenuItem(ComponentTitle + "其他/滑块条")]
        private static void AddSlider() {
            Manager.AddSlider();
	    }

        [MenuItem(StyleComponentTitle + "按钮/一级按钮-权重高")]
        private static void AddStyleButtonLevelOneHight() {
            Manager.AddStyleButtonLevelOneHight();
	    }

        [MenuItem(StyleComponentTitle + "按钮/二级按钮-权重高")]
        private static void AddStyleButtonLevelTwoHight() {
            Manager.AddStyleButtonLevelTwoHight();
	    }

        [MenuItem(StyleComponentTitle + "按钮/二级按钮-权重低")]
        private static void AddStyleButtonLevelTwoLow() {
            Manager.AddStyleButtonLevelTwoLow();
	    }

        [MenuItem(StyleComponentTitle + "页签/垂直列表")]
        private static void AddStyleTabToggleVertical() {
            Manager.AddStyleTabToggleVertical();
	    }

        [MenuItem(StyleComponentTitle + "页签/水平列表")]
        private static void AddStyleTabToggleHorizontal() {
            Manager.AddStyleTabToggleHorizontal();
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

        [MenuItem(ToolTitle + "解除所有嵌套预设绑定")]
        private static void UnpackAllPrefabInstance() {
            Tool.UnpackAllPrefabInstance();
	    }
    }
}