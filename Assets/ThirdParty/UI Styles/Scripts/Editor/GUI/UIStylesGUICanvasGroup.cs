using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class UIStylesGUICanvasGroup : EditorWindow
    {
        private static UnityEngine.Object[] draggedObjects;
        
        /// <summary>
        /// Draw the values
        /// </summary>
        public static void DrawValues ( Style style, StyleComponent componentValues, ref bool checkPath, string findByName )
        {
	        CanvasGroupValues values = componentValues.canvasGroup;
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
                        // alpha
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.alphaEnabled = EditorGUILayout.Toggle ( values.alphaEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.alphaEnabled );
                            {
                                values.alpha = EditorGUILayout.FloatField("Alpha", values.alpha);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // interactable
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.interactableEnabled = EditorGUILayout.Toggle ( values.interactableEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.interactableEnabled );
                            {
                                values.interactable = EditorGUILayout.Toggle("Interactable", values.interactable);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // blocksRaycasts
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.blocksRaycastsEnabled = EditorGUILayout.Toggle ( values.blocksRaycastsEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.blocksRaycastsEnabled );
                            {
                                values.blocksRaycasts = EditorGUILayout.Toggle("Blocks Raycasts", values.blocksRaycasts);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // ignoreParentGroups
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.ignoreParentGroupsEnabled = EditorGUILayout.Toggle ( values.ignoreParentGroupsEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.ignoreParentGroupsEnabled );
                            {
                                values.ignoreParentGroups = EditorGUILayout.Toggle("Ignore Parent Groups", values.ignoreParentGroups);
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
                            if (draggedObj is CanvasGroup)
                            {
	                            componentValues.canvasGroup = CanvasGroupHelper.SetValuesFromComponent((CanvasGroup)draggedObj);
                            }
                            if (draggedObj is GameObject)
                            {
                                GameObject obj = (GameObject)draggedObj;
                                
                                if (obj.GetComponent<CanvasGroup>())
	                                componentValues.canvasGroup = CanvasGroupHelper.SetValuesFromComponent(obj.GetComponent<CanvasGroup>());
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
