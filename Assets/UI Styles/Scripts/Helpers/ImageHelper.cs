using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
	public class ImageHelper 
	{
		/// <summary>
		/// Create Style, pass in an object to match the styles values with the objects values
		/// </summary>
		public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj, bool includeRectTransform)
		{
			Style style = new Style(data, category, styleName, findByName);
			style.rename = !Application.isPlaying;
						
			// Image
			StyleComponent v = new StyleComponent(data, style, StyleComponentType.Image, "Image", "");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj, includeRectTransform );
			
			UIStylesDatabase.Save ();
		}
		
		/// <summary>
		/// Sets the Values from a component
		/// </summary>
		public static ImageValues SetValuesFromComponent ( Component com, bool includeRectTransform )
		{	
			if (com is Image)
				return SetValuesFromComponent ( (Image)com, null, includeRectTransform );
			
			else if (com is RawImage)
				return SetValuesFromComponent ( (RawImage)com, null, includeRectTransform );
			
			else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be Image or RawImage");
			
			return null;
		}
		
		public static ImageValues SetValuesFromComponent ( GameObject obj, bool includeRectTransform )
		{	
			if (obj.GetComponent<Image>())
				return SetValuesFromComponent ( obj.GetComponent<Image>(), obj, includeRectTransform );
			
			else if (obj.GetComponent<RawImage>())
				return SetValuesFromComponent ( obj.GetComponent<RawImage>(), obj, includeRectTransform );
			
			else Debug.LogError ("SetValuesFromComponent: Component not found!");
			
			return null;
		}
		
		public static ImageValues SetValuesFromComponent ( RawImage rawImage, GameObject obj, bool includeRectTransform )
		{	
			ImageValues values = new ImageValues ();
			
			values.componentType = ImageValues.ComponentType.RawImage;
			
			values.texture = rawImage.texture as Texture2D;
			values.color = rawImage.color;
			values.material = rawImage.material;
			values.uvRect = rawImage.uvRect;
			
			#if !PRE_UNITY_5
			values.raycastTarget = rawImage.raycastTarget;
			#endif
			
			// enabled
			values.imageEnabled = true;
			values.textureEnabled = true;
			values.uiRectEnabled = true;
			values.colorEnabled = true;
			values.materialEnabled = true;
			values.raycastTargetEnabled = true;
			values.imageTypeEnabled = true;
			
			if (obj != null)
				return ValueEffects (values, obj, includeRectTransform);
			
			return values;
		}
		
		public static ImageValues SetValuesFromComponent ( Image image, GameObject obj, bool includeRectTransform )
		{
			ImageValues values = new ImageValues ();
			
			values.componentType = ImageValues.ComponentType.Image;
			
			values.image = image.sprite;
			values.color = image.color;
			values.material = image.material;
			
			#if !PRE_UNITY_5
			values.raycastTarget = image.raycastTarget;
			#endif
			
			values.imageType = image.type;
			values.fillMethod = image.fillMethod;	
			
			if ( image.fillMethod == Image.FillMethod.Horizontal )
			{
				if ( image.fillOrigin == (int)Image.OriginHorizontal.Left )
				{
					values.originHorizontal = (int)Image.OriginHorizontal.Left;
				}
				else if ( image.fillOrigin == (int)Image.OriginHorizontal.Right )
				{
					values.originHorizontal = Image.OriginHorizontal.Right;
				}
			}
			else if ( image.fillMethod == Image.FillMethod.Vertical )
			{
				if ( image.fillOrigin == (int)Image.OriginVertical.Top )
				{
					values.originVertical = Image.OriginVertical.Top;
				}
				else if ( image.fillOrigin == (int)Image.OriginVertical.Bottom )
				{
					values.originVertical = (int)Image.OriginVertical.Bottom;
				}
			}
			else if ( image.fillMethod == Image.FillMethod.Radial90 )
			{
				if ( image.fillOrigin == (int)Image.Origin90.BottomLeft )
				{
					values.origin90 = (int)Image.Origin90.BottomLeft;
				}
				else if ( image.fillOrigin == (int)Image.Origin90.BottomRight )
				{
					values.origin90 = Image.Origin90.BottomRight;
				}
				else if ( image.fillOrigin == (int)Image.Origin90.TopLeft )
				{
					values.origin90 = Image.Origin90.TopLeft;
				}
				else if ( image.fillOrigin == (int)Image.Origin90.TopRight )
				{
					values.origin90 = Image.Origin90.TopRight;
				}
			}
			else if ( image.fillMethod == Image.FillMethod.Radial180 )
			{
				if ( image.fillOrigin == (int)Image.Origin180.Bottom )
				{
					values.origin180 = (int)Image.Origin180.Bottom;
				}
				else if ( image.fillOrigin == (int)Image.Origin180.Top )
				{
					values.origin180 = Image.Origin180.Top;
				}
				else if ( image.fillOrigin == (int)Image.Origin180.Left )
				{
					values.origin180 = Image.Origin180.Left;
				}
				else if ( image.fillOrigin == (int)Image.Origin180.Right )
				{
					values.origin180 = Image.Origin180.Right;
				}
			}
			else if ( image.fillMethod == Image.FillMethod.Radial360 )
			{
				if ( image.fillOrigin == (int)Image.Origin360.Bottom )
				{
					values.origin360 = (int)Image.Origin360.Bottom;
				}
				else if ( image.fillOrigin == (int)Image.Origin360.Top )
				{
					values.origin360 = Image.Origin360.Top;
				}
				else if ( image.fillOrigin == (int)Image.Origin360.Left )
				{
					values.origin360 = Image.Origin360.Left;
				}
				else if ( image.fillOrigin == (int)Image.Origin360.Right )
				{
					values.origin360 = Image.Origin360.Right;
				}
			}
			
			values.preserveAspect	= image.preserveAspect;
			values.clockwise = image.fillClockwise;
			values.fillAmount = image.fillAmount;
			values.fillCentre = image.fillCenter;
			
			// enabled
			values.imageEnabled = true;
			values.textureEnabled = true;
			values.uiRectEnabled = true;
			values.colorEnabled = true;
			values.materialEnabled = true;
			values.raycastTargetEnabled = true;
			values.imageTypeEnabled = true;
			
			if ( values.setNativeSize )
				image.SetNativeSize ();
			
			if (obj != null)
				return ValueEffects (values, obj, includeRectTransform);
			
			return values;
		}
		
		
		
		
		
		
		public static ImageValues ValueEffects (ImageValues values, GameObject obj, bool includeRectTransform)
		{		
			bool gotGradient = false;
			bool gotOutline = false;
			bool gotShadow = false;
			
			bool replaceUnityOutline = false;
			bool replaceUnityShadow = false;
			
			Component[] components = obj.GetComponents<Component> ();
			foreach ( Component c in components )
			{
				if ( c != null )
				{
					string component = c.GetType ().ToString ();
					
					if ( !gotGradient )
						gotGradient = component == "UIStyles.Gradient";
					
					if ( !gotOutline )
						gotOutline = component == "UIStyles.Outline";
					
					if ( !gotShadow )
						gotShadow = component == "UIStyles.Shadow";
					
					// Unity Effects
					if ( !replaceUnityOutline )
					{
						replaceUnityOutline = component == "UnityEngine.UI.Outline";
					}
					
					if ( !replaceUnityShadow )
					{
						replaceUnityShadow = component == "UnityEngine.UI.Shadow";
					}
				}
			}
			
			if ( replaceUnityOutline )
			{
				Color colour = new Color();
				Vector2 distance = Vector2.zero;
				bool alpha = true;
				
				while ( obj.GetComponent<UnityEngine.UI.Outline> () )
				{
					colour = obj.GetComponent<UnityEngine.UI.Outline> ().effectColor;
					distance = obj.GetComponent<UnityEngine.UI.Outline> ().effectDistance;
					alpha = obj.GetComponent<UnityEngine.UI.Outline> ().useGraphicAlpha;
					
					#if UNITY_EDITOR
					UnityEditor.Editor.DestroyImmediate ( obj.GetComponent<UnityEngine.UI.Outline> (), true );
					#else
					GameObject.Destroy ( obj.GetComponent<UnityEngine.UI.Outline> () );
					#endif
				}
				
				obj.gameObject.AddComponent<UIStyles.Outline> ();
				obj.gameObject.GetComponent<UIStyles.Outline> ().effectColor = colour;
				obj.gameObject.GetComponent<UIStyles.Outline> ().effectDistance = distance;
				obj.gameObject.GetComponent<UIStyles.Outline> ().useGraphicAlpha = alpha;
				
				gotOutline = true;
			}
			
			if ( replaceUnityShadow )
			{
				Color colour = new Color();
				Vector2 distance = Vector2.zero;
				bool alpha = true;
				
				while(obj.GetComponent<UnityEngine.UI.Shadow> ())
				{
					colour = obj.GetComponent<UnityEngine.UI.Shadow> ().effectColor;
					distance = obj.GetComponent<UnityEngine.UI.Shadow> ().effectDistance;
					alpha = obj.GetComponent<UnityEngine.UI.Shadow> ().useGraphicAlpha;
					
					#if UNITY_EDITOR
					UnityEditor.Editor.DestroyImmediate ( obj.GetComponent<UnityEngine.UI.Shadow> (), true );
					#else
					GameObject.Destroy ( obj.GetComponent<UnityEngine.UI.Shadow> () );
					#endif
				}
				
				obj.gameObject.AddComponent<UIStyles.Shadow> ();
				obj.gameObject.GetComponent<UIStyles.Shadow> ().effectColor = colour;
				obj.gameObject.GetComponent<UIStyles.Shadow> ().effectDistance = distance;
				obj.gameObject.GetComponent<UIStyles.Shadow> ().useGraphicAlpha = alpha;
				
				gotShadow = true;
			}
			
			// Outline	
			values.useOutline = gotOutline;
			
			if ( values.useOutline )
			{
				if (!obj.GetComponent<UIStyles.Outline> ())
					obj.gameObject.AddComponent<UIStyles.Outline> ();
				
				values.outlineColor = obj.GetComponent<UIStyles.Outline> () ? obj.GetComponent<UIStyles.Outline> ().effectColor : obj.GetComponent<UnityEngine.UI.Outline> ().effectColor;
				values.outlineDistance = obj.GetComponent<UIStyles.Outline> () ? obj.GetComponent<UIStyles.Outline> ().effectDistance : obj.GetComponent<UnityEngine.UI.Outline> ().effectDistance;
				values.outlineUseAlpha = obj.GetComponent<UIStyles.Outline> () ? obj.GetComponent<UIStyles.Outline> ().useGraphicAlpha : obj.GetComponent<UnityEngine.UI.Outline> ().useGraphicAlpha;
			}
			
			// Shadow
			values.useShadow = gotShadow;
			
			if ( values.useShadow )
			{
				if (!obj.GetComponent<UIStyles.Shadow> ())
					obj.gameObject.AddComponent<UIStyles.Shadow> ();
				
				values.shadowColor = obj.GetComponent<UIStyles.Shadow> () ? obj.GetComponent<UIStyles.Shadow> ().effectColor : obj.GetComponent<UnityEngine.UI.Shadow> ().effectColor;
				values.shadowDistance = obj.GetComponent<UIStyles.Shadow> () ? obj.GetComponent<UIStyles.Shadow> ().effectDistance : obj.GetComponent<UnityEngine.UI.Shadow> ().effectDistance;
				values.shadowUseAlpha = obj.GetComponent<UIStyles.Shadow> () ? obj.GetComponent<UIStyles.Shadow> ().useGraphicAlpha : obj.GetComponent<UnityEngine.UI.Shadow> ().useGraphicAlpha;
			}
			
			// Gradient																						
			#if !PRE_UNITY_5
			if ( values.overlay == Overlay.GradientOverlay )
			{
				values.gradientTopColor = obj.GetComponent<UIStyles.Gradient> ().topColor;
				values.gradientBottomColor = obj.GetComponent<UIStyles.Gradient> ().bottomColor;
			}
			#endif
						
			values.useOutlineEnabled = gotOutline;
			values.useShadowEnabled = gotShadow;
						
			if (Application.isPlaying && includeRectTransform)
				values.rectTransformValues = RectTransformHelper.SetValuesFromComponent ( obj );
			
			return values;
		}
		
		
		
		
		/// <summary>
		/// Apply the specified values
		/// </summary>
		public static void Apply ( ImageValues values, GameObject obj )
		{
			// set values
			if ( values.imageEnabled )
			{
				if ( values.componentType == UIStyles.ImageValues.ComponentType.Image )
					obj.GetComponent<Image> ().sprite = values.image;
				else if ( values.componentType == UIStyles.ImageValues.ComponentType.RawImage )
					obj.GetComponent<RawImage> ().texture = values.texture;
			}
			
			if ( values.colorEnabled )
			{
				if ( values.componentType == UIStyles.ImageValues.ComponentType.Image )
					obj.GetComponent<Image> ().color = values.color;
				else if ( values.componentType == UIStyles.ImageValues.ComponentType.RawImage )
					obj.GetComponent<RawImage> ().color = values.color;
			}
			
			if ( values.materialEnabled )
			{
				if ( values.componentType == UIStyles.ImageValues.ComponentType.Image )
					obj.GetComponent<Image> ().material = values.material;
				else if ( values.componentType == UIStyles.ImageValues.ComponentType.RawImage )
					obj.GetComponent<RawImage> ().material = values.material;
			}
			
			if ( values.uiRectEnabled )
			{
				if ( values.componentType == UIStyles.ImageValues.ComponentType.RawImage )
					obj.GetComponent<RawImage> ().uvRect = values.uvRect;
			}
			
			#if !PRE_UNITY_5
			if ( values.raycastTargetEnabled )
			{
				if ( values.componentType == UIStyles.ImageValues.ComponentType.Image )
					obj.GetComponent<Image> ().raycastTarget = values.raycastTarget;
				else if ( values.componentType == UIStyles.ImageValues.ComponentType.RawImage )
					obj.GetComponent<RawImage> ().raycastTarget = values.raycastTarget;
			}
			#endif
			
			if ( values.imageTypeEnabled && values.componentType == UIStyles.ImageValues.ComponentType.Image )
			{
				obj.GetComponent<Image> ().type = values.imageType;
				obj.GetComponent<Image> ().fillMethod = values.fillMethod;
				
				if ( values.fillMethod == UnityEngine.UI.Image.FillMethod.Horizontal )
					obj.GetComponent<Image> ().fillOrigin = (int)values.originHorizontal;
				else if ( values.fillMethod == UnityEngine.UI.Image.FillMethod.Vertical )
					obj.GetComponent<Image> ().fillOrigin = (int)values.originVertical;
				else if ( values.fillMethod == UnityEngine.UI.Image.FillMethod.Radial90 )
					obj.GetComponent<Image> ().fillOrigin = (int)values.origin90;
				else if ( values.fillMethod == UnityEngine.UI.Image.FillMethod.Radial180 )
					obj.GetComponent<Image> ().fillOrigin = (int)values.origin180;
				else if ( values.fillMethod == UnityEngine.UI.Image.FillMethod.Radial360 )
					obj.GetComponent<Image> ().fillOrigin = (int)values.origin360;
				
				obj.GetComponent<Image> ().preserveAspect = values.preserveAspect;
				obj.GetComponent<Image> ().fillClockwise = values.clockwise;
				obj.GetComponent<Image> ().fillAmount = values.fillAmount;
				obj.GetComponent<Image> ().fillCenter = values.fillCentre;
				
				if ( values.setNativeSize )
					obj.GetComponent<Image> ().SetNativeSize ();
			}
			
			if (values.useAsMask)
			{
				if ( !obj.GetComponent<Mask> () )
					obj.gameObject.AddComponent<Mask> ();
				
				obj.GetComponent<Mask> ().showMaskGraphic = values.showMaskGraphic;
			}
			else
			{
				if ( obj.GetComponent<Mask> ())
				{
					#if UNITY_EDITOR
					UnityEditor.Editor.DestroyImmediate ( obj.GetComponent<Mask> (), true );
					#else
					GameObject.Destroy ( obj.GetComponent<Mask> () );
					#endif
				}
			}
			
			if ( values.useOutlineEnabled )
			{
				if ( values.useOutline )
				{
					if ( !obj.GetComponent<UIStyles.Outline> () )
					{
						if ( DataHelper.canEditValues ( obj.gameObject ) )
						{
							obj.gameObject.AddComponent<UIStyles.Outline> ();
						}
					}
					
					if ( obj.GetComponent<UIStyles.Outline> () )
					{
						obj.GetComponent<UIStyles.Outline> ().effectColor = values.outlineColor;
						obj.GetComponent<UIStyles.Outline> ().effectDistance = values.outlineDistance;
						obj.GetComponent<UIStyles.Outline> ().useGraphicAlpha = values.outlineUseAlpha;
					}
				}
				else if ( obj.GetComponent<UIStyles.Outline> () )
				{
					#if UNITY_EDITOR
					UnityEditor.Editor.DestroyImmediate ( obj.GetComponent<UIStyles.Outline> (), true );
					#else
					GameObject.Destroy ( obj.GetComponent<UIStyles.Outline> () );
					#endif
				}
			}
			
			if ( values.useShadowEnabled )
			{
				if ( values.useShadow )
				{
					if ( !obj.GetComponent<UIStyles.Shadow> () )
					{
						if ( DataHelper.canEditValues ( obj.gameObject ) )
						{
							obj.gameObject.AddComponent<UIStyles.Shadow> ();
						}
					}
					
					if ( obj.GetComponent<UIStyles.Shadow> () )
					{
						obj.GetComponent<UIStyles.Shadow> ().effectColor = values.shadowColor;
						obj.GetComponent<UIStyles.Shadow> ().effectDistance = values.shadowDistance;
						obj.GetComponent<UIStyles.Shadow> ().useGraphicAlpha = values.shadowUseAlpha;
					}
				}
				else if ( obj.GetComponent<UIStyles.Shadow> () )
				{
					#if UNITY_EDITOR
					UnityEditor.Editor.DestroyImmediate ( obj.GetComponent<UIStyles.Shadow> (), true );
					#else
					GameObject.Destroy ( obj.GetComponent<UIStyles.Shadow> () );
					#endif
				}
			}
			
			#if !PRE_UNITY_5
			if ( values.overlay == UIStyles.Overlay.GradientOverlay )
			{
				if ( !obj.GetComponent<UIStyles.Gradient> () )
				{
					if ( DataHelper.canEditValues ( obj.gameObject ) )
					{
						obj.gameObject.AddComponent<UIStyles.Gradient> ();
					}
				}
				
				if ( obj.GetComponent<UIStyles.Gradient> () )
				{
					obj.GetComponent<UIStyles.Gradient> ().topColor = values.gradientTopColor;
					obj.GetComponent<UIStyles.Gradient> ().bottomColor = values.gradientBottomColor;
				}
			}
			else if ( obj.GetComponent<UIStyles.Gradient> () )
			{
				#if UNITY_EDITOR
				UnityEditor.Editor.DestroyImmediate ( obj.GetComponent<UIStyles.Gradient> (), true );
				#else
				GameObject.Destroy ( obj.GetComponent<UIStyles.Gradient> () );
				#endif
			}
			
			#endif
			
			// Rect Transform
			RectTransformHelper.Apply ( values.rectTransformValues, obj.GetComponent<RectTransform> () );
			
			// Check
			CheckEffects.CheckAll ( obj.gameObject, null, values );
		}
	}
}



















