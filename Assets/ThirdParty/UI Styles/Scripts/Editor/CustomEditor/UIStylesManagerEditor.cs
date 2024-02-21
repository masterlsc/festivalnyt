using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UIStyles;

[CustomEditor(typeof(UIStylesManager))]
public class UIStylesManagerEditor : Editor
{
    /// <summary>
    /// Target script
    /// </summary>
    UIStylesManager target_;
    UIStylesManager script
    {
        get
        {
            if (target_ == null)
                target_ = target as UIStylesManager;

	        return target_;
        }
    }

    // Foldouts
    private bool dataFoldout;
    private bool objectFoldout;

    private int tempDataIndext;

    /// <summary>
    /// Find the data files in the project
    /// </summary>
    /// <param name="obj"></param>
    private void FindAllData(object obj)
    {
        List<StyleDataFile> list = new List<StyleDataFile>();
        string[] objs = AssetDatabase.FindAssets("t:StyleDataFile");
        for (int i = 0; i < objs.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(objs[i]);
            list.Add((StyleDataFile)AssetDatabase.LoadAssetAtPath(path, typeof(StyleDataFile)));
        }

        script.dataList = list;
    }

    /// <summary>
    /// Get the data context menu (right click the data dropdown)
    /// </summary>
    private void DataContext()
    {
        GUI.FocusControl(null);
        GenericMenu menu = new GenericMenu();

        menu.AddItem(new GUIContent("Add/Add All"), false, FindAllData, null);

        string[] objs = AssetDatabase.FindAssets("t:StyleDataFile");
        for (int i = 0; i < objs.Length; i++)
        {
            string guid = objs[i];
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string pathFormat = path.Replace("/", "\\").Replace(".asset", "");

            object obj = AssetDatabase.LoadAssetAtPath(path, typeof(StyleDataFile));


            if (obj != null)
            {
                if (script.dataList.Contains((StyleDataFile)AssetDatabase.LoadAssetAtPath(path, typeof(StyleDataFile))))
                    menu.AddDisabledItem(new GUIContent("Add/" + pathFormat));

                else menu.AddItem(new GUIContent("Add/" + pathFormat), false, DataItemContextAdd, guid);
            }
        }

        menu.AddSeparator("");

        menu.AddItem(new GUIContent("Remove All"), false, delegate
        {
            script.dataList.Clear();
        }, null);

        menu.ShowAsContext();
    }

    /// <summary>
    /// Get the data item context (right click within the data dropdown)
    /// </summary>
    /// <param name="index"></param>
    private void DataItemContext(int index)
    {
        GUI.FocusControl(null);
        GenericMenu menu = new GenericMenu();

	    menu.AddItem(new GUIContent("Load"), false, DataItemLoad, index);

        menu.AddSeparator("");

        menu.AddItem(new GUIContent("Remove"), false, DataItemContextRemove, index);

        menu.ShowAsContext();
    }

    /// <summary>
    /// Add a data file to the data list
    /// </summary>
    /// <param name="obj"></param>
    private void DataItemContextAdd(object obj)
    {
        string guid = (string)obj;

        string path = AssetDatabase.GUIDToAssetPath(guid);
        StyleDataFile styleData = (StyleDataFile)AssetDatabase.LoadAssetAtPath(path, typeof(StyleDataFile));
        script.dataList.Add(styleData);
    }

    /// <summary>
    /// Remove a data file from the data list
    /// </summary>
    /// <param name="obj"></param>
    private void DataItemContextRemove(object obj)
    {
        script.dataList.RemoveAt((int)obj);
    }

    /// <summary>
    /// Set the current loaded data
    /// </summary>
    /// <param name="obj"></param>
	private void DataItemLoad(object obj)
    {
        script.dataIndex = (int)obj;
        script.ApplyCurrentCategory();
    }


    private void DataContentAdd()
    {
        GUI.FocusControl(null);
        GenericMenu menu = new GenericMenu();

        menu.AddItem(new GUIContent("Add/Add All"), false, FindAllData, null);

        string[] objs = AssetDatabase.FindAssets("t:StyleDataFile");
        for (int i = 0; i < objs.Length; i++)
        {
            string guid = objs[i];
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string pathFormat = path.Replace("/", "\\").Replace(".asset", "");

            object obj = AssetDatabase.LoadAssetAtPath(path, typeof(StyleDataFile));


            if (obj != null)
            {
                if (script.dataList.Contains((StyleDataFile)AssetDatabase.LoadAssetAtPath(path, typeof(StyleDataFile))))
                    menu.AddDisabledItem(new GUIContent("Add/" + pathFormat));

                else menu.AddItem(new GUIContent("Add/" + pathFormat), false, DataItemContextAdd, guid);
            }
        }

        menu.ShowAsContext();
    }



    /// <summary>
    /// The loaded data file context menu
    /// </summary>
    private void DataIndexContext()
    {
        GUI.FocusControl(null);
        GenericMenu menu = new GenericMenu();

        if (script.dataList.Count == 0)
        {
            menu.AddDisabledItem(new GUIContent("No Data Found!"));
        }
        else
        {
            for (int i = 0; i < script.dataList.Count; i++)
            {
                if (script.dataList[i] != null)
                    menu.AddItem(new GUIContent(script.dataList[i].name), script.dataList[i].name == script.dataList[script.dataIndex].name, GotDataIndex, i);
            }
        }

        menu.ShowAsContext();
    }
    private void GotDataIndex(object i)
    {
	    script.dataIndex = (int)i;
	    
	    StyleHelper.ApplyAllCategories(script.data, null, ApplyMode.ActiveInScene);
    }


    /// <summary>
    /// Current category context menu
    /// </summary>
    private void CategoryContext(int dataIndext)
    {
        tempDataIndext = dataIndext;

        GUI.FocusControl(null);
        GenericMenu menu = new GenericMenu();

        if (script.dataList.Count == 0)
        {
            menu.AddDisabledItem(new GUIContent("No Data Found!"));
        }
        else if (script.dataList[script.dataIndex].categories.Count == 0)
        {
            menu.AddDisabledItem(new GUIContent("No Categories Found!"));
        }
        else
        {
            for (int i = 0; i < script.dataList[dataIndext].categories.Count; i++)
            {
                if (script.dataList[dataIndext] != null && script.dataList[dataIndext].categories[i] != null)
                    menu.AddItem(new GUIContent(script.dataList[dataIndext].categories[i]), script.dataList[dataIndext].categories[i] == script.dataList[dataIndext].currentCategory, GotCategory, i);
            }
        }

        menu.ShowAsContext();
    }
    private void GotCategory(object i)
    {
        script.categoryIndex = (int)i;
        script.dataList[tempDataIndext].currentCategory = script.dataList[tempDataIndext].categories[(int)i];

        script.ApplyCurrentCategory();
    }

    /// <summary>
    /// Cached objects contect menu
    /// </summary>
    private void objectContent()
    {
        GUI.FocusControl(null);
        GenericMenu menu = new GenericMenu();

        menu.AddItem(new GUIContent("Cache Objects"), false, delegate
        {
            CacheObjects();
        }, null);

        menu.AddSeparator("");

        menu.AddItem(new GUIContent("Clear"), false, delegate
        {
            ClearCache();
        }, null);

        menu.ShowAsContext();
    }

    private void CacheObjects()
    {
        script.cachedObjs = StyleHelper.GetAllObjectsAssignedToStyles(script.dataList.ToArray(), script.applyMode);
    }

    private void ClearCache()
    {
        script.cachedObjs.Clear();
    }



    /// <summary>
    /// GUI
    /// </summary>
    public override void OnInspectorGUI()
    {
        if ((dataFoldout || objectFoldout) && Event.current.type == EventType.Repaint)
	        Repaint();
	    
	    GUILayout.Space(10);
	    if (GUILayout.Button("Scripting API", EditorHelper.buttonSkin))
	    {
		    Application.OpenURL("http://uistyles.training");
	    }

        Event currentEvent = Event.current;
        Rect contextRect;
        Vector2 mousePos = currentEvent.mousePosition;
	    
	    EditorGUILayout.BeginVertical(EditorHelper.StandardPanel(6));
        {
            GUILayout.BeginHorizontal();
            {
                GUI.backgroundColor = Color.clear;
	            if (GUILayout.Button((dataFoldout ? "▼" : "►") + "  Themes (Style Data Files)", EditorHelper.dropdownSkin))
                {
                    if (Event.current.button == 0)
                    {
                        dataFoldout = !dataFoldout;
                    }
                    else if (Event.current.button == 1)
                    {

                    }
                }
                GUI.backgroundColor = Color.white;


                if (GUILayout.Button("+", EditorHelper.buttonSkin, GUILayout.Width(26)))
                {
                    DataContentAdd();
                }

            }
            GUILayout.EndHorizontal();



            if (dataFoldout)
            {
                EditorGUI.indentLevel = 1;

                GUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField(" ", GUILayout.Width(10));

                    EditorGUILayout.LabelField("#", GUILayout.Width(50));

                    EditorGUILayout.LabelField("File Name", GUILayout.MinWidth(10));
                }
                GUILayout.EndHorizontal();

                EditorHelper.StandardSeparator(0, "", "");

                for (int i = 0; i < script.dataList.Count; i++)
                {
                    if (script.dataList[i] != null)
                    {
                        GUILayout.BeginHorizontal();
                        {
                            if (script.dataIndex == i)
                            {
                                EditorGUI.indentLevel = 0;
                                GUI.color = Color.green;
                                EditorGUILayout.LabelField("✓", GUILayout.Width(10));
                                GUI.color = Color.white;
                            }
                            else
                            {
                                EditorGUI.indentLevel = 1;
                                EditorGUILayout.LabelField(" ", GUILayout.Width(10));
                            }

                            EditorGUI.indentLevel = 1;


                            GUILayout.BeginHorizontal();
                            {
                                GUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(50));

                                    EditorGUILayout.LabelField(script.dataList[i].name, GUILayout.MinWidth(10));
                                }
                                GUILayout.EndHorizontal();

                                contextRect = GUILayoutUtility.GetLastRect();
                                EditorGUI.DrawRect(contextRect, Color.clear);

                                if (contextRect.Contains(mousePos) && Event.current.type == EventType.MouseUp)
                                {
                                    DataItemContext(i);
                                }
                            }
                            GUILayout.EndHorizontal();


                            contextRect = GUILayoutUtility.GetLastRect();
                            EditorGUI.DrawRect(contextRect, Color.clear);

                            if (contextRect.Contains(mousePos))
                            {
                                // Draw highlight 
                                EditorHelper.HighlightRect(contextRect);
                            }
                        }
                        GUILayout.EndHorizontal();
                    }
                    else
                    {
                        script.dataList.RemoveAt(i);
                        //EditorGUILayout.LabelField("Null");
                    }
                }

                GUILayout.Space(5);
            }
        }
        EditorGUILayout.EndVertical();






        if (script.dataList != null && script.dataList.Count > 0)
        {
            GUILayout.Space(4);

            EditorGUILayout.BeginVertical(EditorHelper.StandardPanel(6));
            {
                GUILayout.BeginHorizontal();
                {
                    GUI.backgroundColor = Color.clear;
                    if (GUILayout.Button((objectFoldout ? "▼" : "►") + "  Cached Objects (" + script.cachedObjs.Count + ")", EditorHelper.dropdownSkin))
                    {
                        if (Event.current.button == 0)
                        {
                            objectFoldout = !objectFoldout;
                        }
                        else if (Event.current.button == 1)
                        {
                            objectContent();
                        }
                    }
                    GUI.backgroundColor = Color.white;

                    if (GUILayout.Button("↺", EditorHelper.buttonSkin, GUILayout.Width(26)))
                    {
                        CacheObjects();
                    }
                }
                GUILayout.EndHorizontal();

                if (objectFoldout)
                {
                    EditorGUI.indentLevel = 1;
                    if (script.cachedObjs.Count > 0)
                    {
                        GUILayout.Space(5);

                        GUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("#", GUILayout.Width(50));

                            EditorGUILayout.LabelField("Objects name", GUILayout.MinWidth(10));
                        }
                        GUILayout.EndHorizontal();

                        EditorHelper.StandardSeparator(0, "", "");

                        GUILayout.Space(5);

                        int num = 0;
                        foreach (GameObject obj in script.cachedObjs)
                        {
                            if (obj != null)
                            {

                                GUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(new GUIContent(num.ToString(), EditorHelper.GetGameObjectPath(obj)), GUILayout.Width(50));

                                    EditorGUILayout.LabelField(new GUIContent(obj.name, EditorHelper.GetGameObjectPath(obj)), GUILayout.MinWidth(10));
                                }
                                GUILayout.EndHorizontal();

                                contextRect = GUILayoutUtility.GetLastRect();
                                EditorGUI.DrawRect(contextRect, Color.clear);

                                if (contextRect.Contains(mousePos))
                                {
                                    // Draw highlight 
                                    EditorHelper.HighlightRect(contextRect);

                                    if (Event.current.type == EventType.MouseUp)
                                    {
                                        EditorHelper.SelectObjectInProject(obj, false, true);
                                    }
                                }
                            }
                            else
                            {
                                GUILayout.BeginHorizontal();
                                {
                                    GUI.color = Color.red;
                                    EditorGUILayout.LabelField(new GUIContent(num.ToString(), ""), GUILayout.Width(50));
                                    EditorGUILayout.LabelField(new GUIContent("Null", ""), GUILayout.MinWidth(10));
                                    GUI.color = Color.white;
                                }
                                GUILayout.EndHorizontal();
                            }

                            num++;
                        }
                    }
                    GUILayout.Space(5);
                }
            }
            EditorGUILayout.EndVertical();
        }
        else EditorGUILayout.HelpBox("Add data by right clicking the data field", MessageType.Info);
    }
}




















