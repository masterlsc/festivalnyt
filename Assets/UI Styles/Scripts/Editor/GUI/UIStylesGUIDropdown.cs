using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;

namespace UIStyles
{
	public class UIStylesGUIDropdown : EditorWindow
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
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.dropdown.targetGraphicReference == "Null", OnGotTargetGraphicReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.Text || styleComponent.styleComponentType == StyleComponentType.Image)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.dropdown.targetGraphicReference == styleComponent.name, OnGotTargetGraphicReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotTargetGraphicReference (object obj)
		{
			tempStyleComponent.dropdown.targetGraphicReference = (string)obj;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="style"></param>
		private static void templateReferenceContext(Style style)
		{
			GenericMenu menu = new GenericMenu();
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.dropdown.templateReference == "Null", OnGotTemplateReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.RectTransform || styleComponent.styleComponentType == StyleComponentType.Text || styleComponent.styleComponentType == StyleComponentType.Image)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.dropdown.templateReference == styleComponent.name, OnGotTemplateReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotTemplateReference (object obj)
		{
			tempStyleComponent.dropdown.templateReference = (string)obj;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="style"></param>
		private static void CaptionTextReferenceContext(Style style)
		{
			GenericMenu menu = new GenericMenu();
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.dropdown.captionTextReference == "Null", OnGotCaptionTextReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.Text)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.dropdown.captionTextReference == styleComponent.name, OnGotCaptionTextReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotCaptionTextReference (object obj)
		{
			tempStyleComponent.dropdown.captionTextReference = (string)obj;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="style"></param>
		private static void CaptionImageReferenceContext(Style style)
		{
			GenericMenu menu = new GenericMenu();
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.dropdown.captionImageReference == "Null", OnGotCaptionImageReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.Image)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.dropdown.captionImageReference == styleComponent.name, OnGotCaptionImageReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotCaptionImageReference (object obj)
		{
			tempStyleComponent.dropdown.captionImageReference = (string)obj;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="style"></param>
		private static void ItemTextReferenceContext(Style style)
		{
			GenericMenu menu = new GenericMenu();
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.dropdown.itemTextReference == "Null", OnGotItemTextReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.Text)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.dropdown.itemTextReference == styleComponent.name, OnGotItemTextReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotItemTextReference (object obj)
		{
			tempStyleComponent.dropdown.itemTextReference = (string)obj;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="style"></param>
		private static void ItemImageReferenceContext(Style style)
		{
			GenericMenu menu = new GenericMenu();
			
			menu.AddItem(new GUIContent("Null"), tempStyleComponent.dropdown.itemImageReference == "Null", OnGotTitemImageReference, "Null");
			
			menu.AddSeparator("");
			
			foreach (StyleComponent styleComponent in style.styleComponents)
				if (styleComponent.styleComponentType == StyleComponentType.Image)
					menu.AddItem(new GUIContent(styleComponent.name), tempStyleComponent.dropdown.itemImageReference == styleComponent.name, OnGotTitemImageReference, styleComponent.name);
			
			menu.ShowAsContext();
		}
		
		private static void OnGotTitemImageReference (object obj)
		{
			tempStyleComponent.dropdown.itemImageReference = (string)obj;
		}
		
		/// <summary>
		/// Draw the values
		/// </summary>
		public static void DrawValues ( Style style, StyleComponent componentValues, ref bool checkPath, string findByName )
		{
			DropdownValues values = componentValues.dropdown;
			
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
					UIStylesGUITransition.DrawValues(componentValues.dropdown.transitionValues);
					GUILayout.Space ( 10 );
					EditorGUI.indentLevel = 0;
					
					
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
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.dropdown.targetGraphicReference) ? "Null" : componentValues.dropdown.targetGraphicReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
						{
							tempStyleComponent = componentValues;
							TargetGraphicContext(style);
						}
					}
					GUILayout.EndHorizontal ();
					
					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField("Template: ", GUILayout.Width(140));
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.dropdown.templateReference) ? "Null" : componentValues.dropdown.templateReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
						{
							tempStyleComponent = componentValues;
							templateReferenceContext(style);
						}
					}
					GUILayout.EndHorizontal ();
					
					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField("Caption Text: ", GUILayout.Width(140));
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.dropdown.captionTextReference) ? "Null" : componentValues.dropdown.captionTextReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
						{
							tempStyleComponent = componentValues;
							CaptionTextReferenceContext(style);
						}
					}
					GUILayout.EndHorizontal ();
					
					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField("Caption Image: ", GUILayout.Width(140));
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.dropdown.captionImageReference) ? "Null" : componentValues.dropdown.captionImageReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
						{
							tempStyleComponent = componentValues;
							CaptionImageReferenceContext(style);
						}
					}
					GUILayout.EndHorizontal ();
					
					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField("Item Text: ", GUILayout.Width(140));
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.dropdown.itemTextReference) ? "Null" : componentValues.dropdown.itemTextReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
						{
							tempStyleComponent = componentValues;
							ItemTextReferenceContext(style);
						}
					}
					GUILayout.EndHorizontal ();
					
					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField("ItemTextReference: ", GUILayout.Width(140));
						if (GUILayout.Button((string.IsNullOrEmpty(componentValues.dropdown.itemImageReference) ? "Null" : componentValues.dropdown.itemImageReference), EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(40)))
						{
							tempStyleComponent = componentValues;
							ItemImageReferenceContext(style);
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
						if (draggedObj is Dropdown)
						{
							componentValues.dropdown = DropdownHelper.SetValuesFromComponent((Dropdown)draggedObj);
						}
						if (draggedObj is GameObject)
						{
							GameObject obj = (GameObject)draggedObj;
							
							if (obj.GetComponent<Dropdown>())
								componentValues.dropdown = DropdownHelper.SetValuesFromComponent(obj.GetComponent<Dropdown>());
						}
					}
				}
			}
			GUILayout.EndHorizontal ();
		}
	}
}





















