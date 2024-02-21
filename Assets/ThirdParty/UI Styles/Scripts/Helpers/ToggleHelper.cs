using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
	public class ToggleHelper 
	{
		/// <summary>
		/// Create Style, pass in an object to match the styles values with the objects values
		/// </summary>
		public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj, bool includeRectTransform)
		{
			Style style = new Style(data, category, styleName, findByName);
			style.rename = !Application.isPlaying;
			
			StyleComponent v = new StyleComponent(data, style, StyleComponentType.Toggle, "Toggle", "");
			
			if (obj != null)
				v.toggle = SetValuesFromComponent ( obj );
			
			else
			{
				v.toggle.isOnEnabled = true;
				v.toggle.isOn = true;
				v.toggle.targetGraphicReference = "Background";
				v.toggle.checkmarkReference = "Checkmark";
			}
			
			// Rect Transform
			v = new StyleComponent(data, style, StyleComponentType.RectTransform, "RectTransform", "");
			
			if (!includeRectTransform)
			{
				v.rectTransform.sizeDeltaEnabled = true;
				v.rectTransform.sizeDelta = new Vector2(100, 20);
			}
		
			
			// Background
			v = new StyleComponent(data, style, StyleComponentType.Image, "Background", "Background");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Background").gameObject, includeRectTransform );
			
			else 
			{
				v.image.rectTransformValues.positionEnabled = true;
				v.image.rectTransformValues.position = new Vector3(10, -10, 0);
				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(20, 20);
				
				v.image.rectTransformValues.anchorMinEnabled = true;
				v.image.rectTransformValues.anchorMin = new Vector2(0, 1);
				
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
			
			// Checkmark
			v = new StyleComponent(data, style, StyleComponentType.Image, "Checkmark", "Background/Checkmark");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Background/Checkmark").gameObject, includeRectTransform );
			
			else 
			{
				v.image.rectTransformValues.positionEnabled = true;
				v.image.rectTransformValues.position = new Vector3(0, 0, 0);
				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(0, 0);
				
				v.image.rectTransformValues.anchorMinEnabled = true;
				v.image.rectTransformValues.anchorMin = new Vector2(0, 0);
				
				v.image.rectTransformValues.anchorMaxEnabled = true;
				v.image.rectTransformValues.anchorMax = new Vector2(1, 1);
				
				v.image.rectTransformValues.pivotEnabled = true;
				v.image.rectTransformValues.pivot = new Vector2(0.5f, 0.5f);
				
				v.image.colorEnabled = true;
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Checkmark.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Simple;
				#endif
			}
			
			// Label
			v = new StyleComponent(data, style, StyleComponentType.Text, "Label", "Label");
			
			if (obj != null)
				v.text = TextHelper.SetValuesFromComponent ( obj.transform.Find("Label").gameObject, includeRectTransform );
			
			else 
			{
				v.text.rectTransformValues.positionEnabled = true;
				v.text.rectTransformValues.position = new Vector3(24, 0, 0);
				
				v.text.rectTransformValues.sizeDeltaEnabled = true;
				v.text.rectTransformValues.sizeDelta = new Vector2(4, 0);
				
				v.text.rectTransformValues.anchorMinEnabled = true;
				v.text.rectTransformValues.anchorMin = new Vector2(0, 0);
				
				v.text.rectTransformValues.anchorMaxEnabled = true;
				v.text.rectTransformValues.anchorMax = new Vector2(1, 1);
				
				v.text.rectTransformValues.pivotEnabled = true;
				v.text.rectTransformValues.pivot = new Vector2(0.5f, 0.5f);
				
				v.text.textAlignmentEnabled = true;
				v.text.textAlignment = TextAnchor.MiddleLeft;
			}
			
			UIStylesDatabase.Save ();
		}
		
		/// <summary>
		/// Sets the Values from a component
		/// </summary>
		public static ToggleValues SetValuesFromComponent ( Component com )
		{	
			if (com is Toggle)
				return SetValuesFromComponent ( (Toggle)com );
			
			else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be Toggle");
			
			return null;
		}
		public static ToggleValues SetValuesFromComponent ( GameObject obj )
		{	
			if (obj.GetComponent<Toggle>())
				return SetValuesFromComponent ( obj.GetComponent<Toggle>() );
			
			else Debug.LogError ("SetValuesFromComponent: Component not found!");
			
			return null;
		}
		public static ToggleValues SetValuesFromComponent ( Toggle toggle )
		{	
			ToggleValues values = new ToggleValues ();
			
			values.interactableEnabled = true;
			values.transitionValues.transitionEnabled = true;
			
			values.isOnEnabled = true;
			values.toggleTransitionEnabled = true;
			
			values.interactable = toggle.interactable;
			values.transitionValues.transition = toggle.transition;
			
			values.isOn = toggle.isOn;
			values.toggleTransition = toggle.toggleTransition;
			
			values.transitionValues.normalColor = toggle.colors.normalColor;
			values.transitionValues.highlightedColor = toggle.colors.highlightedColor;
			values.transitionValues.pressedColor = toggle.colors.pressedColor;
			values.transitionValues.disabledColor = toggle.colors.disabledColor;
			values.transitionValues.colorMultiplier = toggle.colors.colorMultiplier;
			values.transitionValues.fadeDuration = toggle.colors.fadeDuration;
			
			values.transitionValues.highlightedGraphic	= toggle.spriteState.highlightedSprite;
			values.transitionValues.pressedGraphic = toggle.spriteState.pressedSprite;
			values.transitionValues.disabledGraphic = toggle.spriteState.disabledSprite;
			
			values.transitionValues.normalTrigger = toggle.animationTriggers.normalTrigger;
			values.transitionValues.highlightedTrigger	= toggle.animationTriggers.highlightedTrigger;
			values.transitionValues.pressedTrigger = toggle.animationTriggers.pressedTrigger;
			values.transitionValues.disabledTrigger = toggle.animationTriggers.disabledTrigger;
			
			return values;
		}
		
		public static void Apply ( Style style, ToggleValues values, GameObject obj )
		{
			Toggle toggle = obj.GetComponent<Toggle> ();
			
			// Options
			if ( values.interactableEnabled )
				toggle.interactable = values.interactable;
			if ( values.isOnEnabled )
				toggle.isOn = values.isOn;
			if ( values.toggleTransitionEnabled )
				toggle.toggleTransition = values.toggleTransition;
			
			if ( values.transitionValues.transitionEnabled )
			{
				// Transition 
				toggle.transition = values.transitionValues.transition;
				
				// Transition ColorBlock 
				toggle.colors = TransitionHelper.ApplyColorBlock
				(
					values.transitionValues.normalColor, 
					values.transitionValues.highlightedColor,
					values.transitionValues.pressedColor,
					values.transitionValues.disabledColor,
					values.transitionValues.colorMultiplier,
					values.transitionValues.fadeDuration
				);
				
				// Transition SpriteState
				toggle.spriteState = TransitionHelper.ApplySpriteState
				(
					values.transitionValues.highlightedGraphic,
					values.transitionValues.pressedGraphic,
					values.transitionValues.disabledGraphic
				);
				
				// Transition SpriteState 
				toggle.animationTriggers = TransitionHelper.ApplyAnimationTriggers
				(
					values.transitionValues.normalTrigger,
					values.transitionValues.highlightedTrigger,
					values.transitionValues.pressedTrigger,
					values.transitionValues.disabledTrigger
				);
			}
			
			// References
			SetReferences ( style, toggle, values, obj);
		}
		
		/// <summary>
		/// Set the components references
		/// </summary>
		public static void SetReferences (Style style, Toggle toggle, ToggleValues values, GameObject obj)
		{
			if (values.targetGraphicReference == "Null")
				toggle.targetGraphic = null;
			
			if (values.checkmarkReference == "Null")
				toggle.graphic = null;
			
			while(!obj.name.Contains("(" + style.findByName + ")") && obj.transform.parent != null)
				obj = obj.transform.parent.gameObject;
			
			foreach (StyleComponent com in style.styleComponents)
			{
				if (com.name == values.targetGraphicReference && values.targetGraphicReference != "Null")
				{
					if (obj.transform.Find(com.path))
						toggle.targetGraphic = obj.transform.Find(com.path).gameObject.GetComponent<Graphic>();
				}
				
				if (com.name == values.checkmarkReference && values.checkmarkReference != "Null")
				{
					if (obj.transform.Find(com.path))
						toggle.graphic = obj.transform.Find(com.path).gameObject.GetComponent<Graphic>();
				}
			}
		}
	}
}



















