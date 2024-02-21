using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UIStyles
{
	public class DataHelper 
	{
		public static bool highlightRenameField = false;
		
		/// <summary>
		/// Does this game object have a prefab?.
		/// </summary>
		public static bool isActivePrefabInstance ( GameObject obj )
		{
			#if UNITY_EDITOR
			return 
				PrefabUtility.GetPrefabType ( obj ) == PrefabType.None
				|| PrefabUtility.GetPrefabType ( obj ) == PrefabType.MissingPrefabInstance
				|| PrefabUtility.GetPrefabType ( obj ) == PrefabType.DisconnectedPrefabInstance;
			
			#else
			return false;
			#endif
		}
		
		/// <summary>
		/// is the object the prefab in the project folder not the scene
		/// </summary>
		public static bool isPrefabInFolderNotScene ( GameObject obj )
		{
			#if UNITY_EDITOR
			return 
				PrefabUtility.GetPrefabType ( obj ) == PrefabType.Prefab
				&& PrefabUtility.GetCorrespondingObjectFromSource ( obj ) == null && PrefabUtility.GetPrefabObject ( obj ) != null;
			#else
			return false;
			#endif
		}
		
		public static bool canEditValues ( GameObject obj )
		{
			#if UNITY_EDITOR
			return isActivePrefabInstance ( obj ) || isPrefabInFolderNotScene ( obj );
			#else
			return true;
			#endif
		}
	}
	
}




















