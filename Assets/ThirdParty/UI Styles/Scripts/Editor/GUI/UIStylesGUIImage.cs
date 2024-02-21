using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;

namespace UIStyles
{
	public class UIStylesGUIImage : EditorWindow
	{
		private static UnityEngine.Object[] draggedObjects;
		
		/// <summary>
		/// Overlay context menu
		/// </summary>
		private static void OverlayContextMenu (ImageValues values)
		{
			GenericMenu menu = new GenericMenu ();
			
			menu.AddItem ( new GUIContent ( "Show Image Color" ), values.showImageColorWithGradient, delegate {
				values.showImageColorWithGradient = !values.showImageColorWithGradient;
			}, null );
			
			menu.ShowAsContext ();
		}
		
		/// <summary>
		/// Draw the values
		/// </summary>
		public static void DrawValues ( StyleComponent componentValues, ref bool checkPath, string findByName )
		{
			ImageValues values = componentValues.image;
				
			GUILayout.Space ( -8 );
			EditorGUI.indentLevel = 0;
						
			GUILayout.BeginVertical ( EditorHelper.StandardPanel ( 10 ) );
			{			
				// -------------------------------------------------- //
				// Draw Component Path
				// -------------------------------------------------- //
				UIStylesGUIPath.DrawPath(ref componentValues.path, ref componentValues.renamePath, componentValues.hasPathError, ref checkPath, findByName);
				
				// -------------------------------------------------- //
				// Draw RectTransform
				// -------------------------------------------------- //
				GUILayout.Space ( 5 );
				UIStylesGUIRectTransform.Draw ( ref values.rectTransformValues );
				GUILayout.Space ( 5 );
					
				GUILayout.BeginVertical ( EditorHelper.StandardPanel ( 10 ) );
				{
					// -------------------------------------------------- //
					// Type 
					// -------------------------------------------------- //
					values.componentType = (ImageValues.ComponentType)EditorGUILayout.EnumPopup ( "Type", values.componentType );
					
					// -------------------------------------------------- //
					// Mask 
					// -------------------------------------------------- //
					GUILayout.Space ( 10 );
					EditorGUI.indentLevel = 0;
					EditorGUILayout.LabelField ( "Mask", EditorStyles.boldLabel );
					EditorGUI.indentLevel = 1;
					
					GUILayout.BeginHorizontal ();
					{
						values.useAsMask = (bool)EditorGUILayout.Toggle ( values.useAsMask, GUILayout.MaxWidth ( 26 ) );
						EditorGUILayout.LabelField ( "Is Mask" );
					}
					GUILayout.EndHorizontal ();
					
					if (values.useAsMask)
					{
						EditorGUI.indentLevel = 4;
						values.showMaskGraphic = (bool)EditorGUILayout.Toggle ( "Show Graphic", values.showMaskGraphic );
					}
					
					// -------------------------------------------------- //
					// image 
					// -------------------------------------------------- //
					GUILayout.Space ( 10 );
					EditorGUI.indentLevel = 0;
					EditorGUILayout.LabelField ( "Image", EditorStyles.boldLabel );
					EditorGUI.indentLevel = 1;
					
					if ( values.componentType == UIStyles.ImageValues.ComponentType.Image )
					{
						GUILayout.BeginHorizontal ();
						{
							values.imageEnabled = (bool)EditorGUILayout.Toggle ( values.imageEnabled, GUILayout.MaxWidth ( 26 ) );
								
							EditorGUI.BeginDisabledGroup ( !values.imageEnabled );
							{
								values.image = (Sprite)EditorGUILayout.ObjectField ( "Source Image", values.image, typeof( Sprite ), false );
									
								#if !PRE_UNITY_5
								GUILayout.FlexibleSpace ();
								#endif
							}
							EditorGUI.EndDisabledGroup ();
						}
						GUILayout.EndHorizontal ();
					}
					else if ( values.componentType == UIStyles.ImageValues.ComponentType.RawImage )
					{
						GUILayout.BeginHorizontal ();
						{
							values.textureEnabled = (bool)EditorGUILayout.Toggle ( values.textureEnabled, GUILayout.MaxWidth ( 26 ) );
								
							EditorGUI.BeginDisabledGroup ( !values.textureEnabled );
							{
								values.texture = (Texture2D)EditorGUILayout.ObjectField ( "Texture", values.texture, typeof( Texture2D ), false );
								#if !PRE_UNITY_5
								GUILayout.FlexibleSpace ();
								#endif
							}
							EditorGUI.EndDisabledGroup ();
						}
						GUILayout.EndHorizontal ();
					}
					
					// -------------------------------------------------- //
					// Material 
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal ();
					{
						values.materialEnabled = (bool)EditorGUILayout.Toggle ( values.materialEnabled, GUILayout.MaxWidth ( 26 ) );
							
						EditorGUI.BeginDisabledGroup ( !values.materialEnabled );
						{
							values.material = (Material)EditorGUILayout.ObjectField ( "Material", values.material, typeof( Material ), false );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
						
					// -------------------------------------------------- //
					// Raycast Target 
					// -------------------------------------------------- //
					#if !PRE_UNITY_5
					GUILayout.BeginHorizontal ();
					{
						values.raycastTargetEnabled = (bool)EditorGUILayout.Toggle ( values.raycastTargetEnabled, GUILayout.MaxWidth ( 26 ) );
							
						EditorGUI.BeginDisabledGroup ( !values.raycastTargetEnabled );
						{
							values.raycastTarget = (bool)EditorGUILayout.Toggle ( "Raycast Target:", values.raycastTarget );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					#endif
						
					// -------------------------------------------------- //
					// Image type 
					// -------------------------------------------------- //
					if ( values.componentType == UIStyles.ImageValues.ComponentType.Image )
					{
						GUILayout.BeginHorizontal ();
						{
							values.imageTypeEnabled = (bool)EditorGUILayout.Toggle ( values.imageTypeEnabled, GUILayout.MaxWidth ( 26 ) );
								
							EditorGUI.BeginDisabledGroup ( !values.imageTypeEnabled );
							{
								values.imageType = (UnityEngine.UI.Image.Type)EditorGUILayout.EnumPopup ( "Image Type:", values.imageType );
							}
							EditorGUI.EndDisabledGroup ();
						}
						GUILayout.EndHorizontal ();
							
						EditorGUI.indentLevel = 2;
						EditorGUI.BeginDisabledGroup ( !values.imageTypeEnabled );
						{
							if ( values.imageType == UnityEngine.UI.Image.Type.Sliced || values.imageType == UnityEngine.UI.Image.Type.Tiled )
							{
								GUILayout.BeginHorizontal ();
								{
									GUILayout.Space ( 30 );
									values.fillCentre = (bool)EditorGUILayout.Toggle ( "Fill Centre:", values.fillCentre );
								}
								GUILayout.EndHorizontal ();
							}
							else if ( values.imageType == UnityEngine.UI.Image.Type.Simple )
							{
								GUILayout.BeginHorizontal ();
								{
									GUILayout.Space ( 30 );
									values.preserveAspect = (bool)EditorGUILayout.Toggle ( "Preserve Aspect:", values.preserveAspect );
								}
								GUILayout.EndHorizontal ();
							}
							else if ( values.imageType == UnityEngine.UI.Image.Type.Filled )
							{
								GUILayout.BeginHorizontal ();
								{
									GUILayout.Space ( 30 );
									values.fillMethod = (UnityEngine.UI.Image.FillMethod)EditorGUILayout.EnumPopup ( "Fill Method:", values.fillMethod );
								}
								GUILayout.EndHorizontal ();
									
								if ( values.fillMethod == UnityEngine.UI.Image.FillMethod.Horizontal )
								{
									GUILayout.BeginHorizontal ();
									{
										GUILayout.Space ( 30 );
										values.originHorizontal = (UnityEngine.UI.Image.OriginHorizontal)EditorGUILayout.EnumPopup ( "Fill Origin:", values.originHorizontal );
									}
									GUILayout.EndHorizontal ();
								}
								else if ( values.fillMethod == UnityEngine.UI.Image.FillMethod.Vertical )
								{
									GUILayout.BeginHorizontal ();
									{
										GUILayout.Space ( 30 );
										values.originVertical = (UnityEngine.UI.Image.OriginVertical)EditorGUILayout.EnumPopup ( "Fill Origin:", values.originVertical );
									}
									GUILayout.EndHorizontal ();
								}
								else if ( values.fillMethod == UnityEngine.UI.Image.FillMethod.Radial90 )
								{
									GUILayout.BeginHorizontal ();
									{
										GUILayout.Space ( 30 );
										values.origin90 = (UnityEngine.UI.Image.Origin90)EditorGUILayout.EnumPopup ( "Fill Origin:", values.origin90 );
									}
									GUILayout.EndHorizontal ();
								}
								else if ( values.fillMethod == UnityEngine.UI.Image.FillMethod.Radial180 )
								{
									GUILayout.BeginHorizontal ();
									{
										GUILayout.Space ( 30 );
										values.origin180 = (UnityEngine.UI.Image.Origin180)EditorGUILayout.EnumPopup ( "Fill Origin:", values.origin180 );
									}
									GUILayout.EndHorizontal ();
								}
								else if ( values.fillMethod == UnityEngine.UI.Image.FillMethod.Radial360 )
								{
									GUILayout.BeginHorizontal ();
									{
										GUILayout.Space ( 30 );
										values.origin360 = (UnityEngine.UI.Image.Origin360)EditorGUILayout.EnumPopup ( "Fill Origin:", values.origin360 );
									}
									GUILayout.EndHorizontal ();
								}
									
								if ( values.fillMethod == UnityEngine.UI.Image.FillMethod.Radial90 || values.fillMethod == UnityEngine.UI.Image.FillMethod.Radial180 || values.fillMethod == UnityEngine.UI.Image.FillMethod.Radial360 )
								{
									GUILayout.BeginHorizontal ();
									{
										GUILayout.Space ( 30 );
										values.clockwise = (bool)EditorGUILayout.Toggle ( "Clockwise:", values.clockwise );
									}
									GUILayout.EndHorizontal ();
								}
									
								GUILayout.BeginHorizontal ();
								{
									GUILayout.Space ( 30 );
									values.fillAmount = (float)EditorGUILayout.Slider ( "Fill Amount:", values.fillAmount, 0, 1 );
								}
								GUILayout.EndHorizontal ();
									
								GUILayout.BeginHorizontal ();
								{
									GUILayout.Space ( 30 );
									values.preserveAspect = (bool)EditorGUILayout.Toggle ( "Preserve Aspect:", values.preserveAspect );
								}
								GUILayout.EndHorizontal ();
							}
								
							if ( values.imageType == UnityEngine.UI.Image.Type.Simple || values.imageType == UnityEngine.UI.Image.Type.Filled )
							{
								GUILayout.BeginHorizontal ();
								{
									GUILayout.Space ( 30 );
									values.setNativeSize = (bool)EditorGUILayout.Toggle ( "Set Native Size:", values.setNativeSize );
								}
								GUILayout.EndHorizontal ();
							}
						}
						EditorGUI.EndDisabledGroup ();
					}
					else if ( values.componentType == UIStyles.ImageValues.ComponentType.RawImage )
					{
						GUILayout.BeginHorizontal ();
						{
							values.uiRectEnabled = (bool)EditorGUILayout.Toggle ( values.uiRectEnabled, GUILayout.MaxWidth ( 35 ) );
								
							EditorGUI.BeginDisabledGroup ( !values.uiRectEnabled );
							{
								GUILayout.BeginHorizontal ();
								{
									EditorGUILayout.LabelField ( "UI Rect", GUILayout.MinWidth ( 90 ) );
									values.uvRect = (Rect)EditorGUILayout.RectField ( values.uvRect, GUILayout.MinWidth ( 1 ) );
								}
								GUILayout.EndHorizontal ();
							}
							EditorGUI.EndDisabledGroup ();
						}
						GUILayout.EndHorizontal ();
					}
						
					GUILayout.Space ( 10 );
					EditorGUI.indentLevel = 0;
					EditorGUILayout.LabelField ( "Color", EditorStyles.boldLabel );
					EditorGUI.indentLevel = 1;
						
					// -------------------------------------------------- //
					// Overlay 
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal ();
					{
						values.colorEnabled = (bool)EditorGUILayout.Toggle ( values.colorEnabled, GUILayout.MaxWidth ( 26 ) );
							
						EditorGUI.BeginDisabledGroup ( !values.colorEnabled );
						{
							values.overlay = (Overlay)EditorGUILayout.EnumPopup ( "Overlay:", values.overlay );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					if (values.overlay == UIStyles.Overlay.GradientOverlay)
					{
						Event currentEvent = Event.current;
						Rect contextRect;
						
						contextRect =  GUILayoutUtility.GetLastRect ( );
						EditorGUI.DrawRect ( contextRect, Color.clear );
						
						Vector2 mousePos = currentEvent.mousePosition;
						
						if (contextRect.Contains(mousePos) && Event.current.button == 1 && Event.current.type == EventType.MouseUp )
						{
							OverlayContextMenu (values);
						}
					}
					
					// -------------------------------------------------- //
					// Color
					// -------------------------------------------------- //
					if ( values.overlay == UIStyles.Overlay.ColorOverlay || values.showImageColorWithGradient )
					{
						GUILayout.BeginHorizontal ();
						{
							EditorGUI.indentLevel = 2;
							EditorGUI.BeginDisabledGroup ( !values.colorEnabled );
							{
								GUILayout.BeginHorizontal ();
								{
									GUILayout.Space ( 30 );
									// -------------------------------------------------- //
									// Color Picker 
									// -------------------------------------------------- //
									WindowColorPalette.DrawColorPicker(ref values.colorID, ref values.color, "Image Color");
									GUI.backgroundColor = string.IsNullOrEmpty(values.colorID) ? Color.white : Color.green;
									if ( GUILayout.Button ( "P", GUILayout.Width ( 30 ) ) )
									{
										WindowColorPalette.GetColor ( WindowColorPalette.ReturnToWindow.UIStyles, (Color col, string id ) =>
										{
											values.color = col;
											values.colorID = id;
										} );
									}
									GUI.backgroundColor = Color.white;
								}
								GUILayout.EndHorizontal ();
							}
							EditorGUI.EndDisabledGroup ();
						}
						GUILayout.EndHorizontal ();
					}
						
					#if !PRE_UNITY_5
					if ( values.overlay == UIStyles.Overlay.GradientOverlay )
					{
						EditorGUI.BeginDisabledGroup ( !values.colorEnabled );
						{
							EditorGUI.indentLevel = 2;
							GUILayout.BeginHorizontal ();
							{
								GUILayout.Space ( 30 );
									
								// -------------------------------------------------- //
								// Color Picker 
								// -------------------------------------------------- //
								WindowColorPalette.DrawColorPicker(ref values.gradientTopColorID, ref values.gradientTopColor, "Top Color");
								GUI.backgroundColor = string.IsNullOrEmpty(values.gradientTopColorID) ? Color.white : Color.green;
								if ( GUILayout.Button ( "P", GUILayout.Width ( 30 ) ) )
								{
									WindowColorPalette.GetColor ( WindowColorPalette.ReturnToWindow.UIStyles, (Color col, string id ) =>
									{
										values.gradientTopColor = col;
										values.gradientTopColorID = id;
									} );
								}
								GUI.backgroundColor = Color.white;
							}
							GUILayout.EndHorizontal ();								
								
							EditorGUI.indentLevel = 2;
							GUILayout.BeginHorizontal ();
							{
								GUILayout.Space ( 30 );
									
								// -------------------------------------------------- //
								// Color Picker 
								// -------------------------------------------------- //
								WindowColorPalette.DrawColorPicker(ref values.gradientBottomColorID, ref values.gradientBottomColor, "Bottom Color");
								GUI.backgroundColor = string.IsNullOrEmpty(values.gradientBottomColorID) ? Color.white : Color.green;
								if ( GUILayout.Button ( "P", GUILayout.Width ( 30 ) ) )
								{
									WindowColorPalette.GetColor ( WindowColorPalette.ReturnToWindow.UIStyles, (Color col, string id ) =>
									{
										values.gradientBottomColor = col;
										values.gradientBottomColorID = id;
									} );
								}
								GUI.backgroundColor = Color.white;
							}
							GUILayout.EndHorizontal ();
						}
						EditorGUI.EndDisabledGroup ();
					}
					#endif
						
					// -------------------------------------------------- //
					// Shadow 
					// -------------------------------------------------- //
					GUILayout.Space ( 10 );
					EditorGUI.indentLevel = 0;
					EditorGUILayout.LabelField ( "Effects", EditorStyles.boldLabel );
					EditorGUI.indentLevel = 1;
						
					GUILayout.BeginHorizontal ();
					{
						values.useShadowEnabled = (bool)EditorGUILayout.Toggle ( values.useShadowEnabled, GUILayout.MaxWidth ( 26 ) );
							
						EditorGUI.BeginDisabledGroup ( !values.useShadowEnabled );
						{
							values.useShadow = (bool)EditorGUILayout.Toggle ( "Add Shadow:", values.useShadow );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
						
					EditorGUI.BeginDisabledGroup ( !values.useShadowEnabled );
					{
						if ( values.useShadow )
						{
							EditorGUI.indentLevel = 2;
							GUILayout.BeginHorizontal ();
							{
								GUILayout.Space ( 30 );
									
								values.shadowUseAlpha = (bool)EditorGUILayout.Toggle ( "Use Graphic Alpha:", values.shadowUseAlpha );
							}
							GUILayout.EndHorizontal ();
								
							GUILayout.BeginHorizontal ();
							{
								GUILayout.Space ( 30 );
									
								EditorGUILayout.LabelField ( "Distance", GUILayout.Width ( 115 ) );
								values.shadowDistance = (Vector2)EditorGUILayout.Vector2Field ( "", values.shadowDistance, GUILayout.MinWidth ( 120 ), GUILayout.MinHeight ( 18 ) );
							}
							GUILayout.EndHorizontal ();
								
							GUILayout.BeginHorizontal ();
							{
								GUILayout.Space ( 30 );
									
								// -------------------------------------------------- //
								// Color Picker 
								// -------------------------------------------------- //
								WindowColorPalette.DrawColorPicker(ref values.shadowColorID, ref values.shadowColor, "Shadow Color");
								GUI.backgroundColor = string.IsNullOrEmpty(values.shadowColorID) ? Color.white : Color.green;
								if ( GUILayout.Button ( "P", GUILayout.Width ( 30 ) ) )
								{
									WindowColorPalette.GetColor ( WindowColorPalette.ReturnToWindow.UIStyles, (Color col, string id ) =>
									{
										values.shadowColor = col;
										values.shadowColorID = id;
									} );
								}
								GUI.backgroundColor = Color.white;
							}
							GUILayout.EndHorizontal ();
																
							GUILayout.Space ( 10 );
						}
					}
					EditorGUI.EndDisabledGroup ();
						
					// -------------------------------------------------- //
					// Outline 
					// -------------------------------------------------- //
					EditorGUI.indentLevel = 1;
					GUILayout.BeginHorizontal ();
					{
						values.useOutlineEnabled = (bool)EditorGUILayout.Toggle ( values.useOutlineEnabled, GUILayout.MaxWidth ( 26 ) );
							
						EditorGUI.BeginDisabledGroup ( !values.useOutlineEnabled );
						{
							values.useOutline = (bool)EditorGUILayout.Toggle ( "Add Outline:", values.useOutline );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
						
					EditorGUI.BeginDisabledGroup ( !values.useOutlineEnabled );
					{
						if ( values.useOutline )
						{
							EditorGUI.indentLevel = 2;
							GUILayout.BeginHorizontal ();
							{
								GUILayout.Space ( 30 );
									
								values.outlineUseAlpha = (bool)EditorGUILayout.Toggle ( "Use Graphic Alpha:", values.outlineUseAlpha );
							}
							GUILayout.EndHorizontal ();
								
							GUILayout.BeginHorizontal ();
							{
								GUILayout.Space ( 30 );
									
								EditorGUILayout.LabelField ( "Distance", GUILayout.Width ( 115 ) );
								values.outlineDistance = (Vector2)EditorGUILayout.Vector2Field ( "", values.outlineDistance, GUILayout.MinWidth ( 120 ), GUILayout.MinHeight ( 18 ) );
							}
							GUILayout.EndHorizontal ();
								
							GUILayout.BeginHorizontal ();
							{
								GUILayout.Space ( 30 );
									
								// -------------------------------------------------- //
								// Color Picker 
								// -------------------------------------------------- //
								WindowColorPalette.DrawColorPicker(ref values.outlineColorID, ref values.outlineColor, "Outline Color");
								GUI.backgroundColor = string.IsNullOrEmpty(values.outlineColorID) ? Color.white : Color.green;
								if ( GUILayout.Button ( "P", GUILayout.Width ( 30 ) ) )
								{
									WindowColorPalette.GetColor ( WindowColorPalette.ReturnToWindow.UIStyles, (Color col, string id ) =>
									{
										values.outlineColor = col;
										values.outlineColorID = id;
									} );
								}
								GUI.backgroundColor = Color.white;
							}
							GUILayout.EndHorizontal ();
						}
					}
					EditorGUI.EndDisabledGroup ();
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
						if (draggedObj is Image)
						{
							// Remember recttransform values
							RectTransformValues rt = componentValues.image.rectTransformValues.CloneValues();
							
							// remember shadow values
							bool useShadowEnabled = componentValues.image.useShadowEnabled;
							bool useShadow = componentValues.image.useShadow;
							bool useShadowGraphicAlpha = componentValues.image.shadowUseAlpha;
							Vector2 shadowDistance = componentValues.image.shadowDistance;
							Color shadowColor = componentValues.image.shadowColor;
							
							// remember outline values
							bool useOutlineEnabled = componentValues.image.useOutlineEnabled;
							bool useOutline = componentValues.image.useOutline;
							bool useOutlineGraphicAlpha = componentValues.image.outlineUseAlpha;
							Vector2 outlineDistance = componentValues.image.outlineDistance;
							Color outlineColor = componentValues.image.outlineColor;
							
							componentValues.image = ImageHelper.SetValuesFromComponent((Image)draggedObj, false);
							
							// apply rect transform values
							componentValues.image.rectTransformValues = rt;
							
							// apply shadow values
							componentValues.image.useShadowEnabled = useShadowEnabled;
							componentValues.image.useShadow = useShadow;
							componentValues.image.shadowUseAlpha = useShadowGraphicAlpha;
							componentValues.image.shadowDistance = shadowDistance;
							componentValues.image.shadowColor = shadowColor;
							
							// apply outline values
							componentValues.image.useOutlineEnabled = useOutlineEnabled;
							componentValues.image.useOutline = useOutline;
							componentValues.image.outlineUseAlpha = useOutlineGraphicAlpha;
							componentValues.image.outlineDistance = outlineDistance;
							componentValues.image.outlineColor = outlineColor;
						}
						else if (draggedObj is GameObject)
						{
							GameObject obj = (GameObject)draggedObj;
							
							if (obj.GetComponent<Image>())
								componentValues.image = ImageHelper.SetValuesFromComponent(obj.GetComponent<Image>(), obj);
						}
						
						else if (draggedObj is UIStyles.Outline)
						{
							UIStyles.Outline outline = (UIStyles.Outline)draggedObj;
							componentValues.image.useOutlineEnabled = true;
							componentValues.image.useOutline = true;
							componentValues.image.outlineUseAlpha = outline.useGraphicAlpha;
							componentValues.image.outlineDistance = outline.effectDistance;
							componentValues.image.outlineColor = outline.effectColor;
						}
						else if (draggedObj is UnityEngine.UI.Outline)
						{
							UnityEngine.UI.Outline outline = (UnityEngine.UI.Outline)draggedObj;
							componentValues.image.useOutlineEnabled = true;
							componentValues.image.useOutline = true;
							componentValues.image.outlineUseAlpha = outline.useGraphicAlpha;
							componentValues.image.outlineDistance = outline.effectDistance;
							componentValues.image.outlineColor = outline.effectColor;
						}
						
						else if (draggedObj is UIStyles.Shadow)
						{
							UIStyles.Shadow shadow = (UIStyles.Shadow)draggedObj;
							componentValues.image.useShadowEnabled = true;
							componentValues.image.useShadow = true;
							componentValues.image.shadowUseAlpha = shadow.useGraphicAlpha;
							componentValues.image.shadowDistance = shadow.effectDistance;
							componentValues.image.shadowColor = shadow.effectColor;
						}
						else if (draggedObj is UnityEngine.UI.Shadow)
						{
							UnityEngine.UI.Shadow shadow = (UnityEngine.UI.Shadow)draggedObj;
							componentValues.image.useShadowEnabled = true;
							componentValues.image.useShadow = true;
							componentValues.image.shadowUseAlpha = shadow.useGraphicAlpha;
							componentValues.image.shadowDistance = shadow.effectDistance;
							componentValues.image.shadowColor = shadow.effectColor;
						}
						
						else if (draggedObj is UIStyles.Gradient)
						{
							UIStyles.Gradient gradient = (UIStyles.Gradient)draggedObj;
							componentValues.image.colorEnabled = true;
							componentValues.image.overlay = Overlay.GradientOverlay;
							componentValues.image.gradientTopColor = gradient.topColor;
							componentValues.image.gradientBottomColor = gradient.bottomColor;
						}
					}
				}
			}
			GUILayout.EndVertical ();
		}
	}
}



















