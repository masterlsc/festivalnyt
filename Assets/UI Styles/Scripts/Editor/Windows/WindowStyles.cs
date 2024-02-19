using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Reflection;

namespace UIStyles
{
	[InitializeOnLoad]
    public class WindowStyles : EditorWindow
    {
        /// <summary>
        /// Search string
        /// </summary>
        private string searchString = string.Empty;

        // Scroll
        private Vector2 scrollPosition = Vector2.zero;

	    private bool needToSave = false;
	    private bool checkStyles = true;

        private static bool isWindowUtility;
	    
	    private static GameObject objectToUpdate;

        /// <summary>
        /// Reference to the data
        /// </summary>
        public static StyleDataFile data
        {
            get
            {
                if (UIStylesDatabase.styleData == null)
                    return null;

                return UIStylesDatabase.styleData;
            }
        }

        /// <summary>
        /// Search string
        /// </summary>
        private static List<GameObject> seclectedObjects = new List<GameObject>();
        public static List<GameObject> GetSeclectedObjects
        {
            get
            {
                return seclectedObjects;
            }
        }

        /// <summary>
        /// Current index
        /// </summary>
        private int currentIndex;

        /// <summary>
        /// Category values
        /// </summary>
        private bool addCategory = false;
        private string addCategoryName;
        private bool renameCurrentCategory = false;
        private string newCategoryName = string.Empty;

        /// <summary>
        /// Scenes Updates
        /// </summary>
        private ApplyMode updateAllScenesApplyMode;
        private bool showUpdates;
        private bool updatingAllScenes;
        private string returnToScene;
        private int yieldLoop;
        private int loopStatge;
        private int currentSceneCount;
        private int sceneStartCount;
        private bool showProgressBar = false;
        private bool cancelUpdateInAllScenes = false;
        private bool allCategoies;

        private static List<string> scenePaths = new List<string>();



        private UnityEngine.Object[] draggedObjects_Styles;
        private UnityEngine.Object[] draggedObjects_Component;
        private UnityEngine.Object[] draggedObjects_FindBy;


        /// <summary>
        /// Shows the window
        /// </summary>
        [MenuItem("Window/UI Styles/Window/UI Styles", false, 0)]
        private static void OpenWindow()
        {
            ShowWindow(false);
        }
        public static void ShowWindow(bool utility)
        {
            isWindowUtility = utility;

            WindowStyles window = (WindowStyles)EditorWindow.GetWindow(typeof(WindowStyles), utility, "UI Styles");
            window.minSize = new Vector2(411, 360f);
#if !PRE_UNITY_5
            window.titleContent.text = "UI Styles";
#endif
        }

        /// <summary>
        /// Close the window
        /// </summary>
        private void CloseWindow(object obj)
        {
            WindowStyles w = (WindowStyles)EditorWindow.GetWindow(typeof(WindowStyles));
            w.Close();
        }

        /// <summary>
        /// Is the window doced
        /// </summary>
        /// <returns></returns>
        private bool isWindowDocked()
        {
            BindingFlags fullBinding = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
            MethodInfo isDockedMethod = typeof(EditorWindow).GetProperty("docked", fullBinding).GetGetMethod(true);
            return (bool)isDockedMethod.Invoke(this, null) == true;
        }


        /// <summary>
        /// Shows the editor validator
        /// </summary>
        [MenuItem("Window/UI Styles/Styles Window", true)]
        public static bool ShowEditorValidator()
        {
            return true;
        }

        /// <summary>
        /// Enable
        /// </summary>
        private void OnEnable()
        {
            if (UIStylesDatabase.styleData == null)
                UIStylesDatabase.Load();
        }

        /// <summary>
        /// On window focus
        /// </summary>
        private void OnFocus()
        {
            OnSelectionChange();
        }

        /// <summary>
        /// Raises the selection change event.
        /// </summary>
        private void OnSelectionChange()
        {
            seclectedObjects = Selection.gameObjects.ToList();
            Repaint();
        }
	    
	    
	    
	    
	    
	    
	    
        /// <summary>
        /// Load new settings
        /// </summary>
        private static void Load(object obj)
        {
            string guid = (string)obj;

            if (UIStylesDatabase.stylesDataGUID == guid)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                StyleDataFile styleData = (StyleDataFile)AssetDatabase.LoadAssetAtPath(path, typeof(StyleDataFile));
                EditorHelper.SelectObjectInProject(styleData, false, true);
            }

            else UIStylesDatabase.stylesDataGUID = guid;

            UIStylesDatabase.Load();
            UIStylesDatabase.Save();
        }

        /// <summary>
        /// Create data file
        /// </summary>
        public static void Create(object path)
        {
            StyleHelper.CreateStyleDataFile((string)path, (StyleDataFile data) =>
            {

                // Ping
                EditorHelper.SelectObjectInProject(data, false, true);

                // Get guid
                string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(data));

                // Load
                Load(guid);
            });
        }
	    
	    /// <summary>
	    /// Save
	    /// </summary>
	    /// <param name="data"></param>
	    private void Save (Object data)
	    {
	    	if ( !Application.isPlaying )
	    	{
	    		EditorUtility.SetDirty ( data );
		    	EditorSceneManager.MarkSceneDirty ( EditorSceneManager.GetActiveScene () );
		    	
		    	if (EditorSceneManager.GetActiveScene ().path != string.Empty)
		    	{
			    	EditorSceneManager.SaveScene ( EditorSceneManager.GetActiveScene () );
		    	}
	    	}
	    }



        /// <summary>
        /// Menu Content
        /// </summary>
        private void MenuContent()
        {
            GUI.FocusControl(null);
            GenericMenu menu = new GenericMenu();

            // -------------------------------------------------- //
            // Preferences
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Preferences"), false, delegate
	        {
		        WindowPreferences.OpenPreferences();
	        }, null);
	        
	        menu.AddSeparator("");
	        
            // -------------------------------------------------- //
            // Open color palette
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Color Palette"), false, delegate
	        {
		        WindowColorPalette.ShowWindow(true);
	        }, null);
	        
	        menu.AddSeparator("");
	        
            // -------------------------------------------------- //
            // Save
            // -------------------------------------------------- //
	        if (UIStylesDatabase.styleData != null)
		        menu.AddItem(new GUIContent("Save"), false, delegate
		        {
			        UIStylesDatabase.Save();
		        }, null);

            // -------------------------------------------------- //
            // Load
            // -------------------------------------------------- //
            string[] objs = AssetDatabase.FindAssets("t:StyleDataFile");
            for (int i = 0; i < objs.Length; i++)
            {

                string guid = objs[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                string pathFormat = path.Replace("/", "\\").Replace(".asset", "");

                object obj = AssetDatabase.LoadAssetAtPath(path, typeof(StyleDataFile));

                //StyleData sd = (StyleData)obj;

                if (obj != null)
                    menu.AddItem(new GUIContent("Load/" + pathFormat), UIStylesDatabase.stylesDataGUID == guid, Load, guid);
            }

            // -------------------------------------------------- //
            // Create
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("Create"), false, Create, "");

            menu.AddSeparator("");

            // -------------------------------------------------- //
            // Apply
            // -------------------------------------------------- //
            if (UIStylesDatabase.styleData != null)
            {
                menu.AddSeparator("");

                // -------------------------------------------------- //
                // All Categories All Scene
                // -------------------------------------------------- //
                menu.AddItem(new GUIContent("Apply/All Resources/All Categories/All Scenes"), false, delegate
                {
                    UpdateAllScenes(true, ApplyMode.AllResources);
                }, null);
                menu.AddItem(new GUIContent("Apply/Active In Scene/All Categories/All Scenes"), false, delegate
                {
                    UpdateAllScenes(true, ApplyMode.ActiveInScene);
                }, null);

                // -------------------------------------------------- //
                // This Category All Scene
                // -------------------------------------------------- //
                menu.AddItem(new GUIContent("Apply/All Resources/This Category/All Scenes"), false, delegate
                {
                    UpdateAllScenes(false, ApplyMode.AllResources);
                }, null);
                menu.AddItem(new GUIContent("Apply/Active In Scene/This Category/All Scenes"), false, delegate
                {
                    UpdateAllScenes(false, ApplyMode.ActiveInScene);
                }, null);

                // -------------------------------------------------- //
                // All Categories This Scene
                // -------------------------------------------------- //
                menu.AddItem(new GUIContent("Apply/All Resources/All Categories/This Scene"), false, delegate
                {
                    StyleHelper.ApplyAllCategories(data, null, ApplyMode.AllResources);
                }, null);
                menu.AddItem(new GUIContent("Apply/Active In Scene/All Categories/This Scene"), false, delegate
                {
                    StyleHelper.ApplyAllCategories(data, null, ApplyMode.ActiveInScene);
                }, null);

                // -------------------------------------------------- //
                // This Category
                // -------------------------------------------------- //
                menu.AddItem(new GUIContent("Apply/All Resources/This Category/This Scene"), false, delegate
                {
                    StyleHelper.ApplyCurrentCategory(data, null, ApplyMode.AllResources);
                }, null);
                menu.AddItem(new GUIContent("Apply/Active In Scene/This Category/This Scene"), false, delegate
                {
                    StyleHelper.ApplyCurrentCategory(data, null, ApplyMode.ActiveInScene);
                }, null);

                menu.AddSeparator("");

                // -------------------------------------------------- //
                // Dockable
                // -------------------------------------------------- //
                menu.AddItem(new GUIContent("Window/Dockable"), !isWindowUtility, delegate
                {
                    CloseWindow(null);
                    ShowWindow(!isWindowUtility);

                }, null);

                // -------------------------------------------------- //
                // Close Window
                // -------------------------------------------------- //
                menu.AddItem(new GUIContent("Window/Close Window"), false, CloseWindow, null);
            }

            menu.ShowAsContext();
        }



        /// <summary>
        /// Category context menu
        /// </summary>
        private void CategoryDropdown()
        {
            GenericMenu menu = new GenericMenu();

            // -------------------------------------------------- //
            // Category
            // -------------------------------------------------- //
            data.categories.Sort();
            foreach (string category in data.categories)
            {
                menu.AddItem(new GUIContent(category), data.currentCategory == category, NewCategory, category);
            }

            menu.ShowAsContext();
        }

        /// <summary>
        /// Set to a new category
        /// </summary>
        private void NewCategory(object obj)
        {
            checkStyles = true;
            data.currentCategory = (string)obj;
            UIStylesDatabase.Save();
        }



        /// <summary>
        /// Categories context menu
        /// </summary>
        private void CategoryContext()
        {
            GUI.FocusControl(null);
            GenericMenu menu = new GenericMenu();

            // -------------------------------------------------- //
            // Apply
            // -------------------------------------------------- //
            if (UIStylesDatabase.styleData != null)
            {
                menu.AddSeparator("");

                // -------------------------------------------------- //
                // All Categories All Scene
                // -------------------------------------------------- //
                menu.AddItem(new GUIContent("Apply/All Resources/All Categories/All Scenes"), false, delegate
                {
                    UpdateAllScenes(true, ApplyMode.AllResources);
                }, null);
                menu.AddItem(new GUIContent("Apply/Active In Scene/All Categories/All Scenes"), false, delegate
                {
                    UpdateAllScenes(true, ApplyMode.ActiveInScene);
                }, null);

                // -------------------------------------------------- //
                // This Category All Scene
                // -------------------------------------------------- //
                menu.AddItem(new GUIContent("Apply/All Resources/This Category/All Scenes"), false, delegate
                {
                    UpdateAllScenes(false, ApplyMode.AllResources);
                }, null);
                menu.AddItem(new GUIContent("Apply/Active In Scene/This Category/All Scenes"), false, delegate
                {
                    UpdateAllScenes(false, ApplyMode.ActiveInScene);
                }, null);

                // -------------------------------------------------- //
                // All Categories This Scene
                // -------------------------------------------------- //
                menu.AddItem(new GUIContent("Apply/All Resources/All Categories/This Scene"), false, delegate
                {
                    StyleHelper.ApplyAllCategories(data, null, ApplyMode.AllResources);
                }, null);
                menu.AddItem(new GUIContent("Apply/Active In Scene/All Categories/This Scene"), false, delegate
                {
                    StyleHelper.ApplyAllCategories(data, null, ApplyMode.ActiveInScene);
                }, null);

                // -------------------------------------------------- //
                // This Category
                // -------------------------------------------------- //
                menu.AddItem(new GUIContent("Apply/All Resources/This Category/This Scene"), false, delegate
                {
                    StyleHelper.ApplyCurrentCategory(data, null, ApplyMode.AllResources);
                }, null);
                menu.AddItem(new GUIContent("Apply/Active In Scene/This Category/This Scene"), false, delegate
                {
                    StyleHelper.ApplyCurrentCategory(data, null, ApplyMode.ActiveInScene);
                }, null);

                menu.AddSeparator("");
            }

            // -------------------------------------------------- //
            // Rename
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("Rename"), false, delegate
            {
                newCategoryName = data.currentCategory;
                renameCurrentCategory = true;
                DataHelper.highlightRenameField = true;
            }, null);

            // -------------------------------------------------- //
            // Duplicate
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("Duplicate"), false, delegate
            {

                string newCategory = data.currentCategory + " Copy";

                int index = -1;
                while (UIStylesDatabase.styleData.categories.Contains(newCategory))
                {
                    index++;
                    newCategory += index;
                }

                if (!UIStylesDatabase.styleData.categories.Contains(newCategory))
                    UIStylesDatabase.styleData.categories.Add(newCategory);

                foreach (Style s in StyleHelper.GetStylesAssignedToCategory(UIStylesDatabase.styleData, data.currentCategory))
                {
                    Style newStyle = s.Clone(UIStylesDatabase.styleData);
                    newStyle.category = newCategory;
                }
                UIStylesDatabase.Save();

                data.currentCategory = newCategory;

            }, null);

            // -------------------------------------------------- //
            // Rename
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("Delete"), false, delegate
            {

                if (EditorUtility.DisplayDialog("Warning", "Are you sure you want to delete this category?", "Yes Delete", "No Cancel"))
                {
                    List<Style> removes = new List<Style>();
                    for (int i = 0; i < data.styles.Count; i++)
                        if (data.styles[i].category == data.currentCategory)
                            removes.Add(data.styles[i]);

                    for (int i = 0; i < removes.Count; i++)
                        data.styles.Remove(removes[i]);

                    data.categories.Remove(data.currentCategory);

                    if (data.categories.Count > 0)
                        data.currentCategory = data.categories[0];
                    else
                    {
                        data.currentCategory = string.Empty;
                        UIStylesDatabase.Save();
                    }
                }
            }, null);
            menu.ShowAsContext();
        }
	    

        private void ApplyContext(int i)
        {
            GUI.FocusControl(null);
            GenericMenu menu = new GenericMenu();

            // -------------------------------------------------- //
            // Apply
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("All Resources"), false, delegate
            {
	            StyleHelper.Apply(data, data.styles[i], null, ApplyMode.AllResources);
	            Save (data);
            }, i);

            menu.AddItem(new GUIContent("Active In Scene"), false, delegate
            {
	            StyleHelper.Apply(data, data.styles[i], null, ApplyMode.ActiveInScene);
	            Save (data);
            }, i);

            menu.ShowAsContext();
        }
	    
	    private void OnCreateObjectInCanvas (object obj)
	    {
	    	StyleHelper.CreateStyleInCanvas (data, data.styles[currentIndex], obj == null ? null : (Canvas)obj);
	    }
	    

        /// <summary>
        /// Style context menu
        /// </summary>
        private void StyleContext(int i)
        {
            currentIndex = i;
            GUI.FocusControl(null);
            GenericMenu menu = new GenericMenu();

            // -------------------------------------------------- //
            // Apply
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("Apply/All Resources"), false, delegate
            {
                StyleHelper.Apply(data, data.styles[i], null, ApplyMode.AllResources);
            }, i);

            menu.AddItem(new GUIContent("Apply/Active In Scene"), false, delegate
            {
                StyleHelper.Apply(data, data.styles[i], null, ApplyMode.ActiveInScene);
            }, i);

	        menu.AddSeparator("");
	        
	        // -------------------------------------------------- //
            // Style State
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Style State/Enable"), data.styles[i].styleState == StyleState.Enable, delegate
	        {
		        StyleHelper.SetStyleState(data.styles[i], StyleState.Enable);
	        }, i);
	        
	        menu.AddItem(new GUIContent("Style State/Disable"), data.styles[i].styleState == StyleState.Disable, delegate
	        {
		        StyleHelper.SetStyleState(data.styles[i], StyleState.Disable);
	        }, i);
	        
	        // -------------------------------------------------- //
            // Component State
            // -------------------------------------------------- //
	        //menu.AddSeparator("");
	        menu.AddItem(new GUIContent("Component State/Enable All"), false, delegate
	        {
		        foreach (StyleComponent com in data.styles[i].styleComponents)
			        StyleHelper.SetComponentState(com, StyleComponentState.Enable);
	        }, i);
	        menu.AddItem(new GUIContent("Component State/Enable All And Always Add"), false, delegate
	        {
		        foreach (StyleComponent com in data.styles[i].styleComponents)
			        StyleHelper.SetComponentState(com, StyleComponentState.AlwaysAdd);
	        }, i);
	        menu.AddSeparator("Component State/");
	        menu.AddItem(new GUIContent("Component State/Disable All"), false, delegate
	        {
		        foreach (StyleComponent com in data.styles[i].styleComponents)
			        StyleHelper.SetComponentState(com, StyleComponentState.Disable);
	        }, i);
	        menu.AddItem(new GUIContent("Component State/Disable All And Always Remove"), false, delegate
	        {
		        foreach (StyleComponent com in data.styles[i].styleComponents)
			        StyleHelper.SetComponentState(com, StyleComponentState.AlwaysRemove);
	        }, i);
	        menu.AddItem(new GUIContent("Component State/Disable All And Always Delete"), false, delegate
	        {
		        foreach (StyleComponent com in data.styles[i].styleComponents)
			        StyleHelper.SetComponentState(com, StyleComponentState.AlwaysDelete);
	        }, i);
	        
	        menu.AddSeparator("");
	        	        
	        // -------------------------------------------------- //
            // Create object in canvas
            // -------------------------------------------------- //
	        Canvas[] canvas = UnityEngine.Object.FindObjectsOfType<Canvas> () as Canvas[];
	        
	        menu.AddItem(new GUIContent("Create Object In Canvas/Create New Canvas"), false, delegate
	        {
		        StyleHelper.CreateStyleInCanvas (data, data.styles[i], null);
	        }, i);
	        
	        menu.AddSeparator("Create Object In Canvas/");
	        
	        for (int c = 0; c < canvas.Length; c++) 
	        {
	        	string name = "Create Object In Canvas/" + canvas[c].name.Replace('/', '\\');
	        	
	        	for (int n = 0; n < c; n++) 
		        	name += " ";
	        	
	        	menu.AddItem(new GUIContent(name), false, OnCreateObjectInCanvas, canvas[c]);
	        }
	        
	        
	        menu.AddSeparator("");

            // -------------------------------------------------- //
            // Move up
            // -------------------------------------------------- //
            bool canMoveUp = false;
            int loop = i;
            while (loop > 0)
            {
                loop--;

                if (data.styles[loop].category == data.currentCategory)
                {
                    canMoveUp = true;
                    break;
                }
            }

            if (canMoveUp)
                menu.AddItem(new GUIContent("Move Up"), false, delegate
                {
                    if (i > 0)
                    {
                        int c = i - 1;
                        while (c > 0 && data.styles[c].category != data.currentCategory)
                            c--;

                        Style a = data.styles[i];
                        Style b = data.styles[c];
                        data.styles[i] = b;
                        data.styles[c] = a;
                        Repaint();
                        UIStylesDatabase.Save();
                    }
                }, i);

            else menu.AddDisabledItem(new GUIContent("Move Up"));

            // -------------------------------------------------- //
            // Move down
            // -------------------------------------------------- //
            bool canMoveDown = false;
            loop = i;
            while (loop < data.styles.Count - 1)
            {
                loop++;

                if (data.styles[loop].category == data.currentCategory)
                {
                    canMoveDown = true;
                    break;
                }
            }

            if (canMoveDown)
                menu.AddItem(new GUIContent("Move Down"), false, delegate
                {

                    if (i < data.styles.Count - 1)
                    {
                        int c = i + 1;
                        while (c < data.styles.Count - 1 && data.styles[c].category != data.currentCategory)
                            c++;

                        Style a = data.styles[i];
                        Style b = data.styles[c];
                        data.styles[i] = b;
                        data.styles[c] = a;
                        Repaint();
                        UIStylesDatabase.Save();
                    }
                }, i);

            else menu.AddDisabledItem(new GUIContent("Move Down"));

            menu.AddSeparator("");

            // -------------------------------------------------- //
            // Move to Category
            // -------------------------------------------------- //
            data.categories.Sort();
            foreach (string cat in data.categories)
            {
                if (data.currentCategory != cat)
                    menu.AddItem(new GUIContent("Move to Category/" + cat), false, MoveCategory, cat);

                else menu.AddDisabledItem(new GUIContent("Move to Category/" + cat));
            }

            menu.AddSeparator("");

            // -------------------------------------------------- //
            // Copy
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("Find By Name/Copy To Clipboard"), false, delegate
            {
                EditorGUIUtility.systemCopyBuffer = "(" + data.styles[i].findByName + ")";
            }, i);

            // -------------------------------------------------- //
            // Copy to objects
            // -------------------------------------------------- //
            if (seclectedObjects.Count > 0)
                menu.AddItem(new GUIContent("Find By Name/Add To Selected Objects"), false, delegate
                {
                    foreach (GameObject o in seclectedObjects)
                        StyleHelper.AddFindByName(UIStylesDatabase.styleData, o, data.styles[i].findByName);
                }, i);

            else menu.AddDisabledItem(new GUIContent("Find By Name/Add To Selected Objects"));

	        menu.AddSeparator("");
	        
	        // -------------------------------------------------- //
            // Ungroup all components
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Ungroup All Components"), false, delegate
	        {
		        foreach (StyleComponent com in data.styles[i].styleComponents)
		        {
		        	if (com.styleComponentType == StyleComponentType.Group)
		        		com.group.componentIDs.Clear();
			        
		        	else com.groupID = 0;
		        }
	        }, i);
	        
	        menu.AddSeparator("");

            // -------------------------------------------------- //
            // Rename
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("Rename"), false, delegate
            {
                data.styles[i].rename = true;
                DataHelper.highlightRenameField = true;
            }, i);

            // -------------------------------------------------- //
            // Duplicate
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("Duplicate"), false, delegate
            {
	            checkStyles = true;
                Style s = data.styles[i].Clone(UIStylesDatabase.styleData);
                s.name += " Copy";
            }, i);

            // -------------------------------------------------- //
            // Delete
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("Delete"), false, delegate
            {
                if (EditorUtility.DisplayDialog("Warning", "Are you sure you want to delete this style", "Yes Delete", "No Cancel"))
                {
                	checkStyles = true;
                    data.styles.RemoveAt(i);
                    UIStylesDatabase.Save();
                }
            }, i);

            menu.ShowAsContext();
        }

        /// <summary>
        /// Move to Category
        /// </summary>
        private void MoveCategory(object obj)
        {
            data.styles[currentIndex].category = (string)obj;
        }



        /// <summary>
        /// Style component context menu
        /// </summary>
        public void StyleComponentContext(int i)
        {
            GUI.FocusControl(null);
	        GenericMenu menu = new GenericMenu();
	        
	        if (data.styles[i].styleComponents[currentIndex].styleComponentType != StyleComponentType.Group)
	        {
		        // -------------------------------------------------- //
	            // Component State
	            // -------------------------------------------------- //
		        menu.AddItem(new GUIContent("Component State/Enable"), data.styles[i].styleComponents[currentIndex].styleComponentState == StyleComponentState.Enable, delegate
		        {
			        StyleHelper.SetComponentState(data.styles[i].styleComponents[currentIndex], StyleComponentState.Enable);
		        }, i);
		        menu.AddItem(new GUIContent("Component State/Enable And Always Add"), data.styles[i].styleComponents[currentIndex].styleComponentState == StyleComponentState.AlwaysAdd, delegate
		        {
			        StyleHelper.SetComponentState(data.styles[i].styleComponents[currentIndex], StyleComponentState.AlwaysAdd);
		        }, i);
		        menu.AddSeparator("Component State/");
		        menu.AddItem(new GUIContent("Component State/Disable"), data.styles[i].styleComponents[currentIndex].styleComponentState == StyleComponentState.Disable, delegate
		        {
			        StyleHelper.SetComponentState(data.styles[i].styleComponents[currentIndex], StyleComponentState.Disable);
		        }, i);
		        menu.AddItem(new GUIContent("Component State/Disable And Always Remove"), data.styles[i].styleComponents[currentIndex].styleComponentState == StyleComponentState.AlwaysRemove, delegate
		        {
			        StyleHelper.SetComponentState(data.styles[i].styleComponents[currentIndex], StyleComponentState.AlwaysRemove);
		        }, i);
		        menu.AddItem(new GUIContent("Component State/Disable And Always Delete"), data.styles[i].styleComponents[currentIndex].styleComponentState == StyleComponentState.AlwaysDelete, delegate
		        {
			        StyleHelper.SetComponentState(data.styles[i].styleComponents[currentIndex], StyleComponentState.AlwaysDelete);
		        }, i);
		        
		        menu.AddSeparator("");
	        }
	        
            // -------------------------------------------------- //
            // Move Up
            // -------------------------------------------------- //
	        
	        // Is in group
	        if (data.styles[i].styleComponents[currentIndex].styleComponentType != StyleComponentType.Group && data.styles[i].styleComponents[currentIndex].groupID != 0)
	        {
	        	int groupIndex = StyleHelper.GetGroupComponentByGroupID(data.styles[i], data.styles[i].styleComponents[currentIndex].groupID).group.componentIDs.IndexOf( data.styles[i].styleComponents[currentIndex].id );
		        		        
	        	if (groupIndex > 0)
		        	menu.AddItem(new GUIContent("Move Up"), false, delegate
		        	{
			        	if (groupIndex > 0)
			        	{
				        	int a = StyleHelper.GetGroupComponentByGroupID(data.styles[i], data.styles[i].styleComponents[currentIndex].groupID).group.componentIDs[groupIndex];
				        	int b = StyleHelper.GetGroupComponentByGroupID(data.styles[i], data.styles[i].styleComponents[currentIndex].groupID).group.componentIDs[groupIndex - 1];
				        	StyleHelper.GetGroupComponentByGroupID(data.styles[i], data.styles[i].styleComponents[currentIndex].groupID).group.componentIDs[groupIndex] = b;
				        	StyleHelper.GetGroupComponentByGroupID(data.styles[i], data.styles[i].styleComponents[currentIndex].groupID).group.componentIDs[groupIndex - 1] = a;
				        	Repaint();
				        	UIStylesDatabase.Save();
			        	}
		        	}, i);
		        
	        	else menu.AddDisabledItem(new GUIContent("Move Up"));
	        }
	        
	        // Not group
	        else
	        {		    
		        if (currentIndex > 0)
			        menu.AddItem(new GUIContent("Move Up"), false, delegate
			        {
				        int c = currentIndex - 1;
				        
				        while (c > 0 && data.styles[i].styleComponents[c].groupID != 0 && data.styles[i].styleComponents[c].styleComponentType != StyleComponentType.Group)
					        c--;
				      
				        StyleComponent a = data.styles[i].styleComponents[currentIndex];
				        StyleComponent b = data.styles[i].styleComponents[c];
				        data.styles[i].styleComponents[currentIndex] = b;
				        data.styles[i].styleComponents[c] = a;
				        Repaint();
				        UIStylesDatabase.Save();
			        }, i);
		        
		        else menu.AddDisabledItem(new GUIContent("Move Up"));
	        }

            // -------------------------------------------------- //
            // Move Down
            // -------------------------------------------------- //
	        if (data.styles[i].styleComponents[currentIndex].styleComponentType != StyleComponentType.Group && data.styles[i].styleComponents[currentIndex].groupID != 0)
	        {
	        	int groupIndex = StyleHelper.GetGroupComponentByGroupID(data.styles[i], data.styles[i].styleComponents[currentIndex].groupID).group.componentIDs.IndexOf( data.styles[i].styleComponents[currentIndex].id );
	        	
	        	if (groupIndex < StyleHelper.GetGroupComponentByGroupID(data.styles[i], data.styles[i].styleComponents[currentIndex].groupID).group.componentIDs.Count - 1)
		        	menu.AddItem(new GUIContent("Move Down"), false, delegate
		        	{
			        	if (groupIndex < StyleHelper.GetGroupComponentByGroupID(data.styles[i], data.styles[i].styleComponents[currentIndex].groupID).group.componentIDs.Count - 1)
			        	{
				        	int a = StyleHelper.GetGroupComponentByGroupID(data.styles[i], data.styles[i].styleComponents[currentIndex].groupID).group.componentIDs[groupIndex];
				        	int b = StyleHelper.GetGroupComponentByGroupID(data.styles[i], data.styles[i].styleComponents[currentIndex].groupID).group.componentIDs[groupIndex + 1];
				        	StyleHelper.GetGroupComponentByGroupID(data.styles[i], data.styles[i].styleComponents[currentIndex].groupID).group.componentIDs[groupIndex] = b;
				        	StyleHelper.GetGroupComponentByGroupID(data.styles[i], data.styles[i].styleComponents[currentIndex].groupID).group.componentIDs[groupIndex + 1] = a;
				        	Repaint();
				        	UIStylesDatabase.Save();
			        	}
		        	}, i);
		        
	        	else menu.AddDisabledItem(new GUIContent("Move Down"));
	        }
	        else
	        {		
		    	if (currentIndex < data.styles[i].styleComponents.Count - 1)
		        	menu.AddItem(new GUIContent("Move Down"), false, delegate
		        	{
		        		int c = currentIndex + 1;
		        		
			        	while (c < data.styles[i].styleComponents.Count -1 && data.styles[i].styleComponents[c].groupID != 0 && data.styles[i].styleComponents[c].styleComponentType != StyleComponentType.Group)
				        	c++;

			        	StyleComponent a = data.styles[i].styleComponents[currentIndex];
			        	StyleComponent b = data.styles[i].styleComponents[c];
			        	data.styles[i].styleComponents[currentIndex] = b;
			        	data.styles[i].styleComponents[c] = a;
			        	Repaint();
			        	UIStylesDatabase.Save();
		        	}, i);
		        
	        	else menu.AddDisabledItem(new GUIContent("Move Down"));
	        }
	        
	        menu.AddSeparator("");
	        
	        // -------------------------------------------------- //
            // Move to group
            // -------------------------------------------------- //
	        if (data.styles[i].styleComponents[currentIndex].styleComponentType != StyleComponentType.Group)
	        {
	        	if (data.styles[i].styleComponents[currentIndex].groupID != 0)
	        	{
	        		menu.AddItem(new GUIContent("Move to Group/None"), false, delegate
	        		{
		        		RemoveFromGroup (i, currentIndex, false);
	        		}, null);
	        		
		        	menu.AddSeparator("Move to Group/");
	        	}
	        	
	        	
		        for (int g = 0; g < data.styles[i].styleComponents.Count; g++) 
		        {
			        if (data.styles[i].styleComponents[g].styleComponentType == StyleComponentType.Group)
			        {
			        	if (data.styles[i].styleComponents[g].groupID != data.styles[i].styleComponents[currentIndex].groupID)
			        	{			        		
			        		MoveToGroupValeus v = new MoveToGroupValeus {
			        			style = data.styles[i],
				        		groupComponent = data.styles[i].styleComponents[g],
				        		componentToAdd = data.styles[i].styleComponents[currentIndex]
	        				};
				        	
				        	menu.AddItem(new GUIContent("Move to Group/" + data.styles[i].styleComponents[g].name), false, MoveToGroup, v);
			        	}
			        	else menu.AddDisabledItem(new GUIContent("Move to Group/" + data.styles[i].styleComponents[g].name));
			        }
		        }
		        
		        menu.AddSeparator("");
	        }
	        
	        if (data.styles[i].styleComponents[currentIndex].styleComponentType != StyleComponentType.Group)
	        {
	            // -------------------------------------------------- //
	            // Switch Component
	            // -------------------------------------------------- //
		        menu.AddItem(new GUIContent("Switch Component/UI/Text"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.Text, delegate
	            {
	                data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.Text);
	            }, null);
	            menu.AddItem(new GUIContent("Switch Component/UI/Image"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.Image, delegate
	            {
	                data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.Image);
	            }, null);
	            menu.AddItem(new GUIContent("Switch Component/UI/Button"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.Button, delegate
	            {
	                data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.Button);
	            }, null);
	            menu.AddItem(new GUIContent("Switch Component/UI/InputField"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.InputField, delegate
	            {
	                data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.InputField);
	            }, null);
	            menu.AddItem(new GUIContent("Switch Component/UI/Dropdown"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.Dropdown, delegate
	            {
	                data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.Dropdown);
	            }, null);
	            menu.AddItem(new GUIContent("Switch Component/UI/ScrollRect"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.ScrollRect, delegate
	            {
	                data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.ScrollRect);
	            }, null);
	            menu.AddItem(new GUIContent("Switch Component/UI/Scrollbar"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.Scrollbar, delegate
	            {
	                data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.Scrollbar);
	            }, null);
	            menu.AddItem(new GUIContent("Switch Component/UI/Slider"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.Slider, delegate
	            {
	                data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.Slider);
	            }, null);
	            menu.AddItem(new GUIContent("Switch Component/UI/Toggle"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.Toggle, delegate
	            {
	                data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.Toggle);
	            }, null);
		        menu.AddItem(new GUIContent("Switch Component/Layout/RectTransform"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.RectTransform, delegate
		        {
			        data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.RectTransform);
		        }, null);  
		        menu.AddItem(new GUIContent("Switch Component/Layout/CanvasGroup"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.CanvasGroup, delegate
		        {
			        data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.CanvasGroup);
		        }, null);
		        
		        menu.AddItem(new GUIContent("Switch Component/Layout/LayoutElement"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.LayoutElement, delegate
		        {
			        data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.LayoutElement);
		        }, null);
		        
		        menu.AddItem(new GUIContent("Switch Component/Layout/ContentSizeFitter"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.ContentSizeFitter, delegate
		        {
			        data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.ContentSizeFitter);
		        }, null);
		        
		        menu.AddItem(new GUIContent("Switch Component/Layout/AspectRatioFitter"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.AspectRatioFitter, delegate
		        {
			        data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.AspectRatioFitter);
		        }, null);
		        
		        menu.AddItem(new GUIContent("Switch Component/Layout/HorizontalLayoutGroup"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.HorizontalLayoutGroup, delegate
		        {
			        data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.HorizontalLayoutGroup);
		        }, null);
		        
		        menu.AddItem(new GUIContent("Switch Component/Layout/VerticalLayoutGroup"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.VerticalLayoutGroup, delegate
		        {
			        data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.VerticalLayoutGroup);
		        }, null);
		        
		        menu.AddItem(new GUIContent("Switch Component/Layout/GridLayoutGroup"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.GridLayoutGroup, delegate
		        {
			        data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.GridLayoutGroup);
		        }, null);
		        
		        menu.AddItem(new GUIContent("Switch Component/Layout/Canvas"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.Canvas, delegate
		        {
			        data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.Canvas);
		        }, null);
		        
		        menu.AddItem(new GUIContent("Switch Component/Layout/CanvasScaler"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.CanvasScaler, delegate
		        {
			        data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.CanvasScaler);
		        }, null);
		        
		        menu.AddItem(new GUIContent("Switch Component/Event/GraphicRaycaster"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.GraphicRaycaster, delegate
		        {
			        data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.GraphicRaycaster);
		        }, null);
		        menu.AddItem(new GUIContent("Switch Component/Rendering/Camera"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.Camera, delegate
		        {
			        data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.Camera);
		        }, null);
		        
		        menu.AddItem(new GUIContent("Switch Component/Custom"), data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.Custom, delegate
		        {
			        data.styles[i].styleComponents[currentIndex].SwitchComponent(StyleComponentType.Custom);
		        }, null);
		        
		        /*<UIStylesTag(SwitchComponent)>*/
		        		        
	
		        menu.AddSeparator("");
	        }

            // -------------------------------------------------- //
            // Rename
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("Rename"), false, delegate
            {
                data.styles[i].styleComponents[currentIndex].rename = true;
                DataHelper.highlightRenameField = true;
            }, i);

            // -------------------------------------------------- //
            // Duplicate
            // -------------------------------------------------- //
	        if (data.styles[i].styleComponents[currentIndex].styleComponentType != StyleComponentType.Group)
	        {
	            menu.AddItem(new GUIContent("Duplicate"), false, delegate
	            {
	                data.styles[i].DuplicateComponent(UIStylesDatabase.styleData, data.styles[i].styleComponents[currentIndex]);
	                data.styles[i].CheckForPathError();
	            }, i);
	        }

            // -------------------------------------------------- //
            // Delete
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("Delete"), false, delegate
            {
                if (EditorUtility.DisplayDialog("Warning", "Are you sure you want to delete this component", "Yes Delete", "No Cancel"))
                {
                	if (data.styles[i].styleComponents[currentIndex].styleComponentType == StyleComponentType.Group)
                	{
                		bool deleteContent = false;
                		
                		if (data.styles[i].styleComponents[currentIndex].group.componentIDs.Count > 0)
                			deleteContent = EditorUtility.DisplayDialog("Warning", "Would you like to delete the components within the group?", "Yes Delete All", "No Ungroup");
                		
                		DeleteGroup (deleteContent, data.styles[i], data.styles[i].styleComponents[currentIndex].groupID);
                	}
                	else
                	{
	            		// If its part of a group remove it from the group then delete it
                		int groupID = data.styles[i].styleComponents[currentIndex].groupID;
                		
                		if (groupID != 0)
                		{
                			StyleComponent oldGroup = StyleHelper.GetGroupComponentByGroupID(data.styles[i], data.styles[i].styleComponents[currentIndex].groupID);
	                		
	                		int componentIdInGroupList = oldGroup.group.componentIDs.IndexOf( data.styles[i].styleComponents[currentIndex].id );
	                		
	                		oldGroup.group.componentIDs.RemoveAt(componentIdInGroupList);
                		}
                		
	                	data.styles[i].styleComponents.RemoveAt(currentIndex);
	                	data.styles[i].CheckForPathError();
                	}
                }
            }, i);

            menu.ShowAsContext();
        }
	    
	    private void DeleteGroup (bool deleteContent, Style style, int GroupID)
	    {
	    	// Cant remove from the list we are looping
	    	List <StyleComponent> toRemove = new List<UIStyles.StyleComponent>();
	    	
	    	foreach (StyleComponent com in style.styleComponents)
	    	{
	    		if (com.groupID == GroupID)
	    		{
	    			// If its the group component remove it
	    			if (com.styleComponentType == StyleComponentType.Group)
	    				toRemove.Add(com);
	    			
	    			else
	    			{
	    				if (deleteContent)
		    				toRemove.Add(com);
	    				
	    				else com.groupID = 0;
	    			}
	    		}
	    	}
	    	
	    	foreach (StyleComponent com in toRemove)
	    		style.styleComponents.Remove(com);
	    }
	    
	    private void RemoveFromGroup (int s, int c, bool delete)
	    {
	    	int groupID = data.styles[s].styleComponents[c].groupID;
		    
		    if (groupID != 0)
		    {
			    StyleComponent oldGroup = StyleHelper.GetGroupComponentByGroupID(data.styles[s], data.styles[s].styleComponents[c].groupID);
			    
			    int componentIdInGroupList = oldGroup.group.componentIDs.IndexOf( data.styles[s].styleComponents[c].id );
			    
			    oldGroup.group.componentIDs.RemoveAt(componentIdInGroupList);
		    }
		    
		    if (delete)
		    {
		    	data.styles[s].styleComponents.RemoveAt(c);
			    data.styles[s].CheckForPathError();
		    }
		    else
		    {
		    	data.styles[s].styleComponents[c].groupID = 0;
		    }
	    }
	    
	    
	    private class MoveToGroupValeus 
	    {
	    	public Style style;
	    	public StyleComponent groupComponent;
	    	public StyleComponent componentToAdd;
	    }
	    
	    private void MoveToGroup (object obj)
	    {
	    	MoveToGroupValeus v = (MoveToGroupValeus)obj;
	    	
	    	// Remove from old group
		    if (v.componentToAdd.groupID != 0)
		    {
		    	StyleComponent oldGroup = StyleHelper.GetGroupComponentByGroupID(v.style, v.componentToAdd.groupID);
		    	
			    int componentIdInGroupList = oldGroup.group.componentIDs.IndexOf( v.componentToAdd.id );
			    
			    oldGroup.group.componentIDs.RemoveAt(componentIdInGroupList);
		    }
	    	
	    	// Add the components id to the groups list of ids
		    v.groupComponent.group.componentIDs.Add(v.componentToAdd.id);
		    
		    // Store the groups id in the component 
		    v.componentToAdd.groupID = v.groupComponent.groupID;	    	
	    }


        /// <summary>
        /// Find by name context menu
        /// </summary>
        private void FindByContext(int i)
        {
            GenericMenu menu = new GenericMenu();

            // -------------------------------------------------- //
            // Edit
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("Edit"), false, delegate
            {
                data.styles[i].renameFindBy = true;
            }, i);

            // -------------------------------------------------- //
            // Copy
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("Copy"), false, delegate
            {
                EditorGUIUtility.systemCopyBuffer = "(" + data.styles[i].findByName + ")";
            }, i);

            menu.AddSeparator("");

            // -------------------------------------------------- //
            // Copy to objects
            // -------------------------------------------------- //
            if (seclectedObjects.Count > 0)
                menu.AddItem(new GUIContent("Add To Selected Objects"), false, delegate
                {
                    foreach (GameObject o in seclectedObjects)
                        StyleHelper.AddFindByName(UIStylesDatabase.styleData, o, data.styles[i].findByName);
                }, i);

            else menu.AddDisabledItem(new GUIContent("Add To Selected Objects"));



            menu.ShowAsContext();
        }





        /// <summary>
        /// Categories context menu
        /// </summary>
        private void AddStyleContext()
	    {
            GUI.FocusControl(null);
            GenericMenu menu = new GenericMenu();

            // -------------------------------------------------- //
            // Custom
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("Empty"), false, delegate
            {
                StyleHelper.AddEmptyStyle(UIStylesDatabase.styleData, data.currentCategory, "New Empty Style", "");
            }, null);

            menu.AddSeparator("");

            // -------------------------------------------------- //
            // Text
            // -------------------------------------------------- //
		    menu.AddItem(new GUIContent("UI/Text"), false, delegate
            {
                TextHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Text Style", "", null, false);
            }, null);

            // -------------------------------------------------- //
            // Image
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("UI/Image"), false, delegate
            {
                ImageHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Image Style", "", null, false);
            }, null);

            // -------------------------------------------------- //
            // Button
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("UI/Button"), false, delegate
            {
                ButtonHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Button Style", "", null, false);
            }, null);

            // -------------------------------------------------- //
            // Inputfield
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("UI/InputField"), false, delegate
            {
                InputFieldHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Inputfield Style", "", null, false);
            }, null);

            // -------------------------------------------------- //
            // Dropdown
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("UI/Dropdown"), false, delegate
            {
                DropdownHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Dropdown Style", "", null, false);
            }, null);

            // -------------------------------------------------- //
            // ScrollRect
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("UI/ScrollRect"), false, delegate
            {
                ScrollRectHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New ScrollRect Style", "", null, false);
            }, null);

            // -------------------------------------------------- //
            // Scrollbar
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("UI/Scrollbar"), false, delegate
            {
                ScrollbarHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Scrollbar Style", "", null, false);
            }, null);

            // -------------------------------------------------- //
            // Slider
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("UI/Slider"), false, delegate
            {
                SliderHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Slider Style", "", null, false);
            }, null);

            // -------------------------------------------------- //
            // Toggle
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("UI/Toggle"), false, delegate
            {
                ToggleHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Toggle Style", "", null, false);
            }, null);
	        
	        // -------------------------------------------------- //
            // Rect
            // -------------------------------------------------- //
		    menu.AddItem(new GUIContent("Layout/Rect Transform"), false, delegate
	        {
		        RectTransformHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Rect Transform Style", "", null);
	        }, null);
		    
		    // -------------------------------------------------- //
            // Canvas
            // -------------------------------------------------- //
		    menu.AddItem(new GUIContent("Layout/Canvas"), false, delegate
		    {
			    CanvasHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Canvas Style", "", null);
		    }, null);
		    
		    // -------------------------------------------------- //
            // Canvas Scaler
            // -------------------------------------------------- //
		    menu.AddItem(new GUIContent("Layout/Canvas Scaler"), false, delegate
		    {
			    CanvasScalerHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New CanvasScaler Style", "", null);
		    }, null);
		    
		    // -------------------------------------------------- //
            // CanvasGroup
            // -------------------------------------------------- //
		    menu.AddItem(new GUIContent("Layout/Canvas Group"), false, delegate
		    {
			    CanvasGroupHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Canvas Group Style", "", null);
		    }, null);
		    
		    // -------------------------------------------------- //
            // Layout Element
            // -------------------------------------------------- //
		    menu.AddItem(new GUIContent("Layout/Layout Element"), false, delegate
		    {
			    LayoutElementHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Layout Element Style", "", null);
		    }, null);
		    
		     // -------------------------------------------------- //
            // Content Size Fitter
            // -------------------------------------------------- //
		    menu.AddItem(new GUIContent("Layout/Content Size Fitter"), false, delegate
		    {
			    ContentSizeFitterHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New ContentSizeFitter Style", "", null);
		    }, null);
		    
		    // -------------------------------------------------- //
            // Aspect Ratio Fitter
            // -------------------------------------------------- //
		    menu.AddItem(new GUIContent("Layout/Aspect Ratio Fitter"), false, delegate
		    {
			    AspectRatioFitterHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New AspectRatioFitter Style", "", null);
		    }, null);
		    
		    // -------------------------------------------------- //
            // Horizontal Layout Group
            // -------------------------------------------------- //
		    menu.AddItem(new GUIContent("Layout/Horizontal Layout Group"), false, delegate
		    {
			    HorizontalLayoutGroupHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New HorizontalLayoutGroup Style", "", null);
		    }, null);
		    
		    // -------------------------------------------------- //
            // Vertical Layout Group
            // -------------------------------------------------- //
		    menu.AddItem(new GUIContent("Layout/Vertical Layout Group"), false, delegate
		    {
			    VerticalLayoutGroupHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New VerticalLayoutGroup Style", "", null);
		    }, null);	
		    
		    // -------------------------------------------------- //
            // Grid Layout Group
            // -------------------------------------------------- //
		    menu.AddItem(new GUIContent("Layout/Grid Layout Group"), false, delegate
		    {
			    GridLayoutGroupHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New GridLayoutGroup Style", "", null);
		    }, null);
		    
		    // -------------------------------------------------- //
            // Graphic Raycaster
            // -------------------------------------------------- //
		    menu.AddItem(new GUIContent("Event/Graphic Raycaster"), false, delegate
		    {
			    GraphicRaycasterHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New GraphicRaycaster Style", "", null);
		    }, null);
		    
		    // -------------------------------------------------- //
            // Camera
            // -------------------------------------------------- //
		    menu.AddItem(new GUIContent("Rendering/Camera"), false, delegate
		    {
			    CameraHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Camera Style", "", null);
		    }, null);
		    
		    /*<UIStylesTag(AddStyleContext)>*/
		    
		    
            menu.ShowAsContext();
        }


        /// <summary>
        /// Add component context menu
        /// </summary>
        private void AddComponentContext(int i)
        {
	        GenericMenu menu = new GenericMenu();
	        
	        // -------------------------------------------------- //
            // Group
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Group"), false, delegate
	        {
	        	int groupCount = 1;
	        	
	        	foreach (StyleComponent com in data.styles[i].styleComponents)
	        	{
	        		if (com.styleComponentType == StyleComponentType.Group)
		        		groupCount++;
	        	}
	        	
	        	string name = "Group " + groupCount.ToString();
	        	
		        AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.Group, name);
	        }, i);
	        
	        menu.AddSeparator("");
	        
            // -------------------------------------------------- //
            // Text
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("UI/Text"), false, delegate
            {
                AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.Text);
                data.styles[i].CheckForPathError();
            }, i);

            // -------------------------------------------------- //
            // Image
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("UI/Image"), false, delegate
            {
                AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.Image);
                data.styles[i].CheckForPathError();
            }, i);

            // -------------------------------------------------- //
            // Button
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("UI/Button"), false, delegate
            {
                AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.Button);
                data.styles[i].CheckForPathError();
            }, i);

            // -------------------------------------------------- //
            // InputField
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("UI/InputField"), false, delegate
            {
                AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.InputField);
                data.styles[i].CheckForPathError();
            }, i);

            // -------------------------------------------------- //
            // Dropdown
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("UI/Dropdown"), false, delegate
            {
                AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.Dropdown);
                data.styles[i].CheckForPathError();
            }, i);

            // -------------------------------------------------- //
            // ScrollRect
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("UI/ScrollRect"), false, delegate
            {
                AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.ScrollRect);
                data.styles[i].CheckForPathError();
            }, i);

            // -------------------------------------------------- //
            // Scrollbar
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("UI/Scrollbar"), false, delegate
            {
                AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.Scrollbar);
                data.styles[i].CheckForPathError();
            }, i);

            // -------------------------------------------------- //
            // Slider
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("UI/Slider"), false, delegate
            {
                AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.Slider);
                data.styles[i].CheckForPathError();
            }, i);

            // -------------------------------------------------- //
            // Toggle
            // -------------------------------------------------- //
            menu.AddItem(new GUIContent("UI/Toggle"), false, delegate
            {
                AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.Toggle);
                data.styles[i].CheckForPathError();
            }, i);
	        
	        // -------------------------------------------------- //
            // Rect transform
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Layout/Rect Transform"), false, delegate
	        {
		        AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.RectTransform);
		        data.styles[i].CheckForPathError();
	        }, i);
	        
	        // -------------------------------------------------- //
            // Canvas
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Layout/Canvas"), false, delegate
	        {
		        AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.Canvas);
		        data.styles[i].CheckForPathError();
	        }, i);
	        
	        // -------------------------------------------------- //
            // Canvas Scaler
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Layout/Canvas Scaler"), false, delegate
	        {
		        AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.CanvasScaler);
		        data.styles[i].CheckForPathError();
	        }, i);
	        
	        // -------------------------------------------------- //
            // CanvasGroup
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Layout/Canvas Group"), false, delegate
	        {
		        AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.CanvasGroup);
		        data.styles[i].CheckForPathError();
	        }, i);
	        
	        // -------------------------------------------------- //
            // Layout Element
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Layout/Layout Element"), false, delegate
	        {
		        AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.LayoutElement);
		        data.styles[i].CheckForPathError();
	        }, i);
	        
	        // -------------------------------------------------- //
            // Content Size Fitter
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Layout/Content Size Fitter"), false, delegate
	        {
		        AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.ContentSizeFitter);
		        data.styles[i].CheckForPathError();
	        }, i);
	        
	        // -------------------------------------------------- //
            // Aspect Ratio Fitter
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Layout/Aspect Ratio Fitter"), false, delegate
	        {
		        AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.AspectRatioFitter);
		        data.styles[i].CheckForPathError();
	        }, i);
	        
	        // -------------------------------------------------- //
            // Horizontal Layout Group
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Layout/Horizontal Layout Group"), false, delegate
	        {
		        AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.HorizontalLayoutGroup);
		        data.styles[i].CheckForPathError();
	        }, i);
	        
	        // -------------------------------------------------- //
            // Vertical Layout Group
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Layout/Vertical Layout Group"), false, delegate
	        {
		        AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.VerticalLayoutGroup);
		        data.styles[i].CheckForPathError();
	        }, i);
	        
	        // -------------------------------------------------- //
            // Grid Layout Group
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Layout/Grid Layout Group"), false, delegate
	        {
		        AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.GridLayoutGroup);
		        data.styles[i].CheckForPathError();
	        }, i);
	        
	        // -------------------------------------------------- //
            // Graphic Raycaster
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Event/Graphic Raycaster"), false, delegate
	        {
		        AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.GraphicRaycaster);
		        data.styles[i].CheckForPathError();
	        }, i);
	        
	        // -------------------------------------------------- //
            // Camera
            // -------------------------------------------------- //
	        menu.AddItem(new GUIContent("Rendering/Camera"), false, delegate
	        {
		        AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.Camera);
		        data.styles[i].CheckForPathError();
	        }, i);
	        	        
	        // -------------------------------------------------- //
            // Custom
            // -------------------------------------------------- //
	        menu.AddSeparator("");
	        menu.AddItem(new GUIContent("Custom"), false, delegate
	        {
		        AddComponentToStyle(UIStylesDatabase.styleData, i, StyleComponentType.Custom);
		        data.styles[i].CheckForPathError();
	        }, i);	       	        
	        
	        /*<UIStylesTag(AddComponentContext)>*/
	        
	        
            menu.ShowAsContext();
        }

        /// <summary>
        /// Add Component to a style by its index
        /// </summary>
        public static void AddComponentToStyle(StyleDataFile data, int styleIndex, StyleComponentType type, string name = "")
        {
            StyleComponent values = new StyleComponent(data);
            values.name = string.IsNullOrEmpty(name) ? name + " " + type.ToString() : name;
            values.styleComponentType = type;
	        data.styles[styleIndex].AddComponent(values);
	        
	        if (type == StyleComponentType.Group)
		        values.groupID = data.styles[styleIndex].GetNewGroupID();
	        
            UIStylesDatabase.Save();
        }


        /// <summary>
        /// Add a component to a style and copy from component
        /// </summary>
        public static void AddComponentToStyleAndSetValuesFromComponent(StyleDataFile data, int styleIndex, StyleComponentType type, string name, Component com)
        {
            StyleComponent values = new StyleComponent(data);
            values.name = string.IsNullOrEmpty(name) ? name + " " + type.ToString() : name;
            values.styleComponentType = type;

            if (type == StyleComponentType.Text)
                values.text = TextHelper.SetValuesFromComponent(com, false);
	        
            else if (type == StyleComponentType.Image)
                values.image = ImageHelper.SetValuesFromComponent(com, false);
	        
            else if (type == StyleComponentType.Button)
                values.button = ButtonHelper.SetValuesFromComponent(com);
	        
            else if (type == StyleComponentType.InputField)
                values.inputField = InputFieldHelper.SetValuesFromComponent(com);
	        
            else if (type == StyleComponentType.Dropdown)
                values.dropdown = DropdownHelper.SetValuesFromComponent(com);
	        
            else if (type == StyleComponentType.ScrollRect)
                values.scrollRect = ScrollRectHelper.SetValuesFromComponent(com);
	        
            else if (type == StyleComponentType.Scrollbar)
                values.scrollbar = ScrollbarHelper.SetValuesFromComponent(com);
	        
            else if (type == StyleComponentType.Slider)
                values.slider = SliderHelper.SetValuesFromComponent(com);
	        
            else if (type == StyleComponentType.Toggle)
	            values.toggle = ToggleHelper.SetValuesFromComponent(com);
	        
            else if (type == StyleComponentType.RectTransform)
	            values.rectTransform = RectTransformHelper.SetValuesFromComponent(com);
	        
	        else if (type == StyleComponentType.Camera)
	            values.camera = CameraHelper.SetValuesFromComponent(com);
	        
	        else if (type == StyleComponentType.CanvasGroup)
	            values.canvasGroup = CanvasGroupHelper.SetValuesFromComponent(com);
	        
	        else if (type == StyleComponentType.LayoutElement)
	            values.layoutElement = LayoutElementHelper.SetValuesFromComponent(com);
	        
	        else if (type == StyleComponentType.ContentSizeFitter)
	            values.contentSizeFitter = ContentSizeFitterHelper.SetValuesFromComponent(com);
	        
	        else if (type == StyleComponentType.AspectRatioFitter)
	            values.aspectRatioFitter = AspectRatioFitterHelper.SetValuesFromComponent(com);
	        
	        else if (type == StyleComponentType.HorizontalLayoutGroup)
	            values.horizontalLayoutGroup = HorizontalLayoutGroupHelper.SetValuesFromComponent(com);
	        
	        else if (type == StyleComponentType.VerticalLayoutGroup)
	            values.verticalLayoutGroup = VerticalLayoutGroupHelper.SetValuesFromComponent(com);
	        
	        else if (type == StyleComponentType.GridLayoutGroup)
	            values.gridLayoutGroup = GridLayoutGroupHelper.SetValuesFromComponent(com);
	        
	        else if (type == StyleComponentType.Canvas)
	            values.canvas = CanvasHelper.SetValuesFromComponent(com);
	        
	        else if (type == StyleComponentType.CanvasScaler)
	            values.canvasScaler = CanvasScalerHelper.SetValuesFromComponent(com);
	        
	        else if (type == StyleComponentType.GraphicRaycaster)
	            values.graphicRaycaster = GraphicRaycasterHelper.SetValuesFromComponent(com);
	        
	        /*<UIStylesTag(AddComponentToStyleAndSetValuesFromComponent)>*/
	        
            data.styles[styleIndex].AddComponent(values);

            UIStylesDatabase.Save();
        }


        /// <summary>
        /// Updates all scenes.
        /// </summary>
        private void UpdateAllScenes(bool allCategoies, ApplyMode applyMode)
        {
            this.allCategoies = allCategoies;

            List<string> paths = new List<string>();

            // In build 
            if (UIStylesDatabase.styleData.preferenceData.findSceneMode == UIStyles.PreferenceValues.FindSceneMode.ScenesInBuild)
            {

                foreach (EditorBuildSettingsScene s in UnityEditor.EditorBuildSettings.scenes)
                    paths.Add(s.path);
            }

            // In project
            else
            {
                string folderName = Application.dataPath + (UIStylesDatabase.styleData.preferenceData.findSceneMode == UIStyles.PreferenceValues.FindSceneMode.Project ? "/" : "/" + UIStylesDatabase.styleData.preferenceData.sceneFolder);
                DirectoryInfo dirInfo = new DirectoryInfo(folderName);
                FileInfo[] allFileInfos = dirInfo.GetFiles("*.unity", SearchOption.AllDirectories);

                foreach (var fileInfo in allFileInfos)
                    paths.Add(@fileInfo.FullName);
            }

            scenePaths = paths;
            sceneStartCount = scenePaths.Count;
            returnToScene = SceneManager.GetActiveScene().path;

            updateAllScenesApplyMode = applyMode;
            loopStatge = 0;
            currentSceneCount = 1;
            yieldLoop = 100;
            updatingAllScenes = true;
            showProgressBar = true;
            cancelUpdateInAllScenes = false;
        }


        private void UpdateAllScenesCheck()
        {
            if (cancelUpdateInAllScenes)
            {
                updatingAllScenes = false;
                EditorSceneManager.OpenScene(returnToScene);
            }
            else
            {
                if (loopStatge == 0)
                {
                    //Debug.Log ( "stage 0" );

                    loopStatge = 1;
                    EditorSceneManager.OpenScene(scenePaths[0]);
                }
                else if (loopStatge == 1)
                {
                    //Debug.Log ( "stage 1" );

                    if (yieldLoop > 0)
                        yieldLoop--;
                    else
                        loopStatge = 2;
                }
                else if (loopStatge == 2)
                {
                    //Debug.Log ( "stage 2" );

                    for (int i = 0; i < data.styles.Count; i++)
                        if (allCategoies || data.styles[i].category == data.currentCategory)
                            StyleHelper.ApplyAllCategories(data, null, updateAllScenesApplyMode);

                    UIStylesDatabase.Save();
	                EditorSceneManager.SaveOpenScenes();
	                Save (data);
                    scenePaths.RemoveAt(0);
                    loopStatge = 3;
                }
                else if (loopStatge == 3)
                {
                    //Debug.Log ( "stage 3" );

                    yieldLoop = 100;
                    loopStatge = 4;
                }
                else if (loopStatge == 4)
                {
                    //Debug.Log ( "stage 4" );

                    if (yieldLoop > 0)
                        yieldLoop--;
                    else
                    {
                        loopStatge = 0;
                        currentSceneCount++;

                        if (scenePaths.Count == 0)
                        {
                            //Debug.Log ( "done" );
                            updatingAllScenes = false;
                            EditorSceneManager.OpenScene(returnToScene);
                        }
                    }
                }
            }
        }


        private static float Progress(int i, int length)
        {
            int V = 1;
            int D = length;
            float P2 = (float)V / D;

            return i * P2;
        }

        private string AbsolutePath(string path)
        {
            if (path.StartsWith(Application.dataPath))
            {
                path = "Assets" + path.Substring(Application.dataPath.Length);
            }

            return path;
        }



        private void CloseAllStyles()
        {
            if (data.preferenceData.oneStyleOpenAtOnce)
            {
                for (int i = 0; i < data.styles.Count; i++)
                    if (data.styles[i].category == data.currentCategory)
                        data.styles[i].open = false;
            }
        }

        private void CloseAllComponents(int index)
        {
            if (data.preferenceData.oneComponentOpenAtOnce)
            {
                for (int i = 0; i < data.styles[index].styleComponents.Count; i++)
	                if (data.styles[index].styleComponents[i].styleComponentType != StyleComponentType.Group)
		                data.styles[index].styleComponents[i].open = false;
            }
        }

        /// <summary>
        /// Check for Multiple find by names within the categories,
        /// </summary>
        private void CheckMultipleCategories()
        {
            checkStyles = false;

            // Turn all off
            for (int i = 0; i < data.styles.Count; i++)
                data.styles[i].findByNameExists = false;

            if (!UIStylesDatabase.styleData.preferenceData.disableFindByNameWarning)
            {
                Dictionary<string, Style> findByNames = new Dictionary<string, Style>();
                foreach (Style s in data.preferenceData.allowMultipleFindByNameInSeparateCategories ? StyleHelper.GetStylesAssignedToCategory(UIStylesDatabase.styleData, data.currentCategory) : data.styles)
                {
                    if (findByNames.ContainsKey(s.findByName))
                    {
                        s.findByNameExists = true;
                        findByNames[s.findByName].findByNameExists = true;
                    }
                    else
                    {
                        findByNames.Add(s.findByName, s);
                        s.findByNameExists = false;
                    }
                }
            }
        }
	    
	    
	    

        /// <summary>
        /// Draw the window
        /// </summary>
        private void OnGUI()
	    {
            if (checkStyles && data != null)
            {
                CheckMultipleCategories();
            }

            if (updatingAllScenes)
            {
                UpdateAllScenesCheck();
                Repaint();
            }

            DrawHeader();

            if (UIStylesDatabase.styleData != null)
            {
                DrawLoadingBar();

                DrawStyles();
            }
        }

        private void DrawHeader()
        {
            GUILayout.BeginHorizontal(EditorHelper.toolbarSkin);
            {
                if (GUILayout.Button("+", EditorHelper.buttonSkin, GUILayout.Width(30)))
                {
                    GUI.FocusControl(null);
                    addCategoryName = "New Category";
                    addCategory = true;
                }

                if (UIStylesDatabase.styleData != null)
                {
                    if (renameCurrentCategory)
                    {
                        GUI.SetNextControlName("Rename Category Field");

                        newCategoryName = EditorGUILayout.TextField(newCategoryName);

                        if (DataHelper.highlightRenameField)
                        {
                            GUI.FocusControl("Rename Category Field");
                            DataHelper.highlightRenameField = false;
                        }

                        if (GUILayout.Button("Done", EditorHelper.buttonSkin, GUILayout.Width(80)))
                        {
                            for (int i = 0; i < data.styles.Count; i++)
                            {
                                if (data.styles[i].category == data.currentCategory)
                                {
                                    data.styles[i].category = newCategoryName;
                                }
                            }

                            // Find and rename the currentCategory
                            for (int i = 0; i < data.categories.Count; i++)
                            {
                                if (data.categories[i] == data.currentCategory)
                                {
                                    data.categories[i] = newCategoryName;
                                    break;
                                }
                            }

                            data.currentCategory = newCategoryName;

                            renameCurrentCategory = false;

                            // Remove any categories with the same name
                            List<string> found = new List<string>();
                            foreach (string str in data.categories)
                                if (str == newCategoryName)
                                    found.Add(str);

                            for (int f = 0; f < found.Count; f++)
                                if (f > 0)
                                    data.categories.Remove(found[f]);

                            //foreach (string str in data.categories)
                            //{
                            //	Debug.Log (str);
                            //}

                            UIStylesDatabase.Save();
                        }
                    }

                    // Category
                    else if (GUILayout.Button(string.IsNullOrEmpty(data.currentCategory) ? "No Categories!" : data.currentCategory, EditorHelper.popUpSkin, GUILayout.MinWidth(30)))
                    {
                        if (Event.current.button == 0)
                        {
                            CategoryDropdown();
                        }
                        else if (Event.current.button == 1)
                        {
                            CategoryContext();
                        }
                    }
                }

                GUILayout.Space(5);

                int w = GUI.GetNameOfFocusedControl() == "SeachField" ? 180 : 30;
                GUI.SetNextControlName("SeachField");
                searchString = GUILayout.TextField(searchString, EditorHelper.SeachFieldSkin, GUILayout.MinWidth(w));

                if (GUILayout.Button("", EditorHelper.SeachFieldCancelSkin))
                {
                    // Remove focus if cleared
                    searchString = string.Empty;
                    GUI.FocusControl(null);
                }

                GUILayout.Space(5);


                if (GUILayout.Button("Menu", EditorHelper.dropdownSkin, GUILayout.Width(50)))
                {
                    MenuContent();
                }
            }
            GUILayout.EndHorizontal();

            if (UIStylesDatabase.styleData != null && addCategory)
            {
                GUILayout.BeginHorizontal(EditorHelper.toolbarSkin);
                {
                    addCategoryName = EditorGUILayout.TextField(addCategoryName);

                    if (GUILayout.Button("Add", EditorHelper.buttonSkin, GUILayout.Width(100)))
                    {
                        if (data.categories.Contains(addCategoryName))
                        {
                            EditorUtility.DisplayDialog("Warning", "Category name already exists please choose another", "OK");
                        }
                        else
                        {
                            data.categories.Add(addCategoryName);
                            data.currentCategory = addCategoryName;
                            addCategory = false;
                            UIStylesDatabase.Save();
                        }
                    }
                    if (GUILayout.Button("X", EditorHelper.buttonSkin, GUILayout.Width(30)))
                    {
                        addCategory = false;
                    }
                }
                GUILayout.EndHorizontal();

                EditorGUILayout.HelpBox("To add a new category enter its name above and click add", MessageType.Info);

                GUILayout.Space(10);
            }

            if (UIStylesDatabase.styleData == null)
            {
                EditorGUILayout.HelpBox("Create some settings from the top right \"Menu\" button", MessageType.Info);
            }
        }
	    
	    static bool dragStyle;


        private void DrawLoadingBar()
        {
            if (showProgressBar)
            {
                float value = Progress(sceneStartCount, scenePaths.Count) / sceneStartCount;
                string valueText = updatingAllScenes ? (value * 100).ToString("00") + "%" : "100%";

                if (valueText == "Infinity%")
                {
                    valueText = "100%";
                }

                Rect rect = GUILayoutUtility.GetLastRect();
                rect = new Rect(rect.x, rect.y + 18, rect.width - 36, rect.height);

                EditorGUI.ProgressBar(rect, value, valueText);

                rect = new Rect(rect.x + rect.width + 4, rect.y - 2, 26, 18);

                rect = GUILayoutUtility.GetLastRect();
                rect = new Rect(rect.width - 37, rect.y + 18, 36, rect.height);

                if (GUI.Button(rect, "X", EditorHelper.buttonSkin))
                {
                    if (updatingAllScenes)
                        cancelUpdateInAllScenes = true;

                    else showProgressBar = false;
                }

                GUILayout.Space(18);
                string footer = cancelUpdateInAllScenes ? "Canceled" : scenePaths.Count > 0 ? "Scene: " + currentSceneCount + "/" + sceneStartCount + "    " + AbsolutePath(scenePaths[0]) : "Done";
                EditorGUILayout.LabelField(footer);
            }
        }


	    
        /// <summary>
        /// Draw the styles
        /// </summary>
        private void DrawStyles()
	    {
            GUILayout.Space(5);

            if (data.categories.Count > 0)
            {
                bool addStyleButton = false;
                draggedObjects_Styles = new Object[0];
                EditorHelper.StandardSeparatorCentreTitleAndDrag(10, "Add Style", "", ref addStyleButton, ref draggedObjects_Styles);

                if (addStyleButton)
                {
                    AddStyleContext();
                }

                else
                {
                    // -------------------------------------------------- //
                    // Drag style
                    // -------------------------------------------------- //
                    bool gotDraggedObj = false;
                    foreach (Object draggedObj in draggedObjects_Styles)
                    {
                        if (draggedObj is GameObject)
                        {
	                        gotDraggedObj = true;
	                        objectToUpdate = (GameObject)draggedObj;
	                        StyleHelper.CopyObjectToStyle (data, (GameObject)draggedObj);
                        }
                        else if (draggedObj is Text)
                        {
                            gotDraggedObj = true;
                            TextHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Text Style", "", null, false);
                        }
                        else if (draggedObj is Image || draggedObj is RawImage)
                        {
                            gotDraggedObj = true;
                            ImageHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Image Style", "", null, false);
                        }
                        else if (draggedObj is Button)
                        {
                            gotDraggedObj = true;
                            ButtonHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Button Style", "", null, false);
                        }
                        else if (draggedObj is InputField)
                        {
                            gotDraggedObj = true;
                            InputFieldHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New InputField Style", "", null, false);
                        }
                        else if (draggedObj is Dropdown)
                        {
                            gotDraggedObj = true;
                            DropdownHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Dropdown Style", "", null, false);
                        }
                        else if (draggedObj is ScrollRect)
                        {
                            gotDraggedObj = true;
                            ScrollRectHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Scroll View Style", "", null, false);
                        }
                        else if (draggedObj is Scrollbar)
                        {
                            gotDraggedObj = true;
                            ScrollbarHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Scrollbar Style", "", null, false);
                        }
                        else if (draggedObj is Slider)
                        {
                            gotDraggedObj = true;
                            SliderHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Slider Style", "", null, false);
                        }
                        else if (draggedObj is Toggle)
                        {
                            gotDraggedObj = true;
                            ToggleHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Toggle Style", "", null, false);
                        }
                        else if (draggedObj is RectTransform)
                        {
	                        gotDraggedObj = true;
	                        RectTransformHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Rect Style", "", null);
                        }
	                    
	                    else if (draggedObj is Camera)
                        {
	                        gotDraggedObj = true;
	                        CameraHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Camera Style", "", null);
                        }

	                    else if (draggedObj is CanvasGroup)
                        {
	                        gotDraggedObj = true;
	                        CanvasGroupHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New CanvasGroup Style", "", null);
                        }
	                    
	                    else if (draggedObj is LayoutElement)
                        {
	                        gotDraggedObj = true;
	                        LayoutElementHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New LayoutElement Style", "", null);
                        }
	                    
	                    else if (draggedObj is ContentSizeFitter)
                        {
	                        gotDraggedObj = true;
	                        ContentSizeFitterHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New ContentSizeFitter Style", "", null);
                        }
	                    
	                    else if (draggedObj is AspectRatioFitter)
                        {
	                        gotDraggedObj = true;
	                        AspectRatioFitterHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New AspectRatioFitter Style", "", null);
                        }
	                    
	                    else if (draggedObj is HorizontalLayoutGroup)
                        {
	                        gotDraggedObj = true;
	                        HorizontalLayoutGroupHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New HorizontalLayoutGroup Style", "", null);
                        }
	                    
	                    else if (draggedObj is VerticalLayoutGroup)
                        {
	                        gotDraggedObj = true;
	                        VerticalLayoutGroupHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New VerticalLayoutGroup Style", "", null);
                        }
	                    
	                    else if (draggedObj is GridLayoutGroup)
                        {
	                        gotDraggedObj = true;
	                        GridLayoutGroupHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New GridLayoutGroup Style", "", null);
                        }
	                    
	                    else if (draggedObj is Canvas)
                        {
	                        gotDraggedObj = true;
	                        CanvasHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New Canvas Style", "", null);
                        }
	                    
	                    else if (draggedObj is CanvasScaler)
                        {
	                        gotDraggedObj = true;
	                        CanvasScalerHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New CanvasScaler Style", "", null);
                        }
	                    
	                    else if (draggedObj is GraphicRaycaster)
                        {
	                        gotDraggedObj = true;
	                        GraphicRaycasterHelper.CreateStyle(UIStylesDatabase.styleData, data.currentCategory, "New GraphicRaycaster Style", "", null);
                        }
	                    
	                    /*<UIStylesTag(DragObject)>*/
                    }

                    if (gotDraggedObj)
                    {
                        return; // to stop gui error!
                    }
                }
            }
            else if (!addCategory)
            {
                EditorGUILayout.HelpBox("No Categories! Create Categories from the top left \"+\" button", MessageType.Info);
            }

            GUILayout.Space(10);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);
		    {
			    
			    GUILayout.BeginHorizontal();
			    {
				    EditorGUILayout.LabelField("      Style Name", GUILayout.MinWidth(10));
				    
				    GUILayout.Space(10);
				    				    
				    EditorGUILayout.LabelField("|  State", GUILayout.Width(70));
				    
				    GUILayout.Space(10);
				    
				    EditorGUILayout.LabelField("|  ID", GUILayout.Width(40));
				    
				    GUILayout.Space(10);
				    
				    EditorGUILayout.LabelField("|  Apply", GUILayout.Width(75));
			    }
			    GUILayout.EndHorizontal();
			    
			    EditorHelper.StandardSeparator(0);
			    GUILayout.Space(10);
			    
                // Draw Styles
				for (int s = 0; s < data.styles.Count; s++)
                {
                    // Get the name as lower case, this is better for search
                    string nameToLower = data.styles[s].name.ToLower();
                    if (data.styles[s].category == data.currentCategory && (string.IsNullOrEmpty(searchString) || nameToLower.Contains(searchString.ToLower())))
                    {
                        GUI.color = data.styles[s].open ? EditorHelper.highlightedBackgroundColor : Color.white;
                        GUILayout.BeginVertical(EditorHelper.StandardPanel(6));
                        {
                            GUI.color = Color.white;
                            GUILayout.BeginHorizontal();
                            {
                                if (data.styles[s].rename)
                                {
                                    // -------------------------------------------------- //
                                    // Rename
                                    // -------------------------------------------------- //
                                    GUI.SetNextControlName("Rename Style Field");

                                    data.styles[s].name = EditorGUILayout.TextField("", data.styles[s].name);

                                    if (DataHelper.highlightRenameField)
                                    {
                                        GUI.FocusControl("Rename Style Field");
                                        DataHelper.highlightRenameField = false;
                                    }

                                    GUILayout.Space(5);

                                    if (GUILayout.Button("Done", EditorHelper.buttonSkin, GUILayout.Width(80)))
                                    {
                                        data.styles[s].rename = false;

	                                    if (string.IsNullOrEmpty(data.styles[s].findByName) || objectToUpdate != null)
                                        {
                                            data.styles[s].findByName = data.styles[s].name;
	                                        checkStyles = true;
	                                        
	                                        if (objectToUpdate != null)
	                                        {
	                                        	StyleHelper.AddFindByName(data, objectToUpdate, data.styles[s].findByName);
	                                        	objectToUpdate = null;
	                                        }
                                        }

                                        UIStylesDatabase.Save();
                                    }
                                }
                                else
                                {
                                	
                                	// Error
                                	if (data.styles[s].findByNameExists)
                                	{
	                                	GUILayout.BeginVertical(GUILayout.Width(36));
	                                	{
		                                	GUILayout.Space(-5);
		                                	GUILayout.Box("", EditorHelper.ErrorSymbol, GUILayout.Height(16));
	                                	}
	                                	GUILayout.EndVertical();
                                	}
                                	
                                	// -------------------------------------------------- //
	                                // Name
	                                // -------------------------------------------------- //	                                
	                                
	                                GUILayout.BeginVertical();
	                                {
		                                
		                                GUILayout.BeginHorizontal();
		                                {
		                                	if (GUILayout.Button("", EditorHelper.buttonSkin))
		                                	{
			                                	if (Event.current.button == 0)
			                                	{
				                                	if (!data.styles[s].open)
					                                	CloseAllStyles();
				                                	
				                                	data.styles[s].open = !data.styles[s].open;
			                                	}
			                                	if (Event.current.button == 1)
			                                	{
				                                	StyleContext(s);
			                                	}
		                                	}
		                                	
			                                if (GUILayout.Button(new GUIContent("Apply", UIStylesDocumentation.applyStyle), EditorHelper.buttonSkin, GUILayout.Width(60)))
			                                {
				                                ApplyContext(s);
			                                }
		                                }
		                                GUILayout.EndHorizontal();
		                                
		                                
		                                
		                                GUILayout.Space(-18);
		                                
		                                GUILayout.BeginHorizontal(EditorHelper.buttonSkin);
		                                {
			                                EditorGUILayout.LabelField(data.styles[s].open ? "▼  " + data.styles[s].name : "►  " + data.styles[s].name, GUILayout.MinWidth(10));
			                                			                                
			                                GUI.contentColor =
				                                data.styles[s].styleState == StyleState.Enable ? Color.white : Color.gray;
			                                
			                                string stateName = 
				                                data.styles[s].styleState == StyleState.Enable ? "Enabled" :  "Disabled";
			                                
			                                EditorGUILayout.LabelField("|  " + stateName, GUILayout.Width(80));
			                                
			                                GUI.contentColor = Color.white;
			                                
			                                EditorGUILayout.LabelField("|  " + data.styles[s].id.ToString(), GUILayout.Width(50));
			                                
			                                EditorGUILayout.LabelField("|     Apply", GUILayout.Width(60));
		                                }
		                                GUILayout.EndHorizontal();
	                                }
	                                GUILayout.EndVertical();	
                                }
                            }
	                        GUILayout.EndHorizontal();

                            if (data.styles[s].open)
                            {
                                // -------------------------------------------------- //
                                // Find by
                                // -------------------------------------------------- //
                                GUILayout.Space(5);

                                GUI.backgroundColor = Color.clear;
                                EditorGUILayout.LabelField(new GUIContent("   Find by name: (" + data.styles[s].findByName + ")", UIStylesDocumentation.findByName), EditorHelper.popUpSkin);
                                GUI.backgroundColor = Color.white;
                                GUILayout.BeginHorizontal();
                                {
                                    GUILayout.Space(14);
                                    EditorGUI.BeginDisabledGroup(!data.styles[s].renameFindBy);
                                    {
                                        data.styles[s].findByName = EditorGUILayout.TextField(new GUIContent("", UIStylesDocumentation.findByName), data.styles[s].findByName);
                                    }
                                    EditorGUI.EndDisabledGroup();

                                    if (GUILayout.Button(data.styles[s].renameFindBy ? new GUIContent("Done", "Finish editing the find by name") : new GUIContent("Edit", "Edit the find by name"), EditorHelper.buttonSkin, GUILayout.Width(80)))
                                    {
                                        GUI.FocusControl(null);

                                        if (data.styles[s].renameFindBy)
                                            checkStyles = true;

                                        data.styles[s].renameFindBy = !data.styles[s].renameFindBy;

                                        UIStylesDatabase.Save();
                                    }
                                    GUILayout.Space(14);
                                }
                                GUILayout.EndHorizontal();

                                // -------------------------------------------------- //
                                // Drop Area
                                // -------------------------------------------------- //
                                draggedObjects_FindBy = new UnityEngine.Object[0];
                                EditorHelper.DropArea(ref draggedObjects_FindBy);

                                if (draggedObjects_FindBy.Length > 0)
                                {
                                    foreach (Object o in draggedObjects_FindBy)
                                    {
                                        if (o is GameObject)
                                        {
                                            GameObject obj = (GameObject)o;
                                            StyleHelper.AddFindByName(UIStylesDatabase.styleData, obj, data.styles[s].findByName);
                                        }
                                    }
                                }

                                Event currentEvent = Event.current;
                                Rect contextRect;

                                contextRect = GUILayoutUtility.GetLastRect();
                                EditorGUI.DrawRect(contextRect, Color.clear);

                                Vector2 mousePos = currentEvent.mousePosition;

                                if (contextRect.Contains(mousePos) && Event.current.button == 1 && Event.current.type == EventType.MouseUp)
                                {
                                    FindByContext(s);
                                }

                                if (data.styles[s].findByNameExists)
                                    EditorGUILayout.HelpBox("Find by name already exists, Multiple find by names is not recommended", MessageType.Error);

                                bool button = false;
                                draggedObjects_Component = new Object[0];
                                EditorHelper.StandardSeparatorCentreTitleAndDrag(16, "Add Component", "", ref button, ref draggedObjects_Component);

                                if (button)
                                {
                                    AddComponentContext(s);
                                }

                                // -------------------------------------------------- //
                                // Drag Component
                                // -------------------------------------------------- //
                                bool gotDraggedObj = false;
                                foreach (Object draggedObj in draggedObjects_Component)
                                {
                                    if (draggedObj is Component)
                                    {
                                        gotDraggedObj = true;

                                        Component com = (Component)draggedObj;

                                        // -------------------------------------------------- //
                                        // Drag Component - Button
                                        // -------------------------------------------------- //
                                        if (draggedObj is Button)
                                        {
                                            AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.Button, "Button", com);
                                            data.styles[s].CheckForPathError();
                                        }

                                        // -------------------------------------------------- //
                                        // Drag Component - InputField
                                        // -------------------------------------------------- //
                                        else if (draggedObj is InputField)
                                        {
                                            AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.InputField, "InputField", com);
                                            data.styles[s].CheckForPathError();
                                        }

                                        // -------------------------------------------------- //
                                        // Drag Component - Dropdown
                                        // -------------------------------------------------- //
                                        else if (draggedObj is Dropdown)
                                        {
                                            AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.Dropdown, "Dropdown", com);
                                            data.styles[s].CheckForPathError();
                                        }

                                        // -------------------------------------------------- //
                                        // Drag Component - ScrollRect
                                        // -------------------------------------------------- //
                                        else if (draggedObj is ScrollRect)
                                        {
                                            AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.ScrollRect, "ScrollRect", com);
                                            data.styles[s].CheckForPathError();
                                        }

                                        // -------------------------------------------------- //
                                        // Drag Component - Scrollbar
                                        // -------------------------------------------------- //
                                        else if (draggedObj is Scrollbar)
                                        {
                                            AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.Scrollbar, "Scrollbar", com);
                                            data.styles[s].CheckForPathError();
                                        }

                                        // -------------------------------------------------- //
                                        // Drag Component - Slider
                                        // -------------------------------------------------- //
                                        else if (draggedObj is Slider)
                                        {
                                            AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.Slider, "Slider", com);
                                            data.styles[s].CheckForPathError();
                                        }

                                        // -------------------------------------------------- //
                                        // Drag Component - Toggle
                                        // -------------------------------------------------- //
                                        else if (draggedObj is Toggle)
                                        {
                                            AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.Toggle, "Toggle", com);
                                            data.styles[s].CheckForPathError();
                                        }

                                        // -------------------------------------------------- //
                                        // Drag Component - Image/Raw
                                        // -------------------------------------------------- //
                                        else if (draggedObj is Image || draggedObj is RawImage)
                                        {
                                            AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.Image, "Image", com);
                                            data.styles[s].CheckForPathError();
                                        }

                                        // -------------------------------------------------- //
                                        // Drag Component - Text
                                        // -------------------------------------------------- //
                                        else if (draggedObj is Text)
                                        {
                                            AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.Text, "Text", com);
                                            data.styles[s].CheckForPathError();
                                        }
	                                    
	                                	// -------------------------------------------------- //
                                        // Drag Component - Rect
                                        // -------------------------------------------------- //
                                        else if (draggedObj is RectTransform)
                                        {
	                                        AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.RectTransform, "Rect", com);
	                                        data.styles[s].CheckForPathError();
                                        }
                                        
                                        // -------------------------------------------------- //
                                        // Drag Component - Camera
                                        // -------------------------------------------------- //
                                        else if (draggedObj is Camera)
                                        {
	                                        AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.Camera, "Camera", com);
	                                        data.styles[s].CheckForPathError();
                                        }
                                        
                                        // -------------------------------------------------- //
                                        // Drag Component - CanvasGroup
                                        // -------------------------------------------------- //
                                        else if (draggedObj is CanvasGroup)
                                        {
	                                        AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.CanvasGroup, "CanvasGroup", com);
	                                        data.styles[s].CheckForPathError();
                                        }
                                        
                                        // -------------------------------------------------- //
                                        // Drag Component - LayoutElement
                                        // -------------------------------------------------- //
                                        else if (draggedObj is LayoutElement)
                                        {
	                                        AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.LayoutElement, "LayoutElement", com);
	                                        data.styles[s].CheckForPathError();
                                        }
                                        
                                        // -------------------------------------------------- //
                                        // Drag Component - ContentSizeFitter
                                        // -------------------------------------------------- //
                                        else if (draggedObj is ContentSizeFitter)
                                        {
	                                        AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.ContentSizeFitter, "ContentSizeFitter", com);
	                                        data.styles[s].CheckForPathError();
                                        }
                                        
                                        // -------------------------------------------------- //
                                        // Drag Component - AspectRatioFitter
                                        // -------------------------------------------------- //
                                        else if (draggedObj is AspectRatioFitter)
                                        {
	                                        AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.AspectRatioFitter, "AspectRatioFitter", com);
	                                        data.styles[s].CheckForPathError();
                                        }
                                        
                                        // -------------------------------------------------- //
                                        // Drag Component - HorizontalLayoutGroup
                                        // -------------------------------------------------- //
                                        else if (draggedObj is HorizontalLayoutGroup)
                                        {
	                                        AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.HorizontalLayoutGroup, "HorizontalLayoutGroup", com);
	                                        data.styles[s].CheckForPathError();
                                        }
                                        
                                        // -------------------------------------------------- //
                                        // Drag Component - VerticalLayoutGroup
                                        // -------------------------------------------------- //
                                        else if (draggedObj is VerticalLayoutGroup)
                                        {
	                                        AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.VerticalLayoutGroup, "VerticalLayoutGroup", com);
	                                        data.styles[s].CheckForPathError();
                                        }
                                        
                                        // -------------------------------------------------- //
                                        // Drag Component - GridLayoutGroup
                                        // -------------------------------------------------- //
                                        else if (draggedObj is GridLayoutGroup)
                                        {
	                                        AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.GridLayoutGroup, "GridLayoutGroup", com);
	                                        data.styles[s].CheckForPathError();
                                        }
                                        
                                        // -------------------------------------------------- //
                                        // Drag Component - Canvas
                                        // -------------------------------------------------- //
                                        else if (draggedObj is Canvas)
                                        {
	                                        AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.Canvas, "Canvas", com);
	                                        data.styles[s].CheckForPathError();
                                        }
                                        
                                        // -------------------------------------------------- //
                                        // Drag Component - CanvasScaler
                                        // -------------------------------------------------- //
                                        else if (draggedObj is CanvasScaler)
                                        {
	                                        AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.CanvasScaler, "CanvasScaler", com);
	                                        data.styles[s].CheckForPathError();
                                        }
                                        
                                        // -------------------------------------------------- //
                                        // Drag Component - GraphicRaycaster
                                        // -------------------------------------------------- //
                                        else if (draggedObj is GraphicRaycaster)
                                        {
	                                        AddComponentToStyleAndSetValuesFromComponent(data, s, StyleComponentType.GraphicRaycaster, "GraphicRaycaster", com);
	                                        data.styles[s].CheckForPathError();
                                        }
                                        
                                        /*<UIStylesTag(DragComponent)>*/
                                    }
                                }

                                if (!gotDraggedObj)
                                {
                                    // -------------------------------------------------- //
                                    // Draw Component Values
                                    // -------------------------------------------------- //
	                                
	                                GUILayout.BeginHorizontal();
	                                {
	                                	EditorGUILayout.LabelField("Object Name", GUILayout.MinWidth(10));
	                                		                                	
	                                	EditorGUILayout.LabelField("|  Type", GUILayout.Width(145));
	                                		                                	
	                                	EditorGUILayout.LabelField("|  State", GUILayout.Width(100));
	                                	
	                                	EditorGUILayout.LabelField("|  ID", GUILayout.Width(50));
	                                }
	                                GUILayout.EndHorizontal();
	                               
	                                EditorHelper.StandardSeparator(0);
	                                GUILayout.Space(10);
	                                
	                                // Loop all components
									for (int c = 0; c < data.styles[s].styleComponents.Count; c++)
                                    {
										// Dont draw if the component is in a group
										if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.Group || (data.styles[s].styleComponents[c].styleComponentType != StyleComponentType.Group && data.styles[s].styleComponents[c].groupID == 0))
										{
											DrawComponent (s, c);
										}
										
										
										// Draw the group
										if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.Group)
										{
											GUILayout.BeginHorizontal();
											{
												GUILayout.Space(20);

												GUILayout.BeginVertical();
												{
													if (data.styles[s].styleComponents[c].open)
													{
														GUILayout.Space(10);
														for (int g = 0; g < data.styles[s].styleComponents[c].group.componentIDs.Count; g++) 
														{
															DrawComponent (s, data.styles[s].styleComponents.IndexOf(StyleHelper.GetComponent(data, data.styles[s].styleComponents[c].group.componentIDs[g])));
														}
													}
												}
												GUILayout.EndVertical();
											}
											GUILayout.EndHorizontal();
										}
                                    }
                                }
                            }
                            else
                            {
                                // -------------------------------------------------- //
                                // Find by name Drop Area
                                // -------------------------------------------------- //								
                                draggedObjects_FindBy = new UnityEngine.Object[0];
                                EditorHelper.DropArea(ref draggedObjects_FindBy);

                                if (draggedObjects_FindBy.Length > 0)
                                {
                                    foreach (Object o in draggedObjects_FindBy)
                                    {
                                        if (o is GameObject)
                                        {
                                            GameObject obj = (GameObject)o;
                                            StyleHelper.AddFindByName(UIStylesDatabase.styleData, obj, data.styles[s].findByName);
                                        }
                                    }
                                }
                            }
                        }
                        GUILayout.EndVertical();
                    }
                }
            }
            GUILayout.EndScrollView();

            // Save
            if (GUI.changed)
                needToSave = true;

            if (needToSave && Event.current.type == EventType.MouseUp)
            {
                needToSave = false;
                UIStylesDatabase.Save();
            }
	    }
	    
	    
	    
	    public void DrawComponent (int s, int c)
	    {
	    	
	    	EditorGUI.indentLevel = 0;
		    
		    GUILayout.BeginHorizontal();
		    {
			    if (data.styles[s].styleComponents[c].rename)
			    {
                    // -------------------------------------------------- //
                    // Rename
                    // -------------------------------------------------- //
				    
				    GUI.SetNextControlName("Rename Component Field");
				    
				    data.styles[s].styleComponents[c].name = EditorGUILayout.TextField("", data.styles[s].styleComponents[c].name);
				    
				    if (DataHelper.highlightRenameField)
				    {
					    GUI.FocusControl("Rename Component Field");
					    DataHelper.highlightRenameField = false;
				    }
				    
				    GUILayout.Space(5);
				    
				    if (GUILayout.Button("Done", EditorHelper.buttonSkin, GUILayout.Width(80)))
				    {
				    	if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.Group)
				    	{
				    		// Others components with the same name
				    		int nameCount = 0;
				    		
				    		foreach (StyleComponent com in data.styles[s].styleComponents)
				    		{
				    			if (data.styles[s].styleComponents[c].name == com.name)
					    			nameCount ++;
				    		}
				    		
				    		if (nameCount > 1)
				    		{
				    			EditorUtility.DisplayDialog("Warning", "This name is already taken. Please choose a different name.", "Ok");
				    		}
				    		else
				    		{
				    			data.styles[s].styleComponents[c].rename = false;
					    		UIStylesDatabase.Save();
				    		}
					    		
				    		
				    	}
					    else
					    {
						    data.styles[s].styleComponents[c].rename = false;
						    UIStylesDatabase.Save();
					    }
					   
				    }
			    }
			    else
			    {
				    GUILayout.BeginVertical();
				    {
					    if (GUILayout.Button("", EditorHelper.buttonSkin))
					    {
						    if (Event.current.button == 0)
						    {
							    if (!data.styles[s].styleComponents[c].open)
								    CloseAllComponents(s);
							    
							    data.styles[s].styleComponents[c].open = !data.styles[s].styleComponents[c].open;
						    }
						    if (Event.current.button == 1)
						    {
							    currentIndex = c;
							    StyleComponentContext(s);
						    }
					    }
					    
					    GUILayout.Space(-18);
					    
					    GUILayout.BeginHorizontal();
					    {
						    if (data.styles[s].styleComponents[c].styleComponentType != StyleComponentType.Group)
						    {
							    if (data.styles[s].styleComponents[c].hasPathError)
							    {
								    GUILayout.BeginVertical(GUILayout.Width(10), GUILayout.Height(10));
								    {
									    GUILayout.Space(-5);
									    GUILayout.Box("", EditorHelper.ErrorSymbol);
								    }
								    GUILayout.EndVertical();
							    }
						    }
						    
						    GUILayout.BeginHorizontal(EditorHelper.buttonSkin);
						    {
							    EditorGUILayout.LabelField(data.styles[s].styleComponents[c].open ? "▼  " + data.styles[s].styleComponents[c].name : "►  " + data.styles[s].styleComponents[c].name, GUILayout.MinWidth(10));
							    
							    string type = data.styles[s].styleComponents[c].styleComponentType.ToString();
							    
							    if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.Image && data.styles[s].styleComponents[c].image.useAsMask)
								    type += " / Mask";
							    
							    EditorGUILayout.LabelField(" |  " + type, GUILayout.Width(145));
							    
							    if (data.styles[s].styleComponents[c].styleComponentType != StyleComponentType.Group)
							    {
								    GUI.contentColor =
									    data.styles[s].styleComponents[c].styleComponentState == StyleComponentState.Enable ? Color.white : 
									    data.styles[s].styleComponents[c].styleComponentState == StyleComponentState.Disable ? Color.gray : 
									    data.styles[s].styleComponents[c].styleComponentState == StyleComponentState.AlwaysAdd ? Color.white :
									    data.styles[s].styleComponents[c].styleComponentState == StyleComponentState.AlwaysRemove ? Color.yellow : Color.yellow;
								    
								    string stateName = 
									    data.styles[s].styleComponents[c].styleComponentState == StyleComponentState.Enable ? "Enabled  " : 
									    data.styles[s].styleComponents[c].styleComponentState == StyleComponentState.Disable ? "Disabled  " : 
									    data.styles[s].styleComponents[c].styleComponentState == StyleComponentState.AlwaysAdd ? "Enabled + " :
									    data.styles[s].styleComponents[c].styleComponentState == StyleComponentState.AlwaysRemove ? "Always Remove" : "Always Delete";
								    
								    EditorGUILayout.LabelField(" |  " + stateName, GUILayout.Width(100));
								    
								    GUI.contentColor = Color.white;
								    
								    EditorGUILayout.LabelField(" |  " + data.styles[s].styleComponents[c].id.ToString(), GUILayout.Width(50));
							    }
							    else
							    {
							    	GUILayout.Space(158);
							    }
						    }
						    GUILayout.EndHorizontal();
						    
					    }
					    GUILayout.EndHorizontal();
				    }
				    GUILayout.EndVertical();
			    }
		    }
		    GUILayout.EndHorizontal();
		    
		    if (data.styles[s].styleComponents[c].open)
		    {
			    bool checkError = false;
			    
                // -------------------------------------------------- //
                // Text
                // -------------------------------------------------- //
			    if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.Text)
				    UIStylesGUIText.DrawValues(data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
			    
                // -------------------------------------------------- //
                // Image
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.Image)
				    UIStylesGUIImage.DrawValues(data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
			    
                // -------------------------------------------------- //
                // Button
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.Button)
				    UIStylesGUIButton.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
			    
                // -------------------------------------------------- //
                // InputField
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.InputField)
				    UIStylesGUIInputField.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
			    
                // -------------------------------------------------- //
                // Dropdown
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.Dropdown)
				    UIStylesGUIDropdown.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
			    
                // -------------------------------------------------- //
                // ScrollRect
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.ScrollRect)
				    UIStylesGUIScrollRect.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
			    
                // -------------------------------------------------- //
                // Scrollbar
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.Scrollbar)
				    UIStylesGUIScrollbar.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
			    
                // -------------------------------------------------- //
                // Slider
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.Slider)
				    UIStylesGUISlider.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
			    
                // -------------------------------------------------- //
                // Toggle
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.Toggle)
				    UIStylesGUIToggle.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
			    
                // -------------------------------------------------- //
                // Custom
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.Custom)
				    UIStylesGUICustomComponent.DrawValues(data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
			    
                // -------------------------------------------------- //
                // Rect
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.RectTransform)
				    UIStylesGUIRectTransform.DrawValues(data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
				    				    
				// -------------------------------------------------- //
                // Camera
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.Camera)
				    UIStylesGUICamera.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
				    
				// -------------------------------------------------- //
                // CanvasGroup
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.CanvasGroup)
				    UIStylesGUICanvasGroup.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
				    
				// -------------------------------------------------- //
                // LayoutElement
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.LayoutElement)
				    UIStylesGUILayoutElement.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
				    
				// -------------------------------------------------- //
                // ContentSizeFitter
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.ContentSizeFitter)
				    UIStylesGUIContentSizeFitter.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
				    
				// -------------------------------------------------- //
                // AspectRatioFitter
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.AspectRatioFitter)
				    UIStylesGUIAspectRatioFitter.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
				    
				// -------------------------------------------------- //
                // HorizontalLayoutGroup
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.HorizontalLayoutGroup)
				    UIStylesGUIHorizontalLayoutGroup.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
				    
				// -------------------------------------------------- //
                // VerticalLayoutGroup
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.VerticalLayoutGroup)
				    UIStylesGUIVerticalLayoutGroup.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
				    
				// -------------------------------------------------- //
                // GridLayoutGroup
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.GridLayoutGroup)
				    UIStylesGUIGridLayoutGroup.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
				    
				// -------------------------------------------------- //
                // Canvas
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.Canvas)
				    UIStylesGUICanvas.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
				    
				// -------------------------------------------------- //
                // CanvasScaler
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.CanvasScaler)
				    UIStylesGUICanvasScaler.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
				    
				// -------------------------------------------------- //
                // GraphicRaycaster
                // -------------------------------------------------- //
			    else if (data.styles[s].styleComponents[c].styleComponentType == StyleComponentType.GraphicRaycaster)
				    UIStylesGUIGraphicRaycaster.DrawValues(data.styles[s], data.styles[s].styleComponents[c], ref checkError, data.styles[s].findByName);
				    
				/*<UIStylesTag(DrawValues)>*/
			    
			    if (checkError)
			    {
				    data.styles[s].CheckForPathError();
			    }
		    }
		    
		    if (!data.styles[s].styleComponents[c].open)
			    GUILayout.Space(10);
	    }
    }
}












































































































































