using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
	public class TextHelper
	{
		/// <summary>
		/// The can show warning.
		/// </summary>
		public static bool canShowWarning = false;
		
		/// <summary>
		/// Create Style, pass in an object to match the styles values with the objects values
		/// </summary>
		public static void CreateStyle (StyleDataFile data, string category, string styleName, string findByName, GameObject obj, bool includeRectTransform)
		{
			Style style = new Style(data, category, styleName, findByName);
			style.rename = !Application.isPlaying;
						
			StyleComponent v = new StyleComponent(data, style, StyleComponentType.Text, "Text", "");
			
			if (obj != null)
				v.text = SetValuesFromComponent ( obj, includeRectTransform );
			
			{
				v.text.rectTransformValues.sizeDeltaEnabled = true;
				v.text.rectTransformValues.sizeDelta = new Vector2(160, 30);
			}
				
			UIStylesDatabase.Save ();
		}
		
		/// <summary>
		/// Sets the Values from a component
		/// </summary>
		public static TextValues SetValuesFromComponent ( Component com, bool includeRectTransform )
		{	
			if (com is Text)
				return SetValuesFromComponent ( (Text)com, null, includeRectTransform );
			
			else Debug.LogError ("SetValuesFromComponent: Type is: " + com.GetType() + " but should be Text");
			
			return null;
		}
		
		public static TextValues SetValuesFromComponent ( GameObject obj, bool includeRectTransform )
		{	
			if (obj.GetComponent<Text>())
				return SetValuesFromComponent ( obj.GetComponent<Text>(), obj, includeRectTransform );
			
			else Debug.LogError ("SetValuesFromComponent: Component not found!");
			
			return null;
		}
		
		public static TextValues SetValuesFromComponent ( Text text, GameObject obj, bool includeRectTransform )
		{				
			TextValues values = new TextValues ();
			
			values.font = text.font;
			values.fontStyle = text.fontStyle;
			values.fontSize = text.fontSize;
			values.lineSpacing = text.lineSpacing;
			values.richText = text.supportRichText;
			values.textAlignment = text.alignment;
			values.alignByGeometry = text.alignByGeometry;
			values.horizontalWrapMode = text.horizontalOverflow;
			values.verticalWrapMode = text.verticalOverflow;
			values.bestFit = text.resizeTextForBestFit;
			values.bestFitMinSize = text.resizeTextMinSize;
			values.bestFitMaxSize = text.resizeTextMaxSize;
			values.material = text.material;
			values.raycastTarget = text.raycastTarget;
			//values.fontCase = ;
			values.color = text.color;
						
			// Enabled
			values.textEnabled = false;
			values.fontEnabled = text.font != null;
			values.fontStyleEnabled = true;
			values.fontSizeEnabled = true;
			values.lineSpacingEnabled = true;
			values.richTextEnabled = true;
			values.textAlignmentEnabled = true;
			values.alignByGeometryEnabled = true;
			values.horizontalWrapModeEnabled = true;
			values.verticalWrapModeEnabled = true;
			values.colorEnabled = true;
			values.bestFitEnabled = text.resizeTextForBestFit;
			values.materialEnabled = true;
			values.raycastTargetEnabled = true;
			values.fontCaseEnabled = false;
			
			if (obj != null)
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
				
				values.useOutlineEnabled = gotOutline;
				values.useShadowEnabled = gotShadow;
				
				values.useOutline = gotOutline;
				
				if ( values.useOutline )
				{
					if (!obj.GetComponent<UIStyles.Outline> ())
						obj.gameObject.AddComponent<UIStyles.Outline> ();
					
					values.outlineColor = obj.GetComponent<UIStyles.Outline> () ? obj.GetComponent<UIStyles.Outline> ().effectColor : obj.GetComponent<UnityEngine.UI.Outline> ().effectColor;
					values.outlineDistance = obj.GetComponent<UIStyles.Outline> () ? obj.GetComponent<UIStyles.Outline> ().effectDistance : obj.GetComponent<UnityEngine.UI.Outline> ().effectDistance;
					values.outlineUseAlpha = obj.GetComponent<UIStyles.Outline> () ? obj.GetComponent<UIStyles.Outline> ().useGraphicAlpha : obj.GetComponent<UnityEngine.UI.Outline> ().useGraphicAlpha;
				}
				
				values.useShadow = gotShadow;
				
				if ( values.useShadow )
				{
					if (!obj.GetComponent<UIStyles.Shadow> ())
						obj.gameObject.AddComponent<UIStyles.Shadow> ();
					
					values.shadowColor = obj.GetComponent<UIStyles.Shadow> () ? obj.GetComponent<UIStyles.Shadow> ().effectColor : obj.GetComponent<UnityEngine.UI.Shadow> ().effectColor;
					values.shadowDistance = obj.GetComponent<UIStyles.Shadow> () ? obj.GetComponent<UIStyles.Shadow> ().effectDistance : obj.GetComponent<UnityEngine.UI.Shadow> ().effectDistance;
					values.shadowUseAlpha = obj.GetComponent<UIStyles.Shadow> () ? obj.GetComponent<UIStyles.Shadow> ().useGraphicAlpha : obj.GetComponent<UnityEngine.UI.Shadow> ().useGraphicAlpha;
				}
				
				#if !PRE_UNITY_5
				
				if (obj.GetComponent<UIStyles.Gradient> ())
				{
					values.overlay = Overlay.GradientOverlay;
				}
				
				if ( values.overlay == Overlay.GradientOverlay )
				{
					values.gradientTopColor = obj.GetComponent<UIStyles.Gradient> ().topColor;
					values.gradientBottomColor = obj.GetComponent<UIStyles.Gradient> ().bottomColor;
				}
				#endif
								
				if (Application.isPlaying && includeRectTransform)
					values.rectTransformValues = RectTransformHelper.SetValuesFromComponent ( obj );
			}
			
			return values;
		}
		
		/// <summary>
		/// Create the object in the scene
		/// </summary>
		public static void CreateObject ()
		{
			
		}
		
		
		/// <summary>
		/// Apply the specified values and text.
		/// </summary>
		public static void Apply ( TextValues values, GameObject obj )
		{
			if ( obj.GetComponent<Text> () )
			{
				Text text = obj.GetComponent<Text> ();
				
				if ( values.fontEnabled && values.font == null )
				{
					if ( canShowWarning )
					{
						#if UNITY_EDITOR
						if ( UnityEditor.EditorUtility.DisplayDialog ( "Warning", "You must select a font", "Ok" ) )
							canShowWarning = false;
						#else
						Debug.Log("Warning - You must select a font");
						#endif
					}
				}
				else
				{
					// text
					if ( values.textEnabled )
						text.text = values.text;
					
					// font
					if ( values.fontEnabled )
						text.font = values.font;
					
					// Font Style
					if ( values.fontStyleEnabled )
						text.fontStyle = values.fontStyle;
					
					// Font Size
					if ( values.fontSizeEnabled )
						text.fontSize = values.fontSize;
					
					// Line Spacing
					if ( values.lineSpacingEnabled )
						text.lineSpacing = values.lineSpacing;
					
					// Rich Text
					if ( values.richTextEnabled )
						text.supportRichText = values.richText;
					
					// Text Alignment
					if ( values.textAlignmentEnabled )
						text.alignment = values.textAlignment;
					
					// Text Alignment
					if ( values.alignByGeometryEnabled )
						text.alignByGeometry = values.alignByGeometry;
					
					// HorizontalWrapMode
					if ( values.horizontalWrapModeEnabled )
						text.horizontalOverflow = values.horizontalWrapMode;
					
					// Vertical Wrap Mode
					if ( values.verticalWrapModeEnabled )
						text.verticalOverflow = values.verticalWrapMode;
					
					// Best Fit
					if ( values.bestFitEnabled )
						text.resizeTextForBestFit = values.bestFit;
					
					// Best Fit Min Size
					if ( values.bestFitEnabled )
						text.resizeTextMinSize = values.bestFitMinSize;
					
					// Best Fit Max Size
					if ( values.bestFitEnabled )
						text.resizeTextMaxSize = values.bestFitMaxSize;
					
					// Colour
					if ( values.colorEnabled )
						text.color = values.color;
					
					// Material
					if ( values.materialEnabled )
						text.material = values.material;
					
					#if !PRE_UNITY_5
					// Raycast Target
					if ( values.raycastTargetEnabled )
						text.raycastTarget = values.raycastTarget;
					#endif
					
					// Font Case Enabled
					if ( values.fontCaseEnabled )
					{ 
						if ( !string.IsNullOrEmpty ( text.text ) )
						{
							if ( values.fontCase == FontCase.FirstUppercase )
							{
								text.text = StringFormats.StringToFirstUpper ( text.text );
							}
							else if ( values.fontCase == FontCase.TitleCase )
							{
								text.text = StringFormats.StringToTitleCase ( text.text );
							}
							else
							{
								text.text = values.fontCase == FontCase.Lowercase ? text.text.ToLower () : text.text.ToUpper ();
							}
						}
					}
					
					// Outline
					if ( values.useOutlineEnabled )
					{
						if ( values.useOutline )
						{
							if ( !text.GetComponent<UIStyles.Outline> () )
							{
								if ( DataHelper.canEditValues ( text.gameObject ) )
								{
									text.gameObject.AddComponent<UIStyles.Outline> ();
								}
							}
							
							if ( text.GetComponent<UIStyles.Outline> () )
							{
								text.GetComponent<UIStyles.Outline> ().effectColor = values.outlineColor;
								text.GetComponent<UIStyles.Outline> ().effectDistance = values.outlineDistance;
								text.GetComponent<UIStyles.Outline> ().useGraphicAlpha = values.outlineUseAlpha;
							}
						}
						else if ( text.GetComponent<UIStyles.Outline> () )
						{
							#if UNITY_EDITOR
							UnityEditor.Editor.DestroyImmediate ( text.GetComponent<UIStyles.Outline> (), true );
							#else
							GameObject.Destroy ( text.GetComponent<UIStyles.Outline> () );
							#endif
						}
					}
					
					// Shadow
					if ( values.useShadowEnabled )
					{
						if ( values.useShadow )
						{
							// add the component
							if ( !text.GetComponent<UIStyles.Shadow> () )
							{
								if ( DataHelper.canEditValues ( text.gameObject ) )
								{
									text.gameObject.AddComponent<UIStyles.Shadow> ();
								}
							}
							
							if ( text.GetComponent<UIStyles.Shadow> () )
							{
								text.GetComponent<UIStyles.Shadow> ().effectColor = values.shadowColor;
								text.GetComponent<UIStyles.Shadow> ().effectDistance = values.shadowDistance;
								text.GetComponent<UIStyles.Shadow> ().useGraphicAlpha = values.shadowUseAlpha;
							}
						}
						else if ( text.GetComponent<UIStyles.Shadow> () )
						{
							#if UNITY_EDITOR
							UnityEditor.Editor.DestroyImmediate ( text.GetComponent<UIStyles.Shadow> (), true );
							#else
							GameObject.Destroy ( text.GetComponent<UIStyles.Shadow> () );
							#endif
						}
					}
					
					#if !PRE_UNITY_5
					// Gradient
					if ( values.overlay == UIStyles.Overlay.GradientOverlay )
					{
						if ( !text.GetComponent<UIStyles.Gradient> () )
						{
							if ( DataHelper.canEditValues ( text.gameObject ) )
							{
								text.gameObject.AddComponent<UIStyles.Gradient> ();
							}
						}
						
						if ( text.GetComponent<UIStyles.Gradient> () )
						{
							text.GetComponent<UIStyles.Gradient> ().topColor = values.gradientTopColor;
							text.GetComponent<UIStyles.Gradient> ().bottomColor = values.gradientBottomColor;
						}
					}
					else if ( text.GetComponent<UIStyles.Gradient> () )
					{
						#if UNITY_EDITOR
						UnityEditor.Editor.DestroyImmediate ( text.GetComponent<UIStyles.Gradient> (), true );
						#else
						GameObject.Destroy ( text.GetComponent<UIStyles.Gradient> () );
						#endif
					}
					#endif
				}
			}
			
			// Rect Transform
			RectTransformHelper.Apply ( values.rectTransformValues, obj.GetComponent<RectTransform> () );
			
			// Check
			CheckEffects.CheckAll ( obj, values, null );
		}
	}
}



















