using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class UIStylesGUIVerticalLayoutGroup : EditorWindow
    {
        private static UnityEngine.Object[] draggedObjects;
        
        /// <summary>
        /// Draw the values
        /// </summary>
        public static void DrawValues ( Style style, StyleComponent componentValues, ref bool checkPath, string findByName )
        {
            VerticalLayoutGroupValues values = componentValues.verticalLayoutGroup;
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
                        // Padding
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.paddingEnabled = EditorGUILayout.Toggle ( values.paddingEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.paddingEnabled );
	                        {
		                        GUILayout.Space(-12);
	                            EditorHelper.DrawRectOffset ("Padding", ref values.padding, ref values.paddingDropdown);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // Spacing
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.spacingEnabled = EditorGUILayout.Toggle ( values.spacingEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.spacingEnabled );
                            {
                                values.spacing = EditorGUILayout.FloatField("Spacing", values.spacing);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
	                    GUILayout.EndHorizontal ();
	                    
	                    GUILayout.Space(10);
                        
                        // -------------------------------------------------- //
                        // Child Alignment
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.childAlignmentEnabled = EditorGUILayout.Toggle ( values.childAlignmentEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.childAlignmentEnabled );
                            {
                                values.childAlignment = (TextAnchor)EditorGUILayout.EnumPopup("Child Alignment", values.childAlignment);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
	                    GUILayout.EndHorizontal ();
	                    
	                    GUILayout.Space(10);
	                    
	              #if !PRE_UNITY_5
	                    
	                    EditorGUILayout.LabelField("Child Control Size");
                        
                        // -------------------------------------------------- //
                        // Child Control Width
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.childControlWidthEnabled = EditorGUILayout.Toggle ( values.childControlWidthEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.childControlWidthEnabled );
                            {
                                values.childControlWidth = EditorGUILayout.Toggle("Width", values.childControlWidth);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // Child Control Height
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.childControlHeightEnabled = EditorGUILayout.Toggle ( values.childControlHeightEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.childControlHeightEnabled );
                            {
                                values.childControlHeight = EditorGUILayout.Toggle("Height", values.childControlHeight);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
	                    GUILayout.EndHorizontal ();
	                    
	                    GUILayout.Space(10);
	                    
	                    EditorGUILayout.LabelField("Child Force Expand");
	                    
	             #endif 
                        
                        // -------------------------------------------------- //
                        // Child Force Expand Width
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.childForceExpandWidthEnabled = EditorGUILayout.Toggle ( values.childForceExpandWidthEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.childForceExpandWidthEnabled );
                            {
                                values.childForceExpandWidth = EditorGUILayout.Toggle("Width", values.childForceExpandWidth);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // Child Force Expand Height
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.childForceExpandHeightEnabled = EditorGUILayout.Toggle ( values.childForceExpandHeightEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.childForceExpandHeightEnabled );
                            {
                                values.childForceExpandHeight = EditorGUILayout.Toggle("Height", values.childForceExpandHeight);
                            }
                            EditorGUI.EndDisabledGroup ();
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
                            if (draggedObj is VerticalLayoutGroup)
                            {
                                componentValues.verticalLayoutGroup = VerticalLayoutGroupHelper.SetValuesFromComponent((VerticalLayoutGroup)draggedObj);
                            }
                            if (draggedObj is GameObject)
                            {
                                GameObject obj = (GameObject)draggedObj;
                                
                                if (obj.GetComponent<VerticalLayoutGroup>())
                                    componentValues.verticalLayoutGroup = VerticalLayoutGroupHelper.SetValuesFromComponent(obj.GetComponent<VerticalLayoutGroup>());
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
