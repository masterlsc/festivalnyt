using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UIStyles;

namespace UIStyles
{
    public class ContextMenu : Editor
    {
        private static void UIStylesOnContext()
        {
            WindowStyles.ShowWindow(true);
        }

        private static void UIStylesOnPaletteContext()
        {
            WindowColorPalette.ShowWindow(true);
        }

        [MenuItem("GameObject/UI/UI Styles/Runtime/Create Manager", false, 0)]
        static void CreateRuntimeManager(MenuCommand command)
        {
            if (!Object.FindObjectOfType<UIStylesManager>())
            {
                GameObject obj = new GameObject("UI Styles Manager");
                obj.AddComponent<UIStylesManager>();
            }
            else Debug.LogWarning("UI Styles Manager already exists");
        }

        [MenuItem("GameObject/UI/UI Styles/Open UI Styles ", false, 0)]
        static void OpenUIStyles(MenuCommand command)
        {
            UIStylesOnContext();
        }

        [MenuItem("GameObject/UI/UI Styles/Open Color Palette ", false, 0)]
        static void OpenColorPalette(MenuCommand command)
        {
            UIStylesOnPaletteContext();
        }

        // -------------------------------------------------- //
        // Text
        // -------------------------------------------------- //

        [MenuItem("CONTEXT/Text/UI Styles", false, 1000)]
        private static void UIStylesTextContext(MenuCommand command)
        {
            UIStylesOnContext();
        }

        [MenuItem("CONTEXT/Text/Color Palette", false, 1000)]
        private static void UIStylesTextContextPalette(MenuCommand command)
        {
            UIStylesOnPaletteContext();
        }

        // -------------------------------------------------- //
        // Image
        // -------------------------------------------------- //

        [MenuItem("CONTEXT/Image/UI Styles", false, 1000)]
        private static void UIStylesImageContext(MenuCommand command)
        {
            UIStylesOnContext();
        }

        [MenuItem("CONTEXT/Image/Color Palette", false, 1000)]
        private static void UIStylesImageContextPalette(MenuCommand command)
        {
            UIStylesOnPaletteContext();
        }

        // -------------------------------------------------- //
        // Button
        // -------------------------------------------------- //

        [MenuItem("CONTEXT/Button/UI Styles", false, 1000)]
        private static void UIStylesButtonContext(MenuCommand command)
        {
            UIStylesOnContext();
        }

        [MenuItem("CONTEXT/Button/Color Palette", false, 1000)]
        private static void UIStylesButtonContextPalette(MenuCommand command)
        {
            UIStylesOnPaletteContext();
        }

        // -------------------------------------------------- //
        // InputField
        // -------------------------------------------------- //

        [MenuItem("CONTEXT/InputField/UI Styles", false, 1000)]
        private static void UIStylesInputFieldContext(MenuCommand command)
        {
            UIStylesOnContext();
        }

        [MenuItem("CONTEXT/InputField/Color Palette", false, 1000)]
        private static void UIStylesInputFieldContextPalette(MenuCommand command)
        {
            UIStylesOnPaletteContext();
        }

        // -------------------------------------------------- //
        // Dropdown
        // -------------------------------------------------- //

        [MenuItem("CONTEXT/Dropdown/UI Styles", false, 1000)]
        private static void UIStylesDropdownContext(MenuCommand command)
        {
            UIStylesOnContext();
        }

        [MenuItem("CONTEXT/Dropdown/Color Palette", false, 1000)]
        private static void UIStylesDropdownContextPalette(MenuCommand command)
        {
            UIStylesOnPaletteContext();
        }

        // -------------------------------------------------- //
        // ScrollRect
        // -------------------------------------------------- //

        [MenuItem("CONTEXT/ScrollRect/UI Styles", false, 1000)]
        private static void UIStylesScrollRectContext(MenuCommand command)
        {
            UIStylesOnContext();
        }

        [MenuItem("CONTEXT/ScrollRect/Color Palette", false, 1000)]
        private static void UIStylesScrollRectContextPalette(MenuCommand command)
        {
            UIStylesOnPaletteContext();
        }

        // -------------------------------------------------- //
        // Scrollbar
        // -------------------------------------------------- //

        [MenuItem("CONTEXT/Scrollbar/UI Styles", false, 1000)]
        private static void UIStylesScrollbarContext(MenuCommand command)
        {
            UIStylesOnContext();
        }

        [MenuItem("CONTEXT/Scrollbar/Color Palette", false, 1000)]
        private static void UIStylesScrollbarContextPalette(MenuCommand command)
        {
            UIStylesOnPaletteContext();
        }

        // -------------------------------------------------- //
        // Slider
        // -------------------------------------------------- //

        [MenuItem("CONTEXT/Slider/UI Styles", false, 1000)]
        private static void UIStylesSliderContext(MenuCommand command)
        {
            UIStylesOnContext();
        }

        [MenuItem("CONTEXT/Slider/Color Palette", false, 1000)]
        private static void UIStylesSliderContextPalette(MenuCommand command)
        {
            UIStylesOnPaletteContext();
        }

        // -------------------------------------------------- //
        // Toggle
        // -------------------------------------------------- //

        [MenuItem("CONTEXT/Toggle/UI Styles", false, 1000)]
        private static void UIStylesToggleContext(MenuCommand command)
        {
            UIStylesOnContext();
        }

        [MenuItem("CONTEXT/Toggle/Color Palette", false, 1000)]
        private static void UIStylesToggleContextPalette(MenuCommand command)
        {
            UIStylesOnPaletteContext();
        }
    }
}