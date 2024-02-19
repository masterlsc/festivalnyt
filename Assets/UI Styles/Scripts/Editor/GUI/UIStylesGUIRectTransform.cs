using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UIStyles
{
	public class UIStylesGUIRectTransform : Editor 
	{
		private static UnityEngine.Object[] draggedObjects;
		
		
		/// <summary>
		/// Draw the values
		/// </summary>
		public static void DrawValues ( StyleComponent componentValues, ref bool checkPath, string findByName )
		{
			GUILayout.Space ( -8 );
			EditorGUI.indentLevel = 0;
			
			GUILayout.BeginVertical ( EditorHelper.StandardPanel ( 10 ) );
			{	
				// -------------------------------------------------- //
				// Draw Component Path
				// -------------------------------------------------- //
				UIStylesGUIPath.DrawPath(ref componentValues.path, ref componentValues.renamePath, componentValues.hasPathError, ref checkPath, findByName );
				GUILayout.Space ( 5 );
				
				Draw (ref componentValues.rectTransform, false);
				
			}
			GUILayout.EndVertical ();
		}
		
		/// <summary>
		/// Draw the specified values.
		/// </summary>
		public static void Draw (ref RectTransformValues values, bool addDropdown = true)
		{
			if (values == null)
			{
				values = new RectTransformValues();
			}
			
			GUILayout.BeginVertical(EditorHelper.StandardPanel (10));
			{
				EditorGUI.indentLevel = 0;
				
				if (addDropdown)
					EditorHelper.FoldOut("Rect Transform", "", ref values.foldout);
				
				if (!addDropdown || (addDropdown && values.foldout))
				{
					GUILayout.Space(10);
					
					GUILayout.BeginVertical();
					{
						EditorGUI.indentLevel = 0;
						
						EditorGUI.BeginDisabledGroup (!values.positionEnabled);
						{
							GUILayout.BeginHorizontal();
							{
								EditorGUILayout.LabelField ("", GUILayout.Width (26));
								EditorGUILayout.LabelField(values.anchorMin.x == values.anchorMax.x ? "Pos X" : "Left", GUILayout.MinWidth(10));
								EditorGUILayout.LabelField(values.anchorMin.y == values.anchorMax.y ? "Pos Y" : "Top", GUILayout.MinWidth(10));
								EditorGUILayout.LabelField("Pos Z", GUILayout.MinWidth(10));
							}
							GUILayout.EndHorizontal();
						}
						EditorGUI.EndDisabledGroup ();
						
						GUILayout.BeginHorizontal();
						{
							values.positionEnabled = (bool) EditorGUILayout.Toggle(values.positionEnabled, GUILayout.Width (26));
							
							EditorGUI.BeginDisabledGroup (!values.positionEnabled);
							{
								GUILayout.BeginHorizontal();
								{
									values.position.x = EditorGUILayout.FloatField("", values.position.x, GUILayout.MinWidth(10));
									values.position.y = EditorGUILayout.FloatField("", values.position.y, GUILayout.MinWidth(10));
									values.position.z = EditorGUILayout.FloatField("", values.position.z, GUILayout.MinWidth(10));
								}
								GUILayout.EndHorizontal();
							}
							EditorGUI.EndDisabledGroup ();
						}
						GUILayout.EndHorizontal();
						
						EditorGUI.BeginDisabledGroup (!values.sizeDeltaEnabled);
						{
							GUILayout.BeginHorizontal();
							{
								EditorGUILayout.LabelField ("", GUILayout.Width (26));
								EditorGUILayout.LabelField(values.anchorMin.x == values.anchorMax.x ? "Width" : "Right", GUILayout.MinWidth(10));
								EditorGUILayout.LabelField(values.anchorMin.y == values.anchorMax.y ? "Height" : "Bottom", GUILayout.MinWidth(10));
								EditorGUILayout.LabelField("", GUILayout.MinWidth(10));
							}
							GUILayout.EndHorizontal();
						}
						EditorGUI.EndDisabledGroup ();						
						
						GUILayout.BeginHorizontal();
						{
							values.sizeDeltaEnabled = (bool) EditorGUILayout.Toggle(values.sizeDeltaEnabled, GUILayout.Width (26));
							
							EditorGUI.BeginDisabledGroup (!values.sizeDeltaEnabled);
							{
								GUILayout.BeginHorizontal();
								{
									values.sizeDelta.x = EditorGUILayout.FloatField("", values.sizeDelta.x, GUILayout.MinWidth(10));
									values.sizeDelta.y = EditorGUILayout.FloatField("", values.sizeDelta.y, GUILayout.MinWidth(10));
									EditorGUILayout.LabelField("", GUILayout.MinWidth(10));
								}
								GUILayout.EndHorizontal();
							}
							EditorGUI.EndDisabledGroup ();
						}
						GUILayout.EndHorizontal();
						
						GUILayout.Space(20);
						
						GUILayout.BeginHorizontal();
						{
							values.anchorMinEnabled = (bool) EditorGUILayout.Toggle(values.anchorMinEnabled, GUILayout.Width (26));
							
							EditorGUI.BeginDisabledGroup (!values.anchorMinEnabled);
							{
								values.anchorMin = EditorGUILayout.Vector2Field("Anchor Min", values.anchorMin);
							}
							EditorGUI.EndDisabledGroup ();
						}
						GUILayout.EndHorizontal();
						
						GUILayout.BeginHorizontal();
						{
							values.anchorMaxEnabled = (bool) EditorGUILayout.Toggle(values.anchorMaxEnabled, GUILayout.Width (26));
							
							EditorGUI.BeginDisabledGroup (!values.anchorMaxEnabled);
							{
								values.anchorMax = EditorGUILayout.Vector2Field("Anchor Max", values.anchorMax);
							}
							EditorGUI.EndDisabledGroup ();
						}
						GUILayout.EndHorizontal();
						
						GUILayout.Space(20);
						
						GUILayout.BeginHorizontal();
						{
							values.pivotEnabled = (bool) EditorGUILayout.Toggle(values.pivotEnabled, GUILayout.Width (26));
							
							EditorGUI.BeginDisabledGroup (!values.pivotEnabled);
							{
								values.pivot = EditorGUILayout.Vector2Field("Pivot", values.pivot);
							}
							EditorGUI.EndDisabledGroup ();
						}
						GUILayout.EndHorizontal();						
						
						GUILayout.Space(20);
						
						GUILayout.BeginHorizontal();
						{
							values.rotationEnabled = (bool) EditorGUILayout.Toggle(values.rotationEnabled, GUILayout.Width (26));
							
							EditorGUI.BeginDisabledGroup (!values.rotationEnabled);
							{
								values.rotation = EditorGUILayout.Vector3Field("Rotation", values.rotation);
							}
							EditorGUI.EndDisabledGroup ();
						}
						GUILayout.EndHorizontal();
						
						GUILayout.BeginHorizontal();
						{
							values.scaleEnabled = (bool) EditorGUILayout.Toggle(values.scaleEnabled, GUILayout.Width (26));
							
							EditorGUI.BeginDisabledGroup (!values.scaleEnabled);
							{
								values.scale = EditorGUILayout.Vector3Field("Scale", values.scale);
							}
							EditorGUI.EndDisabledGroup ();
						}
						GUILayout.EndHorizontal();
						
					}
					GUILayout.EndVertical();
				}
			}
			GUILayout.EndVertical();
			
			// -------------------------------------------------- //
			// Drop Area
			// -------------------------------------------------- //
			draggedObjects = new UnityEngine.Object[0];
			EditorHelper.DropArea(ref draggedObjects);
			
			if (draggedObjects.Length > 0)
			{
				foreach (Object draggedObj in draggedObjects) 
				{
					if (draggedObj is RectTransform)
					{
						values = RectTransformHelper.SetValuesFromComponent((RectTransform)draggedObj, true);
					}
					if (draggedObj is GameObject)
					{
						GameObject obj = (GameObject)draggedObj;
						
						if (obj.GetComponent<RectTransform>())
							values = RectTransformHelper.SetValuesFromComponent(obj.GetComponent<RectTransform>(), true);
					}
				}
			}
		}
	}
}





















