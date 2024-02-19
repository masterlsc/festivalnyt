using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using System;
using System.Linq;

namespace UIStyles
{
    public class WindowPreferences : Editor
    {
	    public static string version = "4.1.8";
        public static string documentationLink = "http://messyentertainment.com/ui-styles/";
        public static string contactLink = "http://uistyles.training/support/";

	    private static Vector2 scrollPosition = Vector2.zero;
	    
	    private static Type windowType;
	    private static Type tabType;
	    private static FieldInfo fieldtabs;
	    private static FieldInfo selectedTab;
	    private static FieldInfo fieldContent;
	    private static MethodInfo methodWindow;
	    
	    // Dropdowns
	    public static bool excludedPropertiesDropdown;
	    public static bool excludedFieldsDropdown;
	    public static bool excludedTypesDropdown;
	    
	    public static bool canvasDropdown;
	    public static bool canvasScalerDropdown;
	    public static bool graphicRaycasterDropdown;
	    
	    private static string newExcludedProperty;
	    private static string newExcludedField;
	    private static string newExcludedType;
	    	    
        /// <summary>
        /// Checks the path.
        /// </summary>
        private static string CheckPath(string path, string first)
        {
            if (path.Length < 1)
            {
                path = first;
            }

            if (path[path.Length - 1] == '/')
            {
                string value = path;
                value = value.Substring(0, value.Length - 1);
                path = value;
            }

            if (path.Length > 0 && path[0] != '/')
            {
                string value = "/" + path;
                path = value;
            }

            string[] s = path.Split('/');

            if (s.Length > 1 && s[1] == "Assets")
            {
                string value = "";
                for (int i = 0; i < s.Length; i++)
                {
                    if (i > 1)
                        value += s[i] + "/";
                }
                path = value;
            }

            return path;
        }
       

        [MenuItem("Window/UI Styles/Window/Preferences", false, 3)]
        public static void OpenPreferences()
        {
            if (windowType == null && tabType == null)
            {
                tabType = typeof(Editor).Assembly.GetType("UnityEditor.PreferencesWindow+Section");
                windowType = typeof(Editor).Assembly.GetType("UnityEditor.PreferencesWindow");

                if (tabType != null && windowType != null)
                {
                    fieldContent = tabType.GetField("content");
                    fieldtabs = windowType.GetField("m_Sections", BindingFlags.Instance | BindingFlags.NonPublic);
                    selectedTab = windowType.GetField("m_SelectedSectionIndex", BindingFlags.Instance | BindingFlags.NonPublic);

                    if (fieldtabs != null && selectedTab != null && fieldContent != null)
                    {
                        methodWindow = windowType.GetMethod("ShowPreferencesWindow", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                    }
                }
            }

            if (methodWindow != null)
            {
                methodWindow.Invoke(null, null);
                EditorApplication.update += GoToTab;
            }
        }

        private static void GoToTab()
        {
            EditorWindow w = EditorWindow.focusedWindow;
            if (w.GetType() != windowType || !w)
                return;
            EditorApplication.update -= GoToTab;
            IList tabs = (IList)fieldtabs.GetValue(w);

            if (tabs == null)
                return;

            for (var i = tabs.Count; i-- > 0;)
            {
                var tabObject = tabs[i];
                GUIContent content = (GUIContent)fieldContent.GetValue(tabObject);
                if (content != null && content.text == "UI Styles")
                {
                    selectedTab.SetValue(w, (object)i);
                    w.Repaint();
                    return;
                }
            }
        }
	    
	    
	    private static void ExcludedPropertyContext (string value)
	    {
		    GUI.FocusControl ( null );
		    
		    GenericMenu menu = new GenericMenu ();
		    
		    menu.AddItem ( new GUIContent ( "Remove" ), false, ExcludedPropertyContextRemove, value );
		    
		    menu.ShowAsContext ();
	    }
	    
	    private static void ExcludedPropertyContextRemove (object obj)
	    {
		    if (UIStylesDatabase.styleData.preferenceData.excludedProperties.Contains((string)obj))
			    UIStylesDatabase.styleData.preferenceData.excludedProperties.Remove((string)obj);
		    
	    }
	    
	    private static void ExcludedFieldContext (string value)
	    {
		    GUI.FocusControl ( null );
		    
		    GenericMenu menu = new GenericMenu ();
		    
		    menu.AddItem ( new GUIContent ( "Remove" ), false, ExcludedFieldContextRemove, value );
		    
		    menu.ShowAsContext ();
	    }
	    
	    private static void ExcludedFieldContextRemove (object obj)
	    {
		    if (UIStylesDatabase.styleData.preferenceData.excludedFields.Contains((string)obj))
			    UIStylesDatabase.styleData.preferenceData.excludedFields.Remove((string)obj);
		    
	    }
	    
	    private static void ExcludedTypeContext (string value)
	    {
		    GUI.FocusControl ( null );
		    
		    GenericMenu menu = new GenericMenu ();
		    
		    menu.AddItem ( new GUIContent ( "Remove" ), false, ExcludedTypeContextRemove, value );
		    
		    menu.ShowAsContext ();
	    }
	    
	    private static void ExcludedTypeContextRemove (object obj)
	    {
		    if (UIStylesDatabase.styleData.preferenceData.excludedTypes.Contains((string)obj))
			    UIStylesDatabase.styleData.preferenceData.excludedTypes.Remove((string)obj);
		    
	    }
	    
	    
	    

        [PreferenceItem("UI Styles")]
        private static void PreferencesGUI()
	    {		  
            GUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Version: " + version);

                if (GUILayout.Button("Support", GUILayout.Width(100)))
                {
                    Application.OpenURL(contactLink);
                }
            }
            GUILayout.EndHorizontal();

            EditorHelper.StandardSeparator();

            if (UIStylesDatabase.styleData == null)
            {
                EditorGUILayout.HelpBox("To start using UI Styles you must create some settings", MessageType.Info);

                GUILayout.Space(10);

                if (GUILayout.Button("Create Settings", GUILayout.Width(100)))
                {
                    WindowStyles.Create("");
                }
            }



            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);
            {
                if (UIStylesDatabase.styleData != null)
                {
                    GUILayout.Space(10);

                    EditorGUILayout.LabelField("Find Scenes");
	                UIStylesDatabase.styleData.preferenceData.findSceneMode = (PreferenceValues.FindSceneMode)EditorGUILayout.EnumPopup(UIStylesDatabase.styleData.preferenceData.findSceneMode);

                    if (UIStylesDatabase.styleData.preferenceData.findSceneMode == PreferenceValues.FindSceneMode.Folder)
                    {
                        UIStylesDatabase.styleData.preferenceData.sceneFolder = EditorGUILayout.TextField("", UIStylesDatabase.styleData.preferenceData.sceneFolder);
                        UIStylesDatabase.styleData.preferenceData.sceneFolder = CheckPath(UIStylesDatabase.styleData.preferenceData.sceneFolder, "Scenes");
                    }

                    string message =
                        // Scenes In Build
                        UIStylesDatabase.styleData.preferenceData.findSceneMode == UIStyles.PreferenceValues.FindSceneMode.ScenesInBuild ?
                        "Scenes in Build \nFind scenes in the build settings" :

                        UIStylesDatabase.styleData.preferenceData.findSceneMode == UIStyles.PreferenceValues.FindSceneMode.Project ?
                        "Project \nFind scenes in the whole project" :

                        "Folder \nFind scenes in a defined folder";

                    EditorGUILayout.HelpBox(message, MessageType.Info);

                    GUILayout.Space(10);
                    EditorHelper.StandardSeparator();
                    GUILayout.Space(10);

                    EditorGUILayout.LabelField("Find By Name Warnings");

                    GUILayout.BeginHorizontal();
                    {
                        UIStylesDatabase.styleData.preferenceData.disableFindByNameWarning = EditorGUILayout.Toggle(UIStylesDatabase.styleData.preferenceData.disableFindByNameWarning, GUILayout.Width(18));

                        EditorGUILayout.LabelField("Disable find by name warnings");
                    }
                    GUILayout.EndHorizontal();

                    if (!UIStylesDatabase.styleData.preferenceData.disableFindByNameWarning)
                    {
                        GUILayout.BeginHorizontal();
                        {
                            UIStylesDatabase.styleData.preferenceData.allowMultipleFindByNameInSeparateCategories = EditorGUILayout.Toggle(UIStylesDatabase.styleData.preferenceData.allowMultipleFindByNameInSeparateCategories, GUILayout.Width(18));

                            EditorGUILayout.LabelField("Allow separate categories to contain the same find by names");
                        }
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.Space(10);
                    EditorHelper.StandardSeparator();
                    GUILayout.Space(10);

                    EditorGUILayout.LabelField("Dropdowns");

                    GUILayout.BeginHorizontal();
                    {
                        UIStylesDatabase.styleData.preferenceData.oneStyleOpenAtOnce = EditorGUILayout.Toggle(UIStylesDatabase.styleData.preferenceData.oneStyleOpenAtOnce, GUILayout.Width(18));

                        EditorGUILayout.LabelField("One Style Open At Once");
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        UIStylesDatabase.styleData.preferenceData.oneComponentOpenAtOnce = EditorGUILayout.Toggle(UIStylesDatabase.styleData.preferenceData.oneComponentOpenAtOnce, GUILayout.Width(18));

                        EditorGUILayout.LabelField("One Component Open At Once");
                    }
                    GUILayout.EndHorizontal();

	                EditorGUILayout.HelpBox("Close dropdowns when others open", MessageType.Info);
	                
	                GUILayout.Space(10);
	                EditorHelper.StandardSeparator();
	                GUILayout.Space(10);
	                
	                EditorGUILayout.LabelField("Custom Components");
	                
	                EditorGUI.indentLevel = 0;
	                GUILayout.BeginVertical(EditorHelper.StandardPanel(6));
	                {
	                	EditorHelper.FoldOut("Default Excluded Properties", "", ref excludedPropertiesDropdown);
	                	
	                	if (excludedPropertiesDropdown)
	                	{
	                		GUILayout.Space(10);
	                		
		                	EditorGUILayout.LabelField("Add New Value");
		                	GUILayout.BeginHorizontal();
		                	{
			                	newExcludedProperty = EditorGUILayout.TextField(newExcludedProperty);
			                	
			                	if (GUILayout.Button("ADD", EditorHelper.buttonSkin, GUILayout.Width(60)))
			                	{
			                		if (!StringFormats.IsNullOrWhiteSpace(newExcludedProperty) && !newExcludedProperty.Contains(' '))
			                		{
			                			UIStylesDatabase.styleData.preferenceData.excludedProperties.Add(newExcludedProperty);
				                		newExcludedProperty = string.Empty;
				                		GUI.FocusControl ( null );
				                		
				                		UIStylesDatabase.styleData.preferenceData.excludedProperties.Sort();
			                		}
			                		else EditorUtility.DisplayDialog("Invalid Format", "This format is incorrect", "OK");
			                	}
		                	}
		                	GUILayout.EndHorizontal();
		                	
		                	
	                		GUILayout.Space(10);
	                		EditorGUI.indentLevel = 1;
		                	GUILayout.BeginHorizontal();
		                	{						                	
			                    EditorGUILayout.LabelField("#", GUILayout.Width(50));
						                	
			                	EditorGUILayout.LabelField("Property Name", GUILayout.MinWidth(10));
		                	}
		                	GUILayout.EndHorizontal();
		                	
		                	EditorHelper.StandardSeparator(0, "", "");
		                	
		                	for (int i = 0; i < UIStylesDatabase.styleData.preferenceData.excludedProperties.Count; i++) 
		                	{
		                		GUILayout.BeginHorizontal();
			                	{	
			                		EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(50));
			                		
			                		string property = UIStylesDatabase.styleData.preferenceData.excludedProperties[i];
			                		
			                		EditorGUILayout.LabelField(property);
		                			
		                			Rect lastRect = GUILayoutUtility.GetLastRect();
		                			Event currentEvent = Event.current;
				                	
			                		if (lastRect.Contains(currentEvent.mousePosition))
			                		{
				                		//EditorHelper.HighlightRect(lastRect);
				                		
				                		if (Event.current.button == 1 && Event.current.type == EventType.MouseUp)
				                		{
				                			ExcludedPropertyContext (property);
				                		}
			                		}
			                	}
			                	GUILayout.EndHorizontal();
		                	}
		                	GUILayout.Space(10);
	                	}
	                }
	                GUILayout.EndVertical();
	                
	                EditorGUI.indentLevel = 0;
	                GUILayout.BeginVertical(EditorHelper.StandardPanel(6));
	                {
	                	EditorHelper.FoldOut("Default Excluded Fields", "", ref excludedFieldsDropdown);
	                	
	                	if (excludedFieldsDropdown)
	                	{
		                	GUILayout.Space(10);
	                		
		                	EditorGUILayout.LabelField("Add New Value");
		                	GUILayout.BeginHorizontal();
		                	{
			                	newExcludedField = EditorGUILayout.TextField(newExcludedField);
			                	
			                	if (GUILayout.Button("ADD", EditorHelper.buttonSkin, GUILayout.Width(60)))
			                	{
			                		if (!StringFormats.IsNullOrWhiteSpace(newExcludedField) && !newExcludedField.Contains(' '))
			                		{
			                			UIStylesDatabase.styleData.preferenceData.excludedFields.Add(newExcludedField);
				                		newExcludedField = string.Empty;
				                		GUI.FocusControl ( null );
				                		
				                		UIStylesDatabase.styleData.preferenceData.excludedFields.Sort();
			                		}
			                		else EditorUtility.DisplayDialog("Invalid Format", "This format is incorrect", "OK");
			                	}
		                	}
		                	GUILayout.EndHorizontal();
		                		                		
		                	GUILayout.Space(10);
	                		EditorGUI.indentLevel = 1;
		                	GUILayout.BeginHorizontal();
		                	{						                	
			                	EditorGUILayout.LabelField("#", GUILayout.Width(50));
			                	
			                	EditorGUILayout.LabelField("Field Name", GUILayout.MinWidth(10));
		                	}
		                	GUILayout.EndHorizontal();
		                	
		                	EditorHelper.StandardSeparator(0, "", "");
		                	
		                	for (int i = 0; i < UIStylesDatabase.styleData.preferenceData.excludedFields.Count; i++) 
		                	{
		                		GUILayout.BeginHorizontal();
			                	{
			                		EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(50));
			                		
			                		string field = UIStylesDatabase.styleData.preferenceData.excludedFields[i];
				                		
			                		EditorGUILayout.LabelField(field);
		                			
		                			Rect lastRect = GUILayoutUtility.GetLastRect();
		                			Event currentEvent = Event.current;
			                		
			                		if (lastRect.Contains(currentEvent.mousePosition))
			                		{
				                		//EditorHelper.HighlightRect(lastRect);
				                		
				                		if (Event.current.button == 1 && Event.current.type == EventType.MouseUp)
				                		{
				                			ExcludedFieldContext (field);
				                		}
			                		}
			                	}
			                	GUILayout.EndHorizontal();
		                	}
		                	GUILayout.Space(10);
	                	}
	                }
	                GUILayout.EndVertical();
	                
	                EditorGUI.indentLevel = 0;
	                GUILayout.BeginVertical(EditorHelper.StandardPanel(6));
	                {
	                	EditorHelper.FoldOut("Default Excluded Types", "", ref excludedTypesDropdown);
	                	
	                	if (excludedTypesDropdown)
	                	{
	                		GUILayout.Space(10);
	                		
		                	EditorGUILayout.LabelField("Add New Value");
		                	GUILayout.BeginHorizontal();
		                	{
			                	newExcludedType = EditorGUILayout.TextField(newExcludedType);
			                	
			                	if (GUILayout.Button("ADD", EditorHelper.buttonSkin, GUILayout.Width(60)))
			                	{
			                		if (!StringFormats.IsNullOrWhiteSpace(newExcludedType) && !newExcludedType.Contains(' '))
			                		{
			                			UIStylesDatabase.styleData.preferenceData.excludedTypes.Add(newExcludedType);
				                		newExcludedType = string.Empty;
				                		GUI.FocusControl ( null );
				                		
				                		UIStylesDatabase.styleData.preferenceData.excludedTypes.Sort();
			                		}
			                		else EditorUtility.DisplayDialog("Invalid Format", "This format is incorrect", "OK");
			                	}
		                	}
		                	GUILayout.EndHorizontal();
		                	
		                	GUILayout.Space(10);
	                		EditorGUI.indentLevel = 1;
		                	GUILayout.BeginHorizontal();
		                	{						                	
			                	EditorGUILayout.LabelField("#", GUILayout.Width(50));
			                	
			                	EditorGUILayout.LabelField("Type Name", GUILayout.MinWidth(10));
		                	}
		                	GUILayout.EndHorizontal();
		                	
		                	EditorHelper.StandardSeparator(0, "", "");
		                	
		                	for (int i = 0; i < UIStylesDatabase.styleData.preferenceData.excludedTypes.Count; i++) 
		                	{
		                		GUILayout.BeginHorizontal();
			                	{
			                		EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(50));
			                		
			                		string type = UIStylesDatabase.styleData.preferenceData.excludedTypes[i];
			                		
			                		EditorGUILayout.LabelField(type);
		                			
		                			Rect lastRect = GUILayoutUtility.GetLastRect();
		                			Event currentEvent = Event.current;
			                		
			                		if (lastRect.Contains(currentEvent.mousePosition))
			                		{
				                		//EditorHelper.HighlightRect(lastRect);
				                		
				                		if (Event.current.button == 1 && Event.current.type == EventType.MouseUp)
				                		{
				                			ExcludedTypeContext (type);
				                		}
			                		}
			                	}
			                	GUILayout.EndHorizontal();
		                	}
		                	GUILayout.Space(10);
	                	}
	                }
	                GUILayout.EndVertical();
	                
	                GUILayout.Space(10);
	                EditorHelper.StandardSeparator();
	                GUILayout.Space(10);
	                
	                EditorGUILayout.LabelField("Default Canvas Settings");
	                
	                EditorGUI.indentLevel = 0;
	                GUILayout.BeginVertical(EditorHelper.StandardPanel(6));
	                {
	                	EditorHelper.FoldOut("Canvas", "", ref canvasDropdown);
	                	
	                	if (canvasDropdown)
	                	{
	                		EditorGUI.indentLevel = 1;
	                		GUILayout.Space(10);
	                		
	                		//Render Mode
		                	EditorGUILayout.LabelField("Render Mode");
		                	UIStylesDatabase.styleData.preferenceData.defaultCanvasRenderMode = (RenderMode) EditorGUILayout.EnumPopup(UIStylesDatabase.styleData.preferenceData.defaultCanvasRenderMode);
		                	
		                	GUILayout.Space(10);
		                	
		                	if (UIStylesDatabase.styleData.preferenceData.defaultCanvasRenderMode == RenderMode.ScreenSpaceOverlay || UIStylesDatabase.styleData.preferenceData.defaultCanvasRenderMode == RenderMode.ScreenSpaceCamera)
		                	{
		                		// PixelPerfect
			                	GUILayout.BeginHorizontal();
			                	{
				                	UIStylesDatabase.styleData.preferenceData.defaultCanvasPixelPerfect = EditorGUILayout.Toggle(UIStylesDatabase.styleData.preferenceData.defaultCanvasPixelPerfect, GUILayout.Width(18));
				                	EditorGUILayout.LabelField("PixelPerfect");
			                	}
			                	GUILayout.EndHorizontal();
		                	}
		                	
		                	if (UIStylesDatabase.styleData.preferenceData.defaultCanvasRenderMode == RenderMode.ScreenSpaceOverlay)
		                	{
		                		GUILayout.Space(10);
			                	EditorGUILayout.LabelField("Sort Order");
			                	UIStylesDatabase.styleData.preferenceData.defaultCanvasSortOrder = EditorGUILayout.IntField(UIStylesDatabase.styleData.preferenceData.defaultCanvasSortOrder);
			                	
			                	GUILayout.Space(10);
			                	EditorGUILayout.LabelField("Target Display");
			                	
			                	string[] displays = new string[]{"Display 1", "Display 2", "Display 3", "Display 4", "Display 5", "Display 6", "Display 7", "Display 8"};
			                	int[] values = new int[]{0,1,2,3,4,5,6,7};
			                	
			                	UIStylesDatabase.styleData.preferenceData.defaultCanvasTargetDisplay = EditorGUILayout.IntPopup(UIStylesDatabase.styleData.preferenceData.defaultCanvasTargetDisplay, displays, values);
		                	}
		                	
		                	if (UIStylesDatabase.styleData.preferenceData.defaultCanvasRenderMode == RenderMode.ScreenSpaceCamera || UIStylesDatabase.styleData.preferenceData.defaultCanvasRenderMode == RenderMode.WorldSpace)
		                	{
		                		GUILayout.Space(10);
			                	EditorGUILayout.LabelField("Order In Layer");
			                	UIStylesDatabase.styleData.preferenceData.defaultCanvasSortOrder = EditorGUILayout.IntField(UIStylesDatabase.styleData.preferenceData.defaultCanvasSortOrder);
		                	}
		                	
		                	GUILayout.Space(10);
	                	}
	                }
	                GUILayout.EndVertical();
	                
	                EditorGUI.indentLevel = 0;
	                GUILayout.BeginVertical(EditorHelper.StandardPanel(6));
	                {
	                	EditorHelper.FoldOut("Canvas Scaler", "", ref canvasScalerDropdown);
		                
		                if (canvasScalerDropdown)
		                {
			                EditorGUI.indentLevel = 1;
			                
			                if (UIStylesDatabase.styleData.preferenceData.defaultCanvasRenderMode == RenderMode.WorldSpace)
		                	{
		                		// Render Mode
				                GUILayout.Space(10);
				                EditorGUILayout.LabelField("UI Scale Mode");
				                GUILayout.BeginHorizontal();
				                {
				                	GUILayout.Space(10);
				                	EditorGUI.BeginDisabledGroup (true);
					                {
						                GUILayout.Button("World", EditorHelper.dropdownSkinNormal);
					                }
					                EditorGUI.EndDisabledGroup ();
				                }
			                	GUILayout.EndHorizontal();
		                	}
		                	else
		                	{
		                		// Render Mode
			                	GUILayout.Space(10);
			                	EditorGUILayout.LabelField("UI Scale Mode");
			                	UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerScaleMode = (UnityEngine.UI.CanvasScaler.ScaleMode) EditorGUILayout.EnumPopup(UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerScaleMode);
		                	}
		                	
			                if (UIStylesDatabase.styleData.preferenceData.defaultCanvasRenderMode != RenderMode.WorldSpace && UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerScaleMode == UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize)
		                	{
		                		// Scale Factor
				                GUILayout.Space(10);
				                EditorGUILayout.LabelField("Scale Factor");
				                UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerScaleFactor = EditorGUILayout.FloatField(UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerScaleFactor);
		                	}

			                if (UIStylesDatabase.styleData.preferenceData.defaultCanvasRenderMode == RenderMode.WorldSpace)
		                	{
		                		// Dynamic Pixels Per Unit
				                GUILayout.Space(10);
				                EditorGUILayout.LabelField("Dynamic Pixels Per Unit");
				                UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerDynamicPixelsPerUnit = EditorGUILayout.FloatField(UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerDynamicPixelsPerUnit);				                
		                	}
		                	
		                	
		                	if (UIStylesDatabase.styleData.preferenceData.defaultCanvasRenderMode != RenderMode.WorldSpace && UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerScaleMode == UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize)
		                	{
		                		GUILayout.Space(10);
				                EditorGUILayout.LabelField("Reference Resolution");
			                	UIStylesDatabase.styleData.preferenceData.defaultCanvasResolution = EditorGUILayout.Vector2Field("", UIStylesDatabase.styleData.preferenceData.defaultCanvasResolution);
			                	
			                	GUILayout.Space(10);
				                EditorGUILayout.LabelField("Screen Match Mode");
				                UIStylesDatabase.styleData.preferenceData.defaultCanvasScreenMatchMode = (UnityEngine.UI.CanvasScaler.ScreenMatchMode) EditorGUILayout.EnumPopup(UIStylesDatabase.styleData.preferenceData.defaultCanvasScreenMatchMode);
			                	
			                	if (UIStylesDatabase.styleData.preferenceData.defaultCanvasScreenMatchMode == UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight)
			                	{
				                	EditorGUILayout.LabelField("Match");
			                		UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerMatch = EditorGUILayout.Slider(UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerMatch, 0, 1);
				                	
				                	GUILayout.BeginHorizontal();
				                	{
					                	EditorGUILayout.LabelField("Width", GUILayout.MinWidth(10));
					                	EditorGUILayout.LabelField("Height", GUILayout.Width(110));
				                	}
				                	GUILayout.EndHorizontal();	
			                	}
		                	}
		                	
		                	else if (UIStylesDatabase.styleData.preferenceData.defaultCanvasRenderMode != RenderMode.WorldSpace && UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerScaleMode == UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPhysicalSize)
		                	{
			                	GUILayout.Space(10);
				                EditorGUILayout.LabelField("Physical Unit");
				                UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerPhysicalUnit = (UnityEngine.UI.CanvasScaler.Unit) EditorGUILayout.EnumPopup(UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerPhysicalUnit);
			                	
			                	GUILayout.Space(10);
				                EditorGUILayout.LabelField("Physical Unit");
			                	UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerFallbackScreenDPI = EditorGUILayout.FloatField(UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerFallbackScreenDPI);
			                	
			                	GUILayout.Space(10);
				                EditorGUILayout.LabelField("Default Sprite DPI");
			                	UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerDefaultSpriteDPI = EditorGUILayout.FloatField(UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerFallbackScreenDPI);
			                	
		                	}
		                	
		                	
		                	
		                	// Reference Pixels Per Unit
			                GUILayout.Space(10);
			                EditorGUILayout.LabelField("Reference Pixels Per Unit");
			                UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerPixelsPerUnit = EditorGUILayout.FloatField(UIStylesDatabase.styleData.preferenceData.defaultCanvasScalerPixelsPerUnit);
			                
			                GUILayout.Space(10);
		                }
	                }
	                GUILayout.EndVertical();
	                
	                EditorGUI.indentLevel = 0;
	                GUILayout.BeginVertical(EditorHelper.StandardPanel(6));
	                {
	                	EditorHelper.FoldOut("Graphic Raycaster", "", ref graphicRaycasterDropdown);
		                
		                if (graphicRaycasterDropdown)
		                {
			                EditorGUI.indentLevel = 1;
			                
		                	// Reference Pixels Per Unit
			                GUILayout.Space(10);
			                GUILayout.BeginHorizontal();
		                	{
			                	UIStylesDatabase.styleData.preferenceData.defaultGraphicRaycasterIgnoreReversedGraphics = EditorGUILayout.Toggle(UIStylesDatabase.styleData.preferenceData.defaultGraphicRaycasterIgnoreReversedGraphics, GUILayout.Width(18));
			                	EditorGUILayout.LabelField("Ignore Reversed Graphics");
		                	}
		                	GUILayout.EndHorizontal();			                
		                	
		                	// Blocking Objects
			                GUILayout.Space(10);
			                EditorGUILayout.LabelField("Blocking Objects");
			                UIStylesDatabase.styleData.preferenceData.defaultGraphicRaycasterBlockingObjects = (UnityEngine.UI.GraphicRaycaster.BlockingObjects) EditorGUILayout.EnumPopup(UIStylesDatabase.styleData.preferenceData.defaultGraphicRaycasterBlockingObjects);
			                
			                GUILayout.Space(10);
			                EditorGUILayout.LabelField("Blocking Mask must be done from the hierarchy");
			                
			                GUILayout.Space(10);
		                }
	                }
	                GUILayout.EndVertical();
	                
	                
	                GUILayout.Space(10);
	                EditorHelper.StandardSeparator();
	                GUILayout.Space(10);
                }
            }
            GUILayout.EndScrollView();

            EditorHelper.StandardSeparator();

            if (GUILayout.Button("Thanks for using UI Styles\nAny feedback or requests please contack us.", EditorHelper.Caption(TextAnchor.MiddleCenter, Color.gray)))
            {
                //Application.OpenURL ( "mailto:" + contactEmail );
            }

            if (GUILayout.Button("MessyEntertainment.com", EditorHelper.Caption(TextAnchor.MiddleCenter, Color.gray)))
            {
                Application.OpenURL(documentationLink);
            }

            EditorHelper.StandardSeparator();

            // Save
            if (UIStylesDatabase.styleData != null && GUI.changed)
            {
                UIStylesDatabase.Save();
            }
        }
    }
}





















