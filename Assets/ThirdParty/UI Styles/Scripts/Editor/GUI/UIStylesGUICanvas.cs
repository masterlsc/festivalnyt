using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class UIStylesGUICanvas : EditorWindow
    {
	    private static UnityEngine.Object[] draggedObjects;
	    
	    private static string[] displays = new string[]{"Display 1", "Display 2", "Display 3", "Display 4", "Display 5", "Display 6", "Display 7", "Display 8"};
	    private static int[] displaysValues = new int[]{0,1,2,3,4,5,6,7};
        
        /// <summary>
        /// Draw the values
        /// </summary>
        public static void DrawValues ( Style style, StyleComponent componentValues, ref bool checkPath, string findByName )
        {
            CanvasValues values = componentValues.canvas;
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
                        // Render Mode
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.renderModeEnabled = EditorGUILayout.Toggle ( values.renderModeEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.renderModeEnabled );
                            {
                                values.renderMode = (RenderMode)EditorGUILayout.EnumPopup("Render Mode", values.renderMode);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
	                    GUILayout.EndHorizontal ();
	                                            
                        // -------------------------------------------------- //
                        // Pixel Perfect
                        // -------------------------------------------------- //
	                    if (values.renderMode != RenderMode.WorldSpace )
	                    {
	                    	GUILayout.BeginHorizontal ();
		                    {
			                    values.pixelPerfectEnabled = EditorGUILayout.Toggle ( values.pixelPerfectEnabled, GUILayout.MaxWidth ( 26 ) );
			                    
			                    EditorGUI.indentLevel = 1;
			                    EditorGUI.BeginDisabledGroup ( !values.pixelPerfectEnabled );
			                    {
				                    values.pixelPerfect = EditorGUILayout.Toggle("Pixel Perfect", values.pixelPerfect);
			                    }
			                    EditorGUI.EndDisabledGroup ();
			                    EditorGUI.indentLevel = 0;
		                    }
		                    GUILayout.EndHorizontal ();
	                    }
	                    
	                    // -------------------------------------------------- //
                        // Renderer Camera
                        // -------------------------------------------------- //
	                    if (values.renderMode != RenderMode.ScreenSpaceOverlay)
	                    {
	                    	GUILayout.BeginHorizontal ();
		                    {
			                    values.rendererCameraEnabled = EditorGUILayout.Toggle ( values.rendererCameraEnabled, GUILayout.MaxWidth ( 26 ) );
			                    
			                    EditorGUI.indentLevel = 1;
			                    EditorGUI.BeginDisabledGroup ( !values.rendererCameraEnabled );
			                    {
				                    values.rendererCamera = (Camera)EditorGUILayout.ObjectField(values.renderMode == RenderMode.ScreenSpaceCamera ? "Renderer Camera" : "Event Camera", values.rendererCamera, typeof(Camera), true );
			                    }
			                    EditorGUI.EndDisabledGroup ();
			                    EditorGUI.indentLevel = 0;
		                    }
		                    GUILayout.EndHorizontal ();
	                    }
	                    
	                    // -------------------------------------------------- //
                        // Plane Distance
                        // -------------------------------------------------- //
	                    if (values.renderMode == RenderMode.ScreenSpaceCamera)
	                    {
		                    GUILayout.BeginHorizontal ();
		                    {
			                    values.planeDistanceEnabled = EditorGUILayout.Toggle ( values.planeDistanceEnabled, GUILayout.MaxWidth ( 26 ) );
			                    
			                    EditorGUI.indentLevel = 1;
			                    EditorGUI.BeginDisabledGroup ( !values.planeDistanceEnabled );
			                    {
				                    values.planeDistance = EditorGUILayout.FloatField("Plane Distance", values.planeDistance);
			                    }
			                    EditorGUI.EndDisabledGroup ();
			                    EditorGUI.indentLevel = 0;
		                    }
		                    GUILayout.EndHorizontal (); 
	                    }
	                    
						// -------------------------------------------------- //
                        // Sorting Order
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.sortingOrderEnabled = EditorGUILayout.Toggle ( values.sortingOrderEnabled, GUILayout.MaxWidth ( 26 ) );
	                        
	                        EditorGUI.indentLevel = 1;
                            EditorGUI.BeginDisabledGroup ( !values.sortingOrderEnabled );
                            {
	                            values.sortingOrder = EditorGUILayout.IntField(values.renderMode == RenderMode.ScreenSpaceOverlay ? "Sort Order" : "Order In Layer", values.sortingOrder);
                            }
	                        EditorGUI.EndDisabledGroup ();
	                        EditorGUI.indentLevel = 0;
                        }
                        GUILayout.EndHorizontal ();                        
	                    
	                    // -------------------------------------------------- //
                        // Target Display
                        // -------------------------------------------------- //
	                    if (values.renderMode == RenderMode.ScreenSpaceOverlay)
	                    {
		                    GUILayout.BeginHorizontal ();
		                    {
			                    values.targetDisplayEnabled = EditorGUILayout.Toggle ( values.targetDisplayEnabled, GUILayout.MaxWidth ( 26 ) );
			                    
			                    EditorGUI.indentLevel = 1;
			                    EditorGUI.BeginDisabledGroup ( !values.targetDisplayEnabled );
			                    {
				                    values.targetDisplay = EditorGUILayout.IntPopup("Target Display", values.targetDisplay, displays, displaysValues);
			                    }
			                    EditorGUI.EndDisabledGroup ();
			                    EditorGUI.indentLevel = 0;
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
                            if (draggedObj is Canvas)
                            {
                                componentValues.canvas = CanvasHelper.SetValuesFromComponent((Canvas)draggedObj);
                            }
                            if (draggedObj is GameObject)
                            {
                                GameObject obj = (GameObject)draggedObj;
                                
                                if (obj.GetComponent<Canvas>())
                                    componentValues.canvas = CanvasHelper.SetValuesFromComponent(obj.GetComponent<Canvas>());
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
