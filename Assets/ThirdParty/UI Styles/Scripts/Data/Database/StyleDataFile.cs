using UnityEngine;
using System.Collections.Generic;

namespace UIStyles
{
	[System.Serializable]
	public class StyleDataFile : ScriptableObject
	{
		[HideInInspector] public PreferenceValues preferenceData = new PreferenceValues();
		[HideInInspector] public List <Style> styles = new List<Style>();
		
		[HideInInspector] public List<string> categories = new List<string>();
		[HideInInspector] public string currentCategory = string.Empty;
		
		[HideInInspector] public List<string> layouts = new List<string>();
		[HideInInspector] public int currentLayout = 0;
		
		[HideInInspector] public Dictionary<int, Style> styleIDs = new Dictionary<int, Style>();
		[HideInInspector] public Dictionary<int, StyleComponent> componentIDs = new Dictionary<int, StyleComponent>();
		
		public void AddStyle (Style style)
		{
			if ( !categories.Contains ( style.category ) )
				categories.Add ( style.category );
			
			styles.Add ( style );
		}
		public void InsertStyle (int index, Style style)
		{
			if ( !categories.Contains ( style.category ) )
				categories.Add ( style.category );
			
			styles.Insert ( index, style );
		}
		
		/// <summary>
		/// CacheIDs, helps find the styles and components quicker
		/// </summary>
		public void CacheIDs ()
		{
			// Clear the lists
			styleIDs.Clear();
			componentIDs.Clear();
			
			// Loop styles
			foreach (Style style in styles)
			{
				// If the value is 0, there is no id so give it a new one.
				if (style.id == 0)
					style.id = GetNewStyleID ();
				
				// There should never be multiple styles with the same id
				if (!styleIDs.ContainsKey(style.id))
				{
					// Cache style id
					styleIDs.Add(style.id, style);
				}
				else Debug.LogError("Style ID for " + style.name + " Exists more than once");
				
				// Loop components and cache thair ids
				foreach (StyleComponent component in style.styleComponents)
				{
					// If the value is 0, there is no id so give it a new one.
					if (component.id == 0)
						component.id = GetNewComponentID ();
					
					// Check the parent id is set
					if (component.parentID == 0)
						component.parentID = style.id;
					
					// There should never be multiple components with the same id
					if (!componentIDs.ContainsKey(component.id))
					{
						// Cache component id
						componentIDs.Add(component.id, component);
					}
					else Debug.LogError("Component ID for " + component.name + " Exists more than once");
				}
			}
		}
		
		/// <summary>
		/// Gets a componets parent style id
		/// </summary>
		/// <param name="componentID"></param>
		/// <returns></returns>
		public int GetParentID (int componentID)
		{
			// Loop styles
			foreach (Style style in styles)
			{
				// Loop components
				foreach (StyleComponent component in style.styleComponents)
				{
					if (component.id == componentID)
					{
						return style.id;
					}
				}
			}
			
			return 0;
		}
		
		/// <summary>
		/// Style ID
		/// </summary>
		[HideInInspector] public int styleID;
		public int GetNewStyleID ()
		{
			styleID ++;
			return styleID;
		}
		
		/// <summary>
		/// Component ID
		/// </summary>
		[HideInInspector] public int componentID;
		public int GetNewComponentID ()
		{
			componentID ++;
			return componentID;
		}
	}
}





















