using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
	public class ScrollRectHelper 
	{
		/// <summary>
		/// Create Style, pass in an object to match the styles values with the objects values
		/// </summary>
		public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj, bool includeRectTransform)
		{
			Style style = new Style(data, category, styleName, findByName);
			style.rename = !Application.isPlaying;
			
			// ScrollRect
			StyleComponent v = new StyleComponent(data, style, StyleComponentType.ScrollRect, "ScrollRect", "");
			
			if (obj != null)
				v.scrollRect = SetValuesFromComponent ( obj );
			
			else
			{
				v.scrollRect.contentReference = "Content";
				v.scrollRect.viewportReference = "Viewport";
				v.scrollRect.horizonalScrollbarReference = "Scrollbar Horizontal";
				v.scrollRect.verticalScrollbarReference = "Scrollbar Vertical";
				
				v.scrollRect.horizontalScrollbarVisibilityEnabled = true;
				v.scrollRect.horizontalScrollbarSpacingEnabled = true;
				
				v.scrollRect.verticalScrollbarVisibilityEnabled = true;
				v.scrollRect.verticalScrollbarSpacingEnabled = true;
			}
			
			// Background
			v = new StyleComponent(data, style, StyleComponentType.Image, "Background", "");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj, includeRectTransform );
			
			else 
			{				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(200, 200);
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Sliced;
				v.image.fillCentre = true;
				#endif
			}
			
			// Viewpoint
			v = new StyleComponent(data, style, StyleComponentType.Image, "Viewport", "Viewport");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Viewport").gameObject, includeRectTransform );
			
			else 
			{				
				v.image.rectTransformValues.positionEnabled = true;
				v.image.rectTransformValues.position = new Vector3(0, 0, 0);
				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(17, 17);
				
				v.image.rectTransformValues.anchorMinEnabled = true;
				v.image.rectTransformValues.anchorMin = new Vector2(0, 0);
				
				v.image.rectTransformValues.anchorMaxEnabled = true;
				v.image.rectTransformValues.anchorMax = new Vector2(1, 1);
				
				v.image.rectTransformValues.pivotEnabled = true;
				v.image.rectTransformValues.pivot = new Vector2(0, 1);
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Sliced;
				v.image.fillCentre = true;
				#endif
			}
			
			// Content
			v = new StyleComponent(data, style, StyleComponentType.RectTransform, "Content", "Viewport/Content");
			
			if (!includeRectTransform)
			{
				v.rectTransform.positionEnabled = true;
				v.rectTransform.position = new Vector3(0, 0, 0);
				
				v.rectTransform.sizeDeltaEnabled = true;
				v.rectTransform.sizeDelta = new Vector2(0, 300);
				
				v.rectTransform.anchorMinEnabled = true;
				v.rectTransform.anchorMin = new Vector2(0, 1);
				
				v.rectTransform.anchorMaxEnabled = true;
				v.rectTransform.anchorMax = new Vector2(1, 1);
				
				v.rectTransform.pivotEnabled = true;
				v.rectTransform.pivot = new Vector2(0, 1);
			}
			
			// Scrollbar Horizontal
			v = new StyleComponent(data, style, StyleComponentType.Scrollbar, "Scrollbar Horizontal", "Scrollbar Horizontal");
			
			if (obj != null)
				v.scrollbar = ScrollbarHelper.SetValuesFromComponent ( obj.transform.Find("Scrollbar Horizontal").gameObject );
			
			else
			{
				v.scrollbar.targetGraphicReference = "Scrollbar Horizontal Handle";
				v.scrollbar.handleRectReference = "Scrollbar Horizontal Handle";
				
				v.scrollbar.directionEnabled = true;
				v.scrollbar.direction = Scrollbar.Direction.LeftToRight;
			}
			
			// Scrollbar Horizontal Background
			v = new StyleComponent(data, style, StyleComponentType.Image, "Scrollbar Horizontal Background", "Scrollbar Horizontal");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Scrollbar Horizontal").gameObject, includeRectTransform );
			
			else 
			{				
				v.image.rectTransformValues.positionEnabled = true;
				v.image.rectTransformValues.position = new Vector3(0, 0, 0);
				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(17, 20);
				
				v.image.rectTransformValues.anchorMinEnabled = true;
				v.image.rectTransformValues.anchorMin = new Vector2(0, 0);
				
				v.image.rectTransformValues.anchorMaxEnabled = true;
				v.image.rectTransformValues.anchorMax = new Vector2(1, 0);
				
				v.image.rectTransformValues.pivotEnabled = true;
				v.image.rectTransformValues.pivot = new Vector2(0, 0);
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Sliced;
				v.image.fillCentre = true;
				#endif
			}
			
			// Scrollbar Horizontal Sliding Area
			v = new StyleComponent(data, style, StyleComponentType.RectTransform, "Scrollbar Horizontal Sliding Area", "Scrollbar Horizontal/Sliding Area");
			
			if (!includeRectTransform)
			{
				v.rectTransform.positionEnabled = true;
				v.rectTransform.position = new Vector3(10, 10, 0);
				
				v.rectTransform.sizeDeltaEnabled = true;
				v.rectTransform.sizeDelta = new Vector2(10, 10);
				
				v.rectTransform.anchorMinEnabled = true;
				v.rectTransform.anchorMin = new Vector2(0, 0);
				
				v.rectTransform.anchorMaxEnabled = true;
				v.rectTransform.anchorMax = new Vector2(1, 1);
				
				v.rectTransform.pivotEnabled = true;
				v.rectTransform.pivot = new Vector2(0.5f, 0.5f);
			}
			
			// Scrollbar Horizontal Handle
			v = new StyleComponent(data, style, StyleComponentType.Image, "Scrollbar Horizontal Handle", "Scrollbar Horizontal/Sliding Area/Handle");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Scrollbar Horizontal/Sliding Area/Handle").gameObject, includeRectTransform );
			
			else 
			{				
				v.image.rectTransformValues.positionEnabled = true;
				v.image.rectTransformValues.position = new Vector3(-10, -10, 0);
				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(-10, -10);
				
				v.image.rectTransformValues.anchorMinEnabled = true;
				v.image.rectTransformValues.anchorMin = new Vector2(0, 0);
				
				v.image.rectTransformValues.anchorMaxEnabled = true;
				v.image.rectTransformValues.anchorMax = new Vector2(1, 1);
				
				v.image.rectTransformValues.pivotEnabled = true;
				v.image.rectTransformValues.pivot = new Vector2(0.5f, 0.5f);
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Sliced;
				v.image.fillCentre = true;
				#endif
			}
			
			// Scrollbar Vertical
			v = new StyleComponent(data, style, StyleComponentType.Scrollbar, "Scrollbar Vertical", "Scrollbar Vertical");
			
			if (obj != null)
				v.scrollbar = ScrollbarHelper.SetValuesFromComponent ( obj.transform.Find("Scrollbar Vertical").gameObject );
			
			else
			{
				v.scrollbar.targetGraphicReference = "Scrollbar Vertical Handle";
				v.scrollbar.handleRectReference = "Scrollbar Vertical Handle";
				
				v.scrollbar.directionEnabled = true;
				v.scrollbar.direction = Scrollbar.Direction.BottomToTop;
			}
			
			// Scrollbar Vertical Background			
			v = new StyleComponent(data, style, StyleComponentType.Image, "Scrollbar Vertical Background", "Scrollbar Vertical");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Scrollbar Vertical").gameObject, includeRectTransform );
			
			else 
			{				
				v.image.rectTransformValues.positionEnabled = true;
				v.image.rectTransformValues.position = new Vector3(0, 0, 0);
				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(20, 17);
				
				v.image.rectTransformValues.anchorMinEnabled = true;
				v.image.rectTransformValues.anchorMin = new Vector2(1, 0);
				
				v.image.rectTransformValues.anchorMaxEnabled = true;
				v.image.rectTransformValues.anchorMax = new Vector2(1, 1);
				
				v.image.rectTransformValues.pivotEnabled = true;
				v.image.rectTransformValues.pivot = new Vector2(1, 1);
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Sliced;
				v.image.fillCentre = true;
				#endif
			}
			
			// Scrollbar Vertical Sliding Area
			v = new StyleComponent(data, style, StyleComponentType.RectTransform, "Scrollbar Vertical Sliding Area", "Scrollbar Vertical/Sliding Area");
			
			if (!includeRectTransform)
			{
				v.rectTransform.positionEnabled = true;
				v.rectTransform.position = new Vector3(10, 10, 0);
				
				v.rectTransform.sizeDeltaEnabled = true;
				v.rectTransform.sizeDelta = new Vector2(10, 10);
				
				v.rectTransform.anchorMinEnabled = true;
				v.rectTransform.anchorMin = new Vector2(0, 0);
				
				v.rectTransform.anchorMaxEnabled = true;
				v.rectTransform.anchorMax = new Vector2(1, 1);
				
				v.rectTransform.pivotEnabled = true;
				v.rectTransform.pivot = new Vector2(0.5f, 0.5f);
			}
			
			// Scrollbar Vertical Handle
			v = new StyleComponent(data, style, StyleComponentType.Image, "Scrollbar Vertical Handle", "Scrollbar Vertical/Sliding Area/Handle");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Scrollbar Vertical/Sliding Area/Handle").gameObject, includeRectTransform );
			
			else 
			{				
				v.image.rectTransformValues.positionEnabled = true;
				v.image.rectTransformValues.position = new Vector3(-10, -10, 0);
				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(-10, -10);
				
				v.image.rectTransformValues.anchorMinEnabled = true;
				v.image.rectTransformValues.anchorMin = new Vector2(0, 0.39f);
				
				v.image.rectTransformValues.anchorMaxEnabled = true;
				v.image.rectTransformValues.anchorMax = new Vector2(1, 1);
				
				v.image.rectTransformValues.pivotEnabled = true;
				v.image.rectTransformValues.pivot = new Vector2(0.5f, 0.5f);
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Sliced;
				v.image.fillCentre = true;
				#endif
			}
						
			UIStylesDatabase.Save ();
		}
		
		/// <summary>
		/// Sets the Values from a component
		/// </summary>
		public static ScrollRectValues SetValuesFromComponent ( Component com )
		{	
			if (com is ScrollRect)
				return SetValuesFromComponent ( (ScrollRect)com );
			
			else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be ScrollRect");
			
			return null;
		}
		public static ScrollRectValues SetValuesFromComponent ( GameObject obj )
		{	
			if (obj.GetComponent<ScrollRect>())
				return SetValuesFromComponent ( obj.GetComponent<ScrollRect>() );
			
			else Debug.LogError ("SetValuesFromComponent: Component not found!");
			
			return null;
		}
		public static ScrollRectValues SetValuesFromComponent ( ScrollRect scroll )
		{	
			ScrollRectValues values = new ScrollRectValues ();
			
			values.horizontalEnabled = true;
			values.verticalEnabled = true;
			values.movementTypeEnabled = true;
			values.elasticityEnabled = true;
			values.scrollSensitivityEnabled = true;
			values.showHorizontalScrollbarEnabled = true;
			values.showVerticalScrollbarEnabled = true;
			values.horizontalScrollbarVisibilityEnabled = true;
			values.verticalScrollbarVisibilityEnabled = true;
			values.horizontalScrollbarSpacingEnabled = true;
			values.verticalScrollbarSpacingEnabled = true;
						
			values.horizontal = scroll.horizontal;
			values.vertical = scroll.vertical;
			values.movementType = scroll.movementType;
			values.elasticity = scroll.elasticity;
			values.inertia = scroll.inertia;
			values.decelerationRate = scroll.decelerationRate;
			values.scrollSensitivity = scroll.scrollSensitivity;
			values.horizontalScrollbarVisibility	= scroll.horizontalScrollbarVisibility;
			values.verticalScrollbarVisibility = scroll.verticalScrollbarVisibility;
			values.horizontalScrollbarSpacing = scroll.horizontalScrollbarSpacing;
			values.verticalScrollbarSpacing = scroll.verticalScrollbarSpacing;
			values.showVerticalScrollbar = true;
			values.showHorizontalScrollbar = true;
			
			return values;
		}
		
		
		public static void Apply ( Style style, ScrollRectValues values, GameObject obj )
		{
			ScrollRect scrollView = obj.GetComponent<ScrollRect>();
			
			if ( values.showHorizontalScrollbarEnabled && obj.transform.Find ( "Scrollbar Horizontal" ) )
				obj.transform.Find ( "Scrollbar Horizontal" ).gameObject.SetActive ( values.showHorizontalScrollbar );
			
			if ( values.showVerticalScrollbarEnabled && obj.transform.Find ( "Scrollbar Vertical" ) )
				obj.transform.Find ( "Scrollbar Vertical" ).gameObject.SetActive ( values.showVerticalScrollbar );
			
			if ( values.horizontalEnabled )
				scrollView.horizontal = values.horizontal;
			if ( values.verticalEnabled )
				scrollView.vertical = values.vertical;
			if ( values.movementTypeEnabled )
				scrollView.movementType = values.movementType;
			if ( values.elasticityEnabled )
				scrollView.elasticity = values.elasticity;
			if ( values.inertiaEnabled )
				scrollView.inertia = values.inertia;
			if ( values.decelerationRateEnabled )
				scrollView.decelerationRate = values.decelerationRate;
			if ( values.scrollSensitivityEnabled )
				scrollView.scrollSensitivity = values.scrollSensitivity;
			if ( values.horizontalScrollbarVisibilityEnabled )
				scrollView.horizontalScrollbarVisibility = values.horizontalScrollbarVisibility;
			if ( values.verticalScrollbarVisibilityEnabled )
				scrollView.verticalScrollbarVisibility = values.verticalScrollbarVisibility;
			if ( values.horizontalScrollbarSpacingEnabled )
				scrollView.horizontalScrollbarSpacing = values.horizontalScrollbarSpacing;
			if ( values.verticalScrollbarSpacingEnabled )
				scrollView.verticalScrollbarSpacing = values.verticalScrollbarSpacing;
			
			
			// References
			SetReferences ( style, scrollView, values, obj);
		}
		
		/// <summary>
		/// Set the components references
		/// </summary>
		public static void SetReferences (Style style, ScrollRect scrollView, ScrollRectValues values, GameObject obj)
		{
			if (values.contentReference == "Null")
				scrollView.content = null;
			
			if (values.viewportReference == "Null")
				scrollView.viewport = null;
			
			if (values.horizonalScrollbarReference == "Null")
				scrollView.horizontalScrollbar = null;
			
			if (values.verticalScrollbarReference == "Null")
				scrollView.verticalScrollbar = null;
			
			
			
			while(!obj.name.Contains("(" + style.findByName + ")") && obj.transform.parent != null)
				obj = obj.transform.parent.gameObject;
			
			foreach (StyleComponent com in style.styleComponents)
			{
				if (com.name == values.contentReference && values.contentReference != "Null")
				{
					if (obj.transform.Find(com.path))
						scrollView.content = obj.transform.Find(com.path).gameObject.GetComponent<RectTransform>();
				}
				
				if (com.name == values.viewportReference && values.viewportReference != "Null")
				{
					if (obj.transform.Find(com.path))
						scrollView.viewport = obj.transform.Find(com.path).gameObject.GetComponent<RectTransform>();
				}
				
				if (com.name == values.horizonalScrollbarReference && values.horizonalScrollbarReference != "Null")
				{
					if (obj.transform.Find(com.path))
						scrollView.horizontalScrollbar = obj.transform.Find(com.path).gameObject.GetComponent<Scrollbar>();
				}
				
				if (com.name == values.verticalScrollbarReference && values.verticalScrollbarReference != "Null")
				{
					if (obj.transform.Find(com.path))
						scrollView.verticalScrollbar = obj.transform.Find(com.path).gameObject.GetComponent<Scrollbar>();
				}
			}
		}
	}
}



















