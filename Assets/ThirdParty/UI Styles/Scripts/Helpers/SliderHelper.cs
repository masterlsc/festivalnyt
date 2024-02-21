using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
	public class SliderHelper 
	{
		/// <summary>
		/// Create Style, pass in an object to match the styles values with the objects values
		/// </summary>
		public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj, bool includeRectTransform)
		{
			Style style = new Style(data, category, styleName, findByName);
			style.rename = !Application.isPlaying;
						
			// Slider
			StyleComponent v = new StyleComponent(data, style, StyleComponentType.Slider, "Slider", "");
			
			if (obj != null)
				v.slider = SetValuesFromComponent ( obj );
			
			else
			{
				v.slider.targetGraphicReference = "Background";
				v.slider.fillRectReference = "Fill";
				v.slider.handleRectReference = "Handle";
			}
			
			// Rect Transform
			v = new StyleComponent(data, style, StyleComponentType.RectTransform, "RectTransform", "");
			
			if (!includeRectTransform)
			{
				v.rectTransform.sizeDeltaEnabled = true;
				v.rectTransform.sizeDelta = new Vector2(160, 20);
			}
			
			// Background
			v = new StyleComponent(data, style, StyleComponentType.Image, "Background", "Background");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Background").gameObject, includeRectTransform );
			
			else 
			{
				v.image.rectTransformValues.positionEnabled = true;
				v.image.rectTransformValues.position = new Vector3(0, 0, 0);
				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(0, 0);
				
				v.image.rectTransformValues.anchorMinEnabled = true;
				v.image.rectTransformValues.anchorMin = new Vector2(0, 0.25f);
				
				v.image.rectTransformValues.anchorMaxEnabled = true;
				v.image.rectTransformValues.anchorMax = new Vector2(1, 0.75f);
				
				v.image.rectTransformValues.pivotEnabled = true;
				v.image.rectTransformValues.pivot = new Vector2(0.5f, 0.5f);
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Sliced;
				v.image.fillCentre = true;
				#endif
			}
			
			// Fill Area
			v = new StyleComponent(data, style, StyleComponentType.RectTransform, "Fill Area", "Fill Area");
			
			if (!includeRectTransform)
			{
				v.rectTransform.positionEnabled = true;
				v.rectTransform.position = new Vector3(5, 0, 0);
				
				v.rectTransform.sizeDeltaEnabled = true;
				v.rectTransform.sizeDelta = new Vector2(15, 0);
				
				v.rectTransform.anchorMinEnabled = true;
				v.rectTransform.anchorMin = new Vector2(0,  0.25f);
				
				v.rectTransform.anchorMaxEnabled = true;
				v.rectTransform.anchorMax = new Vector2(1,  0.75f);
				
				v.rectTransform.pivotEnabled = true;
				v.rectTransform.pivot = new Vector2(0.5f, 0.5f);
			}
			
			// Fill
			v = new StyleComponent(data, style, StyleComponentType.Image, "Fill", "Fill Area/Fill");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Fill Area/Fill").gameObject, includeRectTransform );
			
			else 
			{
				v.image.rectTransformValues.positionEnabled = true;
				v.image.rectTransformValues.position = new Vector3(0, 0, 0);
				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(10, 0);
				
				v.image.rectTransformValues.anchorMinEnabled = true;
				v.image.rectTransformValues.anchorMin = new Vector2(0, 0);
				
				v.image.rectTransformValues.anchorMaxEnabled = true;
				v.image.rectTransformValues.anchorMax = new Vector2(0, 1);
				
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
			
			// Handle Slide Area
			v = new StyleComponent(data, style, StyleComponentType.RectTransform, "Handle Slide Area", "Handle Slide Area");
			
			if (!includeRectTransform)
			{
				v.rectTransform.positionEnabled = true;
				v.rectTransform.position = new Vector3(10, 0, 0);
				
				v.rectTransform.sizeDeltaEnabled = true;
				v.rectTransform.sizeDelta = new Vector2(10, 0);
				
				v.rectTransform.anchorMinEnabled = true;
				v.rectTransform.anchorMin = new Vector2(0, 0);
				
				v.rectTransform.anchorMaxEnabled = true;
				v.rectTransform.anchorMax = new Vector2(1, 1);
				
				v.rectTransform.pivotEnabled = true;
				v.rectTransform.pivot = new Vector2(0.5f, 0.5f);
			}
			
			// Handle
			v = new StyleComponent(data, style, StyleComponentType.Image, "Handle", "Handle Slide Area/Handle");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Handle Slide Area/Handle").gameObject, includeRectTransform );
			
			else 
			{
				v.image.rectTransformValues.positionEnabled = true;
				v.image.rectTransformValues.position = new Vector3(0, 0, 0);
				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(20, 0);
				
				v.image.rectTransformValues.anchorMinEnabled = true;
				v.image.rectTransformValues.anchorMin = new Vector2(0, 0);
				
				v.image.rectTransformValues.anchorMaxEnabled = true;
				v.image.rectTransformValues.anchorMax = new Vector2(0, 1);
				
				v.image.rectTransformValues.pivotEnabled = true;
				v.image.rectTransformValues.pivot = new Vector2(0.5f, 0.5f);
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Simple;
				#endif
			}
			
			UIStylesDatabase.Save ();
		}
		
		/// <summary>
		/// Sets the Values from a component
		/// </summary>
		public static SliderValues SetValuesFromComponent ( Component com )
		{	
			if (com is Slider)
				return SetValuesFromComponent ( (Slider)com );
			
			else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be Slider");
			
			return null;
		}
		public static SliderValues SetValuesFromComponent ( GameObject obj )
		{	
			if (obj.GetComponent<Slider>())
				return SetValuesFromComponent ( obj.GetComponent<Slider>() );
			
			else Debug.LogError ("SetValuesFromComponent: Component not found!");
			
			return null;
		}
		public static SliderValues SetValuesFromComponent (Slider slider)
		{	
			SliderValues values = new SliderValues();
			
			values.interactableEnabled = true;
			values.transitionValues.transitionEnabled = true;
			
			values.directionEnabled = true;
			values.minValueEnabled = true;
			values.maxValueEnabled = true;
			values.wholeNumbersEnabled = true;
			values.valueEnabled = true;
			
			values.interactable = slider.interactable;
			values.transitionValues.transition = slider.transition;
			
			values.direction = slider.direction;
			values.minValue = slider.minValue;
			values.maxValue = slider.maxValue;
			values.wholeNumbers = slider.wholeNumbers;
			values.value = slider.value;
			
			values.transitionValues.normalColor 		= slider.colors.normalColor;
			values.transitionValues.highlightedColor	= slider.colors.highlightedColor;
			values.transitionValues.pressedColor		= slider.colors.pressedColor;
			values.transitionValues.disabledColor		= slider.colors.disabledColor;
			values.transitionValues.colorMultiplier 	= slider.colors.colorMultiplier;
			values.transitionValues.fadeDuration		= slider.colors.fadeDuration;
			
			values.transitionValues.highlightedGraphic	= slider.spriteState.highlightedSprite;
			values.transitionValues.pressedGraphic		= slider.spriteState.pressedSprite;
			values.transitionValues.disabledGraphic		= slider.spriteState.disabledSprite;
			
			values.transitionValues.normalTrigger		= slider.animationTriggers.normalTrigger;
			values.transitionValues.highlightedTrigger	= slider.animationTriggers.highlightedTrigger;
			values.transitionValues.pressedTrigger		= slider.animationTriggers.pressedTrigger;
			values.transitionValues.disabledTrigger		= slider.animationTriggers.disabledTrigger;
			
			return values;
		}
		
		
		public static void Apply (Style style, SliderValues values, GameObject obj)
		{
			Slider slider = obj.GetComponent<Slider>();
			
			// Options 
			if (values.interactableEnabled) slider.interactable = values.interactable;
			if (values.directionEnabled) slider.direction = values.direction;
			if (values.minValueEnabled) slider.minValue = values.minValue;
			if (values.maxValueEnabled) slider.maxValue = values.maxValue;
			if (values.wholeNumbersEnabled) slider.wholeNumbers = values.wholeNumbers;
			if (values.valueEnabled) slider.value = values.value;
			
			if (values.transitionValues.transitionEnabled) 
			{
				// Transition
				slider.transition = values.transitionValues.transition;
				
				// Transition ColorBlock 
				slider.colors = TransitionHelper.ApplyColorBlock
				(
					values.transitionValues.normalColor, 
					values.transitionValues.highlightedColor,
					values.transitionValues.pressedColor,
					values.transitionValues.disabledColor,
					values.transitionValues.colorMultiplier,
					values.transitionValues.fadeDuration
				);
				
				// Transition SpriteState 
				slider.spriteState = TransitionHelper.ApplySpriteState
				(
					values.transitionValues.highlightedGraphic,
					values.transitionValues.pressedGraphic,
					values.transitionValues.disabledGraphic
				);
				
				// Transition SpriteState
				slider.animationTriggers = TransitionHelper.ApplyAnimationTriggers
				(
					values.transitionValues.normalTrigger,
					values.transitionValues.highlightedTrigger,
					values.transitionValues.pressedTrigger,
					values.transitionValues.disabledTrigger
				);
			}
			
			// References
			SetReferences ( style, slider, values, obj);
		}
		
		/// <summary>
		/// Set the components references
		/// </summary>
		public static void SetReferences (Style style, Slider slider, SliderValues values, GameObject obj)
		{
			if (values.targetGraphicReference == "Null")
				slider.targetGraphic = null;
			
			if (values.fillRectReference == "Null")
				slider.fillRect = null;
			
			if (values.handleRectReference == "Null")
				slider.handleRect = null;
			
			while(!obj.name.Contains("(" + style.findByName + ")") && obj.transform.parent != null)
				obj = obj.transform.parent.gameObject;
			
			foreach (StyleComponent com in style.styleComponents)
			{
				if (com.name == values.targetGraphicReference && values.targetGraphicReference != "Null")
				{
					if (obj.transform.Find(com.path))
						slider.targetGraphic = obj.transform.Find(com.path).gameObject.GetComponent<Graphic>();
				}
				
				if (com.name == values.fillRectReference && values.fillRectReference != "Null")
				{
					if (obj.transform.Find(com.path))
						slider.fillRect = obj.transform.Find(com.path).gameObject.GetComponent<RectTransform>();
				}
				
				if (com.name == values.handleRectReference && values.handleRectReference != "Null")
				{
					if (obj.transform.Find(com.path))
						slider.handleRect = obj.transform.Find(com.path).gameObject.GetComponent<RectTransform>();
				}
			}
		}
	}
}



















