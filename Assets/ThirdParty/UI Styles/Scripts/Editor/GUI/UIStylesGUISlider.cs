using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;

namespace UIStyles
{
	public class UIStylesGUISlider : EditorWindow
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
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.slider.targetGraphicReference == "Null", OnGotTargetGraphicReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.Text || styleComponent.styleComponentType == StyleComponentType.Image)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.slider.targetGraphicReference == styleComponent.name, OnGotTargetGraphicReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotTargetGraphicReference (object obj)
		{
			tempStyleComponent.slider.targetGraphicReference = (string)obj;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="style"></param>
		private static void FillRectContext(Style style)
		{
			GenericMenu menu = new GenericMenu();
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.slider.fillRectReference == "Null", OnGotFillRectReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.RectTransform || styleComponent.styleComponentType == StyleComponentType.Image)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.slider.fillRectReference == styleComponent.name, OnGotFillRectReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotFillRectReference (object obj)
		{
			tempStyleComponent.slider.fillRectReference = (string)obj;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="style"></param>
		private static void HandleRectContext(Style style)
		{
			GenericMenu menu = new GenericMenu();
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.slider.handleRectReference == "Null", OnGotHandleRectReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.RectTransform || styleComponent.styleComponentType == StyleComponentType.Text || styleComponent.styleComponentType == StyleComponentType.Image)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.slider.handleRectReference == styleComponent.name, OnGotHandleRectReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotHandleRectReference (object obj)
		{
			tempStyleComponent.slider.handleRectReference = (string)obj;
		}
		
		
		
		
		/// <summary>
		/// Draw the values
		/// </summary>
		public static void DrawValues ( Style style, StyleComponent componentValues, ref bool checkPath, string findByName )
		{
			SliderValues values = componentValues.slider;
			
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
					UIStylesGUITransition.DrawValues(componentValues.slider.transitionValues);
					GUILayout.Space ( 10 );
					EditorGUI.indentLevel = 0;
					
					// -------------------------------------------------- //
					// Slider Values
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal();
					{
						values.directionEnabled = (bool) EditorGUILayout.Toggle(values.directionEnabled, GUILayout.MaxWidth (26));
						
						EditorGUI.BeginDisabledGroup (!values.directionEnabled);
						{
							values.direction = (Slider.Direction) EditorGUILayout.EnumPopup("Direction:", values.direction);
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal();
					
					GUILayout.BeginHorizontal();
					{
						values.minValueEnabled = (bool) EditorGUILayout.Toggle(values.minValueEnabled, GUILayout.MaxWidth (26));
						
						EditorGUI.BeginDisabledGroup (!values.minValueEnabled);
						{
							values.minValue = (float) EditorGUILayout.FloatField("Min Value:", values.minValue);
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal();
					
					GUILayout.BeginHorizontal();
					{
						values.maxValueEnabled = (bool) EditorGUILayout.Toggle(values.maxValueEnabled, GUILayout.MaxWidth (26));
						
						EditorGUI.BeginDisabledGroup (!values.maxValueEnabled);
						{
							values.maxValue = (float) EditorGUILayout.FloatField("Max Value:", values.maxValue);
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal();
					
					GUILayout.BeginHorizontal();
					{
						values.wholeNumbersEnabled = (bool) EditorGUILayout.Toggle(values.wholeNumbersEnabled, GUILayout.MaxWidth (26));
						
						EditorGUI.BeginDisabledGroup (!values.wholeNumbersEnabled);
						{
							values.wholeNumbers = (bool) EditorGUILayout.Toggle("Whole Numbers:", values.wholeNumbers);
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal();
					
					if (!values.wholeNumbers)
					{
						GUILayout.BeginHorizontal();
						{
							values.valueEnabled = (bool) EditorGUILayout.Toggle(values.valueEnabled, GUILayout.MaxWidth (26));
							
							EditorGUI.BeginDisabledGroup (!values.valueEnabled);
							{
								values.value = (float) EditorGUILayout.Slider("Value:", values.value, values.minValue, values.maxValue, GUILayout.MinWidth(0));
							}
							EditorGUI.EndDisabledGroup ();
						}
						GUILayout.EndHorizontal();
					}
					else
					{
						GUILayout.BeginHorizontal();
						{
							values.valueEnabled = (bool) EditorGUILayout.Toggle(values.valueEnabled, GUILayout.MaxWidth (26));
							
							EditorGUI.BeginDisabledGroup (!values.valueEnabled);
							{
								values.value = (int) EditorGUILayout.IntSlider("Value:", (int)values.value, (int)values.minValue, (int)values.maxValue, GUILayout.MinWidth(0));
							}
							EditorGUI.EndDisabledGroup ();
						}
						GUILayout.EndHorizontal();
					}
					
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
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.slider.targetGraphicReference) ? "Null" : componentValues.slider.targetGraphicReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
						{
							tempStyleComponent = componentValues;
							TargetGraphicContext(style);
						}
					}
					GUILayout.EndHorizontal ();
					
					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField("Fill Rect: ", GUILayout.Width(140));
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.slider.fillRectReference) ? "Null" : componentValues.slider.fillRectReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
						{
							tempStyleComponent = componentValues;
							FillRectContext(style);
						}
					}
					GUILayout.EndHorizontal ();
					
					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField("Handle Rect: ", GUILayout.Width(140));
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.slider.handleRectReference) ? "Null" : componentValues.slider.handleRectReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
						{
							tempStyleComponent = componentValues;
							HandleRectContext(style);
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
						if (draggedObj is Slider)
						{
							componentValues.slider = SliderHelper.SetValuesFromComponent((Slider)draggedObj);
						}
						if (draggedObj is GameObject)
						{
							GameObject obj = (GameObject)draggedObj;
							
							if (obj.GetComponent<Slider>())
								componentValues.slider = SliderHelper.SetValuesFromComponent(obj.GetComponent<Slider>());
						}
					}
				}
			}
			GUILayout.EndHorizontal ();
		}
	}
}





















