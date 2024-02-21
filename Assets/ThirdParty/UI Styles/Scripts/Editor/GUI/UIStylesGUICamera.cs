using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UIStyles
{
    [System.Serializable]
    public class UIStylesGUICamera : EditorWindow
    {
        private static UnityEngine.Object[] draggedObjects;
        
        /// <summary>
        /// Draw the values
        /// </summary>
        public static void DrawValues ( Style style, StyleComponent componentValues, ref bool checkPath, string findByName )
        {
            CameraValues values = componentValues.camera;
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
                        // clearFlags
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.clearFlagsEnabled = EditorGUILayout.Toggle ( values.clearFlagsEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.clearFlagsEnabled );
                            {
                                values.clearFlags = (CameraClearFlags)EditorGUILayout.EnumPopup("Clear Flags", values.clearFlags);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // backgroundColor
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.backgroundColorEnabled = EditorGUILayout.Toggle ( values.backgroundColorEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.backgroundColorEnabled );
                            {
                                values.backgroundColor = EditorGUILayout.ColorField("Background Color", values.backgroundColor);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // cullingMask
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.cullingMaskEnabled = EditorGUILayout.Toggle ( values.cullingMaskEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.cullingMaskEnabled );
                            {
                                EditorHelper.LayerMaskField( "Culling Mask", ref values.cullingMask);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // orthographic
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.orthographicEnabled = EditorGUILayout.Toggle ( values.orthographicEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.orthographicEnabled );
                            {
                                values.orthographic = EditorGUILayout.Toggle("Orthographic", values.orthographic);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // orthographicSize
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.orthographicSizeEnabled = EditorGUILayout.Toggle ( values.orthographicSizeEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.orthographicSizeEnabled );
                            {
                                values.orthographicSize = EditorGUILayout.FloatField("Orthographic Size", values.orthographicSize);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // fieldOfView
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.fieldOfViewEnabled = EditorGUILayout.Toggle ( values.fieldOfViewEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.fieldOfViewEnabled );
                            {
                                values.fieldOfView = EditorGUILayout.FloatField("Field Of View", values.fieldOfView);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // farClipPlane
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.farClipPlaneEnabled = EditorGUILayout.Toggle ( values.farClipPlaneEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.farClipPlaneEnabled );
                            {
                                values.farClipPlane = EditorGUILayout.FloatField("Far Clip Plane", values.farClipPlane);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // nearClipPlane
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.nearClipPlaneEnabled = EditorGUILayout.Toggle ( values.nearClipPlaneEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.nearClipPlaneEnabled );
                            {
                                values.nearClipPlane = EditorGUILayout.FloatField("Near Clip Plane", values.nearClipPlane);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // rect
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.rectEnabled = EditorGUILayout.Toggle ( values.rectEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.rectEnabled );
                            {
                                values.rect = EditorGUILayout.RectField("Rect", values.rect);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // depth
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.depthEnabled = EditorGUILayout.Toggle ( values.depthEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.depthEnabled );
                            {
                                values.depth = EditorGUILayout.FloatField("Depth", values.depth);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // renderingPath
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.renderingPathEnabled = EditorGUILayout.Toggle ( values.renderingPathEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.renderingPathEnabled );
                            {
                                values.renderingPath = (RenderingPath)EditorGUILayout.EnumPopup("Rendering Path", values.renderingPath);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // targetTexture
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.targetTextureEnabled = EditorGUILayout.Toggle ( values.targetTextureEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.targetTextureEnabled );
                            {
	                            values.targetTexture = (RenderTexture)EditorGUILayout.ObjectField("Target Texture", values.targetTexture, typeof(RenderTexture), true );
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // useOcclusionCulling
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.useOcclusionCullingEnabled = EditorGUILayout.Toggle ( values.useOcclusionCullingEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.useOcclusionCullingEnabled );
                            {
                                values.useOcclusionCulling = EditorGUILayout.Toggle("Use Occlusion Culling", values.useOcclusionCulling);
                            }
                            EditorGUI.EndDisabledGroup ();
                        }
                        GUILayout.EndHorizontal ();
                        
                        // -------------------------------------------------- //
                        // hdr
                        // -------------------------------------------------- //
                        GUILayout.BeginHorizontal ();
                        {
                            values.hdrEnabled = EditorGUILayout.Toggle ( values.hdrEnabled, GUILayout.MaxWidth ( 26 ) );
                            
                            EditorGUI.BeginDisabledGroup ( !values.hdrEnabled );
                            {
                                values.hdr = EditorGUILayout.Toggle("Hdr", values.hdr);
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
                            if (draggedObj is Camera)
                            {
                                componentValues.camera = CameraHelper.SetValuesFromComponent((Camera)draggedObj);
                            }
                            if (draggedObj is GameObject)
                            {
                                GameObject obj = (GameObject)draggedObj;
                                
                                if (obj.GetComponent<Camera>())
                                    componentValues.camera = CameraHelper.SetValuesFromComponent(obj.GetComponent<Camera>());
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
