using UnityEngine;
using UnityEditor;
using UIStyles;

[CustomEditor(typeof(UIStylesButtonComponent)), CanEditMultipleObjects]
public class UIStylesButtonComponentEditor : Editor 
{
	/// <summary>
	/// Target
	/// </summary>
	private UIStylesButtonComponent target_;
	private UIStylesButtonComponent script 
	{
		get 
		{
			if (target_ == null)
				target_ = target as UIStylesButtonComponent;
			
			return target_;
		}
	}
	
	private UIStylesManager manager_;
	private UIStylesManager manager 
	{
		get 
		{
			if (manager_ == null)
			{
				manager_ = FindObjectOfType<UIStylesManager>();
			}
				
			return manager_;
		}
	}
	
	
	
	private void DataIndexContext ()
	{
		GUI.FocusControl ( null );
		GenericMenu menu = new GenericMenu ();
		
		if (manager.dataList.Count == 0)
		{
			menu.AddDisabledItem(new GUIContent("No Data Found!"));
		}
		else
		{
			for (int i = 0; i < manager.dataList.Count; i++) 
			{
				if (manager.dataList[i] != null)
					menu.AddItem ( new GUIContent (manager.dataList[i].name), manager.dataList[i].name == manager.dataList[script.dataIndex].name, GotDataIndex, i );
			}
		}
		
		menu.ShowAsContext ();
	}
	private void GotDataIndex (object i)
	{
		script.dataIndex = (int)i;
		
		if (!manager.dataList[script.dataIndex].categories.Contains(script.category))
			script.category = string.Empty;
	}
	
	private void CategoryContext ()
	{
		GUI.FocusControl ( null );
		GenericMenu menu = new GenericMenu ();
		
		if (manager.dataList.Count == 0)
		{
			menu.AddDisabledItem(new GUIContent("No Data Found!"));
		}
		else if (manager.dataList[script.dataIndex].categories.Count == 0)
		{
			menu.AddDisabledItem(new GUIContent("No Categories Found!"));
		}
		else 
		{
			for (int i = 0; i < manager.dataList[script.dataIndex].categories.Count; i++) 
			{
				if (manager.dataList[script.dataIndex] != null && manager.dataList[script.dataIndex].categories[i] != null)
					menu.AddItem ( new GUIContent (manager.dataList[script.dataIndex].categories[i]), manager.dataList[script.dataIndex].categories[i] == script.category, GotCategory, i );
			}
		}
		
		menu.ShowAsContext ();
	}
	private void GotCategory (object i)
	{
		script.category = manager.data.categories[(int)i];
	}
	
	private void StyleNameContext ()
	{
		GUI.FocusControl ( null );
		GenericMenu menu = new GenericMenu ();
		
		if (manager.dataList.Count == 0)
		{
			menu.AddDisabledItem(new GUIContent("No Data Found!"));
		}
		else if (manager.dataList[script.dataIndex].categories.Count == 0)
		{
			menu.AddDisabledItem(new GUIContent("No Categories Found!"));
		}
		else 
		{
			for (int i = 0; i < manager.dataList[script.dataIndex].styles.Count; i++) 
			{
				if (manager.dataList[script.dataIndex] != null && manager.dataList[script.dataIndex].styles[i] != null)
					menu.AddItem ( new GUIContent (manager.dataList[script.dataIndex].styles[i].name), false, GotStyleName, i );
			}
		}
		
		menu.ShowAsContext ();
	}
	private void GotStyleName (object i)
	{
		script.styleName = manager.dataList[script.dataIndex].styles[(int)i].name;
	}
	
	private void NameContext ()
	{
		GUI.FocusControl ( null );
		GenericMenu menu = new GenericMenu ();
		
		
		
		menu.ShowAsContext ();
	}
	
	/// <summary>
	/// GUI
	/// </summary>
	public override void OnInspectorGUI ()
	{
		if (manager == null)
		{
			EditorGUILayout.HelpBox ( "No UI Styles Manager Found!", MessageType.Info );
			if (GUILayout.Button("Create Manager"))
			{
				GameObject obj = new GameObject("UI Styles Manager");
				obj.AddComponent<UIStylesManager>();
			}
		}
		else 
		{
			if (manager.dataList != null && manager.dataList.Count > 0)
			{
				GUILayout.Space(10);
				
				EditorGUILayout.BeginHorizontal();
				{			
					EditorGUILayout.LabelField("Apply Type", GUILayout.Width(150));
					script.buttonType = (UIStylesButtonComponent.ButtonType)EditorGUILayout.EnumPopup(script.buttonType, EditorHelper.dropdownSkinNormal);
				}
				EditorGUILayout.EndHorizontal();
				GUILayout.Space(4);
				
				if (script.buttonType == UIStylesButtonComponent.ButtonType.ApplyCategory)
				{
					DrawDataIndex ();
					DrawCategory ();
				}
				else if (script.buttonType == UIStylesButtonComponent.ButtonType.ApplyStyle)
				{
					DrawDataIndex ();
					DrawCategory ();
					DrawName ();
				}
				else if (script.buttonType == UIStylesButtonComponent.ButtonType.ApplyCurrentCategory)
				{
					DrawDataIndex ();
				}
				else if (script.buttonType == UIStylesButtonComponent.ButtonType.ApplyAllCategories)
				{
					DrawDataIndex ();
				}
			}
			else
			{
				EditorGUILayout.HelpBox ( "No Data loaded on the the UI Styles Manager", MessageType.Info );
			}
		}
	}
	
	private void DrawDataIndex ()
	{
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField("Data Index", GUILayout.Width(150));
			EditorGUILayout.LabelField(manager.dataList[script.dataIndex].name, EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(10));
			
			Event currentEvent = Event.current;
			Rect contextRect;
			
			contextRect =  GUILayoutUtility.GetLastRect ( );
			EditorGUI.DrawRect ( contextRect, Color.clear );
			
			Vector2 mousePos = currentEvent.mousePosition;
			
			if (contextRect.Contains(mousePos) && Event.current.type == EventType.MouseUp )
			{
				DataIndexContext ();
			}
		}
		EditorGUILayout.EndHorizontal();
		GUILayout.Space(4);
	}
	
	private void DrawCategory ()
	{
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField("Category", GUILayout.Width(150));
			EditorGUILayout.LabelField(script.category, EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(10));
			
			Event currentEvent = Event.current;
			Rect contextRect;
			
			contextRect =  GUILayoutUtility.GetLastRect ( );
			EditorGUI.DrawRect ( contextRect, Color.clear );
			
			Vector2 mousePos = currentEvent.mousePosition;
			
			if (contextRect.Contains(mousePos) && Event.current.type == EventType.MouseUp )
			{
				CategoryContext ();
			}
		}
		EditorGUILayout.EndHorizontal();
		GUILayout.Space(4);
	}
	
	private void DrawName ()
	{
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField("Style Name", GUILayout.Width(150));
			EditorGUILayout.LabelField(script.styleName, EditorHelper.dropdownSkinNormal, GUILayout.MinWidth(10));
			
			Event currentEvent = Event.current;
			Rect contextRect;
			
			contextRect =  GUILayoutUtility.GetLastRect ( );
			EditorGUI.DrawRect ( contextRect, Color.clear );
			
			Vector2 mousePos = currentEvent.mousePosition;
			
			if (contextRect.Contains(mousePos) && Event.current.type == EventType.MouseUp )
			{
				StyleNameContext ();
			}
		}
		EditorGUILayout.EndHorizontal();
		
		GUILayout.Space(4);
	}
}




















