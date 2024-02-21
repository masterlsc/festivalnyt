using UnityEngine;
using UnityEditor;
using System;

namespace UIStyles
{
	
	/*
		// Create ScriptableObject
		ScriptableObjectHelper.Create <CustumComponentData> ("title", "directory", "defaultName", ".extension", (ScriptableObject obj, string path) => {} );
	*/
	
	public class ScriptableObjectHelper : EditorWindow 
	{
		public static void Create <type> (string title, string directory, string defaultName, string extension, Action<ScriptableObject, string> onComplete) where type : UnityEngine.ScriptableObject
		{
			// Get save path
			string path = EditorUtility.SaveFilePanel (title, directory, defaultName, extension);
			
			// Return if path is null as user probable Cancelled
			if (string.IsNullOrEmpty (path))
			{
				if (onComplete != null)
					onComplete(null, string.Empty);
				
				return;
			}
			
			// Get project relative path
			path = FileUtil.GetProjectRelativePath (path);
			
			// Return if project relative path is null as user probable tried saveing out side of the project.
			if (string.IsNullOrEmpty (path))
			{
				// warn the user
				EditorUtility.DisplayDialog ("Warning", "You must save within the project", "Ok");
				
				if (onComplete != null)
					onComplete(null, string.Empty);
				
				return;
			}
			
			// Create and save the scriptable object
			type data = ScriptableObject.CreateInstance<type>();
			
			AssetDatabase.CreateAsset (data, path);
			AssetDatabase.SaveAssets ();
			
			EditorHelper.SelectObjectInProject(data, false, true);
			
			if (onComplete != null)
				onComplete(data, path);
		}
		
		
		public static void Load <type> (string title, string directory, string extension, Action<ScriptableObject> onComplete) where type : UnityEngine.ScriptableObject
		{
			string path = EditorUtility.OpenFilePanel (title, directory, extension);
			
			// Return if path is null as user probable cancelled
			if (string.IsNullOrEmpty (path)) 
			{
				if (onComplete != null)
					onComplete(null);
				
				return;
			}
			
			// We must load within the project.
			if (!path.StartsWith (Application.dataPath))
			{
				// warn the user to load within the project
				EditorUtility.DisplayDialog ("Warning", "You must load within the project", "Ok");
				
				if (onComplete != null)
					onComplete(null);
			}
			else
			{
				string relPath = path.Substring (Application.dataPath.Length - "Assets".Length);
				
				type data = AssetDatabase.LoadAssetAtPath (relPath, typeof(type)) as type;
				
				if (onComplete != null)
					onComplete (data);
			}
		}
	}
}





















