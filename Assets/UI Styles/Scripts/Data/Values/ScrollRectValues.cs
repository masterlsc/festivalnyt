using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class ScrollRectValues
    {
#if !PRE_UNITY_5

        [HideInInspector] public string name = string.Empty;
        [HideInInspector] public string path = string.Empty;
        [HideInInspector] public bool show = true;
        [HideInInspector] public bool isCustom = false;

        // Values
        [HideInInspector] public bool horizontal = true;
        [HideInInspector] public bool vertical = true;
        [HideInInspector] public ScrollRect.MovementType movementType = ScrollRect.MovementType.Elastic;
        [HideInInspector] public float elasticity = 0.1f;
        [HideInInspector] public bool inertia = true;
        [HideInInspector] public float decelerationRate = 0.135f;
        [HideInInspector] public float scrollSensitivity = 1;
        [HideInInspector] public ScrollRect.ScrollbarVisibility horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
        [HideInInspector] public ScrollRect.ScrollbarVisibility verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
        [HideInInspector] public float horizontalScrollbarSpacing = -3;
        [HideInInspector] public float verticalScrollbarSpacing = -3;
        [HideInInspector] public bool showVerticalScrollbar = true;
        [HideInInspector] public bool showHorizontalScrollbar = true;

        [HideInInspector] public string contentReference;
        [HideInInspector] public string viewportReference;
        [HideInInspector] public string horizonalScrollbarReference;
        [HideInInspector] public string verticalScrollbarReference;

        // enabled
        [HideInInspector] public bool showVerticalScrollbarEnabled;
        [HideInInspector] public bool showHorizontalScrollbarEnabled;
        [HideInInspector] public bool horizontalEnabled;
        [HideInInspector] public bool verticalEnabled;
        [HideInInspector] public bool movementTypeEnabled;
        [HideInInspector] public bool elasticityEnabled;
        [HideInInspector] public bool inertiaEnabled;
        [HideInInspector] public bool decelerationRateEnabled;
        [HideInInspector] public bool scrollSensitivityEnabled;
        [HideInInspector] public bool horizontalScrollbarVisibilityEnabled;
        [HideInInspector] public bool verticalScrollbarVisibilityEnabled;
        [HideInInspector] public bool horizontalScrollbarSpacingEnabled;
        [HideInInspector] public bool verticalScrollbarSpacingEnabled;


        public ScrollRectValues CloneValues()
        {
            ScrollRectValues values = new ScrollRectValues();

            values.name = this.name;
            values.path = this.path;
            values.show = this.show;
            values.isCustom = this.isCustom;

            values.horizontal = this.horizontal;
            values.vertical = this.vertical;
            values.movementType = this.movementType;
            values.elasticity = this.elasticity;
            values.inertia = this.inertia;
            values.decelerationRate = this.decelerationRate;
            values.scrollSensitivity = this.scrollSensitivity;
            values.horizontalScrollbarVisibility = this.horizontalScrollbarVisibility;
            values.verticalScrollbarVisibility = this.verticalScrollbarVisibility;
            values.horizontalScrollbarSpacing = this.horizontalScrollbarSpacing;
            values.verticalScrollbarSpacing = this.verticalScrollbarSpacing;
            values.showVerticalScrollbar = this.showVerticalScrollbar;
            values.showHorizontalScrollbar = this.showHorizontalScrollbar;

            values.showVerticalScrollbarEnabled = this.showVerticalScrollbarEnabled;
            values.showHorizontalScrollbarEnabled = this.showHorizontalScrollbarEnabled;
            values.horizontalEnabled = this.horizontalEnabled;
            values.verticalEnabled = this.verticalEnabled;
            values.movementTypeEnabled = this.movementTypeEnabled;
            values.elasticityEnabled = this.elasticityEnabled;
            values.inertiaEnabled = this.inertiaEnabled;
            values.decelerationRateEnabled = this.decelerationRateEnabled;
            values.scrollSensitivityEnabled = this.scrollSensitivityEnabled;
            values.horizontalScrollbarVisibilityEnabled = this.horizontalScrollbarVisibilityEnabled;
            values.verticalScrollbarVisibilityEnabled = this.verticalScrollbarVisibilityEnabled;
            values.horizontalScrollbarSpacingEnabled = this.horizontalScrollbarSpacingEnabled;
            values.verticalScrollbarSpacingEnabled = this.verticalScrollbarSpacingEnabled;

            values.contentReference = this.contentReference;
            values.viewportReference = this.viewportReference;
            values.horizonalScrollbarReference = this.horizonalScrollbarReference;
            values.verticalScrollbarReference = this.verticalScrollbarReference;

            return values;
        }

#endif
    }
}




















