using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace UIStyles
{
	public class UIStylesGUITransition : EditorWindow
	{
		public static void DrawValues ( TransitionValues values )
		{
			GUILayout.BeginVertical ();
			{
				EditorGUILayout.LabelField("Transition");
				GUILayout.Space ( 5 );
				
				EditorGUI.indentLevel = 0;
				GUILayout.BeginHorizontal ();
				{
					values.transitionEnabled = (bool)EditorGUILayout.Toggle ( values.transitionEnabled, GUILayout.MaxWidth ( 26 ) );
					
					EditorGUI.BeginDisabledGroup ( !values.transitionEnabled );
					{
						values.transition = (UnityEngine.UI.Selectable.Transition)EditorGUILayout.EnumPopup ( values.transition );
					}
					EditorGUI.EndDisabledGroup ();
				}
				GUILayout.EndHorizontal ();
				
				GUILayout.Space ( 4 );
				
				if ( values.transition == Selectable.Transition.ColorTint )
				{
					EditorGUI.BeginDisabledGroup ( !values.transitionEnabled );
					{
						// Normal Color 
						EditorGUI.indentLevel = 2;
						GUILayout.BeginHorizontal ();
						{
							// -------------------------------------------------- //
							// Color Picker 
							// -------------------------------------------------- //
							WindowColorPalette.DrawColorPicker(ref values.normalColorID, ref values.normalColor, "Normal Color");
							GUI.backgroundColor = string.IsNullOrEmpty(values.normalColorID) ? Color.white : Color.green;
							if ( GUILayout.Button ( "P", GUILayout.Width ( 30 ) ) )
							{
								WindowColorPalette.GetColor ( WindowColorPalette.ReturnToWindow.UIStyles, (Color col, string id ) =>
								{
									values.normalColor = col;
									values.normalColorID = id;
								} );
							}
							GUI.backgroundColor = Color.white;							
						}
						GUILayout.EndHorizontal ();
						

						// Highlighted Color 
						EditorGUI.indentLevel = 2;
						GUILayout.BeginHorizontal ();
						{
							// -------------------------------------------------- //
							// Color Picker 
							// -------------------------------------------------- //
							WindowColorPalette.DrawColorPicker(ref values.highlightedColorID, ref values.highlightedColor, "Highlighted Color");
							GUI.backgroundColor = string.IsNullOrEmpty(values.highlightedColorID) ? Color.white : Color.green;
							if ( GUILayout.Button ( "P", GUILayout.Width ( 30 ) ) )
							{
								WindowColorPalette.GetColor ( WindowColorPalette.ReturnToWindow.UIStyles, (Color col, string id ) =>
								{
									values.highlightedColor = col;
									values.highlightedColorID = id;
								} );
							}
							GUI.backgroundColor = Color.white;
						}
						GUILayout.EndHorizontal ();
						

						// Pressed Color
						EditorGUI.indentLevel = 2;
						GUILayout.BeginHorizontal ();
						{
							// -------------------------------------------------- //
							// Color Picker 
							// -------------------------------------------------- //
							WindowColorPalette.DrawColorPicker(ref values.pressedColorID, ref values.pressedColor, "Pressed Color");
							GUI.backgroundColor = string.IsNullOrEmpty(values.pressedColorID) ? Color.white : Color.green;
							if ( GUILayout.Button ( "P", GUILayout.Width ( 30 ) ) )
							{
								WindowColorPalette.GetColor ( WindowColorPalette.ReturnToWindow.UIStyles, (Color col, string id ) =>
								{
									values.pressedColor = col;
									values.pressedColorID = id;
								} );
							}
							GUI.backgroundColor = Color.white;
						}
						GUILayout.EndHorizontal ();

						
						// Disabled Color
						EditorGUI.indentLevel = 2;
						GUILayout.BeginHorizontal ();
						{
							// -------------------------------------------------- //
							// Color Picker 
							// -------------------------------------------------- //
							WindowColorPalette.DrawColorPicker(ref values.disabledColorID, ref values.disabledColor, "Disabled Color");
							GUI.backgroundColor = string.IsNullOrEmpty(values.disabledColorID) ? Color.white : Color.green;
							if ( GUILayout.Button ( "P", GUILayout.Width ( 30 ) ) )
							{
								WindowColorPalette.GetColor ( WindowColorPalette.ReturnToWindow.UIStyles, (Color col, string id ) =>
								{
									values.disabledColor = col;
									values.disabledColorID = id;
								} );
							}
							GUI.backgroundColor = Color.white;							
						}
						GUILayout.EndHorizontal ();

						
						// Color Multiplier 
						EditorGUI.indentLevel = 2;
						values.colorMultiplier = (float)EditorGUILayout.Slider ( "Color Multiplier", values.colorMultiplier, 1f, 5f );
						
						// Fade Duration
						
						values.fadeDuration = (float)EditorGUILayout.FloatField ( "Fade Duration", values.fadeDuration );
					}
					EditorGUI.EndDisabledGroup ();
				}
				else if ( values.transition == Selectable.Transition.SpriteSwap )
				{
					//  Graphics 
					
					EditorGUI.indentLevel = 2;
					EditorGUI.BeginDisabledGroup ( !values.transitionEnabled );
					{
						values.highlightedGraphic = (Sprite)EditorGUILayout.ObjectField ( "Highlighted Graphic", values.highlightedGraphic, typeof( Sprite ), false );
						values.pressedGraphic = (Sprite)EditorGUILayout.ObjectField ( "Pressed Graphic", values.pressedGraphic, typeof( Sprite ), false );
						values.disabledGraphic = (Sprite)EditorGUILayout.ObjectField ( "Disabled Graphic", values.disabledGraphic, typeof( Sprite ), false );
					}
					EditorGUI.EndDisabledGroup ();
				}
				else if ( values.transition == Selectable.Transition.Animation )
				{
					//  Animations 
					
					EditorGUI.indentLevel = 2;
					EditorGUI.BeginDisabledGroup ( !values.transitionEnabled );
					{
						values.normalTrigger = (string)EditorGUILayout.TextField ( "Normal Trigger", values.normalTrigger );
						values.highlightedTrigger = (string)EditorGUILayout.TextField ( "Highlighted Trigger", values.highlightedTrigger );
						values.pressedTrigger = (string)EditorGUILayout.TextField ( "Pressed Trigger", values.pressedTrigger );
						values.disabledTrigger = (string)EditorGUILayout.TextField ( "Disabled Trigger", values.disabledTrigger );
					}
					EditorGUI.EndDisabledGroup ();
				}
			}
			GUILayout.EndVertical ();
		}
	}
}





















