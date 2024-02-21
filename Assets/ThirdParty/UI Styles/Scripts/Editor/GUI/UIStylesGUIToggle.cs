using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;

namespace UIStyles
{
	public class UIStylesGUIToggle : EditorWindow
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
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.toggle.targetGraphicReference == "Null", OnGotTargetGraphicReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.Text || styleComponent.styleComponentType == StyleComponentType.Image)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.toggle.targetGraphicReference == styleComponent.name, OnGotTargetGraphicReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotTargetGraphicReference (object obj)
		{
			tempStyleComponent.toggle.targetGraphicReference = (string)obj;
		}
		
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="style"></param>
		private static void CheckmarkContext(Style style)
		{
			GenericMenu menu = new GenericMenu();
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.toggle.checkmarkReference == "Null", OnGotCheckmarkReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.Text || styleComponent.styleComponentType == StyleComponentType.Image)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.toggle.checkmarkReference == styleComponent.name, OnGotCheckmarkReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotCheckmarkReference (object obj)
		{
			tempStyleComponent.toggle.checkmarkReference = (string)obj;
		}
		
		
		
		/// <summary>
		/// Draw the values
		/// </summary>
		public static void DrawValues ( Style style, StyleComponent componentValues, ref bool checkPath, string findByName )
		{
			ToggleValues values = componentValues.toggle;
			
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
					UIStylesGUITransition.DrawValues(componentValues.toggle.transitionValues);
					GUILayout.Space ( 10 );
					EditorGUI.indentLevel = 0;
					
					// -------------------------------------------------- //
					// Toggle Values
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal ();
					{
						values.isOnEnabled = (bool)EditorGUILayout.Toggle ( values.isOnEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.isOnEnabled );
						{
							values.isOn = (bool)EditorGUILayout.Toggle ( "Is On:", values.isOn );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					GUILayout.BeginHorizontal ();
					{
						values.toggleTransitionEnabled = (bool)EditorGUILayout.Toggle ( values.toggleTransitionEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.toggleTransitionEnabled );
						{
							values.toggleTransition = (Toggle.ToggleTransition)EditorGUILayout.EnumPopup ( "Toggle Transition:", values.toggleTransition );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					
					
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
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.toggle.targetGraphicReference) ? "Null" : componentValues.toggle.targetGraphicReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
						{
							tempStyleComponent = componentValues;
							TargetGraphicContext(style);
						}
					}
					GUILayout.EndHorizontal ();
					
					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField("Checkmark Graphic: ", GUILayout.Width(140));
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.toggle.checkmarkReference) ? "Null" : componentValues.toggle.checkmarkReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
						{
							tempStyleComponent = componentValues;
							CheckmarkContext(style);
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
						if (draggedObj is Toggle)
						{
							componentValues.toggle = ToggleHelper.SetValuesFromComponent((Toggle)draggedObj);
						}
						if (draggedObj is GameObject)
						{
							GameObject obj = (GameObject)draggedObj;
							
							if (obj.GetComponent<Toggle>())
								componentValues.toggle = ToggleHelper.SetValuesFromComponent(obj.GetComponent<Toggle>());
						}
					}
				}
			}
			GUILayout.EndHorizontal ();
		}
	}
}





















