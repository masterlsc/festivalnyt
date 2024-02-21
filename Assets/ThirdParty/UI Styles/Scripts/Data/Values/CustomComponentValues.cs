using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UIStyles
{
	[System.Serializable]
	public class CustomComponentValues 
	{
		[HideInInspector] 
		public Component customComponent;
		
		[HideInInspector, FormerlySerializedAs("excludedProperties")] 
		public List<string> excludedList = new List<string>();
		
		public CustomComponentValues CloneValues ()
		{
			CustomComponentValues values = new CustomComponentValues();
			
			values.alwaysAdd 		= this.alwaysAdd;
			values.alwaysRemove 	= this.alwaysRemove;
			values.customComponent	= this.customComponent;
			
			values.excludedList = new List<string>();
			
			foreach (string str in this.excludedList)
				values.excludedList.Add(str);
			
			return values;
		}
		
				
		// ------------------------------ //
		// Obsolete
		// ------------------------------ //
		
		[HideInInspector] 
		public bool alwaysAdd = false;
		
		[HideInInspector] 
		public bool alwaysRemove = false;
		
		// ------------------------------ //
		// Obsolete
		// ------------------------------ //
	}
}





















 