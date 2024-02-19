using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class UIStylesGUICanvasScaler : EditorWindow
    {
        private static UnityEngine.Object[] draggedObjects;
        
        /// <summary>
        /// Draw the values
        /// </summary>
        public static void DrawValues ( Style style, StyleComponent componentValues, ref bool checkPath, string findByName )
        {
            CanvasScalerValues values = componentValues.canvasScaler;
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
                        // Ui Scale Mode
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.uiScaleModeEnabled = EditorGUILayout.Toggle ( values.uiScaleModeEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.uiScaleModeEnabled );
                            {
                                values.uiScaleMode = (CanvasScaler.ScaleMode)EditorGUILayout.EnumPopup("Ui Scale Mode", values.uiScaleMode);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
	                    GUILayout.EndHorizontal ();
	                    
	                    GUILayout.Space(10);
                        
                        // -------------------------------------------------- //
                        // Scale Factor
                        // -------------------------------------------------- //
	                    if (values.uiScaleMode == CanvasScaler.ScaleMode.ConstantPixelSize)
	                    {
	                    	GUILayout.BeginHorizontal ();
		                    {
			                    values.scaleFactorEnabled = EditorGUILayout.Toggle ( values.scaleFactorEnabled, GUILayout.MaxWidth ( 26 ) );
			                    
			                    EditorGUI.BeginDisabledGroup ( !values.scaleFactorEnabled );
			                    {
				                    values.scaleFactor = EditorGUILayout.FloatField("Scale Factor", values.scaleFactor);
				                    if (values.scaleFactor < 0.1f)
					                    values.scaleFactor = 0.1f;
			                    }
			                    EditorGUI.EndDisabledGroup ();
		                    }
		                    GUILayout.EndHorizontal ();
	                    }                        
                        
                        // -------------------------------------------------- //
                        // Reference Resolution
                        // -------------------------------------------------- //
	                    if (values.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize)
	                    {
	                        GUILayout.BeginHorizontal ();
	                        {
	                            values.referenceResolutionEnabled = EditorGUILayout.Toggle ( values.referenceResolutionEnabled, GUILayout.Width ( 26 ) );
	                            
	                            EditorGUI.BeginDisabledGroup ( !values.referenceResolutionEnabled );
		                        {
			                        EditorGUILayout.LabelField("Reference Resolution", GUILayout.Width(145));
			                        
			                        EditorGUILayout.LabelField("X", GUILayout.Width(10));
			                        values.referenceResolution.x = EditorGUILayout.FloatField(values.referenceResolution.x, GUILayout.MinWidth(10));
			                        
			                        EditorGUILayout.LabelField("Y", GUILayout.Width(10));
			                        values.referenceResolution.y = EditorGUILayout.FloatField(values.referenceResolution.y, GUILayout.MinWidth(10));
	                            }
	                            EditorGUI.EndDisabledGroup ();
	                        }
		                    GUILayout.EndHorizontal ();
	                    }
                        
                        // -------------------------------------------------- //
                        // Screen Match Mode
                        // -------------------------------------------------- //
	                    if (values.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize)
	                    {
	                        GUILayout.BeginHorizontal ();
	                        {
	                            values.screenMatchModeEnabled = EditorGUILayout.Toggle ( values.screenMatchModeEnabled, GUILayout.MaxWidth ( 26 ) );
	                            
	                            EditorGUI.BeginDisabledGroup ( !values.screenMatchModeEnabled );
	                            {
	                                values.screenMatchMode = (CanvasScaler.ScreenMatchMode)EditorGUILayout.EnumPopup("Screen Match Mode", values.screenMatchMode);
	                            }
	                            EditorGUI.EndDisabledGroup ();
	                        }
		                    GUILayout.EndHorizontal ();

	                    }
                        
                        // -------------------------------------------------- //
                        // Match Width or Height
                        // -------------------------------------------------- //
	                    if (values.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize && values.screenMatchMode == CanvasScaler.ScreenMatchMode.MatchWidthOrHeight)
	                    {
	                        GUILayout.BeginHorizontal ();
	                        {
	                            values.matchWidthOrHeightEnabled = EditorGUILayout.Toggle ( values.matchWidthOrHeightEnabled, GUILayout.MaxWidth ( 26 ) );
		                        
	                            EditorGUI.BeginDisabledGroup ( !values.matchWidthOrHeightEnabled );
	                            {
		                            values.matchWidthOrHeight = EditorGUILayout.Slider("Match", values.matchWidthOrHeight, 0, 1);
	                            }
	                            EditorGUI.EndDisabledGroup ();
	                        }
		                    GUILayout.EndHorizontal ();
		                    
		                    GUILayout.Space(-5);
		                    GUILayout.BeginHorizontal();
		                    {
		                    	GUILayout.Space(180);
			                    EditorGUILayout.LabelField("Width", GUILayout.MinWidth(10));
			                    EditorGUILayout.LabelField("Height", GUILayout.Width(90));
		                    }
		                    GUILayout.EndHorizontal();	
	                    }
                        
                        // -------------------------------------------------- //
                        // Physical Unit
                        // -------------------------------------------------- //
	                    if (values.uiScaleMode == CanvasScaler.ScaleMode.ConstantPhysicalSize)
	                    {
	                    	GUILayout.BeginHorizontal ();
		                    {
			                    values.physicalUnitEnabled = EditorGUILayout.Toggle ( values.physicalUnitEnabled, GUILayout.MaxWidth ( 26 ) );
			                    
			                    EditorGUI.BeginDisabledGroup ( !values.physicalUnitEnabled );
			                    {
				                    values.physicalUnit = (CanvasScaler.Unit)EditorGUILayout.EnumPopup("Physical Unit", values.physicalUnit);
			                    }
			                    EditorGUI.EndDisabledGroup ();
		                    }
		                    GUILayout.EndHorizontal ();
	                    }
                        
                        // -------------------------------------------------- //
                        // Fallback Screen DPI
                        // -------------------------------------------------- //
	                    if (values.uiScaleMode == CanvasScaler.ScaleMode.ConstantPhysicalSize)
	                    {
	                    	GUILayout.BeginHorizontal ();
		                    {
			                    values.fallbackScreenDPIEnabled = EditorGUILayout.Toggle ( values.fallbackScreenDPIEnabled, GUILayout.MaxWidth ( 26 ) );
			                    
			                    EditorGUI.BeginDisabledGroup ( !values.fallbackScreenDPIEnabled );
			                    {
				                    values.fallbackScreenDPI = EditorGUILayout.FloatField("Fallback Screen DPI", values.fallbackScreenDPI);
			                    }
			                    EditorGUI.EndDisabledGroup ();
		                    }
		                    GUILayout.EndHorizontal ();
	                    }
                        
                        // -------------------------------------------------- //
                        // Default Sprite DPI
                        // -------------------------------------------------- //
	                    if (values.uiScaleMode == CanvasScaler.ScaleMode.ConstantPhysicalSize)
	                    {
	                    	GUILayout.BeginHorizontal ();
		                    {
			                    values.defaultSpriteDPIEnabled = EditorGUILayout.Toggle ( values.defaultSpriteDPIEnabled, GUILayout.MaxWidth ( 26 ) );
			                    
			                    EditorGUI.BeginDisabledGroup ( !values.defaultSpriteDPIEnabled );
			                    {
				                    values.defaultSpriteDPI = EditorGUILayout.FloatField("Default Sprite DPI", values.defaultSpriteDPI);
			                    }
			                    EditorGUI.EndDisabledGroup ();
		                    }
		                    GUILayout.EndHorizontal ();
	                    }
	                    
	                    // -------------------------------------------------- //
                        // Reference Pixels Per Unit
                        // -------------------------------------------------- //
	                    if (values.uiScaleMode == CanvasScaler.ScaleMode.ConstantPixelSize || values.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize)
	                    {
	                    	GUILayout.BeginHorizontal ();
		                    {
			                    values.referencePixelsPerUnitEnabled = EditorGUILayout.Toggle ( values.referencePixelsPerUnitEnabled, GUILayout.MaxWidth ( 26 ) );
			                    
			                    EditorGUI.BeginDisabledGroup ( !values.referencePixelsPerUnitEnabled );
			                    {
				                    values.referencePixelsPerUnit = EditorGUILayout.FloatField("Reference Pixels Per Unit", values.referencePixelsPerUnit);
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
                            if (draggedObj is CanvasScaler)
                            {
                                componentValues.canvasScaler = CanvasScalerHelper.SetValuesFromComponent((CanvasScaler)draggedObj);
                            }
                            if (draggedObj is GameObject)
                            {
                                GameObject obj = (GameObject)draggedObj;
                                
                                if (obj.GetComponent<CanvasScaler>())
                                    componentValues.canvasScaler = CanvasScalerHelper.SetValuesFromComponent(obj.GetComponent<CanvasScaler>());
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
