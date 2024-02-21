using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;


namespace UIStyles
{
	public class UIStylesGUIButton : EditorWindow
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
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.button.targetGraphicReference == "Null", OnGotTargetGraphicReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.Text || styleComponent.styleComponentType == StyleComponentType.Image)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.button.targetGraphicReference == styleComponent.name, OnGotTargetGraphicReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotTargetGraphicReference (object obj)
		{
			tempStyleComponent.button.targetGraphicReference = (string)obj;
		}
		
		/// <summary>
		/// Draw the values
		/// </summary>
		public static void DrawValues ( Style style, StyleComponent componentValues, ref bool checkPath, string findByName )
		{
			ButtonValues values = componentValues.button;
			
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
					UIStylesGUITransition.DrawValues(componentValues.button.transitionValues);
					
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
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.button.targetGraphicReference) ? "Null" : componentValues.button.targetGraphicReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
						{
							tempStyleComponent = componentValues;
							TargetGraphicContext(style);
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
						if (draggedObj is Button)
						{
							componentValues.button = ButtonHelper.SetValuesFromComponent((Button)draggedObj);
						}
						if (draggedObj is GameObject)
						{
							GameObject obj = (GameObject)draggedObj;
							
							if (obj.GetComponent<Button>())
								componentValues.button = ButtonHelper.SetValuesFromComponent(obj.GetComponent<Button>());
						}
					}
				}
			}
			GUILayout.EndHorizontal ();
		}
	}
}





















