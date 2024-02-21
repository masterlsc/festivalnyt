using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace UIStyles
{
	/*
	 	keyboard symbols
		↑	↓	↶	↷	↺	✓	▼	▲	►	◄	■	○	•
	*/
	
	public class EditorHelper
	{
		public static Color highlightedBackgroundColor
		{ get { return EditorGUIUtility.isProSkin ? new Color(0.471f, 0.956f, 1.0f, 1) : new Color(0.750f, 0.876f, 1.0f, 1);}}
		
		public static Sprite defaultBackgroundSprite
		{ get { return AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"); } }
		
		public static Sprite defaultCheckmarkSprite
		{ get { return AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Checkmark.psd"); } }
		
		public static Sprite defaultUISprite
		{ get { return AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd"); } }
		
		public static Sprite defaultInputFieldBackgroundSprite
		{ get { return AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/InputFieldBackground.psd"); } }
		
		public static Sprite defaultKnobSprite
		{ get { return AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd"); } }
				
		
		
		
		private static GUIStyle toolbarSkin_;
		public static GUIStyle toolbarSkin 
		{
			get
			{
				if (toolbarSkin_ == null)
					toolbarSkin_ = GUI.skin.FindStyle("Toolbar");
				
				return toolbarSkin_;
			}
		}
		
		private static GUIStyle buttonSkin_;
		public static GUIStyle buttonSkin 
		{
			get
			{
				if (buttonSkin_ == null)
					buttonSkin_ = GUI.skin.FindStyle("ToolbarButton");
				
				return buttonSkin_;
			}
		}
		
		private static GUIStyle dropdownSkin_;
		public static GUIStyle dropdownSkin 
		{
			get
			{
				if (dropdownSkin_ == null)
					dropdownSkin_ = GUI.skin.FindStyle("ToolbarDropdown");
					
				return dropdownSkin_;
			}
		}
		
		private static GUIStyle popUpSkin_;
		public static GUIStyle popUpSkin 
		{
			get
			{
				if (popUpSkin_ == null)
					popUpSkin_ = GUI.skin.FindStyle("ToolbarPopUp");
					
				return popUpSkin_;
			}
		}
		
		private static GUIStyle SeachFieldSkin_;
		public static GUIStyle SeachFieldSkin 
		{
			get
			{
				if (SeachFieldSkin_ == null)
					SeachFieldSkin_ = GUI.skin.FindStyle("ToolbarSeachTextField");
					
				return SeachFieldSkin_;
			}
		}
		
		private static GUIStyle SeachFieldSkinPopUp_;
		public static GUIStyle SeachFieldSkinPopUp 
		{
			get
			{
				if (SeachFieldSkinPopUp_ == null)
					SeachFieldSkinPopUp_ = GUI.skin.FindStyle("ToolbarSeachTextFieldPopUp");
					
				return SeachFieldSkinPopUp_;
			}
		}
		
		private static GUIStyle SeachFieldCancelSkin_;
		public static GUIStyle SeachFieldCancelSkin 
		{
			get
			{
				if (SeachFieldCancelSkin_ == null)
					SeachFieldCancelSkin_ = GUI.skin.FindStyle("ToolbarSeachCancelButton");
					
				return SeachFieldCancelSkin_;
			}
		}
		
		private static GUIStyle ToggleSkin_;
		public static GUIStyle ToggleSkin 
		{
			get
			{
				if (ToggleSkin_ == null)
					ToggleSkin_ = GUI.skin.FindStyle("OL ToggleWhite");
					
				return ToggleSkin_;
			}
		}
		
		private static GUIStyle MenuButtonSkin_;
		public static GUIStyle MenuButtonSkin 
		{
			get
			{
				if (MenuButtonSkin_ == null)
					MenuButtonSkin_ = GUI.skin.FindStyle("ToolbarButton");
					
				return MenuButtonSkin_;
			}
		}
		
		private static GUIStyle ErrorSymbol_;
		public static GUIStyle ErrorSymbol 
		{
			get
			{
				if (MenuButtonSkin_ == null)
					MenuButtonSkin_ = GUI.skin.FindStyle("Wizard Error");
				
				return MenuButtonSkin_;
			}
		}
		
		
		private static GUIStyle dropdownSkinNormal_;
		public static GUIStyle dropdownSkinNormal 
		{
			get
			{
				if (dropdownSkinNormal_ == null)
					dropdownSkinNormal_ = GUI.skin.FindStyle("Dropdown");
				
				return dropdownSkinNormal_;
			}
		}
		
		public static string GetGameObjectPath(GameObject obj)
		{
			string path = "/" + obj.name;
			while (obj.transform.parent != null)
			{
				obj = obj.transform.parent.gameObject;
				path = "/" + obj.name + path;
			}
			return path;
		}
		
		public static void HighlightRect (Rect rect)
		{
			// Top
			EditorGUI.DrawRect ( new Rect(rect.x, rect.y, rect.width, 1), Color.gray );
			
			// Bottom
			EditorGUI.DrawRect ( new Rect(rect.x, rect.y + rect.height, rect.width, 1), Color.gray );
			
			// Left
			EditorGUI.DrawRect ( new Rect(rect.x, rect.y, 1, rect.height), Color.gray );
			
			// Right
			EditorGUI.DrawRect ( new Rect(rect.x + rect.width, rect.y, 1, rect.height), Color.gray );
		}
				
		/// <summary>
		/// Standard panel.
		/// </summary>
		public static GUIStyle StandardPanel (int margin = 16)
		{
			//GUIStyle style = new GUIStyle ( GUI.skin.box );
			GUIStyle style = new GUIStyle ( GUI.skin.FindStyle("GroupBox") );
			
			style.padding.top = style.padding.bottom = margin;
			style.padding.left = style.padding.right = margin;
			
			return style;
		}

		public static GUIStyle SeparatorLine ()
		{
			GUIStyle line = new GUIStyle ( GUI.skin.box );
			line.border.top = line.border.bottom = 1;
			line.margin.top = line.margin.bottom = 1;
			line.padding.top = line.padding.bottom = 1;
			
			return line;
		}

		/// <summary>
		/// Standard separator.
		/// </summary>
		public static void StandardSeparator (int space = 10, string label = "", string toolTip = "" )
		{
			GUILayout.Space ( space );
			
			if ( !string.IsNullOrEmpty ( label ) )
				EditorGUILayout.LabelField ( new GUIContent ( label, toolTip ), EditorStyles.boldLabel );

			GUILayout.Box ( GUIContent.none, SeparatorLine (), GUILayout.ExpandWidth ( true ), GUILayout.Height ( 1f ) );
			
			GUILayout.Space ( space );
		}
		
		/// <summary>
		/// Drag and Drop
		/// </summary>
		public static void DropArea (ref UnityEngine.Object[] draggedObjects)
		{
			Event evt = Event.current;
			Rect dropArea = GUILayoutUtility.GetLastRect ();
			
			GUI.backgroundColor = Color.clear;
			GUI.Box (dropArea, "");
			GUI.backgroundColor = Color.white;
			
			switch (evt.type) 
			{
			case EventType.DragUpdated:
			case EventType.DragPerform:
				
				if (!dropArea.Contains (evt.mousePosition))
					return;
				
				DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
				
				if (evt.type == EventType.DragPerform)
				{
					DragAndDrop.AcceptDrag ();
					
					draggedObjects = DragAndDrop.objectReferences;
				}
				break;
			}
		}
		
		/// <summary>
		/// Standard separator.
		/// </summary>
		public static void StandardSeparatorCentreTitleAndDrag (int space, string label, string toolTip, ref bool button, ref UnityEngine.Object[] draggedObjects )
		{
			GUILayout.Space ( space +6 );
			
			Vector2 size = Title ( TextAnchor.MiddleCenter, Color.gray ).CalcSize(new GUIContent(label));
			
			EditorGUILayout.BeginHorizontal ();
			{
				GUILayout.Box ( GUIContent.none, SeparatorLine (), GUILayout.ExpandWidth ( true ), GUILayout.Height ( 1f ) );
				
				GUILayout.Space(size.x +20);
				
				GUILayout.Box ( GUIContent.none, SeparatorLine (), GUILayout.ExpandWidth ( true ), GUILayout.Height ( 1f ) );
			}
			EditorGUILayout.EndHorizontal ();
			
			EditorGUILayout.BeginVertical ();
			{
				GUILayout.Space ( -12 );
				GUI.backgroundColor = Color.clear;
				
				button = GUILayout.Button ( new GUIContent ( label, toolTip ), Title ( TextAnchor.MiddleCenter, Color.gray ) );
				
				DropArea (ref draggedObjects);
				
				if (!button)
					button = GUILayout.Button ( new GUIContent ( "Click here", toolTip ), Caption ( TextAnchor.MiddleCenter, Color.gray ) );
				
				DropArea (ref draggedObjects);
					
				GUILayout.Space ( 2 );
				GUI.backgroundColor = Color.white;
			}
			EditorGUILayout.EndVertical ();
			
			GUILayout.Space ( space );
		}
		
		/// <summary>
		/// Standard separator.
		/// </summary>
		public static void StandardSeparatorCentreTitle (int space, string label, string toolTip, ref bool button )
		{
			GUILayout.Space ( space +6 );
			
			Vector2 size = Title ( TextAnchor.MiddleCenter, Color.gray ).CalcSize(new GUIContent(label));
			
			EditorGUILayout.BeginHorizontal ();
			{
				GUILayout.Box ( GUIContent.none, SeparatorLine (), GUILayout.ExpandWidth ( true ), GUILayout.Height ( 1f ) );
				
				GUILayout.Space(size.x +20);
				
				GUILayout.Box ( GUIContent.none, SeparatorLine (), GUILayout.ExpandWidth ( true ), GUILayout.Height ( 1f ) );
			}
			EditorGUILayout.EndHorizontal ();
			
			EditorGUILayout.BeginVertical ();
			{
				GUILayout.Space ( -12 );
				GUI.backgroundColor = Color.clear;
				
				button = GUILayout.Button ( new GUIContent ( label, toolTip ), Title ( TextAnchor.MiddleCenter, Color.gray ) );
				
				if (!button)
					button = GUILayout.Button ( new GUIContent ( "Click here", toolTip ), Caption ( TextAnchor.MiddleCenter, Color.gray ) );
				
				GUILayout.Space ( 2 );
				GUI.backgroundColor = Color.white;
			}
			EditorGUILayout.EndVertical ();
			
			GUILayout.Space ( space );
		}
		

		public static void StandardSeparatorWithButton ( string label, string toolTip, ref bool button, string buttonText )
		{
			GUILayout.Space ( 10 );

			EditorGUILayout.BeginHorizontal ();
			{
				if ( !string.IsNullOrEmpty ( label ) )
					EditorGUILayout.LabelField ( new GUIContent ( label, toolTip ), EditorStyles.boldLabel );
				else
					GUILayout.FlexibleSpace ();

				GUI.backgroundColor = Color.clear;
				if ( GUILayout.Button (buttonText, GUILayout.Width ( 30 )) )
				{
					button = true;
				}
				GUI.backgroundColor = Color.white;
			}
			EditorGUILayout.EndHorizontal ();

			GUILayout.Box ( GUIContent.none, SeparatorLine (), GUILayout.ExpandWidth ( true ), GUILayout.Height ( 1f ) );

			GUILayout.Space ( 10 );
		}



		/// <summary>
		/// Caption the specified alignment and color.
		/// </summary>
		public static GUIStyle Caption_;
		public static GUIStyle Caption ( TextAnchor alignment, Color color )
		{
			if (Caption_ == null)
			{
				Caption_ = new GUIStyle ();
				
				Caption_.alignment = alignment;
				Caption_.fontStyle = FontStyle.Italic;
				Caption_.fontSize = 12;
				Caption_.normal.textColor = color;
				Caption_.margin = new RectOffset ( 6, 6, 0, 0 );
			}
			
			return Caption_;
		}

		/// <summary>
		/// Title the specified alignment and color.
		/// </summary>
		public static GUIStyle Title_;
		public static GUIStyle Title ( TextAnchor alignment, Color color )
		{
			if (Title_ == null)
			{
				Title_ = new GUIStyle ();
				
				Title_.alignment = alignment;
				Title_.fontStyle = FontStyle.Bold;
				Title_.fontSize = 14;
				Title_.normal.textColor = color;
				Title_.margin = new RectOffset ( 6, 6, 0, 0 );
			}
			
			return Title_;
		}

		/// <summary>
		/// Does this game object have a prefab?.
		/// </summary>
		public static bool isActivePrefabInstance ( GameObject obj )
		{
			return 
				PrefabUtility.GetPrefabType ( obj ) == PrefabType.None
			|| PrefabUtility.GetPrefabType ( obj ) == PrefabType.MissingPrefabInstance
			|| PrefabUtility.GetPrefabType ( obj ) == PrefabType.DisconnectedPrefabInstance;
		}

		/// <summary>
		/// is the object the prefab in the project folder not the scene
		/// </summary>
		public static bool isPrefabInFolderNotScene ( GameObject obj )
		{
			return 
				PrefabUtility.GetPrefabType ( obj ) == PrefabType.Prefab
			&& PrefabUtility.GetCorrespondingObjectFromSource ( obj ) == null && PrefabUtility.GetPrefabObject ( obj ) != null;
		}

		/// <summary>
		/// Highlight an object in the project
		/// </summary>
		public static void SelectObjectInProject ( UnityEngine.Object obj, bool select, bool ping )
		{
			if ( select )
				Selection.activeObject = obj;
			
			if ( ping )
				EditorGUIUtility.PingObject ( obj );
		}

		/// <summary>
		/// Highlight an object in the project
		/// </summary>
		public static void SelectObjectInProject ( string path, bool select, bool ping )
		{
			// Check the path has no '/' at the end if it dose remove it
			if ( path[path.Length - 1] == '/' )
				path = path.Substring ( 0, path.Length - 1 );
			
			UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath ( path, typeof( UnityEngine.Object ) );
            
			if ( select )
				Selection.activeObject = obj;
			
			if ( ping )
				EditorGUIUtility.PingObject ( obj );
		}

		/// <summary>
		/// Progress Bar
		/// </summary>
		public static void ProgressBar ( string label, string barLable, float value )
		{
			if ( !string.IsNullOrEmpty ( label ) )
				EditorGUILayout.LabelField ( label );
			
			Rect rect = GUILayoutUtility.GetRect ( 18, 18, "TextField" );
			EditorGUI.ProgressBar ( rect, value, barLable );
			EditorGUILayout.Space ();
		}

		/// <summary>
		/// Foldout
		/// </summary>
		public static void FoldOut ( string label, string toolTip, ref bool value )
		{
			// Draw Fold out
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.GetControlRect ( true, 16f, EditorStyles.foldout );
			Rect foldRect = GUILayoutUtility.GetLastRect ();
			if ( Event.current.type == EventType.MouseDown && Event.current.button == 0 && foldRect.Contains ( Event.current.mousePosition ) )
			{
				value = !value;
				GUI.changed = true;
				Event.current.Use ();
			}
			else if ( Event.current.type == EventType.MouseDown && Event.current.button == 1 && foldRect.Contains ( Event.current.mousePosition ) )
			{
				//Debug.Log ("Right Click");
			}
			
			value = EditorGUI.Foldout ( foldRect, value, new GUIContent ( label, toolTip ) );
			if ( EditorGUI.EndChangeCheck () )
			{
				//Debug.Log ("On Fold Out");
				GUI.FocusControl ( string.Empty );
			}
		}

		/// <summary>
		/// Foldout with arrows.
		/// </summary>
		public static void FoldOutWithArrows ( string label, string toolTip, ref bool value, ref bool button1, ref bool button2 )
		{
			GUILayout.BeginHorizontal ();
			{
				// Draw Fold out
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.GetControlRect ( true, 16f, EditorStyles.foldout );
				Rect foldRect = GUILayoutUtility.GetLastRect ();
				if ( Event.current.type == EventType.MouseDown && Event.current.button == 0 && foldRect.Contains ( Event.current.mousePosition ) )
				{
					value = !value;
					GUI.changed = true;
					Event.current.Use ();
				}
				else if ( Event.current.type == EventType.MouseDown && Event.current.button == 1 && foldRect.Contains ( Event.current.mousePosition ) )
				{
					//Debug.Log ("Right Click");
				}
				
				value = EditorGUI.Foldout ( foldRect, value, new GUIContent ( label, toolTip ) );
				
				GUILayout.FlexibleSpace ();
				
				button1 = GUILayout.Button ( new GUIContent ( "↑", "Move Up" ), GUILayout.Width ( 30 ) );
				button2 = GUILayout.Button ( new GUIContent ( "↓", "Move Down" ), GUILayout.Width ( 30 ) );
			}
			GUILayout.EndHorizontal ();
			
			if ( EditorGUI.EndChangeCheck () )
			{
				//Debug.Log ("On Fold Out");
				GUI.FocusControl ( string.Empty );
			}
		}

		/// <summary>
		/// Foldout with arrows and copy.
		/// </summary>
		public static void FoldOutWithArrowsCopy ( string label, string toolTip, ref bool value, ref bool button1, ref bool button2, ref bool button3, ref bool button4, bool enable4, string copyValue )
		{
			GUILayout.BeginHorizontal ();
			{
				// Draw Fold out
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.GetControlRect ( true, 16f, EditorStyles.foldout );
				Rect foldRect = GUILayoutUtility.GetLastRect ();
				if ( Event.current.type == EventType.MouseDown && Event.current.button == 0 && foldRect.Contains ( Event.current.mousePosition ) )
				{
					value = !value;
					GUI.changed = true;
					Event.current.Use ();
				}
				else if ( Event.current.type == EventType.MouseDown && Event.current.button == 1 && foldRect.Contains ( Event.current.mousePosition ) )
				{
					//Debug.Log ("Right Click");
				}
				
				value = EditorGUI.Foldout ( foldRect, value, new GUIContent ( label, toolTip ) );
				
				GUILayout.FlexibleSpace ();
				
				button3 = GUILayout.Button ( new GUIContent ( "C", copyValue + "\n\nCopy the find by name" ), GUILayout.Width ( 30 ) );
				
				EditorGUI.BeginDisabledGroup ( enable4 );
				{
					button4 = GUILayout.Button ( new GUIContent ( "C+", copyValue + "\n\nAdd the find by name to the selected objects" ), GUILayout.Width ( 30 ) );
				}
				EditorGUI.EndDisabledGroup ();
				
				GUILayout.Label ( "|" );
					
				button1 = GUILayout.Button ( new GUIContent ( "↑", "Move Up" ), GUILayout.Width ( 30 ) );
				button2 = GUILayout.Button ( new GUIContent ( "↓", "Move Down" ), GUILayout.Width ( 30 ) );				
			}
			GUILayout.EndHorizontal ();
			
			if ( EditorGUI.EndChangeCheck () )
			{
				//Debug.Log ("On Fold Out");
				GUI.FocusControl ( string.Empty );
			}
		}

		/// <summary>
		/// Foldout with delete.
		/// </summary>
		public static void FoldOutWithDelete ( string label, string toolTip, ref bool value, ref bool button )
		{
			GUILayout.BeginHorizontal ();
			{
				// Draw Fold out
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.GetControlRect ( true, 16f, EditorStyles.foldout );
				Rect foldRect = GUILayoutUtility.GetLastRect ();
				if ( Event.current.type == EventType.MouseDown && Event.current.button == 0 && foldRect.Contains ( Event.current.mousePosition ) )
				{
					value = !value;
					GUI.changed = true;
					Event.current.Use ();
				}
				else if ( Event.current.type == EventType.MouseDown && Event.current.button == 1 && foldRect.Contains ( Event.current.mousePosition ) )
				{
					//Debug.Log ("Right Click");
				}
				
				value = EditorGUI.Foldout ( foldRect, value, new GUIContent ( label, toolTip ) );
				
				GUILayout.FlexibleSpace ();
				
				button = GUILayout.Button ( new GUIContent ( "X", "Delete" ), GUILayout.Width ( 30 ) );
			}
			GUILayout.EndHorizontal ();
			
			if ( EditorGUI.EndChangeCheck () )
			{
				//Debug.Log ("On Fold Out");
				GUI.FocusControl ( string.Empty );
			}
		}

		/// <summary>
		/// Layer mask field.
		/// </summary>
		public static void LayerMaskField ( string label, ref LayerMask value, bool showSpecial = true )
		{
			value = EditorHelper.GetLayerMaskField ( label, value, showSpecial );
		}

		/// <summary>
		/// Gets the layer mask field.
		/// </summary>
		public static LayerMask GetLayerMaskField ( string label, LayerMask selected )
		{
			return GetLayerMaskField ( label, selected, true );
		}

		public static LayerMask GetLayerMaskField ( string label, LayerMask selected, bool showSpecial )
		{
			List<string> layers = new List<string> ();
			List<int> layerNumbers = new List<int> ();
			
			string selectedLayers = "";
			
			for ( int i = 0; i < 32; i++ )
			{
				string layerName = LayerMask.LayerToName ( i );
				
				if ( layerName != "" )
				{
					if ( selected == (selected | (1 << i)) )
					{
						if ( selectedLayers == "" )
							selectedLayers = layerName;
						else
							selectedLayers = "Mixed";
					}
				}
			}
			
			//EventType lastEvent = Event.current.type;
			
			if ( Event.current.type != EventType.MouseDown && Event.current.type != EventType.ExecuteCommand )
			{
				if ( selected.value == 0 )
					layers.Add ( "Nothing" );
				else if ( selected.value == -1 )
					layers.Add ( "Everything" );
				else
					layers.Add ( selectedLayers );
				
				layerNumbers.Add ( -1 );
			}
			
			if ( showSpecial )
			{
				layers.Add ( (selected.value == 0 ? "✓   " : "      ") + "Nothing" );
				layerNumbers.Add ( -2 );
				
				layers.Add ( (selected.value == -1 ? "✓   " : "      ") + "Everything" );
				layerNumbers.Add ( -3 );
			}
			
			for ( int i = 0; i < 32; i++ )
			{
				string layerName = LayerMask.LayerToName ( i );
				
				if ( layerName != "" )
				{
					if ( selected == (selected | (1 << i)) )
						layers.Add ( "✓   " + layerName );
					else
						layers.Add ( "      " + layerName );
					
					layerNumbers.Add ( i );
				}
			}
			
			bool preChange = GUI.changed;
			
			GUI.changed = false;
			
			int newSelected = 0;
			
			if ( Event.current.type == EventType.MouseDown )
				newSelected = -1;
			
			newSelected = EditorGUILayout.Popup ( label, newSelected, layers.ToArray (), EditorStyles.layerMaskField );
			
			if ( GUI.changed && newSelected >= 0 )
			{
				if ( showSpecial && newSelected == 0 )
				{
					selected = 0;
				}
				else if ( showSpecial && newSelected == 1 )
				{
					selected = -1;
				}
				else
				{
					if ( selected == (selected | (1 << layerNumbers[newSelected])) )
						selected &= ~(1 << layerNumbers[newSelected]);
					else
						selected = selected | (1 << layerNumbers[newSelected]);
				}
			}
			else
				GUI.changed = preChange;
			
			return selected;
		}
		
		
		public static void DrawRectOffset (string title, ref RectOffset rectOffset, ref bool open)
		{
			GUILayout.BeginVertical ();
			{
				FoldOut(title, "", ref open);
				
				if (open)
				{	
					EditorGUI.indentLevel = 2;
					rectOffset.left = EditorGUILayout.IntField("Left", rectOffset.left);
					rectOffset.right = EditorGUILayout.IntField("Right", rectOffset.right);
					rectOffset.top = EditorGUILayout.IntField("Top", rectOffset.top);
					rectOffset.bottom = EditorGUILayout.IntField("Down", rectOffset.bottom);
					EditorGUI.indentLevel = 0;
					GUILayout.Space(10);
				}
			}
			GUILayout.EndVertical ();
		}
	}
}






/*

	EditorGUI.BeginDisabledGroup (bool);
	{
		
	}
	EditorGUI.EndDisabledGroup ();

	
	
	
	private void DrawProperty (string propertyName, string label, string toolTip)
	{
		EditorGUIUtility.LookLikeInspector();
		
		SerializedProperty states = serializedObject.FindProperty (propertyName);
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(states, new GUIContent(new GUIContent(label, toolTip)), true);
		
		if(EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
		
		EditorGUIUtility.LookLikeControls();
	}
	
	
	
	
	Event currentEvent = Event.current;
	Rect contextRect;
	
	contextRect =  GUILayoutUtility.GetLastRect ( );
	EditorGUI.DrawRect ( contextRect, Color.clear );
	
	Vector2 mousePos = currentEvent.mousePosition;
	
	if (contextRect.Contains(mousePos) && Event.current.button == 1 && Event.current.type == EventType.MouseUp )
	{
		// On right click
	}
	
*/

























