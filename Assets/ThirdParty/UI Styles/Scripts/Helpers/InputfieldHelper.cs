using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
	public class InputFieldHelper
	{
		
		/// <summary>
		/// Create Style, pass in an object to match the styles values with the objects values
		/// </summary>
		public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj, bool includeRectTransform)
		{
			Style style = new Style(data, category, styleName, findByName);
			style.rename = !Application.isPlaying;
			
			// Inputfield
			StyleComponent v = new StyleComponent(data, style, StyleComponentType.InputField, "InputField", "");
			
			if (obj != null)
				v.inputField = SetValuesFromComponent ( obj );
			
			else
			{
				v.inputField.targetGraphicReference = "Image";
				v.inputField.placeholderReference = "Placeholder";
				v.inputField.textReference = "Text";
			}
			
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
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/InputFieldBackground.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Sliced;
				v.image.fillCentre = true;
				#endif
			}
			
			// Placeholder
			v = new StyleComponent(data, style, StyleComponentType.Text, "Placeholder", "Placeholder");
			
			if (obj != null)
				v.text = TextHelper.SetValuesFromComponent ( obj.transform.Find("Placeholder").gameObject, includeRectTransform );
			
			else 
			{
				v.text.rectTransformValues.positionEnabled = true;
				v.text.rectTransformValues.position = new Vector3(10, 7, 0);
				
				v.text.rectTransformValues.sizeDeltaEnabled = true;
				v.text.rectTransformValues.sizeDelta = new Vector2(10, 6);
				
				v.text.rectTransformValues.anchorMinEnabled = true;
				v.text.rectTransformValues.anchorMin = new Vector2(0, 0);
				
				v.text.rectTransformValues.anchorMaxEnabled = true;
				v.text.rectTransformValues.anchorMax = new Vector2(1, 1);
				
				v.text.rectTransformValues.pivotEnabled = true;
				v.text.rectTransformValues.pivot = new Vector2(0.5f, 0.5f);
				
				v.text.colorEnabled = true;
			}
			
			// Text
			v = new StyleComponent(data, style, StyleComponentType.Text, "Text", "Text");
			
			if (obj != null)
				v.text = TextHelper.SetValuesFromComponent ( obj.transform.Find("Text").gameObject, includeRectTransform );
			
			else 
			{
				v.text.rectTransformValues.positionEnabled = true;
				v.text.rectTransformValues.position = new Vector3(10, 7, 0);
				
				v.text.rectTransformValues.sizeDeltaEnabled = true;
				v.text.rectTransformValues.sizeDelta = new Vector2(10, 6);
				
				v.text.rectTransformValues.anchorMinEnabled = true;
				v.text.rectTransformValues.anchorMin = new Vector2(0, 0);
				
				v.text.rectTransformValues.anchorMaxEnabled = true;
				v.text.rectTransformValues.anchorMax = new Vector2(1, 1);
				
				v.text.rectTransformValues.pivotEnabled = true;
				v.text.rectTransformValues.pivot = new Vector2(0.5f, 0.5f);
				
				v.text.colorEnabled = true;
				
				v.text.richTextEnabled = true;
				v.text.richText = false;
				
				v.text.textEnabled = true;
				v.text.text = string.Empty;
			}
			
			UIStylesDatabase.Save ();
		}
		
		/// <summary>
		/// Sets the Values from a component
		/// </summary>
		public static InputFieldValues SetValuesFromComponent ( Component com )
		{	
			if (com is InputField)
				return SetValuesFromComponent ( (InputField)com );
			
			else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be InputField");
			
			return null;
		}
		public static InputFieldValues SetValuesFromComponent ( GameObject obj )
		{	
			if (obj.GetComponent<InputField>())
				return SetValuesFromComponent ( obj.GetComponent<InputField>() );
			
			else Debug.LogError ("SetValuesFromComponent: Component not found!");
			
			return null;
		}
		public static InputFieldValues SetValuesFromComponent ( InputField inputF )
		{	
			InputFieldValues values = new InputFieldValues ();
						
			values.interactableEnabled = true;
			values.transitionValues.transitionEnabled = true;

			values.interactable = inputF.interactable;
			values.transitionValues.transition = inputF.transition;
			
			values.characterLimitEnabled = true;
			values.caretBlinkRateEnabled = true;
			values.caretWidthEnabled = true;
			values.customCaretColorEnabled = true;
			values.selectionColorEnabled = true;
			values.hideMobileInputEnabled = true;
			values.readOnlyEnabled = true;
			values.contentTypeEnabled = true;
			
			values.characterLimit = inputF.characterLimit;
			values.contentType = inputF.contentType;
			values.lineType = inputF.lineType;
			values.inputType = inputF.inputType;
			values.keyboardType = inputF.keyboardType;
			values.characterValidation = inputF.characterValidation;
			values.caretBlinkRate = inputF.caretBlinkRate;
			values.caretWidth = inputF.caretWidth;
			values.customCaretColor = inputF.customCaretColor;
			values.caretColor = inputF.caretColor;
			values.selectionColor = inputF.selectionColor;
			values.hideMobileInput = inputF.shouldHideMobileInput;
			values.readOnly = inputF.readOnly;
			
			values.transitionValues.normalColor = inputF.colors.normalColor;
			values.transitionValues.highlightedColor	= inputF.colors.highlightedColor;
			values.transitionValues.pressedColor = inputF.colors.pressedColor;
			values.transitionValues.disabledColor = inputF.colors.disabledColor;
			values.transitionValues.colorMultiplier = inputF.colors.colorMultiplier;
			values.transitionValues.fadeDuration = inputF.colors.fadeDuration;
			
			values.transitionValues.highlightedGraphic	= inputF.spriteState.highlightedSprite;
			values.transitionValues.pressedGraphic = inputF.spriteState.pressedSprite;
			values.transitionValues.disabledGraphic = inputF.spriteState.disabledSprite;
			
			values.transitionValues.normalTrigger = inputF.animationTriggers.normalTrigger;
			values.transitionValues.highlightedTrigger	= inputF.animationTriggers.highlightedTrigger;
			values.transitionValues.pressedTrigger = inputF.animationTriggers.pressedTrigger;
			values.transitionValues.disabledTrigger = inputF.animationTriggers.disabledTrigger;
						
			return values;
		}
		
		
		public static void Apply ( Style style, InputFieldValues values, GameObject obj )
		{
			InputField input = obj.GetComponent<InputField>();
			
			// Options
			if ( values.interactableEnabled )
				input.interactable = values.interactable;
			if ( values.characterLimitEnabled )
				input.characterLimit = values.characterLimit;
			if ( values.caretBlinkRateEnabled )
				input.caretBlinkRate = values.caretBlinkRate;
			
			#if !PRE_UNITY_5 
			if ( values.caretWidthEnabled )
				input.caretWidth = values.caretWidth; 
			if ( values.customCaretColorEnabled )
				input.customCaretColor = values.customCaretColor;
			if ( values.caretColorEnabled )
				input.caretColor = values.caretColor;
			if ( values.readOnlyEnabled )
				input.readOnly = values.readOnly;
			#endif
			
			if ( values.selectionColorEnabled )
				input.selectionColor = values.selectionColor;
			if ( values.hideMobileInputEnabled )
				input.shouldHideMobileInput = values.hideMobileInput;
			
			if ( values.contentTypeEnabled )
			{
				input.contentType = values.contentType;
				input.lineType = values.lineType;
				
				if ( values.contentType == InputField.ContentType.Custom )
				{
					input.inputType = values.inputType;
					input.keyboardType = values.keyboardType;
					input.characterValidation = values.characterValidation;
				}
			}
						
			// Transition
			if ( values.transitionValues.transitionEnabled )
			{
				input.transition = values.transitionValues.transition;
				
				// Transition ColorBlock
				input.colors = TransitionHelper.ApplyColorBlock
				(
					values.transitionValues.normalColor, 
					values.transitionValues.highlightedColor,
					values.transitionValues.pressedColor,
					values.transitionValues.disabledColor,
					values.transitionValues.colorMultiplier,
					values.transitionValues.fadeDuration
				);
				
				// Transition SpriteState
				input.spriteState = TransitionHelper.ApplySpriteState
				(
					values.transitionValues.highlightedGraphic,
					values.transitionValues.pressedGraphic,
					values.transitionValues.disabledGraphic
					
				);
				
				// Transition SpriteState
				input.animationTriggers = TransitionHelper.ApplyAnimationTriggers
				(
					values.transitionValues.normalTrigger,
					values.transitionValues.highlightedTrigger,
					values.transitionValues.pressedTrigger,
					values.transitionValues.disabledTrigger
				);
			}
			
			// References
			SetReferences ( style, input, values, obj);
		}
		
		/// <summary>
		/// Set the components references
		/// </summary>
		public static void SetReferences (Style style, InputField input, InputFieldValues values, GameObject obj)
		{
			if (values.targetGraphicReference == "Null")
				input.targetGraphic = null;
			
			if (values.textReference == "Null")
				input.textComponent = null;
			
			if (values.placeholderReference == "Null")
				input.placeholder = null;
			
			while(!obj.name.Contains("(" + style.findByName + ")") && obj.transform.parent != null)
				obj = obj.transform.parent.gameObject;
			
			foreach (StyleComponent com in style.styleComponents)
			{
				if (com.name == values.targetGraphicReference && values.targetGraphicReference != "Null")
				{
					if (obj.transform.Find(com.path))
						input.targetGraphic = obj.transform.Find(com.path).gameObject.GetComponent<Graphic>();
				}
				
				if (com.name == values.textReference && values.textReference != "Null")
				{
					if (obj.transform.Find(com.path))
						input.textComponent = obj.transform.Find(com.path).gameObject.GetComponent<Text>();
				}
				
				if (com.name == values.placeholderReference && values.placeholderReference != "Null")
				{
					if (obj.transform.Find(com.path))
						input.placeholder = obj.transform.Find(com.path).gameObject.GetComponent<Text>();
				}
			}
		}
	}
}



















