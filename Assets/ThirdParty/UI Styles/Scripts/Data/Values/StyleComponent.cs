using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UIStyles
{
	public enum StyleComponentType 
	{ 
		Text, 
		Image, 
		Button, 
		InputField, 
		Dropdown, 
		ScrollRect, 
		Scrollbar,
		Slider, 
		Toggle, 
		Custom, 
		RectTransform, 
		Group, 
		Camera,
		CanvasGroup,
		LayoutElement,
		ContentSizeFitter,
		AspectRatioFitter,
		HorizontalLayoutGroup,
		VerticalLayoutGroup,
		GridLayoutGroup,
		Canvas,
		CanvasScaler,
		GraphicRaycaster,
		/*<UIStylesTag(StyleComponentType)>*/
	}
	
	public enum StyleComponentState 
	{
		Enable, 
		Disable, 
		AlwaysAdd, 
		AlwaysRemove, 
		AlwaysDelete
	}
	
	[System.Serializable]
	public class StyleComponentGroup
	{
		public List<int> componentIDs = new List<int>();
	}
	
	[System.Serializable]
    public class StyleComponent
    {
#if UNITY_EDITOR
        [HideInInspector]
        public bool open = false;
        [HideInInspector]
        public bool rename = false;
        [HideInInspector]
        public bool renamePath = false;
#endif

        [HideInInspector]
	    public bool hasPathError = false;
	    
	    [HideInInspector]
	    public StyleComponentState styleComponentState = StyleComponentState.Enable;

        [HideInInspector]
        public int id;
        [HideInInspector]
	    public int parentID;
	    [HideInInspector]
	    public int groupID;
        [HideInInspector]
        public string name = string.Empty;
        [HideInInspector]
	    public string path = string.Empty;

        /// <summary>
        /// Component values
        /// </summary>
	    [HideInInspector]
	    public StyleComponentType styleComponentType;
	    [HideInInspector]
	    public StyleComponentGroup group;
        [HideInInspector]
        public TextValues text;
        [HideInInspector]
        public ImageValues image;
        [HideInInspector]
        public ButtonValues button;
        [HideInInspector]
        public InputFieldValues inputField;
        [HideInInspector]
        public DropdownValues dropdown;
        [HideInInspector]
        public ScrollRectValues scrollRect;
        [HideInInspector]
        public ScrollbarValues scrollbar;
        [HideInInspector]
        public SliderValues slider;
        [HideInInspector]
        public ToggleValues toggle;
        [HideInInspector]
	    public CustomComponentValues custom;
	    [HideInInspector]
	    public RectTransformValues rectTransform;
	    [HideInInspector]
	    public CameraValues camera;
	    [HideInInspector]
	    public CanvasGroupValues canvasGroup;
	    [HideInInspector]
	    public LayoutElementValues layoutElement;
	    [HideInInspector]
	    public ContentSizeFitterValues contentSizeFitter;
	    [HideInInspector]
	    public AspectRatioFitterValues aspectRatioFitter;
	    [HideInInspector]
	    public HorizontalLayoutGroupValues horizontalLayoutGroup;
	    [HideInInspector]
	    public VerticalLayoutGroupValues verticalLayoutGroup;
	    [HideInInspector]
	    public GridLayoutGroupValues gridLayoutGroup;
	    [HideInInspector]
	    public CanvasValues canvas;
	    [HideInInspector]
	    public CanvasScalerValues canvasScaler;
	    [HideInInspector]
	    public GraphicRaycasterValues graphicRaycaster;
	    /*<UIStylesTag(StyleComponentVar)>*/
	    


        /// <summary>
        /// Using this override will not initialise the component, it will not automatically be given an id, neither will it be added to the list of components within its style.
        /// </summary>
        public StyleComponent() { }

        /// <summary>
        /// Using this override will initialise the component to give it an id but it will not add it to the list of components within its style, it will default the type to Text and have an empty name and path.
        /// </summary>
        /// param: data = The data file to use
        public StyleComponent(StyleDataFile data)
        {
            Initialise(data, 0, StyleComponentType.Text, "", "");
        }

        /// <summary>
        /// Using this override will initialise the component to give it an id but it will not add it to the list of components within its style, it will set its type, name and path.
        /// </summary>
        /// param: data 			= The data file to use
        /// param: type				= The style component type.
        /// param: componentName	= The name of the component
        /// param: componentPath	= The components path.
        public StyleComponent(StyleDataFile data, StyleComponentType type, string componentName, string componentPath = "")
        {
            Initialise(data, 0, type, componentName, componentPath);
        }

        /// <summary>
        /// Using this override will initialise the component to give it an id and add it to the list of components within its style, it will also set its type, name and path.
        /// </summary>
        /// param: data 			= The data file to use
        /// param: type				= The style component type.
        /// param: styleID			= The styles id, the style the component will be added to
        /// param: componentName	= The name of the component
        /// param: componentPath	= The components path.
        public StyleComponent(StyleDataFile data, int styleID, StyleComponentType type, string componentName, string componentPath = "")
        {
            Initialise(data, styleID, type, componentName, componentPath);
        }

        /// <summary>
        /// New ComponentValues
        /// </summary>
        /// param: data 			= The data file to use
        /// param: style			= The style, the style the component will be added to
        /// param: type				= The style component type.
        /// param: componentName	= The name of the component
        /// param: componentPath	= The components path.
        public StyleComponent(StyleDataFile data, Style style, StyleComponentType type, string componentName, string componentPath = "")
        {
            Initialise(data, style.id, type, componentName, componentPath);
        }

        /// <summary>
        /// Using this override will initialise the component to give it an id and add it to the list of components within its style, it will also set its type, name and path.
        /// </summary>
        /// param: data 				= The data file to use
        /// param: stylesCategory		= The category the style is in
        /// param: styleName			= The styles name, the style the component will be added to
        /// param: type					= The style component type.
        /// param: componentName		= The name of the component
        /// param: componentPath		= The components path.
        public StyleComponent(StyleDataFile data, string stylesCategory, string styleName, StyleComponentType type, string componentName, string componentPath = "")
        {
            Initialise(data, StyleHelper.GetStyleID(data, stylesCategory, styleName), type, componentName, componentPath);
        }

        private void Initialise(StyleDataFile data, int styleID, StyleComponentType type, string componentName, string componentPath = "")
        {
            id = data.GetNewComponentID();
	        data.componentIDs.Add(id, this);

            this.SetType(type);
            this.name = componentName;
	        this.path = componentPath;
	        
	        if (StyleHelper.GetStyle(data, styleID) != null)
	        {
		        this.parentID = StyleHelper.GetStyle(data, styleID).id;
		        StyleHelper.GetStyle(data, styleID).AddComponent(this);
		        
		        if (type == StyleComponentType.Group && this.groupID == 0)
			        this.groupID = StyleHelper.GetStyle(data, parentID).GetNewGroupID();
		    }
        }

        public void SetType(StyleComponentType type)
        {
            styleComponentType = type;
	        
	        if (type == StyleComponentType.Group)
		        group = new StyleComponentGroup();
	        
	        if (type == StyleComponentType.Text)
                text = new TextValues();
	        
	        if (type == StyleComponentType.Image)
                image = new ImageValues();
	        
	        if (type == StyleComponentType.Button)
	            button = new ButtonValues();
	        
	        if (type == StyleComponentType.Dropdown)
		        dropdown = new DropdownValues();
	        
	        if (type == StyleComponentType.InputField)
                inputField = new InputFieldValues();
	        
	        if (type == StyleComponentType.ScrollRect)
                scrollRect = new ScrollRectValues();
	        
	        if (type == StyleComponentType.Scrollbar)
                scrollbar = new ScrollbarValues();
	        
	        if (type == StyleComponentType.Slider)
                slider = new SliderValues();
	        
	        if (type == StyleComponentType.Toggle)
                toggle = new ToggleValues();
	        
	        if (type == StyleComponentType.Custom)
	            custom = new CustomComponentValues();
	        
	        if (type == StyleComponentType.RectTransform)
		        rectTransform = new RectTransformValues();
	        
	        if (type == StyleComponentType.Camera)
		        camera = new CameraValues();
	        
	        if (type == StyleComponentType.CanvasGroup)
		        canvasGroup = new CanvasGroupValues();
	        if (type == StyleComponentType.LayoutElement)
		        layoutElement = new LayoutElementValues();
	        if (type == StyleComponentType.ContentSizeFitter)
		        contentSizeFitter = new ContentSizeFitterValues();
	        if (type == StyleComponentType.AspectRatioFitter)
		        aspectRatioFitter = new AspectRatioFitterValues();
	        if (type == StyleComponentType.HorizontalLayoutGroup)
		        horizontalLayoutGroup = new HorizontalLayoutGroupValues();
	        if (type == StyleComponentType.VerticalLayoutGroup)
		        verticalLayoutGroup = new VerticalLayoutGroupValues();
	        if (type == StyleComponentType.GridLayoutGroup)
		        gridLayoutGroup = new GridLayoutGroupValues();
	        if (type == StyleComponentType.Canvas)
		        canvas = new CanvasValues();
	        if (type == StyleComponentType.CanvasScaler)
		        canvasScaler = new CanvasScalerValues();
	        if (type == StyleComponentType.GraphicRaycaster)
		        graphicRaycaster = new GraphicRaycasterValues();
	        /*<UIStylesTag(SetType)>*/
        }

        public void SwitchComponent(StyleComponentType newType)
	    {
		    if (styleComponentType == StyleComponentType.Group)
			    group = null;
		    
		    if (styleComponentType == StyleComponentType.Text)
                text = null;
		    
		    if (styleComponentType == StyleComponentType.Image)
                image = null;
		    
		    if (styleComponentType == StyleComponentType.Button)
	            button = null;
		    
		    if (styleComponentType == StyleComponentType.Dropdown)
		        dropdown = null;
		    
		    if (styleComponentType == StyleComponentType.InputField)
                inputField = null;
		    
		    if (styleComponentType == StyleComponentType.ScrollRect)
                scrollRect = null;
		    
		    if (styleComponentType == StyleComponentType.Scrollbar)
                scrollbar = null;
		    
		    if (styleComponentType == StyleComponentType.Slider)
                slider = null;
		    
		    if (styleComponentType == StyleComponentType.Toggle)
                toggle = null;
		    
		    if (styleComponentType == StyleComponentType.Custom)
	            custom = null;
		    
		    if (styleComponentType == StyleComponentType.RectTransform)
		        rectTransform = null;
		    
		    if (styleComponentType == StyleComponentType.Camera)
			    camera = null;
		    
		    if (styleComponentType == StyleComponentType.CanvasGroup)
			    canvasGroup = null;
		    
		    if (styleComponentType == StyleComponentType.LayoutElement)
			    layoutElement = null;
		    
		    if (styleComponentType == StyleComponentType.ContentSizeFitter)
			    contentSizeFitter = null;
		    
		    if (styleComponentType == StyleComponentType.AspectRatioFitter)
			    aspectRatioFitter = null;
		    
		    if (styleComponentType == StyleComponentType.HorizontalLayoutGroup)
			    horizontalLayoutGroup = null;
		    
		    if (styleComponentType == StyleComponentType.VerticalLayoutGroup)
			    verticalLayoutGroup = null;
		    
		    if (styleComponentType == StyleComponentType.GridLayoutGroup)
			    gridLayoutGroup = null;
		    
		    if (styleComponentType == StyleComponentType.Canvas)
			    canvas = null;
		    
		    if (styleComponentType == StyleComponentType.CanvasScaler)
			    canvasScaler = null;
		    
		    if (styleComponentType == StyleComponentType.GraphicRaycaster)
			    graphicRaycaster = null;
		    
		    /*<UIStylesTag(SetTypeSwitch)>*/
		    

            SetType(newType);
        }

	    public StyleComponent Clone(StyleDataFile data, Style style)
        {
            StyleComponent values = new StyleComponent(data);

            values.styleComponentType = this.styleComponentType;

			#if UNITY_EDITOR
            values.rename = this.rename;
            values.open = this.open;
            values.renamePath = false;
			#endif
	        
	        values.name = this.name;
	        values.path = this.path;
	        
	        values.parentID = style.id;
	        values.groupID = this.groupID;

            values.hasPathError = this.hasPathError;

            values.text = this.text.CloneValues();
            values.image = this.image.CloneValues();
            values.button = this.button.CloneValues();
            values.inputField = this.inputField.CloneValues();
            values.dropdown = this.dropdown.CloneValues();
            values.scrollRect = this.scrollRect.CloneValues();
            values.scrollbar = this.scrollbar.CloneValues();
            values.slider = this.slider.CloneValues();
            values.toggle = this.toggle.CloneValues();
	        values.custom = this.custom.CloneValues();
	        values.rectTransform = this.rectTransform.CloneValues();
	        values.camera = this.camera.CloneValues();
	        values.canvasGroup = this.canvasGroup.CloneValues();
	        values.layoutElement = this.layoutElement.CloneValues();
	        values.contentSizeFitter = this.contentSizeFitter.CloneValues();
	        values.aspectRatioFitter = this.aspectRatioFitter.CloneValues();
	        values.horizontalLayoutGroup = this.horizontalLayoutGroup.CloneValues();
	        values.verticalLayoutGroup = this.verticalLayoutGroup.CloneValues();
	        values.gridLayoutGroup = this.gridLayoutGroup.CloneValues();
	        values.canvas = this.canvas.CloneValues();
	        values.canvasScaler = this.canvasScaler.CloneValues();
	        values.graphicRaycaster = this.graphicRaycaster.CloneValues();
	        /*<UIStylesTag(StyleComponentClone)>*/

	        
	        if (values.styleComponentType == StyleComponentType.Group)
	        	values.group = new StyleComponentGroup();
	        
            return values;
        }
    }
}










 













































































