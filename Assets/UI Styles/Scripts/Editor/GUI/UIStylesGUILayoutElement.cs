using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class UIStylesGUILayoutElement : EditorWindow
    {
        private static UnityEngine.Object[] draggedObjects;
        
        /// <summary>
        /// Draw the values
        /// </summary>
        public static void DrawValues ( Style style, StyleComponent componentValues, ref bool checkPath, string findByName )
        {
            LayoutElementValues values = componentValues.layoutElement;
            GUILayout.Space ( -8 );
            EditorGUI.indentLevel = 0;
            
            GUILayout.BeginVertical ( EditorHelper.StandardPanel ( 10 ) );
            {
                // -------------------------------------------------- //
                // Draw Component Path
                // -------------------------------------------------- //
                GUILayout.BeginVertical ();
                {
                    UIStylesGUIPath.DrawPath(ref componentValues.path, ref componentValues.renamePath, componentValues.hasPathError, ref checkPath, findByName);
                    GUILayout.Space ( 5 );
                    
                    GUILayout.BeginVertical ( EditorHelper.StandardPanel ( 10 ) );
                    {
                        // -------------------------------------------------- //
                        // Ignore Layout
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.ignoreLayoutEnabled = EditorGUILayout.Toggle ( values.ignoreLayoutEnabled, GUILayout.Width ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.ignoreLayoutEnabled );
	                        {
		                        EditorGUILayout.LabelField("Ignore Layout", GUILayout.Width ( 100 ));
                                values.ignoreLayout = EditorGUILayout.Toggle(values.ignoreLayout, GUILayout.MinWidth ( 10 ));
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
	                    GUILayout.EndHorizontal ();
	                    
	                    if (!values.ignoreLayout)
	                    {
	                    	GUILayout.Space(10);
	                    	
	                    	// -------------------------------------------------- //
	                        // Min Width
	                        // -------------------------------------------------- //
	                        GUILayout.BeginHorizontal ();
	                        {
	                            values.minWidthEnabled = EditorGUILayout.Toggle ( values.minWidthEnabled, GUILayout.Width ( 26 ) );
	                            
	                            EditorGUI.BeginDisabledGroup ( !values.minWidthEnabled );
		                        {
		                        	GUILayout.BeginHorizontal ();
			                        {
			                        	EditorGUILayout.LabelField("Min Width", GUILayout.MaxWidth ( 100 ));
			                        	values.minWidthAllow = EditorGUILayout.Toggle ( values.minWidthAllow, GUILayout.Width ( 20 ) );
			                        	
			                        	if (values.minWidthAllow)
			                        	{
			                        		if (values.minWidth == -1)
				                        		values.minWidth = 0;
				                        	
			                        		values.minWidth = EditorGUILayout.FloatField(values.minWidth, GUILayout.MinWidth ( 10 ));
			                        	}
				                        
				                        else values.minWidth = -1;
		                        	}
			                        GUILayout.EndHorizontal ();
	                            }
	                            EditorGUI.EndDisabledGroup ();
	                        }
	                        GUILayout.EndHorizontal ();
	                        
	                        // -------------------------------------------------- //
	                        // Min Height
	                        // -------------------------------------------------- //
		                    GUILayout.BeginHorizontal ();
		                    {
			                    values.minHeightEnabled = EditorGUILayout.Toggle ( values.minHeightEnabled, GUILayout.Width ( 26 ) );
			                    
			                    EditorGUI.BeginDisabledGroup ( !values.minHeightEnabled );
			                    {
				                    GUILayout.BeginHorizontal ();
				                    {
					                    EditorGUILayout.LabelField("Min Height", GUILayout.MaxWidth ( 100 ));
					                    values.minHeightAllow = EditorGUILayout.Toggle ( values.minHeightAllow, GUILayout.Width ( 20 ) );
					                    
					                    if (values.minHeightAllow)
					                    {
						                    if (values.minHeight == -1)
							                    values.minHeight = 0;
						                    
						                    values.minHeight = EditorGUILayout.FloatField(values.minHeight, GUILayout.MinWidth ( 10 ));
					                    }
					                    
					                    else values.minHeight = -1;
				                    }
				                    GUILayout.EndHorizontal ();
			                    }
			                    EditorGUI.EndDisabledGroup ();
		                    }
		                    GUILayout.EndHorizontal ();
		                    
	                        // -------------------------------------------------- //
	                        // Preferred Width
	                        // -------------------------------------------------- //
		                    GUILayout.BeginHorizontal ();
		                    {
			                    values.preferredWidthEnabled = EditorGUILayout.Toggle ( values.preferredWidthEnabled, GUILayout.Width ( 26 ) );
			                    
			                    EditorGUI.BeginDisabledGroup ( !values.preferredWidthEnabled );
			                    {
				                    GUILayout.BeginHorizontal ();
				                    {
					                    EditorGUILayout.LabelField("Preferred Width", GUILayout.MaxWidth ( 100 ));
					                    values.preferredWidthAllow = EditorGUILayout.Toggle ( values.preferredWidthAllow, GUILayout.Width ( 20 ) );
					                    
					                    if (values.preferredWidthAllow)
					                    {
						                    if (values.preferredWidth == -1)
							                    values.preferredWidth = 0;
						                    
						                    values.preferredWidth = EditorGUILayout.FloatField(values.preferredWidth, GUILayout.MinWidth ( 10 ));
					                    }
					                    
					                    else values.preferredWidth = -1;
				                    }
				                    GUILayout.EndHorizontal ();
			                    }
			                    EditorGUI.EndDisabledGroup ();
		                    }
		                    GUILayout.EndHorizontal ();
	                        
	                        // -------------------------------------------------- //
	                        // Preferred Height
	                        // -------------------------------------------------- //
		                    GUILayout.BeginHorizontal ();
		                    {
			                    values.preferredHeightEnabled = EditorGUILayout.Toggle ( values.preferredHeightEnabled, GUILayout.Width ( 26 ) );
			                    
			                    EditorGUI.BeginDisabledGroup ( !values.preferredHeightEnabled );
			                    {
				                    GUILayout.BeginHorizontal ();
				                    {
					                    EditorGUILayout.LabelField("Preferred Height", GUILayout.MaxWidth ( 100 ));
					                    values.preferredHeightAllow = EditorGUILayout.Toggle ( values.preferredHeightAllow, GUILayout.Width ( 20 ) );
					                    
					                    if (values.preferredHeightAllow)
					                    {
						                    if (values.preferredHeight == -1)
							                    values.preferredHeight = 0;
						                    
						                    values.preferredHeight = EditorGUILayout.FloatField(values.preferredHeight, GUILayout.MinWidth ( 10 ));
					                    }
					                    
					                    else values.preferredHeight = -1;
				                    }
				                    GUILayout.EndHorizontal ();
			                    }
			                    EditorGUI.EndDisabledGroup ();
		                    }
		                    GUILayout.EndHorizontal ();
	                        
	                        // -------------------------------------------------- //
	                        // Flexible Width
	                        // -------------------------------------------------- //
		                    GUILayout.BeginHorizontal ();
		                    {
			                    values.flexibleWidthEnabled = EditorGUILayout.Toggle ( values.flexibleWidthEnabled, GUILayout.Width ( 26 ) );
			                    
			                    EditorGUI.BeginDisabledGroup ( !values.flexibleWidthEnabled );
			                    {
				                    GUILayout.BeginHorizontal ();
				                    {
					                    EditorGUILayout.LabelField("Flexible Width", GUILayout.MaxWidth ( 100 ));
					                    values.flexibleWidthAllow = EditorGUILayout.Toggle ( values.flexibleWidthAllow, GUILayout.Width ( 20 ) );
					                    
					                    if (values.flexibleWidthAllow)
					                    {
						                    if (values.flexibleWidth == -1)
							                    values.flexibleWidth = 0;
						                    
						                    values.flexibleWidth = EditorGUILayout.FloatField(values.flexibleWidth, GUILayout.MinWidth ( 10 ));
					                    }
					                    
					                    else values.flexibleWidth = -1;
				                    }
				                    GUILayout.EndHorizontal ();
			                    }
			                    EditorGUI.EndDisabledGroup ();
		                    }
		                    GUILayout.EndHorizontal ();
	                        
	                        // -------------------------------------------------- //
	                        // Flexible Height
	                        // -------------------------------------------------- //
		                    GUILayout.BeginHorizontal ();
		                    {
			                    values.flexibleHeightEnabled = EditorGUILayout.Toggle ( values.flexibleHeightEnabled, GUILayout.Width ( 26 ) );
			                    
			                    EditorGUI.BeginDisabledGroup ( !values.flexibleHeightEnabled );
			                    {
				                    GUILayout.BeginHorizontal ();
				                    {
					                    EditorGUILayout.LabelField("Flexible Height", GUILayout.MaxWidth ( 100 ));
					                    values.flexibleHeightAllow = EditorGUILayout.Toggle ( values.flexibleHeightAllow, GUILayout.Width ( 20 ) );
					                    
					                    if (values.flexibleHeightAllow)
					                    {
						                    if (values.flexibleHeight == -1)
							                    values.flexibleHeight = 0;
						                    
						                    values.flexibleHeight = EditorGUILayout.FloatField(values.flexibleHeight, GUILayout.MinWidth ( 10 ));
					                    }
					                    
					                    else values.flexibleHeight = -1;
				                    }
				                    GUILayout.EndHorizontal ();
			                    }
			                    EditorGUI.EndDisabledGroup ();
		                    }
		                    GUILayout.EndHorizontal ();
	                    }
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
                            if (draggedObj is LayoutElement)
                            {
                                componentValues.layoutElement = LayoutElementHelper.SetValuesFromComponent((LayoutElement)draggedObj);
                            }
                            if (draggedObj is GameObject)
                            {
                                GameObject obj = (GameObject)draggedObj;
                                
                                if (obj.GetComponent<LayoutElement>())
                                    componentValues.layoutElement = LayoutElementHelper.SetValuesFromComponent(obj.GetComponent<LayoutElement>());
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
