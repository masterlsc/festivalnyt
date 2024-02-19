using UnityEngine;
using System.Collections.Generic;

namespace UIStyles
{
	[System.Serializable]
	public class PaletteDataFile : ScriptableObject
	{
		[HideInInspector] public string currentCategory;
		[HideInInspector] public List<string> categories = new List<string>();
		[HideInInspector] public List<Palette> palettes = new List<Palette>();
		
		[HideInInspector] public Dictionary<int, Palette> paletteIDs = new Dictionary<int, Palette>();
		[HideInInspector] public Dictionary<int, PaletteColor> colorIDs = new Dictionary<int, PaletteColor>();
		[HideInInspector] public Dictionary<string, PaletteColor> colorGUIDs = new Dictionary<string, PaletteColor>();
		
		/// <summary>
		/// Palette ID
		/// </summary>
		[HideInInspector] public int paletteID;
		public int GetNewPaletteID ()
		{
			paletteID ++;
			return paletteID;
		}
		
		/// <summary>
		/// Color ID
		/// </summary>
		[HideInInspector] public int colorID;
		public int GetNewColorID ()
		{
			colorID ++;
			return colorID;
		}
		
		
		/// <summary>
		/// Cache Color GUID's
		/// </summary>
		public void CacheColor()
		{
			paletteIDs.Clear();
			colorIDs.Clear();
			colorGUIDs.Clear();
			
			// Loop palettes
			foreach (Palette pal in palettes)
			{
				// If the value is 0, there is no id so give it a new one.
				if (pal.id == 0)
					pal.id = GetNewPaletteID ();
				
				// There should never be multiple palettes with the same id
				if (!paletteIDs.ContainsKey(pal.id))
				{
					// Cache palette id
					paletteIDs.Add(pal.id, pal);
				}
				else Debug.LogError("Palette ID for " + pal.name + " Exists more than once");
				
				
				// Loop colors in palette
				foreach (PaletteColor col in pal.colors)
				{
					// Cache GUID, If the value is empty, there is no guid so give it a new one.
					if ( string.IsNullOrEmpty(col.guid) )
						col.guid = System.Guid.NewGuid().ToString();
					
					// Cache GUID
					if (!colorGUIDs.ContainsKey(col.guid))
					{
						colorGUIDs.Add(col.guid, col);
					}
					else Debug.LogError("Color GUID for " + col.name + " Exists more than once");
					
					
					// Cache ID, If the value is 0, there is no id so give it a new one.
					if (col.id == 0)
						col.id = GetNewColorID ();
					
					// Cache ID
					if (!colorIDs.ContainsKey(col.id))
					{
						colorIDs.Add(col.id, col);
					}
					else Debug.LogError("Color ID: " + col.name + " Exists more than once");
				}
			}
		}
		
		/// <summary>
		/// Get Color By GUID, used for getting the linked color
		/// </summary>
		public PaletteColor GetColorByGUID (string id)
		{			
			// If the color is found return it
			if (colorGUIDs.ContainsKey(id))
				return colorGUIDs[id];
			
			// Try add the color by loading the list
			if (!colorGUIDs.ContainsKey(id))
			{
				CacheColor();
				
				// If the color is found now, return it
				if (colorGUIDs.ContainsKey(id))
					return colorGUIDs[id];
			}
			
			// Color not found, return null
			return null;
		}
	}
}





















