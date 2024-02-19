using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class UIStylesGUIGraphicRaycaster : EditorWindow
    {
        private static UnityEngine.Object[] draggedObjects;
        
        /// <summary>
        /// Draw the values
        /// </summary>
        public static void DrawValues ( Style style, StyleComponent componentValues, ref bool checkPath, string findByName )
        {
            GraphicRaycasterValues values = componentValues.graphicRaycaster;
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
                        // Ignore Reversed Graphics
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.ignoreReversedGraphicsEnabled = EditorGUILayout.Toggle ( values.ignoreReversedGraphicsEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.ignoreReversedGraphicsEnabled );
                            {
                                values.ignoreReversedGraphics = EditorGUILayout.Toggle("Ignore Reversed Graphics", values.ignoreReversedGraphics);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // Blocking Objects
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.blockingObjectsEnabled = EditorGUILayout.Toggle ( values.blockingObjectsEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.blockingObjectsEnabled );
                            {
                                values.blockingObjects = (GraphicRaycaster.BlockingObjects)EditorGUILayout.EnumPopup("Blocking Objects", values.blockingObjects);
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
                            if (draggedObj is GraphicRaycaster)
                            {
                                componentValues.graphicRaycaster = GraphicRaycasterHelper.SetValuesFromComponent((GraphicRaycaster)draggedObj);
                            }
                            if (draggedObj is GameObject)
                            {
                                GameObject obj = (GameObject)draggedObj;
                                
                                if (obj.GetComponent<GraphicRaycaster>())
                                    componentValues.graphicRaycaster = GraphicRaycasterHelper.SetValuesFromComponent(obj.GetComponent<GraphicRaycaster>());
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
