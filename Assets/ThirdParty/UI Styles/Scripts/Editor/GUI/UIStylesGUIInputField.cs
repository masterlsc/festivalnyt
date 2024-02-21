using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;

namespace UIStyles
{
	public class UIStylesGUIInputField : EditorWindow
	{
		private static UnityEngine.Object[] draggedObjects;
		
		
		private static StyleComponent tempStyleComponent;
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="style"></param>
		private static void TargetGraphicContext(Style style)
		{
			GenericMenu menu = new GenericMenu();
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.inputField.targetGraphicReference == "Null", OnGotTargetGraphicReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.Text || styleComponent.styleComponentType == StyleComponentType.Image)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.inputField.targetGraphicReference == styleComponent.name, OnGotTargetGraphicReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotTargetGraphicReference (object obj)
		{
			tempStyleComponent.inputField.targetGraphicReference = (string)obj;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="style"></param>
		private static void TextContext(Style style)
		{
			GenericMenu menu = new GenericMenu();
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.inputField.textReference == "Null", OnGotTextReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.Text)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.inputField.textReference == styleComponent.name, OnGotTextReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotTextReference (object obj)
		{
			tempStyleComponent.inputField.textReference = (string)obj;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="style"></param>
		private static void PlaceholderContext(Style style)
		{
			GenericMenu menu = new GenericMenu();
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.inputField.placeholderReference == "Null", OnGotPlaceholderReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.Text)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.inputField.placeholderReference == styleComponent.name, OnGotPlaceholderReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotPlaceholderReference (object obj)
		{
			tempStyleComponent.inputField.placeholderReference = (string)obj;
		}
			
			
			
		
		/// <summary>
		/// Draw the values
		/// </summary>
		public static void DrawValues ( Style style, StyleComponent componentValues, ref bool checkPath, string findByName )
		{
			InputFieldValues values = componentValues.inputField;
			
			GUILayout.Space ( -8 );
			EditorGUI.indentLevel = 0;
			
			GUILayout.BeginVertical ( EditorHelper.StandardPanel ( 10 ) );
			{	
				// -------------------------------------------------- //
				// Draw Component Path
				// -------------------------------------------------- //
				UIStylesGUIPath.DrawPath(ref componentValues.path, ref componentValues.renamePath, componentValues.hasPathError, ref checkPath, findByName);
				GUILayout.Space ( 5 );
				
				GUILayout.BeginVertical ( EditorHelper.StandardPanel ( 10 ) );
				{
					// -------------------------------------------------- //
					// Interactable
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal ();
					{
						values.interactableEnabled = (bool)EditorGUILayout.Toggle ( values.interactableEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.interactableEnabled );
						{
							values.interactable = (bool)EditorGUILayout.Toggle ( "Interactable:", values.interactable );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					// -------------------------------------------------- //
					// Transition Values
					// -------------------------------------------------- //
					GUILayout.Space ( 10 );
					UIStylesGUITransition.DrawValues(componentValues.inputField.transitionValues);
					GUILayout.Space ( 10 );
					EditorGUI.indentLevel = 0;
					
					// -------------------------------------------------- //
					// InputField Values
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal ();
					{
						values.characterLimitEnabled = (bool)EditorGUILayout.Toggle ( values.characterLimitEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.characterLimitEnabled );
						{
							values.characterLimit = (int)EditorGUILayout.IntField ( "Character Limit:", values.characterLimit );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					GUILayout.BeginHorizontal ();
					{
						values.caretBlinkRateEnabled = (bool)EditorGUILayout.Toggle ( values.caretBlinkRateEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.caretBlinkRateEnabled );
						{
							values.caretBlinkRate = EditorGUILayout.Slider ( "Caret Blink Rate:", values.caretBlinkRate, 0f, 4f, GUILayout.MinWidth ( 0f ) );
						}
						EditorGUI.EndDisabledGroup ();
						
					}
					GUILayout.EndHorizontal ();
					
					GUILayout.BeginHorizontal ();
					{
						values.caretWidthEnabled = (bool)EditorGUILayout.Toggle ( values.caretWidthEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.caretWidthEnabled );
						{
							values.caretWidth = EditorGUILayout.IntSlider ( "Caret Width:", values.caretWidth, 1, 5, GUILayout.MinWidth ( 0f ) );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					
					GUILayout.BeginHorizontal ();
					{
						values.customCaretColorEnabled = (bool)EditorGUILayout.Toggle ( values.customCaretColorEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.customCaretColorEnabled );
						{
							values.customCaretColor = (bool)EditorGUILayout.Toggle ( "Custum Caret Color:", values.customCaretColor );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					if ( values.customCaretColor )
					{
						GUILayout.BeginHorizontal ();
						{
							values.caretColorEnabled = (bool)EditorGUILayout.Toggle ( values.caretColorEnabled, GUILayout.MaxWidth ( 26 ) );
							
							EditorGUI.BeginDisabledGroup ( !values.caretColorEnabled );
							{
								// -------------------------------------------------- //
								// Color Picker 
								// -------------------------------------------------- //
								WindowColorPalette.DrawColorPicker(ref values.caretColorID, ref values.caretColor, "Caret Color");
								GUI.backgroundColor = string.IsNullOrEmpty(values.caretColorID) ? Color.white : Color.green;
								if ( GUILayout.Button ( "P", GUILayout.Width ( 30 ) ) )
								{
									WindowColorPalette.GetColor ( WindowColorPalette.ReturnToWindow.UIStyles, (Color col, string id ) =>
									{
										values.caretColor = col;
										values.caretColorID = id;
									} );
								}
								GUI.backgroundColor = Color.white;
							}
							EditorGUI.EndDisabledGroup ();								
						}
						GUILayout.EndHorizontal ();
					}
					
					GUILayout.BeginHorizontal ();
					{
						values.selectionColorEnabled = (bool)EditorGUILayout.Toggle ( values.selectionColorEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.selectionColorEnabled );
						{
							// -------------------------------------------------- //
							// Color Picker 
							// -------------------------------------------------- //
							WindowColorPalette.DrawColorPicker(ref values.selectionColorID, ref values.selectionColor, "Selection Color");
							GUI.backgroundColor = string.IsNullOrEmpty(values.selectionColorID) ? Color.white : Color.green;
							if ( GUILayout.Button ( "P", GUILayout.Width ( 30 ) ) )
							{
								WindowColorPalette.GetColor ( WindowColorPalette.ReturnToWindow.UIStyles, (Color col, string id ) =>
								{
									values.selectionColor = col;
									values.selectionColorID = id;
								} );
							}
							GUI.backgroundColor = Color.white;
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
										
					GUILayout.BeginHorizontal ();
					{
						values.hideMobileInputEnabled = (bool)EditorGUILayout.Toggle ( values.hideMobileInputEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.hideMobileInputEnabled );
						{
							values.hideMobileInput = (bool)EditorGUILayout.Toggle ( "Hide Mobile Input:", values.hideMobileInput );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					GUILayout.BeginHorizontal ();
					{
						values.readOnlyEnabled = (bool)EditorGUILayout.Toggle ( values.readOnlyEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.readOnlyEnabled );
						{
							values.readOnly = (bool)EditorGUILayout.Toggle ( "Read Only:", values.readOnly );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					
					GUILayout.BeginHorizontal ();
					{
						values.contentTypeEnabled = (bool)EditorGUILayout.Toggle ( values.contentTypeEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.contentTypeEnabled );
						{
							values.contentType = (InputField.ContentType)EditorGUILayout.EnumPopup ( "Content Type:", values.contentType );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					EditorGUI.BeginDisabledGroup ( !values.contentTypeEnabled );
					{
						EditorGUI.indentLevel = 1;
						if ( values.contentType == InputField.ContentType.Standard || values.contentType == InputField.ContentType.Autocorrected || values.contentType == InputField.ContentType.Custom )
						{
							GUILayout.BeginHorizontal ();
							{
								GUILayout.Space ( 30 );
								values.lineType = (InputField.LineType)EditorGUILayout.EnumPopup ( "Line Type:", values.lineType );
							}
							GUILayout.EndHorizontal ();
						}
						
						if ( values.contentType == InputField.ContentType.Custom )
						{
							GUILayout.BeginHorizontal ();
							{
								GUILayout.Space ( 30 );
								values.inputType = (InputField.InputType)EditorGUILayout.EnumPopup ( "Input Type:", values.inputType );
							}
							GUILayout.EndHorizontal ();
							
							GUILayout.BeginHorizontal ();
							{
								GUILayout.Space ( 30 );
								values.keyboardType = (TouchScreenKeyboardType)EditorGUILayout.EnumPopup ( "keyboard Type:", values.keyboardType );
							}
							GUILayout.EndHorizontal ();
							
							GUILayout.BeginHorizontal ();
							{
								GUILayout.Space ( 30 );
								values.characterValidation = (InputField.CharacterValidation)EditorGUILayout.EnumPopup ( "Character Validation:", values.characterValidation );
							}
							GUILayout.EndHorizontal ();
						}
						
						EditorGUI.indentLevel = 0;
					}
					EditorGUI.EndDisabledGroup ();
					
					
					
					// -------------------------------------------------- //
					// References
					// -------------------------------------------------- //
					GUILayout.Space ( 10 );
					EditorGUI.indentLevel = 0;
					EditorGUILayout.LabelField("References");
					EditorGUI.indentLevel = 1;
					
					
					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField("Target Graphic: ", GUILayout.Width(140));
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.inputField.targetGraphicReference) ? "Null" : componentValues.inputField.targetGraphicReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
						{
							tempStyleComponent = componentValues;
							TargetGraphicContext(style);
						}
					}
					GUILayout.EndHorizontal ();
					
					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField("Text: ", GUILayout.Width(140));
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.inputField.textReference) ? "Null" : componentValues.inputField.textReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
						{
							tempStyleComponent = componentValues;
							TextContext(style);
						}
					}
					GUILayout.EndHorizontal ();
					
					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField("Placeholder: ", GUILayout.Width(140));
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.inputField.placeholderReference) ? "Null" : componentValues.inputField.placeholderReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
						{
							tempStyleComponent = componentValues;
							PlaceholderContext(style);
						}
					}
					GUILayout.EndHorizontal ();
				}
				GUILayout.EndVertical ();
				
				// -------------------------------------------------- //
				// Drop Area
				// -------------------------------------------------- //
				draggedObjects = new UnityEngine.Object[0];
				EditorHelper.DropArea(ref draggedObjects);
				
				if (draggedObjects.Length > 0)
				{
					foreach (Object draggedObj in draggedObjects) 
					{
						if (draggedObj is InputField)
						{
							componentValues.inputField = InputFieldHelper.SetValuesFromComponent((InputField)draggedObj);
						}
						if (draggedObj is GameObject)
						{
							GameObject obj = (GameObject)draggedObj;
							
							if (obj.GetComponent<InputField>())
								componentValues.inputField = InputFieldHelper.SetValuesFromComponent(obj.GetComponent<InputField>());
						}
					}
				}
			}
			GUILayout.EndHorizontal ();
		}
	}
}





















