using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class UIStylesGUIGridLayoutGroup : EditorWindow
    {
        private static UnityEngine.Object[] draggedObjects;
        
        /// <summary>
        /// Draw the values
        /// </summary>
        public static void DrawValues ( Style style, StyleComponent componentValues, ref bool checkPath, string findByName )
        {
            GridLayoutGroupValues values = componentValues.gridLayoutGroup;
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
                        // Cell Size
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.cellSizeEnabled = EditorGUILayout.Toggle ( values.cellSizeEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.cellSizeEnabled );
                            {
                                values.cellSize = EditorGUILayout.Vector2Field("Cell Size", values.cellSize);
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
                                values.spacing = EditorGUILayout.Vector2Field("Spacing", values.spacing);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // Start Corner
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.startCornerEnabled = EditorGUILayout.Toggle ( values.startCornerEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.startCornerEnabled );
                            {
                                values.startCorner = (GridLayoutGroup.Corner)EditorGUILayout.EnumPopup("Start Corner", values.startCorner);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // Start Axis
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.startAxisEnabled = EditorGUILayout.Toggle ( values.startAxisEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.startAxisEnabled );
                            {
                                values.startAxis = (GridLayoutGroup.Axis)EditorGUILayout.EnumPopup("Start Axis", values.startAxis);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
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
                        
                        // -------------------------------------------------- //
                        // Constraint
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.constraintEnabled = EditorGUILayout.Toggle ( values.constraintEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.constraintEnabled );
                            {
                                values.constraint = (GridLayoutGroup.Constraint)EditorGUILayout.EnumPopup("Constraint", values.constraint);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // Constraint Count
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.constraintCountEnabled = EditorGUILayout.Toggle ( values.constraintCountEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.constraintCountEnabled );
                            {
                                values.constraintCount = EditorGUILayout.IntField("Constraint Count", values.constraintCount);
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
                            if (draggedObj is GridLayoutGroup)
                            {
                                componentValues.gridLayoutGroup = GridLayoutGroupHelper.SetValuesFromComponent((GridLayoutGroup)draggedObj);
                            }
                            if (draggedObj is GameObject)
                            {
                                GameObject obj = (GameObject)draggedObj;
                                
                                if (obj.GetComponent<GridLayoutGroup>())
                                    componentValues.gridLayoutGroup = GridLayoutGroupHelper.SetValuesFromComponent(obj.GetComponent<GridLayoutGroup>());
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
