using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UIStyles
{
	public class UIStylesGUIPath : EditorWindow 
	{
		private static bool highlightField = false;
		private static UnityEngine.Object[] draggedObjects;
		
		public static void DrawPath (ref string path, ref bool renamePath, bool pathError, ref bool checkPath, string findByName)
		{
			GUILayout.BeginVertical ();
			{
				// -------------------------------------------------- //
				// Path
				// -------------------------------------------------- //
				GUILayout.Space ( 5 );
				
				GUI.backgroundColor = Color.clear;
				GUILayout.BeginHorizontal ();
				{
					GUILayout.Space ( 10 );
					EditorGUILayout.LabelField(new GUIContent("Component Path:", "This is the path to the component \n\nThe component path works in the same way as directories, its used for when the component is a child of the game object we find with the find by name."), EditorHelper.dropdownSkin, GUILayout.Width ( 100 ));
				}
				GUILayout.EndHorizontal ();
				GUI.backgroundColor = Color.white;
				
				GUILayout.BeginHorizontal ();
				{
					GUILayout.Space ( 14 );
					EditorGUI.BeginDisabledGroup (!renamePath);
					{
						GUI.SetNextControlName("Rename Path Field");
						path = EditorGUILayout.TextField("", path);
						
						if (highlightField)
						{
							GUI.FocusControl("Rename Path Field");
							highlightField = false;
						}
					}
					EditorGUI.EndDisabledGroup ();
					
					if ( GUILayout.Button ( renamePath ? "Done" : "Edit", EditorHelper.buttonSkin, GUILayout.Width ( 80 ) ) )
					{
						GUI.FocusControl(null);
						renamePath = !renamePath;
						
						if (renamePath)
						{
							highlightField = true;
						}
						
						else 
						{
							UIStylesDatabase.Save ();
							checkPath = true;
						}
					}
					GUILayout.Space ( 14 );
				}
				GUILayout.EndHorizontal ();
				
				if (pathError)
					EditorGUILayout.HelpBox ( "Multiple components have the same path!", MessageType.Error );
				
				Event currentEvent = Event.current;
				Rect contextRect;
				
				contextRect =  GUILayoutUtility.GetLastRect ( );
				EditorGUI.DrawRect ( contextRect, Color.clear );
				
				Vector2 mousePos = currentEvent.mousePosition;
				
				if (contextRect.Contains(mousePos) && Event.current.button == 1 && Event.current.type == EventType.MouseUp )
				{
					GUI.FocusControl(null);
					renamePath = true;
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
				if (!string.IsNullOrEmpty(findByName))
				{
					foreach (Object draggedObj in draggedObjects) 
					{
						path = StyleHelper.GetPath ((GameObject)draggedObj, findByName, true);
					}
					checkPath = true;
				}
				else Debug.LogError ("To find the path you must have a find by name");
			}

		}
		
		
	}
}



















