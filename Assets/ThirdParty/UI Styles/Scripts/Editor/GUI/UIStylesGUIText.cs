using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

using System.Collections.Generic;

namespace UIStyles
{
	public class UIStylesGUIText : EditorWindow
	{
		private static UnityEngine.Object[] draggedObjects;
		
		/// <summary>
		/// Overlay context menu
		/// </summary>
		private static void OverlayContextMenu (TextValues values)
		{
			GenericMenu menu = new GenericMenu ();
			
			menu.AddItem ( new GUIContent ( "Show Font Color" ), values.showFontColorWithGradient, delegate {
				values.showFontColorWithGradient = !values.showFontColorWithGradient;
			}, null );
			
			menu.ShowAsContext ();
		}
		
		/// <summary>
		/// Draw the values
		/// </summary>
		public static void DrawValues ( StyleComponent componentValues, ref bool checkPath, string findByName )
		{
			TextValues values = componentValues.text;
				
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
					// Text
					// -------------------------------------------------- //
					
					EditorGUI.indentLevel = 0;
					EditorGUILayout.LabelField ( "Text", EditorStyles.boldLabel );
					EditorGUI.indentLevel = 1;
					
					GUILayout.BeginHorizontal ();
					{
						
						values.textEnabled = (bool)EditorGUILayout.Toggle ( values.textEnabled, GUILayout.MaxWidth ( 26 ) );
						EditorGUILayout.LabelField ( "Text" );
					}
					GUILayout.EndHorizontal ();
					
					if (values.textEnabled)
					{
						//EditorGUI.BeginDisabledGroup ( !values.textEnabled );
						//{
							values.text = EditorGUILayout.TextArea (values.text );
						//}
						//EditorGUI.EndDisabledGroup ();
					}
					
					
					GUILayout.Space ( 10 );
					EditorGUI.indentLevel = 0;
					EditorGUILayout.LabelField ( "Character", EditorStyles.boldLabel );
					EditorGUI.indentLevel = 1;
					
					// -------------------------------------------------- //
					// Font Case
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal ();
					{
						values.fontCaseEnabled = (bool)EditorGUILayout.Toggle ( values.fontCaseEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.fontCaseEnabled );
						{
							values.fontCase = (FontCase)EditorGUILayout.EnumPopup ( "Font Case:", values.fontCase );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					// -------------------------------------------------- //
					// Font 
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal ();
					{
						values.fontEnabled = (bool)EditorGUILayout.Toggle ( values.fontEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.fontEnabled );
						{
							values.font = (Font)EditorGUILayout.ObjectField ( "Font", values.font, typeof( Font ), false );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					
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
					// Font Style 
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal ();
					{
						values.fontStyleEnabled = (bool)EditorGUILayout.Toggle ( values.fontStyleEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.fontStyleEnabled );
						{
							values.fontStyle = (FontStyle)EditorGUILayout.EnumPopup ( "Font Style:", values.fontStyle );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					// -------------------------------------------------- //
					// Font Size 
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal ();
					{
						values.fontSizeEnabled = (bool)EditorGUILayout.Toggle ( values.fontSizeEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.fontSizeEnabled );
						{
							values.fontSize = (int)EditorGUILayout.IntField ( values.bestFit ? "Font Size:  (Best Fit)" : "Font Size:", values.fontSize );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					// -------------------------------------------------- //
					// Line Spacing 
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal ();
					{
						values.lineSpacingEnabled = (bool)EditorGUILayout.Toggle ( values.lineSpacingEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.lineSpacingEnabled );
						{
							values.lineSpacing = (float)EditorGUILayout.FloatField ( "Line Spacing:", values.lineSpacing );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					// -------------------------------------------------- //
					// Rich Text 
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal ();
					{
						values.richTextEnabled = (bool)EditorGUILayout.Toggle ( values.richTextEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.richTextEnabled );
						{
							values.richText = (bool)EditorGUILayout.Toggle ( "Rich Text:", values.richText );
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
							values.raycastTarget = (bool)EditorGUILayout.Toggle ( "Raycast Target", values.raycastTarget );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					#endif
					
					GUILayout.Space ( 10 );
					EditorGUI.indentLevel = 0;
					EditorGUILayout.LabelField ( "Paragraph", EditorStyles.boldLabel );
					EditorGUI.indentLevel = 1;
					
					// -------------------------------------------------- //
					// Text Alignment
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal ();
					{
						values.textAlignmentEnabled = (bool)EditorGUILayout.Toggle ( values.textAlignmentEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.textAlignmentEnabled );
						{
							values.textAlignment = (TextAnchor)EditorGUILayout.EnumPopup ( "Text Alignment:", values.textAlignment );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					// -------------------------------------------------- //
					// Align By Geometry
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal ();
					{
						values.alignByGeometryEnabled = (bool)EditorGUILayout.Toggle ( values.alignByGeometryEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.alignByGeometryEnabled );
						{
							values.alignByGeometry = EditorGUILayout.Toggle ( "Align By Geometry:", values.alignByGeometry );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					// -------------------------------------------------- //
					// Horizontal Overflow
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal ();
					{
						values.horizontalWrapModeEnabled = (bool)EditorGUILayout.Toggle ( values.horizontalWrapModeEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.horizontalWrapModeEnabled );
						{
							values.horizontalWrapMode = (HorizontalWrapMode)EditorGUILayout.EnumPopup ( "Horizontal Overflow:", values.horizontalWrapMode );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					// -------------------------------------------------- //
					// Vertical Overflow 
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal ();
					{
						values.verticalWrapModeEnabled = (bool)EditorGUILayout.Toggle ( values.verticalWrapModeEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.verticalWrapModeEnabled );
						{
							values.verticalWrapMode = (VerticalWrapMode)EditorGUILayout.EnumPopup ( "Vertical Overflow:", values.verticalWrapMode );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					// -------------------------------------------------- //
					// Best Fit
					// -------------------------------------------------- //
					GUILayout.BeginHorizontal ();
					{
						values.bestFitEnabled = (bool)EditorGUILayout.Toggle ( values.bestFitEnabled, GUILayout.MaxWidth ( 26 ) );
						
						EditorGUI.BeginDisabledGroup ( !values.bestFitEnabled );
						{
							values.bestFit = (bool)EditorGUILayout.Toggle ( "Best Fit:", values.bestFit );
						}
						EditorGUI.EndDisabledGroup ();
					}
					GUILayout.EndHorizontal ();
					
					// -------------------------------------------------- //
					// Best Fit min & max 
					// -------------------------------------------------- //
					if ( values.bestFit )
					{
						EditorGUI.BeginDisabledGroup ( !values.bestFitEnabled );
						{
							GUILayout.BeginHorizontal ();
							{
								GUILayout.Space ( 30 );
								
								values.bestFitMinSize = (int)EditorGUILayout.IntField ( "Min Size:", values.bestFitMinSize );
							}
							GUILayout.EndHorizontal ();
							GUILayout.BeginHorizontal ();
							{
								GUILayout.Space ( 30 );
								
								values.bestFitMaxSize = (int)EditorGUILayout.IntField ( "Max Size:", values.bestFitMaxSize );
							}
							GUILayout.EndHorizontal ();
						}
						EditorGUI.EndDisabledGroup ();
						GUILayout.Space ( 5 );
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
					// Colour
					// -------------------------------------------------- //
					if ( values.overlay == Overlay.ColorOverlay || values.showFontColorWithGradient )
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
								WindowColorPalette.DrawColorPicker(ref values.colorID, ref values.color, "Font Color");
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
					
					#if !PRE_UNITY_5
					if ( values.overlay == Overlay.GradientOverlay )
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
						if (draggedObj is Text)
						{
							// Remember recttransform values
							RectTransformValues rt = componentValues.text.rectTransformValues.CloneValues();
							
							// remember shadow values
							bool useShadowEnabled = componentValues.text.useShadowEnabled;
							bool useShadow = componentValues.text.useShadow;
							bool useShadowGraphicAlpha = componentValues.text.shadowUseAlpha;
							Vector2 shadowDistance = componentValues.text.shadowDistance;
							Color shadowColor = componentValues.text.shadowColor;
							
							// remember outline values
							bool useOutlineEnabled = componentValues.text.useOutlineEnabled;
							bool useOutline = componentValues.text.useOutline;
							bool useOutlineGraphicAlpha = componentValues.text.outlineUseAlpha;
							Vector2 outlineDistance = componentValues.text.outlineDistance;
							Color outlineColor = componentValues.text.outlineColor;
							
							componentValues.text = TextHelper.SetValuesFromComponent((Text)draggedObj, false);
							
							// apply rect transform values
							componentValues.text.rectTransformValues = rt;
							
							// apply shadow values
							componentValues.text.useShadowEnabled = useShadowEnabled;
							componentValues.text.useShadow = useShadow;
							componentValues.text.shadowUseAlpha = useShadowGraphicAlpha;
							componentValues.text.shadowDistance = shadowDistance;
							componentValues.text.shadowColor = shadowColor;
							
							// apply outline values
							componentValues.text.useOutlineEnabled = useOutlineEnabled;
							componentValues.text.useOutline = useOutline;
							componentValues.text.outlineUseAlpha = useOutlineGraphicAlpha;
							componentValues.text.outlineDistance = outlineDistance;
							componentValues.text.outlineColor = outlineColor;
						}
						else if (draggedObj is GameObject)
						{
							GameObject obj = (GameObject)draggedObj;
							
							if (obj.GetComponent<Text>())
								componentValues.text = TextHelper.SetValuesFromComponent(obj.GetComponent<Text>(), obj);
						}
						
						else if (draggedObj is UIStyles.Outline)
						{
							UIStyles.Outline outline = (UIStyles.Outline)draggedObj;
							componentValues.text.useOutlineEnabled = true;
							componentValues.text.useOutline = true;
							componentValues.text.outlineUseAlpha = outline.useGraphicAlpha;
							componentValues.text.outlineDistance = outline.effectDistance;
							componentValues.text.outlineColor = outline.effectColor;
						}
						else if (draggedObj is UnityEngine.UI.Outline)
						{
							UnityEngine.UI.Outline outline = (UnityEngine.UI.Outline)draggedObj;
							componentValues.text.useOutlineEnabled = true;
							componentValues.text.useOutline = true;
							componentValues.text.outlineUseAlpha = outline.useGraphicAlpha;
							componentValues.text.outlineDistance = outline.effectDistance;
							componentValues.text.outlineColor = outline.effectColor;
						}
						
						else if (draggedObj is UIStyles.Shadow)
						{
							UIStyles.Shadow shadow = (UIStyles.Shadow)draggedObj;
							componentValues.text.useShadowEnabled = true;
							componentValues.text.useShadow = true;
							componentValues.text.shadowUseAlpha = shadow.useGraphicAlpha;
							componentValues.text.shadowDistance = shadow.effectDistance;
							componentValues.text.shadowColor = shadow.effectColor;
						}
						else if (draggedObj is UnityEngine.UI.Shadow)
						{
							UnityEngine.UI.Shadow shadow = (UnityEngine.UI.Shadow)draggedObj;
							componentValues.text.useShadowEnabled = true;
							componentValues.text.useShadow = true;
							componentValues.text.shadowUseAlpha = shadow.useGraphicAlpha;
							componentValues.text.shadowDistance = shadow.effectDistance;
							componentValues.text.shadowColor = shadow.effectColor;
						}
						
						else if (draggedObj is UIStyles.Gradient)
						{
							UIStyles.Gradient gradient = (UIStyles.Gradient)draggedObj;
							componentValues.text.colorEnabled = true;
							componentValues.text.overlay = Overlay.GradientOverlay;
							componentValues.text.gradientTopColor = gradient.topColor;
							componentValues.text.gradientBottomColor = gradient.bottomColor;
						}
					}
				}
			}
			GUILayout.EndVertical ();
		}
	}
}




















