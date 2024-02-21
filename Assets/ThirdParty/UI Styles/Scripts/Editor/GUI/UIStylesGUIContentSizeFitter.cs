using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class UIStylesGUIContentSizeFitter : EditorWindow
    {
        private static UnityEngine.Object[] draggedObjects;
        
        /// <summary>
        /// Draw the values
        /// </summary>
        public static void DrawValues ( Style style, StyleComponent componentValues, ref bool checkPath, string findByName )
        {
            ContentSizeFitterValues values = componentValues.contentSizeFitter;
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
                        // Horizontal Fit
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.horizontalFitEnabled = EditorGUILayout.Toggle ( values.horizontalFitEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.horizontalFitEnabled );
                            {
                                values.horizontalFit = (ContentSizeFitter.FitMode)EditorGUILayout.EnumPopup("Horizontal Fit", values.horizontalFit);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // Vertical Fit
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.verticalFitEnabled = EditorGUILayout.Toggle ( values.verticalFitEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.verticalFitEnabled );
                            {
                                values.verticalFit = (ContentSizeFitter.FitMode)EditorGUILayout.EnumPopup("Vertical Fit", values.verticalFit);
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
                            if (draggedObj is ContentSizeFitter)
                            {
                                componentValues.contentSizeFitter = ContentSizeFitterHelper.SetValuesFromComponent((ContentSizeFitter)draggedObj);
                            }
                            if (draggedObj is GameObject)
                            {
                                GameObject obj = (GameObject)draggedObj;
                                
                                if (obj.GetComponent<ContentSizeFitter>())
                                    componentValues.contentSizeFitter = ContentSizeFitterHelper.SetValuesFromComponent(obj.GetComponent<ContentSizeFitter>());
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
