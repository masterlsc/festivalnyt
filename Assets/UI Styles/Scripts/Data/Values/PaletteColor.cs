using UnityEngine;
using System.Collections;

namespace UIStyles
{
	[System.Serializable]
	public class PaletteColor
	{
		#if UNITY_EDITOR
		[HideInInspector] [System.NonSerialized]public bool rename;
		[HideInInspector] [System.NonSerialized]public bool delete;
		[HideInInspector] [System.NonSerialized]public bool moveUp;
		[HideInInspector] [System.NonSerialized]public bool moveDown;
		#endif
		
		// The id is used for finding colors at runtime
		[HideInInspector] public int id;
		
		// The GUID is used for linking color fields
		[HideInInspector] public string guid;
		
		// Name of the color
		[HideInInspector] public string name;
		
		// Name of the pallete this color is in
		[HideInInspector] public string paletteName;
		
		// Category the palette is in
		[HideInInspector] public string category;
		
		// The color
		[HideInInspector] public Color color;
		
		/// <summary>
		/// Using this override will not initialise the paletteColor, it will not automatically be given an id, neither will it be added to the list of colors within its palette.
		/// </summary>
		public PaletteColor () {}
		
		/// <summary>
		/// Using this override will initialise the paletteColor, it will automatically be given an id and added to the list of colors within its palette.
		/// </summary>
		public PaletteColor (PaletteDataFile data)
		{
			Initialise (data);
		}
		
		private void Initialise (PaletteDataFile data)
		{
			// Set ID
			id = data.GetNewColorID();
			data.colorIDs.Add(id, this);
			
			// Set GUID
			guid = System.Guid.NewGuid().ToString();
			data.colorGUIDs.Add(guid, this);
		}
	}
}









