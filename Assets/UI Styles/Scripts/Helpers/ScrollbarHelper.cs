using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
	public class ScrollbarHelper 
	{
		/// <summary>
		/// Create Style, pass in an object to match the styles values with the objects values
		/// </summary>
		public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj, bool includeRectTransform)
		{
			Style style = new Style(data, category, styleName, findByName);
			style.rename = !Application.isPlaying;
			
			// Scrollbar
			StyleComponent v = new StyleComponent(data, style, StyleComponentType.Scrollbar, "Scrollbar", "");
			
			if (obj != null)
				v.scrollbar = SetValuesFromComponent ( obj );
			
			else
			{
				v.scrollbar.targetGraphicReference = "Background";
				v.scrollbar.handleRectReference = "Handle";
			}
			
			// Background
			v = new StyleComponent(data, style, StyleComponentType.Image, "Background", "");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj, includeRectTransform );
			
			else 
			{				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(160, 20);
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Sliced;
				v.image.fillCentre = true;
				#endif
			}
			
			// Rect Transform
			v = new StyleComponent(data, style, StyleComponentType.RectTransform, "Sliding Area", "Sliding Area");
			
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
			
			// Handle
			v = new StyleComponent(data, style, StyleComponentType.Image, "Handle", "Sliding Area/Handle");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Sliding Area/Handle").gameObject, includeRectTransform );
			
			else 
			{
				v.image.rectTransformValues.positionEnabled = true;
				v.image.rectTransformValues.position = new Vector3(-10, -10, 0);
				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(-10, -10);
				
				v.image.rectTransformValues.anchorMinEnabled = true;
				v.image.rectTransformValues.anchorMin = new Vector2(0, 0);
				
				v.image.rectTransformValues.anchorMaxEnabled = true;
				v.image.rectTransformValues.anchorMax = new Vector2(0.2f, 1);
				
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
		public static ScrollbarValues SetValuesFromComponent ( Component com )
		{	
			if (com is Scrollbar)
				return SetValuesFromComponent ( (Scrollbar)com );
			
			else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be Scrollbar");
			
			return null;
		}
		public static ScrollbarValues SetValuesFromComponent ( GameObject obj )
		{	
			if (obj.GetComponent<Scrollbar>())
				return SetValuesFromComponent ( obj.GetComponent<Scrollbar>() );
			
			else Debug.LogError ("SetValuesFromComponent: Component not found!");
			
			return null;
		}
		public static ScrollbarValues SetValuesFromComponent (Scrollbar bar)
		{	
			ScrollbarValues values = new ScrollbarValues();
			
			values.interactableEnabled = true;
			values.transitionValues.transitionEnabled = true;
			
			values.directionEnabled = true;
			values.valueEnabled = true;
			values.sizeEnabled = true;
			values.numberOfStepsEnabled = true;
			
			values.interactable = bar.interactable;
			values.transitionValues.transition = bar.transition;
			
			values.direction = bar.direction;
			values.size = bar.size;
			values.value = bar.value;
			values.numberOfSteps = bar.numberOfSteps;
			
			values.transitionValues.normalColor 		= bar.colors.normalColor;
			values.transitionValues.highlightedColor	= bar.colors.highlightedColor;
			values.transitionValues.pressedColor		= bar.colors.pressedColor;
			values.transitionValues.disabledColor		= bar.colors.disabledColor;
			values.transitionValues.colorMultiplier 	= bar.colors.colorMultiplier;
			values.transitionValues.fadeDuration		= bar.colors.fadeDuration;
			
			values.transitionValues.highlightedGraphic	= bar.spriteState.highlightedSprite;
			values.transitionValues.pressedGraphic		= bar.spriteState.pressedSprite;
			values.transitionValues.disabledGraphic		= bar.spriteState.disabledSprite;
			
			values.transitionValues.normalTrigger		= bar.animationTriggers.normalTrigger;
			values.transitionValues.highlightedTrigger	= bar.animationTriggers.highlightedTrigger;
			values.transitionValues.pressedTrigger		= bar.animationTriggers.pressedTrigger;
			values.transitionValues.disabledTrigger		= bar.animationTriggers.disabledTrigger;
						
			return values;
		}
		
		public static void Apply (Style style, ScrollbarValues values, GameObject obj)
		{
			Scrollbar scrollbar = obj.GetComponent<Scrollbar>();
			
			// Options 
			if (values.interactableEnabled) scrollbar.interactable = values.interactable;
			if (values.directionEnabled) scrollbar.direction = values.direction;
			if (values.valueEnabled) scrollbar.value = values.value;
			if (values.sizeEnabled) scrollbar.size = values.size;
			if (values.numberOfStepsEnabled) scrollbar.numberOfSteps = values.numberOfSteps;
			
			if (values.transitionValues.transitionEnabled) 
			{
				// Transition
				scrollbar.transition = values.transitionValues.transition;
				
				// Transition ColorBlock
				scrollbar.colors = TransitionHelper.ApplyColorBlock
				(
					values.transitionValues.normalColor, 
					values.transitionValues.highlightedColor,
					values.transitionValues.pressedColor,
					values.transitionValues.disabledColor,
					values.transitionValues.colorMultiplier,
					values.transitionValues.fadeDuration
				);
				
				// Transition SpriteState 
				scrollbar.spriteState = TransitionHelper.ApplySpriteState
				(
					values.transitionValues.highlightedGraphic,
					values.transitionValues.pressedGraphic,
					values.transitionValues.disabledGraphic
				);
				
				// Transition SpriteState 
				scrollbar.animationTriggers = TransitionHelper.ApplyAnimationTriggers
				(
					values.transitionValues.normalTrigger,
					values.transitionValues.highlightedTrigger,
					values.transitionValues.pressedTrigger,
					values.transitionValues.disabledTrigger
				);
			}
			
			// References
			SetReferences ( style, scrollbar, values, obj);
		}
		
		/// <summary>
		/// Set the components references
		/// </summary>
		public static void SetReferences (Style style, Scrollbar scrollbar, ScrollbarValues values, GameObject obj)
		{
			if (values.targetGraphicReference == "Null")
				scrollbar.targetGraphic = null;
			
			if (values.handleRectReference == "Null")
				scrollbar.handleRect = null;
			
			while(!obj.name.Contains("(" + style.findByName + ")") && obj.transform.parent != null)
				obj = obj.transform.parent.gameObject;
			
			foreach (StyleComponent com in style.styleComponents)
			{
				if (com.name == values.targetGraphicReference && values.targetGraphicReference != "Null")
				{
					if (obj.transform.Find(com.path))
						scrollbar.targetGraphic = obj.transform.Find(com.path).gameObject.GetComponent<Graphic>();
				}
				
				if (com.name == values.handleRectReference && values.handleRectReference != "Null")
				{
					if (obj.transform.Find(com.path))
						scrollbar.handleRect = obj.transform.Find(com.path).gameObject.GetComponent<RectTransform>();
				}
			}
		}
	}
}



















