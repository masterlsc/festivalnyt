using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UIStyles
{
	[System.Serializable]
	public class Palette
	{
		#if UNITY_EDITOR
		[HideInInspector] public bool open;
		[HideInInspector] [System.NonSerialized]public bool rename;
		#endif
		
		[HideInInspector] public int id;
		
		[HideInInspector] public List<PaletteColor> colors = new List<PaletteColor>();
		
		
		[SerializeField] private string name_;
		[HideInInspector] public string name 
		{
			get{return name_;} 
			set 
			{
				name_ = value;
				foreach (PaletteColor col in colors)
				{
					col.paletteName = value;
				}
			}
		}
		
		[SerializeField] private string category_;
		[HideInInspector] public string category 
		{
			get{return category_;} 
			set 
			{
				category_ = value;
				foreach (PaletteColor col in colors)
				{
					col.category = value;
				}
			}
		}
		
		/// <summary>
		/// Using this override will not initialise the palette, it will not automatically be given an id, neither will it be added to the list of palettes within its data file.
		/// </summary>
		public Palette () {}
		
		public Palette (PaletteDataFile data)
		{
			Initialise (data, "", "");
		}
		
		/// <summary>
		/// Using this override will initialise the palette, it will automatically be given an id and added to the list of palettes within its data file.
		/// </summary>
		/// param: data 		= The data file to use
		/// param: category	    = The category the palette is in
		/// param: paletteName	= The name of the palette
		public Palette (PaletteDataFile data, string category, string paletteName)
		{
			Initialise (data, category, paletteName);
		}
		
		private void Initialise (PaletteDataFile data, string category, string paletteName)
		{
			// Set ID
			id = data.GetNewPaletteID();
			data.paletteIDs.Add(id, this);
			
			this.name = paletteName;
			this.category = category;
			
			data.palettes.Add(this);
		}
		
		/// <summary>
		/// Add a color to the palette
		/// </summary>
		/// param: data 		= The data file to use
		/// param: colorName    = The name of the new palette color
		/// param: newColor		= The new color
		public void AddColor (PaletteDataFile data, string colorName, Color newColor)
		{
			PaletteColor col = new PaletteColor(data);
			col.name = colorName;
			col.color = newColor;
			col.category = this.category;
			col.paletteName = this.name;
			colors.Add(col);
		}
	}
}









