using UnityEngine;
using UnityEditor;
using System;
using UIStyles;

[CustomPropertyDrawer(typeof(Color))]
public class UIStylesColorExtension : PropertyDrawer 
{		
	private bool gotNewColor = false;
	private Color newColor;
	private Color tempColor;
		
	/// <summary>
	/// Raises the GU event.
	/// </summary>
	public override void OnGUI(Rect rect, SerializedProperty Property, GUIContent label)
	{
		label = EditorGUI.BeginProperty(rect, label, Property);
		
		rect = EditorGUI.PrefixLabel(rect, GUIUtility.GetControlID(FocusType.Passive), label);
		
		if (gotNewColor)
		{
			Property.colorValue = newColor;
			gotNewColor = false;
		}
		else if (Selection.gameObjects.Length < 2)
		{
			Property.colorValue = EditorGUI.ColorField(new Rect(rect.x, rect.y, (rect.width - 35), rect.height), Property.colorValue);
			tempColor = Property.colorValue;
		}
		else
		{
			tempColor = EditorGUI.ColorField(new Rect(rect.x, rect.y, (rect.width - 35), rect.height), Property.colorValue);
			
			if (tempColor != Property.colorValue)
				Property.colorValue = tempColor;
		}
		
		if (GUI.Button(new Rect((rect.x + (rect.width - 30) ), rect.y, 30, rect.height), "P"))
		{		
			WindowColorPalette.GetColor( WindowColorPalette.ReturnToWindow.Inspector, (Color col, string id) => {
				
				gotNewColor = true;
				newColor = col;	
			});
		}
			
		EditorGUI.EndProperty();
	}
}










 