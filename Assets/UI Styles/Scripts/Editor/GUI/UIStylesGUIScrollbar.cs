using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;

namespace UIStyles
{
	public class UIStylesGUIScrollbar : EditorWindow
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
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.scrollbar.targetGraphicReference == "Null", OnGotTargetGraphicReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.Text || styleComponent.styleComponentType == StyleComponentType.Image)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.scrollbar.targetGraphicReference == styleComponent.name, OnGotTargetGraphicReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotTargetGraphicReference (object obj)
		{
			tempStyleComponent.scrollbar.targetGraphicReference = (string)obj;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="style"></param>
		private static void HandleRectContext(Style style)
		{
			GenericMenu menu = new GenericMenu();
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.scrollbar.handleRectReference == "Null", OnGotHandleRectReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.RectTransform || styleComponent.styleComponentType == StyleComponentType.Text || styleComponent.styleComponentType == StyleComponentType.Image)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.scrollbar.handleRectReference == styleComponent.name, OnGotHandleRectReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotHandleRectReference (object obj)
		{
			tempStyleComponent.scrollbar.handleRectReference = (string)obj;
		}
		
		/// <summary>
		/// Draw the values
		/// </summary>
		public static void DrawValues ( Style style, StyleComponent componentValues, ref bool checkPath, string findByName )
		{
			ScrollbarValues values = componentValues.scrollbar;
			
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
					UIStylesGUITransition.DrawValues(componentValues.scrollbar.transitionValues);
					GUILayout.Space ( 10 );
					EditorGUI.indentLevel = 0;
					
					// -------------------------------------------------- //
					// Scrollbar Values
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal();
					{
						values.directionEnabled = (bool) EditorGUILayout.Toggle(values.directionEnabled, GUILayout.MaxWidth (26));
						
						EditorGUI.BeginDisabledGroup (!values.directionEnabled);
						{
							values.direction = (Scrollbar.Direction) EditorGUILayout.EnumPopup("Direction:", values.direction);
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal();
					
					GUILayout.BeginHorizontal();
					{
						values.valueEnabled = (bool) EditorGUILayout.Toggle(values.valueEnabled, GUILayout.MaxWidth (26));
						
						EditorGUI.BeginDisabledGroup (!values.valueEnabled);
						{
							values.value = (float) EditorGUILayout.Slider("Value:", values.value, 0, 1, GUILayout.MinWidth(0));
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal();
					
					GUILayout.BeginHorizontal();
					{
						values.sizeEnabled = (bool) EditorGUILayout.Toggle(values.sizeEnabled, GUILayout.MaxWidth (26));
						
						EditorGUI.BeginDisabledGroup (!values.sizeEnabled);
						{
							values.size = (float) EditorGUILayout.Slider("size:", values.size, 0, 1, GUILayout.MinWidth(0));
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal();
					
					GUILayout.BeginHorizontal();
					{
						values.numberOfStepsEnabled = (bool) EditorGUILayout.Toggle(values.numberOfStepsEnabled, GUILayout.MaxWidth (26));
						
						EditorGUI.BeginDisabledGroup (!values.numberOfStepsEnabled);
						{
							values.numberOfSteps = (int) EditorGUILayout.IntSlider("Number Of Steps:", values.numberOfSteps, 0, 11, GUILayout.MinWidth(0));
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal();
					
					
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
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.scrollbar.targetGraphicReference) ? "Null" : componentValues.scrollbar.targetGraphicReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
						{
							tempStyleComponent = componentValues;
							TargetGraphicContext(style);
						}
					}
					GUILayout.EndHorizontal ();
										
					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField("Handle Rect: ", GUILayout.Width(140));
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.scrollbar.handleRectReference) ? "Null" : componentValues.scrollbar.handleRectReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
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
						if (draggedObj is Scrollbar)
						{
							componentValues.scrollbar = ScrollbarHelper.SetValuesFromComponent((Scrollbar)draggedObj);
						}
						if (draggedObj is GameObject)
						{
							GameObject obj = (GameObject)draggedObj;
							
							if (obj.GetComponent<Scrollbar>())
								componentValues.scrollbar = ScrollbarHelper.SetValuesFromComponent(obj.GetComponent<Scrollbar>());
						}
					}
				}
			}
			GUILayout.EndHorizontal ();
		}
	}
}





















