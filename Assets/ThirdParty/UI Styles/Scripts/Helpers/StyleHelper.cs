using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
#endif

namespace UIStyles
{
	public enum ApplyMode 
	{ AllResources, ActiveInScene }
	
	public enum PremadeStyle 
	{ Empty, Text, Image, Button, InputField, Dropdown, ScrollRect, Scrollbar, Slider, Toggle }
	
	public class StyleHelper
	{		
		#if UNITY_EDITOR
		/// <summary>
		/// Create style data file (ScriptableObject)
		/// </summary>
		/// param: path	  = Path where the file will save
		/// param: OnDone = Action to pass back the data file
		public static void CreateStyleDataFile (string path, Action<StyleDataFile> OnDone)
		{
			#if UNITY_EDITOR
			// is path is null get user to choose path 
			if (string.IsNullOrEmpty(path))
				path = EditorUtility.SaveFilePanel ( "Create UI Styles Data File", Application.dataPath, "UI Styles Data", "asset" );
			#endif
			
			// Return if path is null
			if ( string.IsNullOrEmpty ( path ) )
				return;
			
			// Get project relative path
			path = FileUtil.GetProjectRelativePath ( path );
			
			// Return if project relative path is null as user probable tried saveing out side of the project.
			if ( string.IsNullOrEmpty ( path ) )
			{
				#if UNITY_EDITOR
				// warn the user
				EditorUtility.DisplayDialog ( "Warning", "You must save within the project", "Ok" );
				#endif
				
				return;
			}
			
			// Create and save the scriptable object
			StyleDataFile data = ScriptableObject.CreateInstance<StyleDataFile> ();
			data.categories.Add("Default");
			data.currentCategory = "Default";
			
			AssetDatabase.CreateAsset ( data, path );
			AssetDatabase.SaveAssets ();
			
			if (OnDone != null)
				OnDone(data);
		}
		
		/// <summary>
		/// Create palette data file (ScriptableObject)
		/// </summary>
		/// param: path	  = Path where the file will save
		/// param: OnDone = Action to pass back the data file
		public static void CreatePaletteDataFile (string path, Action<PaletteDataFile> OnDone)
		{
			// Get save path
			if (string.IsNullOrEmpty(path))
				path = EditorUtility.SaveFilePanel ( "Create Color Palette Data File", Application.dataPath, "Color Palette Data", "asset" );
			
			// Return if path is null as user probable cancelled
			if ( string.IsNullOrEmpty ( path ) )
				return;
			
			// Get project relative path
			path = FileUtil.GetProjectRelativePath ( path );
			
			// Return if project relative path is null as user probable tried saveing out side of the project.
			if ( string.IsNullOrEmpty ( path ) )
			{
				// warn the user
				EditorUtility.DisplayDialog ( "Warning", "You must save within the project", "Ok" );
				return;
			}
			
			// Create and save the scriptable object
			PaletteDataFile data = ScriptableObject.CreateInstance<PaletteDataFile> ();
			data.categories.Add("Default");
			data.currentCategory = "Default";
			
			AssetDatabase.CreateAsset ( data, path );
			AssetDatabase.SaveAssets ();
			
			if (OnDone != null)
				OnDone(data);
		}
		#endif
		
		
		
		
		
		
		
		
		
		
		public static void CreateStyleInCanvas (StyleDataFile data, Style style, Canvas canvas)
		{
			if (style.styleComponents.Count == 0)
			{
				#if UNITY_EDITOR
				EditorUtility.DisplayDialog ( "Warning", "No components have been added to this style, this would result in createing an empty onject", "Ok" );
				#endif
			}
			else
			{
				GameObject rootObj = new GameObject("(" + style.findByName + ")");
				rootObj.AddComponent<RectTransform>();
				
				// Create new canvas
				if (canvas == null)
				{
					GameObject canvasObj = new GameObject("Canvas");
					canvas = canvasObj.AddComponent<Canvas>();
					canvas.renderMode = data.preferenceData.defaultCanvasRenderMode;
					canvas.pixelPerfect = data.preferenceData.defaultCanvasPixelPerfect;
					canvas.sortingOrder = data.preferenceData.defaultCanvasSortOrder;
					canvas.targetDisplay = data.preferenceData.defaultCanvasTargetDisplay;
					
					CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
					scaler.uiScaleMode = data.preferenceData.defaultCanvasScalerScaleMode;
					scaler.scaleFactor = data.preferenceData.defaultCanvasScalerScaleFactor;
					scaler.referencePixelsPerUnit = data.preferenceData.defaultCanvasScalerPixelsPerUnit;
					scaler.referenceResolution = data.preferenceData.defaultCanvasResolution;
					scaler.screenMatchMode = data.preferenceData.defaultCanvasScreenMatchMode;
					scaler.matchWidthOrHeight = data.preferenceData.defaultCanvasScalerMatch;
					scaler.fallbackScreenDPI = data.preferenceData.defaultCanvasScalerFallbackScreenDPI;
					scaler.defaultSpriteDPI = data.preferenceData.defaultCanvasScalerDefaultSpriteDPI;
					
					GraphicRaycaster graphicRaycaster = canvasObj.AddComponent<GraphicRaycaster>();
					graphicRaycaster.ignoreReversedGraphics = data.preferenceData.defaultGraphicRaycasterIgnoreReversedGraphics;
					graphicRaycaster.blockingObjects = data.preferenceData.defaultGraphicRaycasterBlockingObjects;
				}
				
				rootObj.transform.SetParent(canvas.transform, false);
				
				Apply (data, style, rootObj, true);
				
				#if UNITY_EDITOR
				Selection.activeObject = rootObj;
				EditorGUIUtility.PingObject ( rootObj );
				#endif
			}
		}
		
		
		/// <summary>
		/// Create a hierarchy directory
		/// </summary>
		/// <param name="rootObj"></param>
		/// <param name="path"></param>
		public static void CreatObjectChildDirectory (GameObject rootObj, string path)
		{
			// If the path is not null
			if (!StringFormats.IsNullOrWhiteSpace(path))
			{
				string[] pathSplit = path.Split('/');
				
				string[] floders = new string[pathSplit.Length];
				
				for (int i = 0; i < pathSplit.Length; i++) 
				{
					for (int f = 0; f < i +1; f++) 
					{
						floders[i] += pathSplit[f];
						
						if (f != i)
							floders[i] += "/";
					}
				}
				
				for (int i = 0; i < floders.Length; i++) 
				{
					if (!rootObj.transform.Find(floders[i]))
					{
						string[] name = floders[i].Split('/');
						GameObject obj = new GameObject(name[name.Length -1]);
						obj.AddComponent<RectTransform>();
						
						if (i == 0)
							obj.transform.SetParent(rootObj.transform, false);
						else
							obj.transform.SetParent(rootObj.transform.Find(floders[i -1]), false);
					}
				}
			}
		}
		
		
		public static string GetPath (GameObject obj, string findByName, bool showError )
		{
			string path = string.Empty;
			if (obj is GameObject)
			{
				GameObject childObj = (GameObject)obj;
				GameObject findByObj = FindParentWithFindBy(childObj, findByName);
				
				if (findByObj != null)
				{
					string childPath = GetGameObjectsPath(childObj);
					string parentPath = GetGameObjectsPath(findByObj);
					
					//Debug.Log (childPath);
					//Debug.Log (parentPath);
					
					path = childPath.Replace(parentPath, "");
					
					if (path[0] == '/')
					{
						string str = path.Remove(0, 1);
						path = str;
					}
					
					//Debug.Log (path);
				}
				else if (showError)
					Debug.LogError ("No find by name was found in path");
			}
			
			return path;
		}
		
		public static GameObject FindParentWithFindBy(GameObject childObject, string findBy)
		{
			Transform t = childObject.transform;
			while (t.parent != null)
			{
				if (t.parent.name.Contains(findBy))
				{
					return t.parent.gameObject;
				}
				t = t.parent.transform;
			}
			return null; // Could not find a parent with given find by.
		}
		
		public static string GetGameObjectsPath(GameObject obj)
		{
			string path = "/" + obj.name;
			while (obj.transform.parent != null)
			{
				obj = obj.transform.parent.gameObject;
				path = "/" + obj.name + path;
			}
			return path;
		}
		
		
		
		
		
		
		
		/// <summary>
		/// Use this override to find the color by its id.
		/// </summary>
		/// param: data = The data file to use
		/// param: id	= The colors id
		public static PaletteColor GetColor (PaletteDataFile data, int id)
		{
			if (data.colorIDs.ContainsKey(id))
				return data.colorIDs[id];
			
			return null;
		}
		
		/// <summary>
		/// Use this override to find the color by category, palette name and color name
		/// </summary>
		/// param: data 	   = The data file to use
		/// param: category	   = The category the palette is in
		/// param: paletteName = The name of the palette
		/// param: colorName   = The name of the color
		public static PaletteColor GetColor (PaletteDataFile data, string category, string paletteName, string colorName)
		{
			foreach (Palette pal in data.palettes)
			{
				if (pal.category == category && pal.name == paletteName)
				{
					foreach (PaletteColor c in pal.colors)
					{
						if (c.name == colorName)
						{
							return c;
						}
					}
				}
			}
			
			return null;
		}
		
		/// <summary>
		/// Get a colors id
		/// </summary>
		/// param: data        = The data file to use
		/// param: category	   = The category the palette is in
		/// param: paletteName = The name of the palette
		/// param: colorName   = The name of the color
		public static int GetColorID (PaletteDataFile data, string category, string paletteName, string colorName)
		{
			foreach (Palette pal in data.palettes)
			{
				if (pal.name == paletteName)
				{
					foreach (PaletteColor c in pal.colors)
					{
						if (c.name == colorName)
						{
							return c.id;
						}
					}
				}
			}
			
			return 0;
		}
		
		
		
		
		
		
		/// <summary>
		/// Use this override to find the palette by id
		/// </summary>
		/// param: data = The data file to use
		/// param: id	= The colors id
		public static Palette GetPalette (PaletteDataFile data, int id)
		{
			if (data.paletteIDs.ContainsKey(id))
				return data.paletteIDs[id];
			
			return null;
		}
		
		/// <summary>
		/// Use this override to find the palette by its category and name
		/// </summary>
		/// param: data 		= The data file to use
		/// param: category	    = The category the palette is in
		/// param: paletteName	= The name of the palette
		public static Palette GetPalette (PaletteDataFile data, string category, string paletteName)
		{
			foreach (Palette pal in data.palettes)
			{
				if (pal.category == category && pal.name == paletteName)
				{
					return pal;
				}
			}
			
			return null;
		}
		
		/// <summary>
		/// Get a palette id
		/// </summary>
		/// param: data         = The data file to use
		/// param: category	    = The category the palette is in
		/// param: paletteName  = The name of the palette
		public static int GetPaletteID (PaletteDataFile data, string category, string paletteName)
		{
			foreach (Palette pal in data.palettes)
			{
				if (pal.category == category && pal.name == paletteName)
				{
					return pal.id;
				}
			}
			
			return 0;
		}
		
		/// <summary>
		/// Delete palette
		/// </summary>
		/// param: data = The data file to use
		/// param: id	= The palettes id
		public static void DeletPalette (PaletteDataFile data, int id)
		{
			data.palettes.Remove (GetPalette(data, id));
		}
		
		/// <summary>
		/// Delete palette
		/// </summary>
		/// param: data 		= The data file to use
		/// param: category		= The category the palette is in
		/// param: paletteName	= The name of the palette
		public static void DeletPalette (PaletteDataFile data, string category, string paletteName)
		{
			data.palettes.Remove (GetPalette(data, category, paletteName));
		}
		
		
		
		
		/// <summary>
		/// Create new style
		/// </summary>
		/// param: data 		= The data file to use
		/// param: premadeStyle = The Pre-made style to create, eg. Empty, Text, Image, Button, InputField, Dropdown, ScrollRect, Scrollbar, Slider, Toggle
		/// param: category		= The category the add the style to
		/// param: name			= The name of the style
		/// param: obj			= If you pass in an object with a component on UI Styles will create the new style using them values.
		public static void AddPremadeStyle ( StyleDataFile data, PremadeStyle premadeStyle, string category, string name, string findByName, GameObject obj, bool includeRectTransform )
		{
			if ( premadeStyle == PremadeStyle.Empty )
				AddEmptyStyle ( data, category, name, findByName );
			else if ( premadeStyle == PremadeStyle.Text )
				TextHelper.CreateStyle ( data, category, name, findByName, obj, includeRectTransform );
			else if ( premadeStyle == PremadeStyle.Image )
				ImageHelper.CreateStyle ( data, category, name, findByName, obj, includeRectTransform );
			else if ( premadeStyle == PremadeStyle.Button )
				ButtonHelper.CreateStyle ( data, category, name, findByName, obj, includeRectTransform );
			else if ( premadeStyle == PremadeStyle.InputField )
				InputFieldHelper.CreateStyle ( data, category, name, findByName, obj, includeRectTransform );
			else if ( premadeStyle == PremadeStyle.Dropdown )
				DropdownHelper.CreateStyle ( data, category, name, findByName, obj, includeRectTransform );
			else if ( premadeStyle == PremadeStyle.ScrollRect )
				ScrollRectHelper.CreateStyle ( data, category, name, findByName, obj, includeRectTransform );
			else if ( premadeStyle == PremadeStyle.Scrollbar )
				ScrollbarHelper.CreateStyle ( data, category, name, findByName, obj, includeRectTransform );
			else if ( premadeStyle == PremadeStyle.Slider )
				SliderHelper.CreateStyle ( data, category, name, findByName, obj, includeRectTransform );
			else if ( premadeStyle == PremadeStyle.Toggle )
				ToggleHelper.CreateStyle ( data, category, name, findByName, obj, includeRectTransform );
		}
		
		/// <summary>
		/// Create an empty style
		/// </summary>
		/// param: data 	  = The data file to use
		/// param: category	  = The category to add the style to
		/// param: name		  = The name of the style
		/// param: findByName = The styles find By Name
		public static void AddEmptyStyle ( StyleDataFile data, string category, string styleName, string findByName )
		{
			Style style = new Style(data, category, styleName, findByName);
			style.rename = !Application.isPlaying;
			
			UIStylesDatabase.Save ();
			DataHelper.highlightRenameField = true;
		}
		
		/// <summary>
		/// Get style by category and name
		/// </summary>
		/// param: data 	= The data file to use
		/// param: category	= The category the style is in
		/// param: name		= The name of the style
		public static Style GetStyle (StyleDataFile data, string category, string styleName)
		{
			for (int i = 0; i < data.styles.Count; i++) 
			{
				if (data.styles[i].category == category && data.styles[i].name == styleName)
				{
					return data.styles[i];
				}
			}
			
			return null;
		}
		
		/// <summary>
		/// Get style by id
		/// </summary>
		/// param: data = The data file to use
		/// param: id	= The styles id
		public static Style GetStyle (StyleDataFile data, int id)
		{
			if (data.styleIDs.ContainsKey(id))
			{
				return data.styleIDs[id];
			}
			else
			{
				return null;
			}
		}
		
		
		/// <summary>
		/// Get the styles id
		/// </summary>
		/// param: data 	= The data file to use
		/// param: category	= The category the style is in
		/// param: name		= The name of the style
		public static int GetStyleID (StyleDataFile data, string category, string styleName)
		{
			for (int i = 0; i < data.styles.Count; i++) 
			{
				if (data.styles[i].category == category && data.styles[i].name == styleName)
				{
					return data.styles[i].id;
				}
			}
			
			return 0;
		}
		
		/// <summary>
		/// Delete style
		/// </summary>
		/// param: data = The data file to use
		/// param: id	= The styles id
		public static void DeletStyle (StyleDataFile data, int id)
		{
			data.styles.Remove (GetStyle(data, id));
		}
		
		/// <summary>
		/// Delete style
		/// </summary>
		/// param: data 		= The data file to use
		/// param: category		= The category the style is in
		/// param: styleName	= The name of the style
		public static void DeletStyle (StyleDataFile data, string category, string styleName)
		{
			data.styles.Remove (GetStyle(data, category, styleName));
		}
		
		/// <summary>
		/// Rename style
		/// </summary>
		/// param: data			= The data file to use
		/// param: id			= The styles id
		/// param: newName		= The new name
		public static void RenameStyle (StyleDataFile data, int id, string newName)
		{
			GetStyle(data, id).name = newName;
		}
		
		/// <summary>
		/// Rename style
		/// </summary>
		/// param: data 		= The data file to use
		/// param: category		= The category the style is in
		/// param: styleName	= The name of the style
		/// param: newName		= The new name
		public static void RenameStyle (StyleDataFile data, string category, string styleName, string newName)
		{
			Style style = GetStyle(data, category, styleName).Clone(data);
			DuplicateStyle (data, style, newName);
		}
		
		/// <summary>
		/// Duplicate style
		/// </summary>
		/// param: data			= The data file to use
		/// param: id			= The styles id
		/// param: newName		= The new name
		public static void DuplicateStyle (StyleDataFile data, int id, string newName)
		{
			Style style = GetStyle(data, id).Clone(data);
			DuplicateStyle (data, style, newName);
		}
		
		/// <summary>
		/// Duplicate style
		/// </summary>
		/// param: data 		= The data file to use
		/// param: category		= The category the style is in
		/// param: styleName	= The name of the style
		public static void DuplicateStyle (StyleDataFile data, string category, string styleName, string newName)
		{
			DuplicateStyle (data, GetStyle(data, category, styleName).Clone(data), newName);
		}
		
		private static void DuplicateStyle (StyleDataFile data, Style style, string newName)
		{
			if (string.IsNullOrEmpty(newName))
				style.name += " Copy";
			
			else style.name = newName;
		}
		
		
		
		/// <summary>
		/// Change the components state
		/// </summary>
		/// param: styleComponent	= The style component
		/// param: newState  		= The new state
		public static void SetStyleState (Style style, StyleState newState)
		{
			style.styleState = newState;
		}
		
		/// <summary>
		/// Change the components state
		/// </summary>
		/// param: styleComponent	= The style component
		/// param: newState  		= The new state
		public static void SetComponentState (StyleComponent styleComponent, StyleComponentState newState)
		{
			styleComponent.styleComponentState = newState;
		}
		
		
		/// <summary>
		/// Use this override to find the component by its id
		/// </summary>
		/// param: data = The data file to use
		/// param: id  	= The components id
		public static StyleComponent GetComponent ( StyleDataFile data, int id )
		{
			return data.componentIDs[id];
		}
		
		/// <summary>
		/// Use this override to find the component by its category name, styles name and components name
		/// </summary>
		/// param: data 			= The data file to use
		/// param: category 		= The category the style is in
		/// param: styleName		= The name of the style
		/// param: componentName	= The name of the component
		public static StyleComponent GetComponent (StyleDataFile data, string category, string styleName, string componentName)
		{
			for (int i = 0; i < data.styles.Count; i++) 
			{
				if (data.styles[i].category == category && data.styles[i].name == styleName)
				{
					for (int c = 0; c < data.styles[i].styleComponents.Count; c++) 
					{
						if (data.styles[i].styleComponents[c].name == componentName)
						{
							return data.styles[i].styleComponents[c];
						}
					}
				}
			}
			
			return null;
		}
		
		/// <summary>
		/// Get the components id
		/// </summary>
		/// param: data 			= The data file to use
		/// param: category 		= The category the style is in
		/// param: styleName		= The name of the style
		/// param: componentName	= The name of the component
		public static int GetComponentID (StyleDataFile data, string category, string styleName, string componentName)
		{
			for (int i = 0; i < data.styles.Count; i++) 
			{
				if (data.styles[i].category == category && data.styles[i].name == styleName)
				{
					for (int c = 0; c < data.styles[i].styleComponents.Count; c++) 
					{
						if (data.styles[i].styleComponents[c].name == componentName)
						{
							return data.styles[i].styleComponents[c].id;
						}
					}
				}
			}
			
			return 0;
		}
		
		/// <summary>
		/// Delete style
		/// </summary>
		/// param: data = The data file to use
		/// param: id	= The styles id
		public static void DeletComponent (StyleDataFile data, int id)
		{
			StyleComponent com = GetComponent(data, id);
			
			// Check parents ID
			if (com.parentID == 0)
				com.parentID = data.GetParentID(id);
			
			Style style = GetStyle( data, com.parentID );
			
			for (int i = 0; i < style.styleComponents.Count; i++) 
			{
				if (style.styleComponents[i].id == id)
				{
					style.styleComponents.RemoveAt(i);
				}
			}
		}
		
		/// <summary>
		/// Delete style
		/// </summary>
		/// param: data 			= The data file to use
		/// param: category			= The category the style is in
		/// param: styleName		= The name of the style
		/// param: componentName	= The name of the component
		public static void DeletComponent (StyleDataFile data, string category, string styleName, string componentName)
		{			
			for (int i = 0; i < data.styles.Count; i++) 
			{
				if (data.styles[i].category == category && data.styles[i].name == styleName)
				{
					for (int c = 0; c < data.styles[i].styleComponents.Count; c++) 
					{
						if (data.styles[i].styleComponents[c].name == componentName)
						{
							data.styles[i].styleComponents.RemoveAt(c);
						}
					}
				}
			}
		}
		
		/// <summary>
		/// Duplicate style
		/// </summary>
		/// param: data			= The data file to use
		/// param: id			= The components id
		/// param: newName		= The new name
		public static void RenameComponent (StyleDataFile data, int id, string newName)
		{
			StyleComponent com = GetComponent(data, id);
			if (com != null)
				com.name = newName;
		}
		
		/// <summary>
		/// Duplicate style
		/// </summary>
		/// param: data 			= The data file to use
		/// param: category			= The category the style is in
		/// param: styleName		= The name of the style
		/// param: componentsName	= The name of the component
		/// param: newName			= The new name
		public static void RenameComponent (StyleDataFile data, string category, string styleName, string componentName, string newName)
		{
			StyleComponent com = GetComponent(data, category, styleName, componentName);
			if (com != null)
				com.name = newName;
		}
		
		/// <summary>
		/// Duplicate component
		/// </summary>
		/// param: data			= The data file to use
		/// param: id			= The components id
		/// param: newName		= The new name
		public static void DuplicateComponent (StyleDataFile data, int id, string newName)
		{
			StyleComponent com = GetComponent(data, id);
			
			// Check parents ID
			if (com.parentID == 0)
				com.parentID = data.GetParentID(id);
			
			GetStyle(data, com.parentID).DuplicateComponent(data, com, newName);
		}
		
		/// <summary>
		/// Duplicate component
		/// </summary>
		/// param: data 			= The data file to use
		/// param: category			= The category the style is in
		/// param: styleName		= The name of the style
		/// param: componentsName	= The name of the component
		/// param: newName			= The new name
		public static void DuplicateComponent (StyleDataFile data, string category, string styleName, string componentName, string newName)
		{
			StyleComponent com = GetComponent(data, category, styleName, componentName);
			
			// Check parents ID
			if (com.parentID == 0)
				com.parentID = data.GetParentID(com.id);
			
			GetStyle(data, com.parentID).DuplicateComponent(data, com, newName);
		}
		
		/// <summary>
		/// Find a group component by its group id
		/// </summary>
		/// <param name="groupID"></param>
		/// <returns></returns>
		public static StyleComponent GetGroupComponentByGroupID (Style style, int groupID)
		{
			foreach (StyleComponent com in style.styleComponents)
			{
				// If its a group
				if (com.styleComponentType == StyleComponentType.Group)
				{
					// And the id matches the on we want
					if (com.groupID == groupID)
					{
						return com;
					}
				}
			}
			
			return null;
		}
		
		public static void AddComponentsToGroups (Style style)
		{
			foreach (StyleComponent com in style.styleComponents)
			{
				// Is part of a group but not the group
				if (com.styleComponentType != StyleComponentType.Group && com.groupID != 0)
				{
					//Debug.Log (GetGroupComponentByGroupID(style, com.groupID).id);
					
					if (GetGroupComponentByGroupID(style, com.groupID) != null && !GetGroupComponentByGroupID(style, com.groupID).group.componentIDs.Contains(com.id))
						GetGroupComponentByGroupID(style, com.groupID).group.componentIDs.Add(com.id);
				}
			}
		}
		
		
		
		
		
		/// <summary>
		/// Get a list of styles assigned to the given category
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		public static List<Style> GetStylesAssignedToCategory ( StyleDataFile data, string category )
		{
			if ( data != null )
			{
				List <Style> styles = new List<Style> ();
				
				foreach ( Style s in data.styles )
				{
					if ( s.category == category )
					{
						styles.Add ( s );
					}
				}
				return styles;
			}
			else
				return new List<Style> ();
		}
		
				
		/// <summary>
		/// Adds the name to the gameobject
		/// </summary>
		/// param: data 			= The data file to use, this is used to find any old find by names that may need to be removed
		/// param: obj				= The object to add the name to
		/// param: findByName		= The find by name
		public static void AddFindByName ( StyleDataFile data, GameObject obj, string findByName )
		{
			if ( obj != null )
			{
				// Remove old name
				for ( int i = 0; i < data.styles.Count; i++ )
				{
					string oldFindByName = "(" + data.styles[i].findByName + ")";
					
					if ( obj.name.Contains ( oldFindByName ) )
					{
						string newName = obj.name.Replace ( oldFindByName, "" );
						obj.name = newName;
					}
				}
				
				if (findByName[0] != '(' && findByName[findByName.Length -1] != ')')
					findByName = "(" + findByName + ")";
				
				// Add new name
				if ( !obj.name.Contains ( findByName ) )
				{
					// Check if it needs a space before
					if (!string.IsNullOrEmpty(obj.name) && obj.name [ obj.name.Length -1 ] != ' ')
						obj.name += " ";
					
					obj.name += findByName;
				}
				
				// Check theres no space at the start
				while ( obj.name.Length > 0 && obj.name [ 0 ] == ' ' )
				{
					string value = obj.name;
					value = value.Remove ( 0, 1 );
					obj.name = value;
				}
			}
			else Debug.Log ( "No GameObject" );
		}
		
		
		/// <summary>
		/// Find all the objects assigned to a style
		/// </summary>
		public static GameObject[] GetAllObjects (ApplyMode applyMode)
		{
			if (Application.isPlaying)
				Debug.Log ("Finding Objects at runtime is not recommended, cache them with the UIStylesManager in the scene");
			
			GameObject[] obj = applyMode == ApplyMode.ActiveInScene ? UnityEngine.Object.FindObjectsOfType<UnityEngine.GameObject> () : Resources.FindObjectsOfTypeAll ( typeof( UnityEngine.GameObject ) ) as UnityEngine.GameObject[];
			return obj;
		}
		
		public static List<string> GetAllFindByNames (StyleDataFile data)
		{
			return GetAllFindByNames (new StyleDataFile[]{data});
		}
		
		public static List<string> GetAllFindByNames (StyleDataFile[] data)
		{
			List<string> list = new List<string>();
			
			foreach (StyleDataFile d in data)
			{
				foreach (Style style in d.styles)
				{
					if (!list.Contains(style.findByName))
						list.Add(style.findByName);
				}
			}
			
			return list;
		}
		
		public static List<GameObject> GetAllObjectsAssignedToStyles (StyleDataFile data, ApplyMode applyMode)
		{
			return GetAllObjectsAssignedToStyles (new StyleDataFile[]{data}, applyMode);
		}
		
		public static List<GameObject> GetAllObjectsAssignedToStyles (StyleDataFile[] data, ApplyMode applyMode)
		{
			List<GameObject> values = new List<GameObject>();
			List<string> findBys = GetAllFindByNames (data);
			GameObject[] objs = GetAllObjects (applyMode);
			
			foreach (string findBy in findBys)
			{
				foreach (GameObject obj in objs)
				{					
					if ( obj.name.Contains ( "(" + findBy + ")" ) )
					{
						if (!values.Contains(obj))
						{
							values.Add(obj);
						}
					}
				}
			}
			
			return values;
		}
		
		
		/// <summary>
		/// Apply current category
		/// </summary>
		/// param: data	= The data file to use
		/// param: obj	= The objects to apply the style to, note if null or empty, UI Styles will try find all gameObjects, which is not recommended at runtime
		/// applyMode	= The applyMode to use, AllResources or ActiveInScene
		public static void ApplyAllCategories ( StyleDataFile data, GameObject[] obj = null, ApplyMode applyMode = ApplyMode.AllResources )
		{
			if ( obj == null || obj.Length == 0 )
				obj = GetAllObjects (applyMode);
			
			for ( int i = 0; i < data.styles.Count; i++ )
				StyleHelper.Apply ( data, data.styles[i], obj );
		}
		
		/// <summary>
		/// Apply current category
		/// </summary>
		/// param: data	= The data file to use
		/// param: obj	= The objects to apply the style to, note if null or empty, UI Styles will try find all gameObjects, which is not recommended at runtime
		/// applyMode	= The applyMode to use, AllResources or ActiveInScene
		public static void ApplyCurrentCategory ( StyleDataFile data, GameObject[] obj = null, ApplyMode applyMode = ApplyMode.AllResources )
		{
			if ( obj == null || obj.Length == 0 )
				obj = GetAllObjects (applyMode);
			
			ApplyCategory ( data, data.currentCategory, obj );
		}
		
		/// <summary>
		/// Apply all styles in a category
		/// </summary>
		/// param: data 		= The data file to use
		/// param: category		= The category the styles are in
		/// param: obj			= The objects to apply the style to, if null or empty, UI Styles will try find all gameObjects, which is not recommended at runtime
		/// applyMode			= The applyMode to use, AllResources or ActiveInScene
		public static void ApplyCategory ( StyleDataFile data, string category, GameObject[] obj = null, ApplyMode applyMode = ApplyMode.AllResources )
		{
			if ( obj == null || obj.Length == 0 )
				obj = GetAllObjects (applyMode);
			
			for ( int i = 0; i < data.styles.Count; i++ )
				if ( data.styles[i].category == category )
					StyleHelper.Apply ( data, data.styles[i], obj );
		}
		
		/// <summary>
		/// Apply a Style
		/// </summary>
		/// param: data 		= The data file to use
		/// param: id			= The unique style ID
		/// param: objs			= The objects to apply the style to, if null or empty, UI Styles will try find all gameObjects, which is not recommended at runtime
		/// param: applyMode	= The applyMode to use, AllResources or ActiveInScene
		public static void ApplyStyle ( StyleDataFile data, int id, GameObject[] objs = null, ApplyMode applyMode = ApplyMode.AllResources )
		{
			if ( objs == null || objs.Length == 0 )
				objs = GetAllObjects (applyMode);
			
			StyleHelper.Apply ( data, data.styleIDs[id], objs );
		}
		
		/// <summary>
		/// Apply a Style
		/// </summary>
		/// param: data 		= The data file to use
		/// param: category		= The category the style is in
		/// param: styleName	= The name of the style to apply
		/// param: obj			= The objects to apply the style to, note if null or empty, UI Styles will try find all gameObjects, which is not recommended at runtime
		public static void ApplyStyle ( StyleDataFile data, string category, string styleName, GameObject[] objs = null, ApplyMode applyMode = ApplyMode.AllResources )
		{
			if ( objs == null || objs.Length == 0 )
				objs = GetAllObjects (applyMode);
			
			for ( int i = 0; i < data.styles.Count; i++ )
				if ( data.styles[i].category == category &&  data.styles[i].name == styleName)
					StyleHelper.Apply ( data, data.styles[i], objs );
		}		
		
		/// <summary>
		/// Apply a Style
		/// </summary>
		/// param: data = The data file to use
		/// param: id	= The unique style ID
		/// param: obj	= The objects to apply the style to
		public static void ApplyStyle ( StyleDataFile data, int id, GameObject obj )
		{
			StyleHelper.Apply ( data, data.styleIDs[id], obj );
		}
		
		/// <summary>
		/// Apply a Style
		/// </summary>
		/// param: data 		= The data file to use
		/// param: category		= The category the style is in
		/// param: styleName	= The name of the style to apply
		/// param: obj			= The objects to apply the style to
		public static void ApplyStyle ( StyleDataFile data, string category, string styleName, GameObject obj )
		{
			for ( int i = 0; i < data.styles.Count; i++ )
				if ( data.styles[i].category == category &&  data.styles[i].name == styleName)
					StyleHelper.Apply ( data, data.styles[i], obj );
		}
		
		/// <summary>
		/// Apply to all objects in the array
		/// </summary>
		/// <param name="style"></param>
		/// <param name="obj"></param>
		public static void Apply ( StyleDataFile data, Style style, GameObject[] obj = null, ApplyMode applyMode = ApplyMode.AllResources )
		{
			// Find all components
			if ( obj == null )
			{
				obj = GetAllObjects (applyMode);
			}
			
			for ( int i = 0; i < obj.Length; i++ )
			{
				Apply ( data, style, obj[i] );
			}
		}
		
		/// <summary>
		/// Apply to one object
		/// </summary>
		/// <param name="style"></param>
		/// <param name="obj"></param>
		public static void Apply ( StyleDataFile data, Style style, GameObject obj, bool forceAlwaysAdd = false )
		{
			if (style.styleState != StyleState.Disable && obj != null && obj.name.Contains ( "(" + style.findByName + ")" ))
			{
				// Add/Chaeck components here to make sure the components exist before any style tries to set its references
				AddComponents ( data, style, obj, forceAlwaysAdd );
				
				// Loop the reordered list
				foreach ( StyleComponent values in style.styleComponents )
				{
					bool alwaysDelete = values.styleComponentState == StyleComponentState.AlwaysDelete;
					bool disabled = alwaysDelete || values.styleComponentState == StyleComponentState.Disable;
					
					if ( !disabled && obj.transform.Find ( values.path ) )
					{						
						GameObject objAtPath = obj.transform.Find ( values.path ).gameObject;
						
						if ( values.styleComponentType == StyleComponentType.Text )
						{
							if ( objAtPath.GetComponent<Text> () )
								TextHelper.Apply ( values.text, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.Image )
						{
							if ( objAtPath.GetComponent<Image> () )
								ImageHelper.Apply ( values.image, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.Button )
						{
							if ( objAtPath.GetComponent<Button> () )
								ButtonHelper.Apply ( style, values.button, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.InputField )
						{
							if ( objAtPath.GetComponent<InputField> () )
								InputFieldHelper.Apply ( style, values.inputField, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.Dropdown )
						{
							if ( objAtPath.GetComponent<Dropdown> () )
								DropdownHelper.Apply ( style, values.dropdown, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.ScrollRect )
						{
							if ( objAtPath.GetComponent<ScrollRect> () )
								ScrollRectHelper.Apply ( style, values.scrollRect, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.Scrollbar )
						{
							if ( objAtPath.GetComponent<Scrollbar> () )
								ScrollbarHelper.Apply ( style, values.scrollbar, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.Slider )
						{
							if ( objAtPath.GetComponent<Slider> () )
								SliderHelper.Apply ( style, values.slider, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.Toggle )
						{
							if ( objAtPath.GetComponent<Toggle> () )
								ToggleHelper.Apply ( style, values.toggle, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.RectTransform )
						{
							if ( objAtPath.GetComponent<RectTransform> () )
								RectTransformHelper.Apply ( values.rectTransform, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.Custom )
						{
							if ( objAtPath.GetComponent (values.custom.customComponent.GetType().Name))
								CustomComponentHelper.Apply ( data, values.custom, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.Camera )
						{
							if ( objAtPath.GetComponent<Camera> () )
								CameraHelper.Apply ( values.camera, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.CanvasGroup )
						{
							if ( objAtPath.GetComponent<CanvasGroup> () )
								CanvasGroupHelper.Apply ( values.canvasGroup, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.LayoutElement )
						{
							if ( objAtPath.GetComponent<LayoutElement> () )
								LayoutElementHelper.Apply ( values.layoutElement, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.ContentSizeFitter )
						{
							if ( objAtPath.GetComponent<ContentSizeFitter> () )
								ContentSizeFitterHelper.Apply ( values.contentSizeFitter, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.AspectRatioFitter )
						{
							if ( objAtPath.GetComponent<AspectRatioFitter> () )
								AspectRatioFitterHelper.Apply ( values.aspectRatioFitter, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.HorizontalLayoutGroup )
						{
							if ( objAtPath.GetComponent<HorizontalLayoutGroup> () )
								HorizontalLayoutGroupHelper.Apply ( values.horizontalLayoutGroup, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.VerticalLayoutGroup )
						{
							if ( objAtPath.GetComponent<VerticalLayoutGroup> () )
								VerticalLayoutGroupHelper.Apply ( values.verticalLayoutGroup, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.GridLayoutGroup )
						{
							if ( objAtPath.GetComponent<GridLayoutGroup> () )
								GridLayoutGroupHelper.Apply ( values.gridLayoutGroup, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.Canvas )
						{
							if ( objAtPath.GetComponent<Canvas> () )
								CanvasHelper.Apply ( values.canvas, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.CanvasScaler )
						{
							if ( objAtPath.GetComponent<CanvasScaler> () )
								CanvasScalerHelper.Apply ( values.canvasScaler, objAtPath );
						}
						
						else if ( values.styleComponentType == StyleComponentType.GraphicRaycaster )
						{
							if ( objAtPath.GetComponent<GraphicRaycaster> () )
								GraphicRaycasterHelper.Apply ( values.graphicRaycaster, objAtPath );
						}
						
						/*<UIStylesTag(Appy)>*/
						
												
						#if UNITY_EDITOR
						if ( !Application.isPlaying )
							EditorUtility.SetDirty ( objAtPath );
						#endif
					}
				}
			}
		}
		
		
		
		/// <summary>
		/// Apply to one object
		/// </summary>
		/// <param name="style"></param>
		/// <param name="obj"></param>
		public static void AddComponents ( StyleDataFile data, Style style, GameObject obj, bool forceAlwaysAdd = false )
		{
			if (style.styleState != StyleState.Disable && obj != null && obj.name.Contains ( "(" + style.findByName + ")" ))
			{
				// Loop the reordered list
				foreach ( StyleComponent values in style.styleComponents )
				{
					bool alwaysAdd = forceAlwaysAdd || values.styleComponentState == StyleComponentState.AlwaysAdd;
					bool alwaysRemove = values.styleComponentState == StyleComponentState.AlwaysRemove;
					bool alwaysDelete = values.styleComponentState == StyleComponentState.AlwaysDelete;
					
					if ( alwaysAdd || obj.transform.Find ( values.path ) )
					{
						// Check the path
						CreatObjectChildDirectory (obj, values.path);
						
						GameObject objAtPath = obj.transform.Find ( values.path ).gameObject;
						
						if ( values.styleComponentType == StyleComponentType.Text )
						{
							if (alwaysAdd && !objAtPath.GetComponent<Text> ())
							{
								objAtPath.AddComponent<Text> ();
								objAtPath.GetComponent<Text> ().text = "New Text";
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<Text> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<Text> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.Image )
						{
							if (alwaysAdd && !objAtPath.GetComponent<Image> ())
							{
								objAtPath.AddComponent<Image> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<Image> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<Image> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.Button )
						{
							if (alwaysAdd && !objAtPath.GetComponent<Button> ())
							{
								objAtPath.AddComponent<Button> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<Button> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<Button> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.InputField )
						{
							if (alwaysAdd && !objAtPath.GetComponent<InputField> ())
							{
								objAtPath.AddComponent<InputField> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<InputField> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<InputField> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.Dropdown )
						{
							if (alwaysAdd && !objAtPath.GetComponent<Dropdown> ())
							{
								objAtPath.AddComponent<Dropdown> ();
								
								List<Dropdown.OptionData> dataOptionData = new List<Dropdown.OptionData>();
								
								for (int i = 1; i <= 3; i++) 
								{
									Dropdown.OptionData optionData = new Dropdown.OptionData();
									optionData.text = "Option " + i;
									dataOptionData.Add(optionData);
								}
								
								objAtPath.GetComponent<Dropdown> ().options = dataOptionData;
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<Dropdown> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<Dropdown> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.ScrollRect )
						{
							if (alwaysAdd && !objAtPath.GetComponent<ScrollRect> ())
							{
								objAtPath.AddComponent<ScrollRect> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<ScrollRect> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<ScrollRect> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.Scrollbar )
						{
							if (alwaysAdd && !objAtPath.GetComponent<Scrollbar> ())
							{
								objAtPath.AddComponent<Scrollbar> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<Scrollbar> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<Scrollbar> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.Slider )
						{
							if (alwaysAdd && !objAtPath.GetComponent<Slider> ())
							{
								objAtPath.AddComponent<Slider> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<Slider> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<Slider> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.Toggle )
						{
							if (alwaysAdd && !objAtPath.GetComponent<Toggle> ())
							{
								objAtPath.AddComponent<Toggle> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<Toggle> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<Toggle> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.RectTransform )
						{
							if (alwaysAdd && !objAtPath.GetComponent<RectTransform> ())
							{
								objAtPath.AddComponent<RectTransform> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<RectTransform> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<RectTransform> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.Custom )
						{
							string typeName = values.custom.customComponent.GetType().ToString();
							if (alwaysAdd && !objAtPath.GetComponent (typeName))
							{
								objAtPath.AddComponent(Type.GetType(typeName));
							}
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent (typeName), true );
								#else
								GameObject.Destroy ( obj.GetComponent (typeName) );
								#endif
							}
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.Camera )
						{
							if (alwaysAdd && !objAtPath.GetComponent<Camera> ())
							{
								objAtPath.AddComponent<Camera> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<Camera> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<Camera> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.CanvasGroup )
						{
							if (alwaysAdd && !objAtPath.GetComponent<CanvasGroup> ())
							{
								objAtPath.AddComponent<CanvasGroup> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<CanvasGroup> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<CanvasGroup> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.LayoutElement )
						{
							if (alwaysAdd && !objAtPath.GetComponent<LayoutElement> ())
							{
								objAtPath.AddComponent<LayoutElement> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<LayoutElement> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<LayoutElement> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.ContentSizeFitter )
						{
							if (alwaysAdd && !objAtPath.GetComponent<ContentSizeFitter> ())
							{
								objAtPath.AddComponent<ContentSizeFitter> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<ContentSizeFitter> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<ContentSizeFitter> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.AspectRatioFitter )
						{
							if (alwaysAdd && !objAtPath.GetComponent<AspectRatioFitter> ())
							{
								objAtPath.AddComponent<AspectRatioFitter> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<AspectRatioFitter> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<AspectRatioFitter> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.HorizontalLayoutGroup )
						{
							if (alwaysAdd && !objAtPath.GetComponent<HorizontalLayoutGroup> ())
							{
								objAtPath.AddComponent<HorizontalLayoutGroup> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<HorizontalLayoutGroup> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<HorizontalLayoutGroup> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.VerticalLayoutGroup )
						{
							if (alwaysAdd && !objAtPath.GetComponent<VerticalLayoutGroup> ())
							{
								objAtPath.AddComponent<VerticalLayoutGroup> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<VerticalLayoutGroup> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<VerticalLayoutGroup> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.GridLayoutGroup )
						{
							if (alwaysAdd && !objAtPath.GetComponent<GridLayoutGroup> ())
							{
								objAtPath.AddComponent<GridLayoutGroup> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<GridLayoutGroup> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<GridLayoutGroup> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.Canvas )
						{
							if (alwaysAdd && !objAtPath.GetComponent<Canvas> ())
							{
								objAtPath.AddComponent<Canvas> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<Canvas> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<Canvas> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.CanvasScaler )
						{
							if (alwaysAdd && !objAtPath.GetComponent<CanvasScaler> ())
							{
								objAtPath.AddComponent<CanvasScaler> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<CanvasScaler> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<CanvasScaler> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						else if ( values.styleComponentType == StyleComponentType.GraphicRaycaster )
						{
							if (alwaysAdd && !objAtPath.GetComponent<GraphicRaycaster> ())
							{
								objAtPath.AddComponent<GraphicRaycaster> ();
							}
							
							else if (alwaysRemove)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath.GetComponent<GraphicRaycaster> (), true );
								#else
								GameObject.Destroy ( obj.GetComponent<GraphicRaycaster> () );
								#endif
							}
							
							else if (alwaysDelete)
							{
								#if UNITY_EDITOR
								UnityEditor.Editor.DestroyImmediate ( objAtPath, true );
								#else
								GameObject.Destroy ( obj );
								#endif
							}
						}
						
						/*<UIStylesTag(AddComponents)>*/
					}
				}
			}
		}
		
		
		
		
		public static void CopyObjectToStyle (StyleDataFile data, GameObject objRoot)
		{						
			Transform[] objs = objRoot.GetComponentsInChildren<Transform>() as Transform[];
			
			List <string> folders = new List<string>(); 
			
			foreach (Transform trans in objs)
			{
				GameObject obj = trans.gameObject;
				
				string path = "/" + obj.name;
				while (obj.transform.parent != null && obj != objRoot)
				{
					obj = obj.transform.parent.gameObject;
					path = "/" + obj.name + path;
				}
				
				
				if (path[0] == '/')
				{
					string newString = path.Remove(0, obj.name.Length +1);
					path = newString;
				}
				
				if (path.Length > 0)
				{
					if (path[0] == '/')
					{
						string newString = path.Remove(0, 1);
						path = newString;
					}
				}
				folders.Add(path);
			}
						
			Style style = new Style(data, data.currentCategory, objRoot.name, objRoot.name);
			style.rename = !Application.isPlaying;
			
			foreach (string path in folders)
			{
				//Debug.Log (path);
				
				if (objRoot.transform.Find(path))
				{
					GameObject obj = objRoot.transform.Find(path).gameObject;
					
					Component[] components = obj.GetComponents<Component> ();
					foreach ( Component c in components )
					{
						if (c is Button)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.Button, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false);
							
							if (obj != null)
								v.button = ButtonHelper.SetValuesFromComponent ( obj );
						}
						else if (c is  InputField )
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.InputField, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.inputField = InputFieldHelper.SetValuesFromComponent ( obj );
							
							InputField input = (InputField)c;
							
							v.inputField.targetGraphicReference = input.placeholder.gameObject.name;
							
							v.inputField.placeholderReference = input.placeholder.gameObject.name;
							
							input = (InputField)c;
							v.inputField.textReference = input.textComponent.gameObject.name;
						}
						else if (c is Dropdown)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.Dropdown, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.dropdown = DropdownHelper.SetValuesFromComponent ( obj );						
						}
						else if (c is ScrollRect)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.ScrollRect, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.scrollRect = ScrollRectHelper.SetValuesFromComponent ( obj );
						}
						else if (c is Scrollbar)
						{						
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.Scrollbar, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.scrollbar = ScrollbarHelper.SetValuesFromComponent ( obj );
						}
						else if (c is Slider)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.Slider, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.slider = SliderHelper.SetValuesFromComponent ( obj );
						}
						else if (c is Toggle)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.Toggle, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.toggle = ToggleHelper.SetValuesFromComponent ( obj );
						}
						else if (c is Image)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.Image, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							v.image.componentType = ImageValues.ComponentType.Image;
							
							if (obj != null)
								v.image = ImageHelper.SetValuesFromComponent ( obj, true );
						}
						else if (c is Text)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.Text, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.text = TextHelper.SetValuesFromComponent ( obj, true );
						}
						else if (c is RectTransform)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.RectTransform, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.rectTransform = RectTransformHelper.SetValuesFromComponent ( obj );
						}
						
						else if (c is Camera)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.Camera, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.camera = CameraHelper.SetValuesFromComponent ( obj );
						}
						
						else if (c is CanvasGroup)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.CanvasGroup, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.canvasGroup = CanvasGroupHelper.SetValuesFromComponent ( obj );
						}
						
						else if (c is LayoutElement)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.LayoutElement, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.layoutElement = LayoutElementHelper.SetValuesFromComponent ( obj );
						}
						
						else if (c is ContentSizeFitter)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.ContentSizeFitter, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.contentSizeFitter = ContentSizeFitterHelper.SetValuesFromComponent ( obj );
						}
						
						else if (c is AspectRatioFitter)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.AspectRatioFitter, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.aspectRatioFitter = AspectRatioFitterHelper.SetValuesFromComponent ( obj );
						}
						
						else if (c is HorizontalLayoutGroup)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.HorizontalLayoutGroup, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.horizontalLayoutGroup = HorizontalLayoutGroupHelper.SetValuesFromComponent ( obj );
						}
						
						else if (c is VerticalLayoutGroup)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.VerticalLayoutGroup, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.verticalLayoutGroup = VerticalLayoutGroupHelper.SetValuesFromComponent ( obj );
						}
						
						else if (c is GridLayoutGroup)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.GridLayoutGroup, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.gridLayoutGroup = GridLayoutGroupHelper.SetValuesFromComponent ( obj );
						}
						
						else if (c is Canvas)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.Canvas, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.canvas = CanvasHelper.SetValuesFromComponent ( obj );
						}
						
						else if (c is CanvasScaler)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.CanvasScaler, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.canvasScaler = CanvasScalerHelper.SetValuesFromComponent ( obj );
						}
						
						else if (c is GraphicRaycaster)
						{
							StyleComponent v = new StyleComponent(data, style, StyleComponentType.GraphicRaycaster, obj.name, "");
							v.path = GetPath (obj, objRoot.name, false );
							
							if (obj != null)
								v.graphicRaycaster = GraphicRaycasterHelper.SetValuesFromComponent ( obj );
						}
						
						/*<UIStylesTag(CopyObjectToStyle)>*/
					}
				}
			}
		}
	}
}



















 




















































