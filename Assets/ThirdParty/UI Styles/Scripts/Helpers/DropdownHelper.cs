using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
	public class DropdownHelper 
	{
		/// <summary>
		/// Create Style, pass in an object to match the styles values with the objects values
		/// </summary>
		public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj, bool includeRectTransform)
		{
			Style style = new Style(data, category, styleName, findByName);
			style.rename = !Application.isPlaying;
			
			// Dropdown
			StyleComponent v = new StyleComponent(data, style, StyleComponentType.Dropdown, "Dropdown", "");
			
			if (obj != null)
				v.dropdown = SetValuesFromComponent ( obj );
			
			else
			{
				v.dropdown.targetGraphicReference = "Background";
				v.dropdown.templateReference = "Template (Background)";
				v.dropdown.captionTextReference = "Label";
				v.dropdown.captionImageReference = "Null";
				v.dropdown.itemTextReference = "Item Label";
				v.dropdown.itemImageReference = "Null";
			}
						
			// Background
			v = new StyleComponent(data, style, StyleComponentType.Image, "Background", "");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj, includeRectTransform );
			
			else 
			{				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(160, 30);
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Sliced;
				v.image.fillCentre = true;
				#endif
			}
			
			// Label			
			v = new StyleComponent(data, style, StyleComponentType.Text, "Label", "Label");
			
			if (obj != null)
				v.text = TextHelper.SetValuesFromComponent ( obj.transform.Find("Label").gameObject, includeRectTransform );
			
			else 
			{
				v.text.rectTransformValues.positionEnabled = true;
				v.text.rectTransformValues.position = new Vector3(10, 7, 0);
				
				v.text.rectTransformValues.sizeDeltaEnabled = true;
				v.text.rectTransformValues.sizeDelta = new Vector2(25, 6);
				
				v.text.rectTransformValues.anchorMinEnabled = true;
				v.text.rectTransformValues.anchorMin = new Vector2(0, 0);
				
				v.text.rectTransformValues.anchorMaxEnabled = true;
				v.text.rectTransformValues.anchorMax = new Vector2(1, 1);
				
				v.text.rectTransformValues.pivotEnabled = true;
				v.text.rectTransformValues.pivot = new Vector2(0.5f, 0.5f);
				
				v.text.colorEnabled = true;
			}
			
			// Arrow
			v = new StyleComponent(data, style, StyleComponentType.Image, "Arrow", "Arrow");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Arrow").gameObject, includeRectTransform );
			
			else 
			{
				v.image.rectTransformValues.positionEnabled = true;
				v.image.rectTransformValues.position = new Vector3(-15, 0, 0);
				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(20, 20);
				
				v.image.rectTransformValues.anchorMinEnabled = true;
				v.image.rectTransformValues.anchorMin = new Vector2(1, 0.5f);
				
				v.image.rectTransformValues.anchorMaxEnabled = true;
				v.image.rectTransformValues.anchorMax = new Vector2(1, 0.5f);
				
				v.image.rectTransformValues.pivotEnabled = true;
				v.image.rectTransformValues.pivot = new Vector2(0.5f, 0.5f);
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/DropdownArrow.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Simple;
				#endif
			}
			
			// Dropdown Background
			v = new StyleComponent(data, style, StyleComponentType.Image, "Template (Background)", "Template");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Template").gameObject, includeRectTransform );
			
			else
			{
				v.image.rectTransformValues.positionEnabled = true;
				v.image.rectTransformValues.position = new Vector3(0, 2, 0);
				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(0, 150);
				
				v.image.rectTransformValues.anchorMinEnabled = true;
				v.image.rectTransformValues.anchorMin = new Vector2(0, 0);
				
				v.image.rectTransformValues.anchorMaxEnabled = true;
				v.image.rectTransformValues.anchorMax = new Vector2(1, 0);
				
				v.image.rectTransformValues.pivotEnabled = true;
				v.image.rectTransformValues.pivot = new Vector2(0.5f, 1);
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Sliced;
				v.image.fillCentre = true;
				#endif
			}
			
			// Scroll rect
			v = new StyleComponent(data, style, StyleComponentType.ScrollRect, "Template", "Template");
			
			if (obj != null)
				v.scrollRect = ScrollRectHelper.SetValuesFromComponent ( obj.transform.Find("Template").gameObject );
			
			else
			{
				v.scrollRect.contentReference = "Content";
				v.scrollRect.viewportReference = "Viewport";
				v.scrollRect.horizonalScrollbarReference = "Null";
				v.scrollRect.verticalScrollbarReference = "Scrollbar";
				
				v.scrollRect.horizontalEnabled = true;
				v.scrollRect.horizontal = false;
				
				v.scrollRect.movementTypeEnabled = true;
				v.scrollRect.movementType = ScrollRect.MovementType.Clamped;
				
				v.scrollRect.verticalScrollbarVisibilityEnabled = true;
				v.scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
				v.scrollRect.verticalScrollbarSpacingEnabled = true;
				v.scrollRect.verticalScrollbarSpacing = -3;
			}
			
			// Viewport
			v = new StyleComponent(data, style, StyleComponentType.Image, "Viewport", "Template/Viewport");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Template/Viewport").gameObject, includeRectTransform );
			
			else
			{
				v.image.rectTransformValues.positionEnabled = true;
				v.image.rectTransformValues.position = new Vector3(0, 0, 0);
				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(17, 0);
				
				v.image.rectTransformValues.anchorMinEnabled = true;
				v.image.rectTransformValues.anchorMin = new Vector2(0, 0);
				
				v.image.rectTransformValues.anchorMaxEnabled = true;
				v.image.rectTransformValues.anchorMax = new Vector2(1, 1);
				
				v.image.rectTransformValues.pivotEnabled = true;
				v.image.rectTransformValues.pivot = new Vector2(0, 1);
				
				v.image.useAsMask = true;
				v.image.showMaskGraphic = false;
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Sliced;
				v.image.fillCentre = true;
				#endif
			}
			
			// Content
			v = new StyleComponent(data, style, StyleComponentType.RectTransform, "Content", "Template/Viewport/Content");
			
			if (!includeRectTransform)
			{
				v.rectTransform.positionEnabled = true;
				v.rectTransform.position = new Vector3(0, 0, 0);
				
				v.rectTransform.sizeDeltaEnabled = true;
				v.rectTransform.sizeDelta = new Vector2(0, 28);
				
				v.rectTransform.anchorMinEnabled = true;
				v.rectTransform.anchorMin = new Vector2(0, 1);
				
				v.rectTransform.anchorMaxEnabled = true;
				v.rectTransform.anchorMax = new Vector2(1, 1);
				
				v.rectTransform.pivotEnabled = true;
				v.rectTransform.pivot = new Vector2(0.5f, 1);
			}
			
			// Item Rect
			v = new StyleComponent(data, style, StyleComponentType.RectTransform, "Item Rect", "Template/Viewport/Content/Item");
			
			if (!includeRectTransform)
			{
				v.rectTransform.positionEnabled = true;
				v.rectTransform.position = new Vector3(0, 0, 0);
				
				v.rectTransform.sizeDeltaEnabled = true;
				v.rectTransform.sizeDelta = new Vector2(0, 20);
				
				v.rectTransform.anchorMinEnabled = true;
				v.rectTransform.anchorMin = new Vector2(0, 0.5f);
				
				v.rectTransform.anchorMaxEnabled = true;
				v.rectTransform.anchorMax = new Vector2(1, 0.5f);
				
				v.rectTransform.pivotEnabled = true;
				v.rectTransform.pivot = new Vector2(0.5f, 0.5f);
			}
			
			// Toggle (Item)
			v = new StyleComponent(data, style, StyleComponentType.Toggle, "Toggle (Item)", "Template/Viewport/Content/Item");
			
			if (obj != null)
				v.toggle = ToggleHelper.SetValuesFromComponent (  obj.transform.Find("Template/Viewport/Content/Item").gameObject );
			
			else
			{
				v.toggle.isOnEnabled = true;
				v.toggle.isOn = true;
				v.toggle.targetGraphicReference = "Item Background";
				v.toggle.checkmarkReference = "Item Checkmark";
			}
			
			// Item Background
			v = new StyleComponent(data, style, StyleComponentType.Image, "Item Background", "Template/Viewport/Content/Item/Item Background");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Template/Viewport/Content/Item/Item Background").gameObject, includeRectTransform );
			
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
				
				/*
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Sliced;
				v.image.fillCentre = true;
				#endif
				*/
			}
			
			// Item Checkmark
			v = new StyleComponent(data, style, StyleComponentType.Image, "Item Checkmark", "Template/Viewport/Content/Item/Item Checkmark");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Template/Viewport/Content/Item/Item Checkmark").gameObject, includeRectTransform );
			
			else
			{
				v.image.rectTransformValues.positionEnabled = true;
				v.image.rectTransformValues.position = new Vector3(10, 0, 0);
				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(20, 20);
				
				v.image.rectTransformValues.anchorMinEnabled = true;
				v.image.rectTransformValues.anchorMin = new Vector2(0, 0.5f);
				
				v.image.rectTransformValues.anchorMaxEnabled = true;
				v.image.rectTransformValues.anchorMax = new Vector2(0, 0.5f);
				
				v.image.rectTransformValues.pivotEnabled = true;
				v.image.rectTransformValues.pivot = new Vector2(0.5f, 0.5f);
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Checkmark.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Simple;
				#endif
			}
			
			// Item Label			
			v = new StyleComponent(data, style, StyleComponentType.Text, "Item Label", "Template/Viewport/Content/Item/Item Label");
			
			if (obj != null)
				v.text = TextHelper.SetValuesFromComponent ( obj.transform.Find("Template/Viewport/Content/Item/Item Label").gameObject, includeRectTransform );
			
			else 
			{
				v.text.rectTransformValues.positionEnabled = true;
				v.text.rectTransformValues.position = new Vector3(20, 3, 0);
				
				v.text.rectTransformValues.sizeDeltaEnabled = true;
				v.text.rectTransformValues.sizeDelta = new Vector2(10, 1);
				
				v.text.rectTransformValues.anchorMinEnabled = true;
				v.text.rectTransformValues.anchorMin = new Vector2(0, 0);
				
				v.text.rectTransformValues.anchorMaxEnabled = true;
				v.text.rectTransformValues.anchorMax = new Vector2(1, 1);
				
				v.text.rectTransformValues.pivotEnabled = true;
				v.text.rectTransformValues.pivot = new Vector2(0.5f, 0.5f);
				
				v.text.colorEnabled = true;
			}
			
			// Scrollbar
			v = new StyleComponent(data, style, StyleComponentType.Scrollbar, "Scrollbar", "Template/Scrollbar");
			
			if (obj != null)
				v.scrollbar = ScrollbarHelper.SetValuesFromComponent ( obj.transform.Find("Template/Scrollbar").gameObject );
			
			else
			{
				v.scrollbar.targetGraphicReference = "Scrollbar Handle";
				v.scrollbar.handleRectReference = "Scrollbar Handle";
				
				v.scrollbar.directionEnabled = true;
				v.scrollbar.direction = Scrollbar.Direction.BottomToTop;
			}
			
			// Scrollbar Background
			v = new StyleComponent(data, style, StyleComponentType.Image, "Scrollbar Background", "Template/Scrollbar");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Template/Scrollbar").gameObject, includeRectTransform );
			
			else 
			{				
				v.image.rectTransformValues.positionEnabled = true;
				v.image.rectTransformValues.position = new Vector3(0, 0, 0);
				
				v.image.rectTransformValues.sizeDeltaEnabled = true;
				v.image.rectTransformValues.sizeDelta = new Vector2(20, 0);
				
				v.image.rectTransformValues.anchorMinEnabled = true;
				v.image.rectTransformValues.anchorMin = new Vector2(1, 0);
				
				v.image.rectTransformValues.anchorMaxEnabled = true;
				v.image.rectTransformValues.anchorMax = new Vector2(1, 1);
				
				v.image.rectTransformValues.pivotEnabled = true;
				v.image.rectTransformValues.pivot = new Vector2(1, 1);
				
				#if UNITY_EDITOR
				v.image.imageEnabled = true;
				v.image.image = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
				
				v.image.imageTypeEnabled = true;
				v.image.imageType = Image.Type.Sliced;
				v.image.fillCentre = true;
				#endif
			}
			
			// Scrollbar Sliding Area
			v = new StyleComponent(data, style, StyleComponentType.RectTransform, "Scrollbar Sliding Area", "Template/Scrollbar/Sliding Area");
			
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
			
			// Scrollbar Handle
			v = new StyleComponent(data, style, StyleComponentType.Image, "Scrollbar Handle", "Template/Scrollbar/Sliding Area/Handle");
			
			if (obj != null)
				v.image = ImageHelper.SetValuesFromComponent ( obj.transform.Find("Template/Scrollbar/Sliding Area/Handle").gameObject, includeRectTransform );
			
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
			
			UIStylesDatabase.Save ();
		}
		
		/// <summary>
		/// Sets the Values from a component
		/// </summary>
		public static DropdownValues SetValuesFromComponent ( Component com )
		{	
			if (com is Dropdown)
				return SetValuesFromComponent ( (Dropdown)com );
			
			else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be Dropdown");
			
			return null;
		}
		public static DropdownValues SetValuesFromComponent ( GameObject obj )
		{	
			if (obj.GetComponent<Dropdown>())
				return SetValuesFromComponent ( obj.GetComponent<Dropdown>() );
			
			else Debug.LogError ("SetValuesFromComponent: Component not found!");
			
			return null;
		}
		public static DropdownValues SetValuesFromComponent ( Dropdown dropdown )
		{	
			DropdownValues values = new DropdownValues ();
			
			values.interactableEnabled = true;
			values.transitionValues.transitionEnabled = true;
			
			values.interactable = dropdown.interactable;
			values.transitionValues.transition = dropdown.transition;
			values.transitionValues.normalColor = dropdown.colors.normalColor;
			values.transitionValues.highlightedColor = dropdown.colors.highlightedColor;
			values.transitionValues.pressedColor = dropdown.colors.pressedColor;
			values.transitionValues.disabledColor = dropdown.colors.disabledColor;
			values.transitionValues.colorMultiplier = dropdown.colors.colorMultiplier;
			values.transitionValues.fadeDuration = dropdown.colors.fadeDuration;
			
			values.transitionValues.highlightedGraphic	= dropdown.spriteState.highlightedSprite;
			values.transitionValues.pressedGraphic = dropdown.spriteState.pressedSprite;
			values.transitionValues.disabledGraphic = dropdown.spriteState.disabledSprite;
			
			values.transitionValues.normalTrigger = dropdown.animationTriggers.normalTrigger;
			values.transitionValues.highlightedTrigger = dropdown.animationTriggers.highlightedTrigger;
			values.transitionValues.pressedTrigger = dropdown.animationTriggers.pressedTrigger;
			values.transitionValues.disabledTrigger = dropdown.animationTriggers.disabledTrigger;
						
			return values;
		}
		
		
		public static void Apply ( Style style, DropdownValues values, GameObject obj )
		{
			Dropdown dropDown = obj.GetComponent<Dropdown> ();
			
			// Options
			if ( values.interactableEnabled )
				dropDown.interactable = values.interactable;
						
			// Transition
			if ( values.transitionValues.transitionEnabled )
			{
				dropDown.transition = values.transitionValues.transition;
				
				// Transition ColorBlock
				dropDown.colors = TransitionHelper.ApplyColorBlock
				(
					values.transitionValues.normalColor, 
					values.transitionValues.highlightedColor,
					values.transitionValues.pressedColor,
					values.transitionValues.disabledColor,
					values.transitionValues.colorMultiplier,
					values.transitionValues.fadeDuration
				);
				
				// Transition SpriteState
				dropDown.spriteState = TransitionHelper.ApplySpriteState
				(
					values.transitionValues.highlightedGraphic,
					values.transitionValues.pressedGraphic,
					values.transitionValues.disabledGraphic
					
				);
				
				// Transition SpriteState
				dropDown.animationTriggers = TransitionHelper.ApplyAnimationTriggers
				(
					values.transitionValues.normalTrigger,
					values.transitionValues.highlightedTrigger,
					values.transitionValues.pressedTrigger,
					values.transitionValues.disabledTrigger
				);
			}
			
			// References
			SetReferences ( style, dropDown, values, obj);
		}
		
		/// <summary>
		/// Set the components references
		/// </summary>
		public static void SetReferences (Style style, Dropdown dropDown, DropdownValues values, GameObject obj)
		{
			if (values.targetGraphicReference == "Null")
				dropDown.targetGraphic = null;
			
			if (values.templateReference == "Null")
				dropDown.template = null;
			
			if (values.captionTextReference == "Null")
				dropDown.captionText = null;
			
			if (values.captionImageReference == "Null")
				dropDown.captionImage = null;
			
			if (values.itemTextReference == "Null")
				dropDown.itemText = null;
			
			if (values.itemImageReference == "Null")
				dropDown.itemImage = null;
			
			while(!obj.name.Contains("(" + style.findByName + ")") && obj.transform.parent != null)
				obj = obj.transform.parent.gameObject;
			
			foreach (StyleComponent com in style.styleComponents)
			{
				if (com.name == values.targetGraphicReference && values.targetGraphicReference != "Null")
				{
					if (obj.transform.Find(com.path))
						dropDown.targetGraphic = obj.transform.Find(com.path).gameObject.GetComponent<Graphic>();
				}
				
				if (com.name == values.templateReference && values.templateReference != "Null")
				{
					if (obj.transform.Find(com.path))
						dropDown.template = obj.transform.Find(com.path).gameObject.GetComponent<RectTransform>();
				}
				
				if (com.name == values.captionTextReference && values.captionTextReference != "Null")
				{
					if (obj.transform.Find(com.path))
						dropDown.captionText = obj.transform.Find(com.path).gameObject.GetComponent<Text>();
				}
				
				if (com.name == values.captionImageReference && values.captionImageReference != "Null")
				{
					if (obj.transform.Find(com.path))
						dropDown.captionImage = obj.transform.Find(com.path).gameObject.GetComponent<Image>();
				}
				
				if (com.name == values.itemTextReference && values.itemTextReference != "Null")
				{
					if (obj.transform.Find(com.path))
						dropDown.itemText = obj.transform.Find(com.path).gameObject.GetComponent<Text>();
				}
				
				if (com.name == values.itemImageReference && values.itemImageReference != "Null")
				{
					if (obj.transform.Find(com.path))
						dropDown.itemImage = obj.transform.Find(com.path).gameObject.GetComponent<Image>();
				}
			}
		}
	}
}



















