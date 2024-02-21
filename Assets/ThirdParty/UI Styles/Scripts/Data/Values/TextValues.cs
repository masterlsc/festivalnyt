using UnityEngine;

namespace UIStyles
{
    public enum FontCase { FirstUppercase, Lowercase, Uppercase, TitleCase }
    public enum Overlay
    {
        ColorOverlay,
#if !PRE_UNITY_5
        GradientOverlay
#endif
    }

    [System.Serializable]
    public class TextValues
    {
        // Rect transform data
        [HideInInspector] public RectTransformValues rectTransformValues = new RectTransformValues();

        [HideInInspector] public string text;
        [HideInInspector] public Font font;
        [HideInInspector] public FontStyle fontStyle = FontStyle.Normal;
        [HideInInspector] public int fontSize = 16;
        [HideInInspector] public float lineSpacing = 1;
        [HideInInspector] public bool richText = true;
        [HideInInspector] public TextAnchor textAlignment = TextAnchor.MiddleLeft;
        [HideInInspector] public bool alignByGeometry = false;
        [HideInInspector] public HorizontalWrapMode horizontalWrapMode = HorizontalWrapMode.Wrap;
        [HideInInspector] public VerticalWrapMode verticalWrapMode = VerticalWrapMode.Truncate;
        [HideInInspector] public bool bestFit;
        [HideInInspector] public int bestFitMinSize = 10;
        [HideInInspector] public int bestFitMaxSize = 40;
        [HideInInspector] public Material material;
        [HideInInspector] public bool raycastTarget = true;
        [HideInInspector] public FontCase fontCase;
        [HideInInspector] public Color color = Color.black;
        [HideInInspector] public string colorID;

        [HideInInspector] public Overlay overlay;
        [HideInInspector] public Color gradientTopColor = Color.white;
        [HideInInspector] public string gradientTopColorID;
        [HideInInspector] public Color gradientBottomColor = Color.black;
        [HideInInspector] public string gradientBottomColorID;
        [HideInInspector] public bool showFontColorWithGradient = false;


        // effects
        [HideInInspector] public bool useShadow;
        [HideInInspector] public Color shadowColor = new Color(0, 0, 0, .5f);
        [HideInInspector] public string shadowColorID;
        [HideInInspector] public Vector2 shadowDistance = new Vector2(1, -1);
        [HideInInspector] public bool shadowUseAlpha = true;

        [HideInInspector] public bool useOutline;
        [HideInInspector] public Color outlineColor = new Color(0, 0, 0, .5f);
        [HideInInspector] public string outlineColorID;
        [HideInInspector] public Vector2 outlineDistance = new Vector2(1, -1);
        [HideInInspector] public bool outlineUseAlpha = true;

        // enabled
        [HideInInspector] public bool textEnabled;
        [HideInInspector] public bool fontEnabled;
        [HideInInspector] public bool fontStyleEnabled;
        [HideInInspector] public bool fontSizeEnabled;
        [HideInInspector] public bool lineSpacingEnabled;
        [HideInInspector] public bool richTextEnabled;
        [HideInInspector] public bool textAlignmentEnabled;
        [HideInInspector] public bool alignByGeometryEnabled;
        [HideInInspector] public bool horizontalWrapModeEnabled;
        [HideInInspector] public bool verticalWrapModeEnabled;
        [HideInInspector] public bool bestFitEnabled;
        [HideInInspector] public bool colorEnabled;
        [HideInInspector] public bool materialEnabled;
        [HideInInspector] public bool raycastTargetEnabled;
        [HideInInspector] public bool fontCaseEnabled;
        [HideInInspector] public bool useShadowEnabled;
        [HideInInspector] public bool useOutlineEnabled;


        public TextValues CloneValues()
        {
            TextValues values = new TextValues();

            values.rectTransformValues = this.rectTransformValues.CloneValues();

            values.text = this.text;
            values.font = this.font;
            values.fontStyle = this.fontStyle;
            values.fontSize = this.fontSize;
            values.lineSpacing = this.lineSpacing;
            values.richText = this.richText;
            values.textAlignment = this.textAlignment;
            values.horizontalWrapMode = this.horizontalWrapMode;
            values.verticalWrapMode = this.verticalWrapMode;
            values.bestFit = this.bestFit;
            values.bestFitMinSize = this.bestFitMinSize;
            values.bestFitMaxSize = this.bestFitMaxSize;
            values.material = this.material;
            values.raycastTarget = this.raycastTarget;
            values.fontCase = this.fontCase;
            values.color = this.color;
            values.colorID = this.colorID;

            values.overlay = this.overlay;
            values.gradientTopColor = this.gradientTopColor;
            values.gradientTopColorID = this.gradientTopColorID;
            values.gradientBottomColor = this.gradientBottomColor;
            values.gradientBottomColorID = this.gradientBottomColorID;
            values.showFontColorWithGradient = this.showFontColorWithGradient;

            values.useShadow = this.useShadow;
            values.shadowColor = this.shadowColor;
            values.shadowColorID = this.shadowColorID;
            values.shadowDistance = this.shadowDistance;
            values.shadowUseAlpha = this.shadowUseAlpha;

            values.useOutline = this.useOutline;
            values.outlineColor = this.outlineColor;
            values.outlineColorID = this.outlineColorID;
            values.outlineDistance = this.outlineDistance;
            values.outlineUseAlpha = this.outlineUseAlpha;

            values.textEnabled = this.textEnabled;
            values.fontEnabled = this.fontEnabled;
            values.fontStyleEnabled = this.fontStyleEnabled;
            values.fontSizeEnabled = this.fontSizeEnabled;
            values.lineSpacingEnabled = this.lineSpacingEnabled;
            values.richTextEnabled = this.richTextEnabled;
            values.textAlignmentEnabled = this.textAlignmentEnabled;
            values.horizontalWrapModeEnabled = this.horizontalWrapModeEnabled;
            values.verticalWrapModeEnabled = this.verticalWrapModeEnabled;
            values.bestFitEnabled = this.bestFitEnabled;
            values.colorEnabled = this.colorEnabled;
            values.materialEnabled = this.materialEnabled;
            values.raycastTargetEnabled = this.raycastTargetEnabled;
            values.fontCaseEnabled = this.fontCaseEnabled;
            values.useShadowEnabled = this.useShadowEnabled;
            values.useOutlineEnabled = this.useOutlineEnabled;

            return values;
        }
    }
}





















