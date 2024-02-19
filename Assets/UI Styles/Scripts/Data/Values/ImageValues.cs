using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
	[System.Serializable]
	public class ImageValues 
	{
		// Rect transform data
		[HideInInspector] public RectTransformValues rectTransformValues = new RectTransformValues();
						
		public enum ComponentType {Image, RawImage}
		[HideInInspector] public ComponentType componentType = ComponentType.Image;
		
		[HideInInspector] public bool useAsMask;
		[HideInInspector] public bool showMaskGraphic;
		
		// Values
		[HideInInspector] public Sprite image;
		[HideInInspector] public Texture2D texture;
		[HideInInspector] public Rect uvRect; 
		[HideInInspector] public Color color = Color.black;
		[HideInInspector] public string colorID;
		[HideInInspector] public Material material;
		[HideInInspector] public bool raycastTarget = true;
		[HideInInspector] public Image.Type imageType;
		[HideInInspector] public Image.FillMethod fillMethod;
		[HideInInspector] public Image.OriginHorizontal originHorizontal;
		[HideInInspector] public Image.OriginVertical originVertical;
		[HideInInspector] public Image.Origin90 origin90;
		[HideInInspector] public Image.Origin180 origin180;
		[HideInInspector] public Image.Origin360 origin360;
		[HideInInspector] public float fillAmount = 1;
		[HideInInspector] public bool clockwise = true;
		[HideInInspector] public bool fillCentre;
		[HideInInspector] public bool preserveAspect;
		[HideInInspector] public bool setNativeSize;
		
		[HideInInspector] public Overlay overlay;
		[HideInInspector] public Color gradientTopColor = Color.white;
		[HideInInspector] public string gradientTopColorID;
		[HideInInspector] public Color gradientBottomColor = Color.black;
		[HideInInspector] public string gradientBottomColorID;
		[HideInInspector] public bool showImageColorWithGradient = false;
		
		
		// effects
		[HideInInspector] public bool useShadow;
		[HideInInspector] public Color shadowColor = new Color (0,0,0,.5f);
		[HideInInspector] public string shadowColorID;
		[HideInInspector] public Vector2 shadowDistance = new Vector2(1, -1);
		[HideInInspector] public bool shadowUseAlpha = true;
		
		[HideInInspector] public bool useOutline;
		[HideInInspector] public Color outlineColor = new Color (0,0,0,.5f);
		[HideInInspector] public string outlineColorID;
		[HideInInspector] public Vector2 outlineDistance = new Vector2(1, -1);
		[HideInInspector] public bool outlineUseAlpha = true;
		
		// enabled
		[HideInInspector]public bool imageEnabled;
		[HideInInspector]public bool textureEnabled;
		[HideInInspector]public bool uiRectEnabled;
		[HideInInspector]public bool colorEnabled;
		[HideInInspector]public bool materialEnabled;
		[HideInInspector]public bool raycastTargetEnabled;
		[HideInInspector]public bool imageTypeEnabled;
		
		[HideInInspector]public bool useShadowEnabled;
		[HideInInspector]public bool useOutlineEnabled;
		
		
		public ImageValues CloneValues ()
		{
			ImageValues values = new ImageValues();
			
			values.rectTransformValues			= this.rectTransformValues.CloneValues();
			
			values.componentType				= this.componentType;
			
			values.useAsMask 					= this.useAsMask;
			values.showMaskGraphic				= this.showMaskGraphic;
			
			values.image						= this.image;
			values.texture						= this.texture;
			values.uvRect						= this.uvRect; 
			values.color						= this.color;
			values.colorID						= this.colorID;
			values.material 					= this.material;
			values.raycastTarget				= this.raycastTarget;
			values.imageType					= this.imageType;
			values.fillMethod					= this.fillMethod;
			values.originHorizontal 			= this.originHorizontal;
			values.originVertical				= this.originVertical;
			values.origin90 					= this.origin90;
			values.origin180					= this.origin180;
			values.origin360					= this.origin360;
			values.fillAmount					= this.fillAmount;
			values.clockwise					= this.clockwise;
			values.fillCentre					= this.fillCentre;
			values.preserveAspect				= this.preserveAspect;
			values.setNativeSize				= this.setNativeSize;
			
			values.overlay						= this.overlay;
			values.gradientTopColor 			= this.gradientTopColor;
			values.gradientTopColorID			= this.gradientTopColorID;
			values.gradientBottomColor			= this.gradientBottomColor;
			values.gradientBottomColorID		= this.gradientBottomColorID;
			values.showImageColorWithGradient	= this.showImageColorWithGradient;
			
			values.useShadow					= this.useShadow;
			values.shadowColor					= this.shadowColor;
			values.shadowColorID				= this.shadowColorID;
			values.shadowDistance				= this.shadowDistance;
			values.shadowUseAlpha				= this.shadowUseAlpha;
			
			values.useOutline					= this.useOutline;
			values.outlineColor 				= this.outlineColor;
			values.outlineColorID				= this.outlineColorID;
			values.outlineDistance				= this.outlineDistance;
			values.outlineUseAlpha				= this.outlineUseAlpha;
			
			values.imageEnabled 				= this.imageEnabled;
			values.textureEnabled				= this.textureEnabled;
			values.uiRectEnabled				= this.uiRectEnabled;
			values.colorEnabled 				= this.colorEnabled;
			values.materialEnabled				= this.materialEnabled;
			values.raycastTargetEnabled 		= this.raycastTargetEnabled;
			values.imageTypeEnabled 			= this.imageTypeEnabled;
			
			values.useShadowEnabled 			= this.useShadowEnabled;
			values.useOutlineEnabled			= this.useOutlineEnabled;
			
			return values;
		}
	}
}



















