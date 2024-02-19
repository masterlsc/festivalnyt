using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
	public class ButtonHelper 
	{
		/// <summary>
		/// Create Style, pass in an object to match the styles values with the objects values
		/// </summary>
		public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj, bool includeRectTransform)
		{
			Style style = new Style(data, category, styleName, findByName);
			style.rename = !Application.isPlaying;
			
			// Button
			StyleComponent v = new StyleComponent(data, style, StyleComponentType.Button, "Button", "");
			
			if (obj != null)
				v.button = SetValuesFromComponent ( obj );
			
			else v.button.targetGraphicReference = "Image";
			
			// Image
			v = new StyleComponent(data, style, StyleComponentType.Image, "Image", "");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj, includeRectTransform );
			
			else 
			{
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(160, 30);
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Sliced;
				v.image.fillCentre = true;
				#endif
			}
			
			// Text
			v = new StyleComponent(data, style, StyleComponentType.Text, "Text", "Text");
			
			if (obj != null)
				v.text = TextHelper.SetValuesFromComponent ( obj.transform.Find("Text").gameObject, includeRectTransform );
			
			else 
			{
				v.text.rectTransformValues.positionEnabled = true;
				v.text.rectTransformValues.position = new Vector3(0, 0, 0);
				
				v.text.rectTransformValues.sizeDeltaEnabled = true;
				v.text.rectTransformValues.sizeDelta = new Vector2(0, 0);
				
				v.text.rectTransformValues.anchorMinEnabled = true;
				v.text.rectTransformValues.anchorMin = new Vector2(0, 0);
				
				v.text.rectTransformValues.anchorMaxEnabled = true;
				v.text.rectTransformValues.anchorMax = new Vector2(1, 1);
				
				v.text.rectTransformValues.pivotEnabled = true;
				v.text.rectTransformValues.pivot = new Vector2(0.5f, 0.5f);
				
				v.text.colorEnabled = true;
				v.text.textAlignmentEnabled = true;
				v.text.textAlignment = TextAnchor.MiddleCenter;
			}
			
			UIStylesDatabase.Save ();
		}
		
		/// <summary>
		/// Sets the Values from a component
		/// </summary>
		public static ButtonValues SetValuesFromComponent ( Component com )
		{	
			if (com is Button)
				return SetValuesFromComponent ( (Button)com );
			
			else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be Button");
			
			return null;
		}
		public static ButtonValues SetValuesFromComponent ( GameObject obj )
		{	
			if (obj.GetComponent<Button>())
				return SetValuesFromComponent ( obj.GetComponent<Button>() );
			
			else Debug.LogError ("SetValuesFromComponent: Component not found!");
			
			return null;
		}
		public static ButtonValues SetValuesFromComponent ( Button button )
		{	
			ButtonValues values = new ButtonValues ();
						
			values.interactableEnabled					= true;
			values.transitionValues.transitionEnabled	= true;
			
			values.interactable 						= button.interactable;
			values.transitionValues.transition			= button.transition;
			values.transitionValues.normalColor 		= button.colors.normalColor;
			values.transitionValues.highlightedColor	= button.colors.highlightedColor;
			values.transitionValues.pressedColor		= button.colors.pressedColor;
			values.transitionValues.disabledColor		= button.colors.disabledColor;
			values.transitionValues.colorMultiplier 	= button.colors.colorMultiplier;
			values.transitionValues.fadeDuration		= button.colors.fadeDuration;
			
			values.transitionValues.highlightedGraphic	= button.spriteState.highlightedSprite;
			values.transitionValues.pressedGraphic		= button.spriteState.pressedSprite;
			values.transitionValues.disabledGraphic 	= button.spriteState.disabledSprite;
			
			values.transitionValues.normalTrigger		= button.animationTriggers.normalTrigger;
			values.transitionValues.highlightedTrigger	= button.animationTriggers.highlightedTrigger;
			values.transitionValues.pressedTrigger		= button.animationTriggers.pressedTrigger;
			values.transitionValues.disabledTrigger 	= button.animationTriggers.disabledTrigger;
						
			return values;
		}
		
		public static void Apply ( Style style, ButtonValues values, GameObject obj )
		{
			Button button = obj.GetComponent<Button>();
			
			// Options
			if ( values.interactableEnabled )
				button.interactable = values.interactable;
						
			// Transition
			if ( values.transitionValues.transitionEnabled )
			{
				button.transition = values.transitionValues.transition;
				
				// Transition ColorBlock
				button.colors = TransitionHelper.ApplyColorBlock
				(
					values.transitionValues.normalColor, 
					values.transitionValues.highlightedColor,
					values.transitionValues.pressedColor,
					values.transitionValues.disabledColor,
					values.transitionValues.colorMultiplier,
					values.transitionValues.fadeDuration
				);
				
				// Transition SpriteState
				button.spriteState = TransitionHelper.ApplySpriteState
				(
					values.transitionValues.highlightedGraphic,
					values.transitionValues.pressedGraphic,
					values.transitionValues.disabledGraphic
					
				);
				
				// Transition SpriteState
				button.animationTriggers = TransitionHelper.ApplyAnimationTriggers
				(
					values.transitionValues.normalTrigger,
					values.transitionValues.highlightedTrigger,
					values.transitionValues.pressedTrigger,
					values.transitionValues.disabledTrigger
				);
			}
			
			SetReferences ( style, button, values, obj);
		}
		
		/// <summary>
		/// Set the components references
		/// </summary>
		public static void SetReferences (Style style, Button button, ButtonValues values, GameObject obj)
		{
			// References
			if (values.targetGraphicReference == "Null")
			{
				button.targetGraphic = null;
			}
			else
			{
				while(!obj.name.Contains("(" + style.findByName + ")") && obj.transform.parent != null)
					obj = obj.transform.parent.gameObject;
				
				foreach (StyleComponent com in style.styleComponents)
				{
					if (com.name == values.targetGraphicReference)
					{
						if (obj.transform.Find(com.path))
							button.targetGraphic = obj.transform.Find(com.path).gameObject.GetComponent<Graphic>();
					}
				}
			}
		}
	}
}



















