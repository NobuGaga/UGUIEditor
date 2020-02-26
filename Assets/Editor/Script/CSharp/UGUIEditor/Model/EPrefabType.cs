
namespace UGUIEditor {

    internal enum EPrefabType {

        FullScreenWindow,
        Window,
        Image,
        RawImage,
        Text,
        InputText,
        StyleOneButton,
        Toggle,
        ProcessBar,
        Slider,
        ScrollItem,
        HorizontalScrollView,
        VerticalScrollView,
        VerticalGridScrollView,
        StyleSmallWindow,
        StyleMiddleWindow,
        StyleButtonLevelOneHight,
        StyleButtonLevelTwoHight,
        StyleButtonLevelTwoLow,
        StyleTabToggleVertical,
        StyleTabToggleHorizontal,
    }

    internal static class EPrefabTypeExtension {

        private static EPrefabType[] m_arrayNestPrefab = new EPrefabType[] {
            EPrefabType.StyleButtonLevelOneHight,
            EPrefabType.StyleButtonLevelTwoHight,
            EPrefabType.StyleButtonLevelTwoLow,
        };
        
        public static bool isNestPrefab(this EPrefabType type) {
            for (ushort index = 0; index < m_arrayNestPrefab.Length; index++)
                if (type == m_arrayNestPrefab[index])
                    return true;
            return false;
        }
    }
}