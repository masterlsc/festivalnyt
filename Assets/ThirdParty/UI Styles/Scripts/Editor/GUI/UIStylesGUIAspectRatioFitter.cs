using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class UIStylesGUIAspectRatioFitter : EditorWindow
    {
        private static UnityEngine.Object[] draggedObjects;
        
        /// <summary>
        /// Draw the values
        /// </summary>
        public static void DrawValues ( Style style, StyleComponent componentValues, ref bool checkPath, string findByName )
        {
            AspectRatioFitterValues values = componentValues.aspectRatioFitter;
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
                        // Aspect Mode
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.aspectModeEnabled = EditorGUILayout.Toggle ( values.aspectModeEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.aspectModeEnabled );
                            {
                                values.aspectMode = (AspectRatioFitter.AspectMode)EditorGUILayout.EnumPopup("Aspect Mode", values.aspectMode);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // Aspect Ratio
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.aspectRatioEnabled = EditorGUILayout.Toggle ( values.aspectRatioEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.aspectRatioEnabled );
                            {
                                values.aspectRatio = EditorGUILayout.FloatField("Aspect Ratio", values.aspectRatio);
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
                            if (draggedObj is AspectRatioFitter)
                            {
                                componentValues.aspectRatioFitter = AspectRatioFitterHelper.SetValuesFromComponent((AspectRatioFitter)draggedObj);
                            }
                            if (draggedObj is GameObject)
                            {
                                GameObject obj = (GameObject)draggedObj;
                                
                                if (obj.GetComponent<AspectRatioFitter>())
                                    componentValues.aspectRatioFitter = AspectRatioFitterHelper.SetValuesFromComponent(obj.GetComponent<AspectRatioFitter>());
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
