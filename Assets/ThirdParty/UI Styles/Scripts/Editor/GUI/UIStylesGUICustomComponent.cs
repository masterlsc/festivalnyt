using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace UIStyles
{
	public class UIStylesGUICustomComponent : EditorWindow
	{
		private enum GetFilter {FieldInfo, PropertyInfo, FieldInfoAndPropertyInfo, Type}
		
		const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
		
		private static CustomComponentValues tempValues;
		
		private static void ExcludedPropertiesContext (StyleDataFile data, CustomComponentValues Values, GetFilter getFilter, bool excludeAll)
		{
			tempValues = Values;
			
			GUI.FocusControl ( null );
			
			GenericMenu menu = new GenericMenu ();
			
			List<string> list = new List<string> ();
			List<string> excludedBackingField = new List<string> ();
			
			// ---------- //
			// Properties
			// ---------- //
			if (getFilter == GetFilter.Type || getFilter == GetFilter.PropertyInfo || getFilter == GetFilter.FieldInfoAndPropertyInfo)
			{
				PropertyInfo[] properties = Values.customComponent.GetType().GetProperties(flags);
				foreach (PropertyInfo property in properties)
				{
					// Check the type hasn't been excluded
					if (!ReflectionHelper.IsTypeExcluded(data, property.PropertyType.ToString()))
					{
						if (getFilter == GetFilter.Type)
						{
							// Check the type hasn't already been added
							if (!list.Contains(property.PropertyType.ToString()))
								list.Add(property.PropertyType.ToString());
						}
						
						// Check the property hasn't been excluded
						else if (!ReflectionHelper.IsPropertyExcluded(data, property.Name))
						{
							list.Add(property.Name);
							excludedBackingField.Add("<" + property.Name + ">k__BackingField");
						}
						else if (excludeAll)
							list.Add(property.Name);
							
					}
				}
			}	
			
			// ---------- //
			// Fields
			// ---------- //
			if (getFilter == GetFilter.Type || getFilter == GetFilter.FieldInfo || getFilter == GetFilter.FieldInfoAndPropertyInfo)
			{
				FieldInfo[] fields = Values.customComponent.GetType().GetFields(flags);
				foreach (FieldInfo field in fields)
				{
					// Check the type hasn't been excluded
					if (!ReflectionHelper.IsTypeExcluded(data, field.FieldType.ToString()))
					{
						if (getFilter == GetFilter.Type)
						{
							// Check the type hasn't already been added
							if (!list.Contains(field.FieldType.ToString()))
								list.Add(field.FieldType.ToString());
						}
						
						// Check the field hasn't been excluded
						else if (!ReflectionHelper.IsFieldExcluded(data, field.Name) && !excludedBackingField.Contains(field.Name))
						{
							list.Add(field.Name);
						}
						else if (excludeAll)
							list.Add(field.Name);
					}
				}
			}						
			
			list.Sort();
				
			foreach (string str in list)
			{
				menu.AddItem ( new GUIContent ( str ), tempValues.excludedList.Contains(str), OnExcludedProperties, str );
				
				if (excludeAll)
				{
					if (!tempValues.excludedList.Contains(str))
						tempValues.excludedList.Add(str);
					
					tempValues.excludedList.Sort();
				}
			}
			
			if (!excludeAll)
				menu.ShowAsContext ();
		}
		
		private static void OnExcludedProperties (object obj)
		{			
			string value = (string)obj;
			
			if (tempValues.excludedList.Contains(value))
				tempValues.excludedList.Remove(value);
			
			else tempValues.excludedList.Add(value);
			
			tempValues.excludedList.Sort();
		}
		
		
		
		private static void PropertyContext (string value)
		{
			GUI.FocusControl ( null );
			
			GenericMenu menu = new GenericMenu ();
			
			menu.AddItem ( new GUIContent ( "Remove" ), false, PropertyContextRemove, value );
			
			menu.ShowAsContext ();
		}
		
		private static void PropertyContextRemove (object obj)
		{
			if (tempValues.excludedList.Contains((string)obj))
				tempValues.excludedList.Remove((string)obj);
				
		}
		
		
		
		/// <summary>
		/// Draw the values
		/// </summary>
		public static void DrawValues ( StyleComponent componentValues, ref bool checkPath, string findByName )
		{
			CustomComponentValues values = componentValues.custom;
			
			GUILayout.Space ( -8 );
			EditorGUI.indentLevel = 0;
			
			GUILayout.BeginVertical ( EditorHelper.StandardPanel ( 10 ) );
			{	
				// -------------------------------------------------- //
				// Draw Component Path
				// -------------------------------------------------- //
				UIStylesGUIPath.DrawPath(ref componentValues.path, ref componentValues.renamePath, componentValues.hasPathError, ref checkPath, findByName );
				GUILayout.Space ( 5 );
				
				GUILayout.BeginVertical ( EditorHelper.StandardPanel ( 10 ) );
				{
					EditorGUILayout.LabelField ( "Template Component" );
					values.customComponent = (Component)EditorGUILayout.ObjectField ( "", values.customComponent, typeof( Component ), false );
					
					if ( values.customComponent != null )
					{
						EditorGUILayout.LabelField ( new GUIContent ( values.customComponent.GetType ().ToString (), "" ), EditorStyles.miniLabel );
						
						GUILayout.Space ( 10 );
						
						if ( values.alwaysRemove )
							values.alwaysAdd = false;
						
						GUILayout.Space ( 10 );
						
						
						if (GUILayout.Button("Exclude", EditorHelper.buttonSkin))
						{
							ExcludedPropertiesContext ( UIStylesDatabase.styleData, values, GetFilter.FieldInfoAndPropertyInfo, false);
						}
						if (GUILayout.Button("Exclude All", EditorHelper.buttonSkin))
						{
							ExcludedPropertiesContext ( UIStylesDatabase.styleData, values, GetFilter.FieldInfoAndPropertyInfo, true);
						}
						if (GUILayout.Button("Remove All", EditorHelper.buttonSkin))
						{
							values.excludedList.Clear();
						}
						
						/*
						GUILayout.BeginHorizontal();
						{
							EditorGUILayout.LabelField("#", GUILayout.Width(50));
							
							EditorGUILayout.LabelField("Name", GUILayout.MinWidth(10));
							
							EditorGUILayout.LabelField("Type", GUILayout.MinWidth(10));
						}
						GUILayout.EndHorizontal();
						*/
						
						GUILayout.Space ( 5 );
						foreach (string str in values.excludedList)
						{
							EditorGUILayout.LabelField("- " + str);
							
							Event currentEvent = Event.current;
							Rect contextRect;
							
							contextRect =  GUILayoutUtility.GetLastRect ( );
							EditorGUI.DrawRect ( contextRect, Color.clear );
														
							Vector2 mousePos = currentEvent.mousePosition;
							
							if (contextRect.Contains(mousePos))
							{
								if (Event.current.button == 1 && Event.current.type == EventType.MouseUp)
								{
									tempValues = values;
									PropertyContext (str);
								}								
							}
						}
					}
				}
				GUILayout.EndVertical ();
			}
			GUILayout.EndVertical ();
		}
		
		
	}
	
}












 