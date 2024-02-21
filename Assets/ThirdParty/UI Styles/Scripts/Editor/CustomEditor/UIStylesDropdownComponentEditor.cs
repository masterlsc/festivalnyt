using UnityEngine;
using UnityEditor;
using UIStyles;

[CustomEditor(typeof(UIStylesDropdownComponent)), CanEditMultipleObjects]
public class UIStylesDropdownComponentEditor : Editor 
{
	/// <summary>
	/// Target
	/// </summary>
	UIStylesDropdownComponent target_;
	UIStylesDropdownComponent script 
	{
		get 
		{
			if (target_ == null)
				target_ = target as UIStylesDropdownComponent;
			
			return target_;
		}
	}
	
	/// <summary>
	/// GUI
	/// </summary>
	public override void OnInspectorGUI ()
	{
		GUILayout.Space(10);
		
		script.dropdownType = (UIStylesDropdownComponent.DropdownType)EditorGUILayout.EnumPopup("Dropdown Type", script.dropdownType);
		
		script.autoApply = EditorGUILayout.Toggle("Auto Apply", script.autoApply);
	}
}




















